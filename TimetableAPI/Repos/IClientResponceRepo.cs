using Microsoft.Extensions.Options;
using TimetableAPI.Dtos;
using TimetableAPI.Services;

namespace TimetableAPI.Repos
{
    public interface IClientResponceRepo
    {
        bool SaveChanges();

        UserAutoAnswerDto AutoriseUser(UserAutoRequestDto request, IOptions<SMTPConfig> _options);

        bool EmailCodeAuto(EmailAutoRequestDto request);

        IEnumerable<TimetableReadAnswerDto> GetSchedulers(TimetableReadRequestDto request);

        void PostComment(CommentCreateDto comment);

        bool TotalizerClick(TotalizerUpdateDto totalizer);

        IEnumerable<Group> GetGroups();

        //Async methods:

        bool SaveChangesAsync();

        Task<UserAutoAnswerDto> AutoriseUserAsync(UserAutoRequestDto request, IOptions<SMTPConfig> _options);

        Task<EmailAutoAnswerDto> EmailCodeAutoAsync(EmailAutoRequestDto request);

        Task<TimetableReadAnswerDto> GetSchedulersAsync(TimetableReadRequestDto request);

        Task<CommentAnswerDto> PostCommentAsync(CommentCreateDto comment);

        Task<TotalizerAnswerDto> TotalizerClickAsync(TotalizerUpdateDto totalizer);

        Task<GetUserInfoDto> GetUserInfoAsync(string token);

        Task<GroupAnswerDto> GetGroupsAsync();

        //Task<bool> CloseSessionAsync(CloseSessionDto request);

    }
}
