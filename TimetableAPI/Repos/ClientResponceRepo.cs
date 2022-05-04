using TimetableAPI.Dtos;

namespace TimetableAPI.Repos
{
    public class ClientResponceRepo : IClientResponceRepo
    {
        private readonly ApplicationContext _context;

        public ClientResponceRepo(ApplicationContext context)
        {
            _context = context;
        }

        public UserAutoAnswerDto AutoriseUser(string Login, string Password)
        {
            var user = new UserAutoAnswerDto();

            if (_context.Users.Where(s => s.Login.Equals(Login) && s.Password.Equals(Password)).Select(s => s.Login).Any())
            {
                user.AnswerOption = 1;
                user.IdentityToken = 131244;
            }
            else
            {
                user.AnswerOption = 1;
                user.IdentityToken = null;
            }
      
            return user;
        }

        public bool SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}
