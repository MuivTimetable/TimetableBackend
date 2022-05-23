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

        public async Task<UserAutoAnswerDto> AutoriseUserAsync(UserAutoRequestDto request, IOptions<SMTPConfig> _options)
        {
            var user = new UserAutoAnswerDto();

            if (await _context.Users.Where(s => s.Login.Equals(request.Login) && s.Password.Equals(request.Password)).Select(s => s.User_id).AnyAsync())
            {
                if(! await _context.Users.Where(s => s.Login.Equals(request.Login) && s.Password.Equals(request.Password)).Select(s => s.Token).ContainsAsync(null))
                {
                    user.AnswerOption = 0;
                    user.IdentityToken = null;
                }
                else
                {
                    User dbUser = await _context.Users.Where(s => s.Login.Equals(request.Login) && s.Password.Equals(request.Password)).FirstOrDefaultAsync();
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

                    await SaveChangesAsync();
                }
            }
            else
            {
                user.AnswerOption = 1;
                user.IdentityToken = null;
            }
      
            return user;
        }

        public async Task<bool> EmailCodeAutoAsync(EmailAutoDto request)
        {

            var user = await _context.Users.Where(s => s.Token.Equals(request.Token)).FirstOrDefaultAsync();

            if(user == null)
            {
                return false;
            }

            if (user.AuthCode.Equals(request.EmailCode))
            {
                user.AuthCode = null;
                await SaveChangesAsync();
                return true;
            }

            user.AuthCode = null;
            user.Token = null;

            await SaveChangesAsync();
            return false;
        }

        public async Task<GroupAnswerDto> GetGroupsAsync()
        {
            return new GroupAnswerDto() { Groups = await _context.Groups.ToListAsync() };
        }

        public async Task<TimetableReadAnswerDto> GetSchedulersAsync(TimetableReadRequestDto request)
        {
            int? groupId;

            if((request.Token == null && request.Group_id == null) 
                || (request.Token != null && request.Group_id != null))
            {
                return null;
            }

            var answer = new List<Timetables>();

            if (request.Token != null)
            {
                groupId = await _context.Users.Where(s => s.Token.Equals(request.Token)).Select(s => s.Group_id).FirstOrDefaultAsync();

                if (groupId == null) return null;
            }
            else
            {
                groupId = request.Group_id;

                if (await _context.Groups.Where(s => s.Group_id.Equals(groupId)).FirstOrDefaultAsync() == null) return null;            
            }

            var monday = DateTime.Now;

            while(monday.DayOfWeek != DayOfWeek.Monday)
            {
                monday = monday.AddDays(-1);
            }

            while(monday.DayOfWeek != DayOfWeek.Sunday)
            {
                var schedulerDay = await _context.SchedulerDates.Where
                    (s => s.Work_Year.Equals(monday.Year) 
                    && s.Work_Month.Equals(monday.Month) 
                    && s.Work_Day.Equals(monday.Day))
                    .FirstOrDefaultAsync();

                if (schedulerDay == null)
                {
                    if (monday.DayOfWeek.Equals(DayOfWeek.Sunday))
                    {
                        break;
                    }
                    monday = monday.AddDays(+1);
                    continue;
                }

                var answerItem = new Timetables()
                {
                    Day_id = schedulerDay.Day_id,
                    Work_Date_Name = schedulerDay.Work_Date_Name,
                    Work_Year = schedulerDay.Work_Year,
                    Work_Month = schedulerDay.Work_Month,
                    Work_Day = schedulerDay.Work_Day,
                    DayOfTheWeek = monday.DayOfWeek.ToString()
                };

                var couplesId = await _context.Schedulers_Groups.
                    Join(_context.Schedulers, s => s.Scheduler_id, p => p.Scheduler_id, (s,p) => new {group = s.Group_id, id = p.Scheduler_id}).
                    Where(s => s.group.Equals(groupId)).
                    ToListAsync();

                var schedulers = new SchedulersInDays[couplesId.Count];

                for (int i=0; i < couplesId.Count; i++)
                {
                    var couple = await _context.Schedulers.Where(s => s.Scheduler_id.Equals(couplesId[i].id)).FirstOrDefaultAsync();

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







            return (new TimetableReadAnswerDto {Timetables = answer });
        }

        public async Task<bool> PostCommentAsync(CommentCreateDto comment)
        {

            var user = await _context.Users.Where(s => s.Token.Equals(comment.Token)).FirstOrDefaultAsync();

            if (user == null)
            {
                return false;
            }

            switch (user.Permission_id)

            {
                case 2:
                case 3:
                    var scheduler = await _context.Schedulers.Where(s => s.Scheduler_id.Equals(comment.Scheduler_id)).FirstOrDefaultAsync();

                    if (scheduler == null)
                    {
                        break;
                    }

                    scheduler.Comment = comment.Comment;
                    break;

                default: return false;
            }

            await SaveChangesAsync();

            return true;
        }

        public async Task<bool> TotalizerClickAsync(TotalizerUpdateDto totalizer)
        {
            var user = await _context.Users.Where(s => s.Token.Equals(totalizer.Token)).FirstOrDefaultAsync();
            if (user == null)
            {
                return false;
            }

            switch (user.Permission_id)
            {
                case 1:
                case 2:
                    for (int i = 0; i < totalizer.Scheduler_id.Length; i++)
                    {
                        var scheduler = await _context.Schedulers.Where(s => s.Scheduler_id.Equals(totalizer.Scheduler_id[i])).FirstOrDefaultAsync();

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

                default: return false;
            }

            await SaveChangesAsync();

            return true;
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await (_context.SaveChangesAsync()) >= 0;
        }


        //Sync methods:

        public bool SaveChanges()
        {
            throw new NotImplementedException();
        }

        public UserAutoAnswerDto AutoriseUser(UserAutoRequestDto request, IOptions<SMTPConfig> _options)
        {
            throw new NotImplementedException();
        }

        public bool EmailCodeAuto(EmailAutoDto request)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TimetableReadAnswerDto> GetSchedulers(TimetableReadRequestDto request)
        {
            throw new NotImplementedException();
        }

        public void PostComment(CommentCreateDto comment)
        {
            throw new NotImplementedException();
        }

        public bool TotalizerClick(TotalizerUpdateDto totalizer)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Group> GetGroups()
        {
            throw new NotImplementedException();
        }

        bool IClientResponceRepo.SaveChangesAsync()
        {
            throw new NotImplementedException();
        }
    }
}
