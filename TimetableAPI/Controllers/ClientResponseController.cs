using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using TimetableAPI.Deserializator;
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
        private readonly IDeserializator _deserializator;

        public ClientResponseController(IClientResponceRepo repository, IMapper mapper, IOptions<SMTPConfig> options, IDeserializator deserializator)
        {
            _repository = repository;
            _mapper = mapper;
            _options = options;
            _deserializator = deserializator;
        }


        [HttpPost("auto")]
        public async Task<ActionResult<UserAutoAnswerDto>> AutoriseUserAsync(UserAutoRequestDto userAutoRequestDto)
        {
            var item = await _repository.AutoriseUserAsync(userAutoRequestDto, _options);
            return Ok(item);
        }

        [HttpPost("verify")]
        public async Task<ActionResult<UserAutoAnswerDto>> VerifyEmailAsync(EmailAutoDto emailAutoDto)
        {
            var item = await _repository.EmailCodeAutoAsync(emailAutoDto);
            return Ok(item);
        }

        [HttpGet("groups")]
        public async Task<ActionResult<IEnumerable<Models.Group>>> GetGroupsAsync()
        {
            var item = await _repository.GetGroupsAsync();

            return Ok(item);
        }

        [HttpPost("scheduler")]
        public async Task<ActionResult<IEnumerable<TimetableReadAnswerDto>>> GetSchedulersAsync(TimetableReadRequestDto timetableReadRequestDto)
        {
            var item = await _repository.GetSchedulersAsync(timetableReadRequestDto);
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
        public async Task<ActionResult> PostCommentAsync(CommentCreateDto commentCreateDto)
        {
            var result = await _repository.PostCommentAsync(commentCreateDto);
            return Ok(result);
        }

        [HttpPost("totalizer")]
        public async Task<ActionResult> TotalizerClickAsync(TotalizerUpdateDto totalizerUpdateDto)
        {
            var result = await _repository.TotalizerClickAsync(totalizerUpdateDto);
            return Ok(result);
        }

        [HttpGet("startdes")]
        public ActionResult RunDeserializer(int code)
        {
            if (code == 1111)
            {
               var result = _deserializator.ShedulerDeserializator();
                _deserializator.DBContentRemover();
                return Ok(result);
            }
            else return BadRequest("А ну, супостат, отведуй силушки богатырской!!!");
        }
    }
}
