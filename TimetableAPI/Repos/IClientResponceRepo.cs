using TimetableAPI.Dtos;

namespace TimetableAPI.Repos
{
    public interface IClientResponceRepo
    {
        bool SaveChanges();

        UserAutoAnswerDto AutoriseUser(string Login, string Password);

        //void SendAutoEmail();

        //void AutoriseUser();

        //void AutoriseUser();

    }
}
