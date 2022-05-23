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

        IEnumerable<TimetableReadAnswerDto> GetSchedulers(TimetableReadRequestDto request);

        void PostComment(CommentCreateDto comment);

        bool TotalizerClick(TotalizerUpdateDto totalizer);

        IEnumerable<Group> GetGroups();

        //Async methods:

        bool SaveChangesAsync();

        Task<UserAutoAnswerDto> AutoriseUserAsync(UserAutoRequestDto request, IOptions<SMTPConfig> _options);

        Task<bool> EmailCodeAutoAsync(EmailAutoDto request);

        Task<TimetableReadAnswerDto> GetSchedulersAsync(TimetableReadRequestDto request);

        Task<bool> PostCommentAsync(CommentCreateDto comment);

        Task<bool> TotalizerClickAsync(TotalizerUpdateDto totalizer);

        Task<GroupAnswerDto> GetGroupsAsync();

    }
}
