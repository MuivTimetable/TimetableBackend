using System.Text;
using TimetableAPI.Dtos;

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

        public UserAutoAnswerDto AutoriseUser(UserAutoRequestDto request)
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

                    //TODO Добавить вызов отправки почтовых сообщений

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

        public void EmailCodeAuto()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Group> GetGroups()
        {
            return _context.Groups.ToList();
        }

        public IEnumerable<Scheduler> GetSchedulers()
        {
            throw new NotImplementedException();
        }

        public void PostComment()
        {
            throw new NotImplementedException();
        }

        public void TotalizerClick()
        {
            throw new NotImplementedException();
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges()) >= 0;
        }

        public void PostComment(CommentCreateDto comment)
        {
            throw new NotImplementedException();
        }

        public void TotalizerClick(TotalizerUpdateDto totalizer)
        {
            throw new NotImplementedException();
        }
    }
}
