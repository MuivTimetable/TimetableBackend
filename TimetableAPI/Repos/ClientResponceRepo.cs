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
            var DbUser = await _context.Users.Where(
                s => s.Login.Equals(request.Login) 
                && s.Password.Equals(request.Password)).
                FirstOrDefaultAsync();

            if (DbUser != null || request.UserIdentity == null)
            {
                var sessions = await _context.Session.Where(
                    s => s.User_id.Equals(DbUser.User_id)).
                    ToListAsync();
                
                if(sessions.Count() > 0)
                {
                    foreach(Session session in sessions)
                    {
                        if(session.SessionIdentificator == request.UserIdentity)
                        {
                            return new UserAutoAnswerDto { AutoAnswerOption = 0, IdentityToken = DbUser.Token };
                        }
                    }
                }

                DbUser.preToken = CreateToken() + "p";

                DbUser.AuthCode = rand.Next(100, 999);

                var sender = new SMTPSender();
                sender.Send(DbUser.Email, (int)DbUser.AuthCode, _options);

                await SaveChangesAsync();

                return new UserAutoAnswerDto { AutoAnswerOption = 2, IdentityToken = DbUser.preToken };
              
            }
      
            return new UserAutoAnswerDto { AutoAnswerOption = 1, IdentityToken = null};
        }

        public async Task<EmailAutoAnswerDto> EmailCodeAutoAsync(EmailAutoRequestDto request)
        {

            var user = await _context.Users.Where(s => s.preToken.Equals(request.EmailAutoToken)).FirstOrDefaultAsync();

            if(user == null)
            {
                return new EmailAutoAnswerDto { VerifyAnswerOption = false, Token = null};
            }

            if (user.AuthCode.Equals(request.EmailCode))
            {
                user.AuthCode = null;

                await _context.Session.AddAsync(new Session { User_id = user.User_id, SessionIdentificator = request.UserIdentity, User = user });

                if (user.Token == null)
                {
                    user.Token = CreateToken();
                }

                user.preToken = null;

                await SaveChangesAsync();

                return new EmailAutoAnswerDto { VerifyAnswerOption = true, Token = user.Token};
            }

            user.AuthCode = null;
            user.preToken = null;

            await SaveChangesAsync();
            return new EmailAutoAnswerDto { VerifyAnswerOption = false, Token = null};
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


                //TODO: Добавить иф если это студент или это преподаватель - разные пары

                var couplesId = await _context.Schedulers_Groups.
                    Join(_context.Schedulers, s => s.Scheduler_id, p => p.Scheduler_id, (s,p) => new {group = s.Group_id, day_id = p.Day_id, scheduler_id = p.Scheduler_id}).
                    Where(s => s.group.Equals(groupId) && s.day_id.Equals(answerItem.Day_id)).Select(p => p.scheduler_id).
                    ToListAsync();

                var schedulers = new SchedulersInDays[couplesId.Count];

                for (int i=0; i < couplesId.Count; i++)
                {
                    var couple = await _context.Schedulers.Where(s => s.Scheduler_id.Equals(couplesId[i])).FirstOrDefaultAsync();
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

            return new TimetableReadAnswerDto {Timetables = answer };
        }

        public async Task<CommentAnswerDto> PostCommentAsync(CommentCreateDto comment)
        {

            var user = await _context.Users.Where(
                s => s.Token.Equals(comment.Token))
                .FirstOrDefaultAsync();

            if (user == null)
            {
                return new CommentAnswerDto { CommentAnswerInfo = "Пользователя с данным токеном не существует!", CommentAnswerOption = false };
            }

            if (user.Group_id != await _context.Schedulers_Groups.Where(
                sg => sg.Scheduler_id.Equals(comment.Scheduler_id) &&
                sg.Group_id.Equals(user.Group_id)).Select(sg => sg.Group_id).
                FirstOrDefaultAsync())
            {
                return new CommentAnswerDto { CommentAnswerInfo = "Отказано в доступе: Пользователь не может комментировать занятия данной группы!", CommentAnswerOption = false };
            }

            switch (user.Permission_id)

            {
                case 2:
                case 3:
                    var scheduler = await _context.Schedulers.Where(
                        s => s.Scheduler_id.Equals(comment.Scheduler_id))
                        .FirstOrDefaultAsync();

                    if (scheduler == null)
                    {
                        break;
                    }

                    scheduler.Comment = comment.Comment;
                    break;

                default: return new CommentAnswerDto { CommentAnswerInfo = "Отказано в доступе: Пользователь не имеет права комментирования занятий!", CommentAnswerOption = false };
            }

            await SaveChangesAsync();

            return new CommentAnswerDto { CommentAnswerInfo = "Операция пройдена успешно!", CommentAnswerOption = true };
        }


        public async Task<TotalizerAnswerDto> TotalizerClickAsync(TotalizerUpdateDto totalizer)
        {
            var user = await _context.Users.Where(
                s => s.Token.Equals(totalizer.Token))
                .FirstOrDefaultAsync();
            if (user == null)
            {
                return new TotalizerAnswerDto { TotalizerAnswerInfo = "Пользователя с данным токеном не существует!", TotalizerAnswerOption = false};
            }

            for(int i = 0; i < totalizer.Scheduler_id.Length; i++)
            {
                if (user.Group_id != await _context.Schedulers_Groups.Where(
                    sg => sg.Scheduler_id.Equals(totalizer.Scheduler_id[i]) && 
                    sg.Group_id.Equals(user.Group_id)).
                    Select(sg => sg.Group_id).
                    FirstOrDefaultAsync())
                {
                    return new TotalizerAnswerDto { TotalizerAnswerInfo = "Отказано в доступе: Пользователь не может объявлять о своём присутствии/отсутствии на занятиях данной группы!", TotalizerAnswerOption = false };
                }
            }

            switch (user.Permission_id)
            {
                case 1:
                case 2:
                    for (int i = 0; i < totalizer.Scheduler_id.Length; i++)
                    {
                        var scheduler = await _context.Schedulers.Where(
                            s => s.Scheduler_id.Equals(totalizer.Scheduler_id[i]))
                            .FirstOrDefaultAsync();

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
                    }
                    break;

                default: return new TotalizerAnswerDto { TotalizerAnswerInfo = "Отказано в доступе: Пользователь не имеет права объявлять о своём присутствии/отсутствии на занятиях!", TotalizerAnswerOption = false };
            }

            await SaveChangesAsync();

            return new TotalizerAnswerDto { TotalizerAnswerInfo = "Операция пройдена успешно!", TotalizerAnswerOption = true };
        }

       /* public async Task<bool> CloseSessionAsync(CloseSessionDto request)
        {
            var user = await _context.Users.Where(u => u.Token.Equals(request.Token)).FirstOrDefaultAsync();

            if(user == null)
            {
                return false;
            }

            var sessions = await _context.Sessions.Where(s => s.User_id.Equals(user.User_id)).ToListAsync();

            foreach(Session session in sessions)
            {
                if(session.SessionIdentificator == request.UserIdentity)
                {
                    sessions.Remove(session);
                    break;
                }
            }

            if(sessions.Count == 0)
            {
                user.Token = null;
            }

            return true;
        }*/


        public async Task<bool> SaveChangesAsync()
        {
            return await (_context.SaveChangesAsync()) >= 0;
        }

        public string CreateToken()
        {
            var mashToken = DateTime.Now.ToString().ToCharArray();
            StringBuilder preToken = new StringBuilder();

            for (int i = 0; i < mashToken.Length; i++)
            {
                preToken.Append(Convert.ToByte(mashToken[i]));
            }

            return preToken.ToString();
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

        public bool EmailCodeAuto(EmailAutoRequestDto request)
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
