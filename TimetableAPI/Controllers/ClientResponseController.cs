using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using TimetableAPI.Dtos;
using TimetableAPI.Repos;
using TimetableAPI.Services;

namespace TimetableAPI.Controllers
{
    [Route("api/crc")]
    [ApiController]
    public class ClientResponseController : ControllerBase
    {

        private readonly IClientResponceRepo _repository;
        private readonly IMapper _mapper;
        private readonly IOptions<SMTPConfig> _options;

        public ClientResponseController(IClientResponceRepo repository, IMapper mapper, IOptions<SMTPConfig> options)
        {
            _repository = repository;
            _mapper = mapper;
            _options = options;
        }


        [HttpPost("auto")]
        public ActionResult<UserAutoAnswerDto> AutoriseUser(UserAutoRequestDto userAutoRequestDto)
        {
            var item = _repository.AutoriseUser(userAutoRequestDto, _options);
            return Ok(item);
        }

        [HttpPost("verify")]
        public ActionResult<UserAutoAnswerDto> VerifyEmail(EmailAutoDto emailAutoDto)
        {
            var item = _repository.EmailCodeAuto(emailAutoDto);
            return Ok(item);
        }

        [HttpGet("groups")]
        public ActionResult<IEnumerable<Group>> GetGroups()
        {
            var item = _repository.GetGroups();

            return Ok(_mapper.Map<IEnumerable<GroupReadDto>>(item));
        }

        [HttpPost("scheduler")]
        public ActionResult<IEnumerable<TimetableReadAnswerDto>> GetSchedulers(TimetableReadRequestDto timetableReadRequestDto)
        {
            var item = _repository.GetSchedulers(timetableReadRequestDto);
            if(item != null)
            {
                return Ok(item);
            }
            else
            {
                return BadRequest("Атрибуты не указаны или указаны одновременно!");
            }
        }

        [HttpPost("comment")]
        public ActionResult PostComment(CommentCreateDto commentCreateDto)
        {
            _repository.PostComment(commentCreateDto);
            return Ok();
        }

        [HttpPost("totalizer")]
        public ActionResult TotalizerClick(TotalizerUpdateDto totalizerUpdateDto)
        {
            _repository.TotalizerClick(totalizerUpdateDto);
            return Ok();
        }
    }
}
