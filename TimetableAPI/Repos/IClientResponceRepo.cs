using Microsoft.Extensions.Options;
using TimetableAPI.Dtos;
using TimetableAPI.Services;

namespace TimetableAPI.Repos
{
    public interface IClientResponceRepo
    {
        bool SaveChanges();

        UserAutoAnswerDto AutoriseUser(UserAutoRequestDto request, IOptions<SMTPConfig> _options);

        bool EmailCodeAuto(EmailAutoDto request);

        //TODO: понять как и что правильно передавать
        IEnumerable<TimetableReadAnswerDto> GetSchedulers(TimetableReadRequestDto request);

        void PostComment(CommentCreateDto comment);

        void TotalizerClick(TotalizerUpdateDto totalizer);

        IEnumerable<Group> GetGroups();

    }
}
