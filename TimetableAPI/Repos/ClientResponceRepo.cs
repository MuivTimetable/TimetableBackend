using Microsoft.Extensions.Options;
using System.Text;
using TimetableAPI.Dtos;
using TimetableAPI.Services;

namespace TimetableAPI.Repos
{
    public class ClientResponceRepo : IClientResponceRepo
    {
        private readonly ApplicationContext _context;

        Random rand;
        public ClientResponceRepo(ApplicationContext context)
        {
            _context = context;
            rand = new Random();
        }

        public UserAutoAnswerDto AutoriseUser(UserAutoRequestDto request, IOptions<SMTPConfig> _options)
        {
            var user = new UserAutoAnswerDto();

            if (_context.Users.Where(s => s.Login.Equals(request.Login) && s.Password.Equals(request.Password)).Select(s => s.User_id).Any())
            {
                if(!_context.Users.Where(s => s.Login.Equals(request.Login) && s.Password.Equals(request.Password)).Select(s => s.Token).Contains(null))
                {
                    user.AnswerOption = 0;
                    user.IdentityToken = null;
                }
                else
                {
                    User dbUser = _context.Users.Where(s => s.Login.Equals(request.Login) && s.Password.Equals(request.Password)).FirstOrDefault();
                    user.AnswerOption = 3;
                    var preToken = DateTime.Now.ToString().ToCharArray();
                    StringBuilder token = new StringBuilder();
                    for(int i = 0; i < preToken.Length; i++)
                    {
                        token.Append(Convert.ToByte(preToken[i]));
                    }
                    user.IdentityToken = token.ToString();

                    dbUser.Token = user.IdentityToken;

                    dbUser.AuthCode = rand.Next(100, 999);

                    var sender = new SMTPSender();
                    sender.Send(dbUser.Email, (int)dbUser.AuthCode, _options);

                    SaveChanges();
                }
            }
            else
            {
                user.AnswerOption = 1;
                user.IdentityToken = null;
            }
      
            return user;
        }

        public bool EmailCodeAuto(EmailAutoDto request)
        {

            var user = _context.Users.Where(s => s.Token.Equals(request.Token)).FirstOrDefault();

            if(user == null)
            {
                return false;
            }

            if (user.AuthCode.Equals(request.EmailCode))
            {
                user.AuthCode = null;
                SaveChanges();
                return true;
            }

            user.AuthCode = null;
            user.Token = null;

            SaveChanges();
            return false;
        }

        public IEnumerable<Group> GetGroups()
        {
            return _context.Groups.ToList();
        }

        public IEnumerable<TimetableReadAnswerDto> GetSchedulers(TimetableReadRequestDto request)
        {
            int? groupId;

            if((request.Token == null && request.Group_id == null) 
                || (request.Token != null && request.Group_id != null))
            {
                return null;
            }

            var answer = new List<TimetableReadAnswerDto>();

            if (request.Token != null)
            {
                groupId = _context.Users.Where(s => s.Token.Equals(request.Token)).Select(s => s.Group_id).FirstOrDefault();

                if (groupId == null) return null;
            }
            else
            {
                groupId = request.Group_id;

                if (_context.Groups.Where(s => s.Group_id.Equals(groupId)).FirstOrDefault() == null) return null;            
            }

            var monday = DateTime.Now;

            while(monday.DayOfWeek != DayOfWeek.Monday)
            {
                monday = monday.AddDays(-1);
            }

            while(monday.DayOfWeek != DayOfWeek.Sunday)
            {
                var schedulerDay = _context.SchedulerDates.Where
                    (s => s.Work_Year.Equals(monday.Year) 
                    && s.Work_Month.Equals(monday.Month) 
                    && s.Work_Day.Equals(monday.Day))
                    .FirstOrDefault();

                if (schedulerDay == null)
                {
                    if (monday.DayOfWeek.Equals(DayOfWeek.Sunday))
                    {
                        break;
                    }
                    monday = monday.AddDays(+1);
                    continue;
                }

                var answerItem = new TimetableReadAnswerDto()
                {
                    Day_id = schedulerDay.Day_id,
                    Work_Date_Name = schedulerDay.Work_Date_Name,
                    Work_Year = schedulerDay.Work_Year,
                    Work_Month = schedulerDay.Work_Month,
                    Work_Day = schedulerDay.Work_Day,
                    DayOfTheWeek = monday.DayOfWeek.ToString()
                };

                var couplesId = _context.Schedulers_Groups.
                    Join(_context.Schedulers, s => s.Scheduler_id, p => p.Scheduler_id, (s,p) => new {group = s.Group_id, id = p.Scheduler_id}).
                    Where(s => s.group.Equals(groupId)).
                    ToList();

                var schedulers = new SchedulersInDays[couplesId.Count];

                for (int i=0; i < couplesId.Count; i++)
                {
                    var couple = _context.Schedulers.Where(s => s.Scheduler_id.Equals(couplesId[i].id)).FirstOrDefault();

                    var schedulerItem = new SchedulersInDays()
                    {
                        Area = couple.Area,
                        Cathedra = couple.Cathedra,
                        Comment = couple.Comment,
                        Place = couple.Place,
                        Scheduler_id = couple.Scheduler_id,
                        Totalizer = couple.Totalizer,
                        Tutor = couple.Tutor,
                        WorkEnd = couple.Work_end,
                        WorkStart = couple.Work_start,
                        WorkType = couple.Work_type,
                        Branch = couple.Branch                        
                    };

                    schedulers[i] = schedulerItem;
                }
                answerItem.Schedulers = schedulers;

                answer.Add(answerItem);

                monday = monday.AddDays(+1);
            }

            //TODO: сделай разделение на 3 инта и постом проверку в цикле + Сделай миграцию!







            return (answer);
        }

        public void PostComment(CommentCreateDto comment)
        {

            var user = _context.Users.Where(s => s.Token.Equals(comment.Token)).FirstOrDefault();

            if (user == null)
            {
                return;
            }

            switch (user.Permission_id)

            {
                case 2:
                case 3:
                    var scheduler = _context.Schedulers.Where(s => s.Scheduler_id.Equals(comment.Scheduler_id)).FirstOrDefault();

                    if (scheduler == null)
                    {
                        break;
                    }

                    scheduler.Comment = comment.Comment;
                    break;

                default: return;
            }

            SaveChanges();
        }

        public void TotalizerClick(TotalizerUpdateDto totalizer)
        {
            var user = _context.Users.Where(s => s.Token.Equals(totalizer.Token)).FirstOrDefault();
            if (user == null)
            {
                return;
            }

            switch (user.Permission_id)
            {
                case 1:
                case 2:
                    for (int i = 0; i < totalizer.Scheduler_id.Length; i++)
                    {
                        var scheduler = _context.Schedulers.Where(s => s.Scheduler_id.Equals(totalizer.Scheduler_id[i])).FirstOrDefault();

                        if(scheduler == null)
                        {
                            break;
                        }

                        if (totalizer.MoreOrLess)
                        {
                            scheduler.Totalizer++;
                        }
                        else
                        {
                            scheduler.Totalizer--;
                        }
                       // scheduler.Totalizer = totalizer.MoreOrLess ? scheduler.Totalizer++ : scheduler.Totalizer--;
                    }
                    break;

                default: return;
            }

            SaveChanges();
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges()) >= 0;
        }
    }
}
