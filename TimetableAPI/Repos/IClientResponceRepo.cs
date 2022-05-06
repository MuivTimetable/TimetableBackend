using TimetableAPI.Dtos;

namespace TimetableAPI.Repos
{
    public interface IClientResponceRepo
    {
        bool SaveChanges();

        UserAutoAnswerDto AutoriseUser(UserAutoRequestDto request);

        void EmailCodeAuto();

        //TODO: понять как и что правильно передавать
        IEnumerable<Scheduler> GetSchedulers();

        void PostComment(CommentCreateDto comment);

        void TotalizerClick(TotalizerUpdateDto totalizer);

        IEnumerable<Group> GetGroups();

    }
}
