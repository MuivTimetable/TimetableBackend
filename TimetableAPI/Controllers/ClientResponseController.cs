using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TimetableAPI.Deserializator;
using TimetableAPI.Dtos;
using TimetableAPI.Repos;

namespace TimetableAPI.Controllers
{
    [Route("api/crc")]
    [ApiController]
    public class ClientResponseController : ControllerBase
    {

        private readonly IClientResponceRepo _repository;
        private readonly IDeserializator _deserializator;
        private readonly IMapper _mapper;

        public ClientResponseController(IClientResponceRepo repository, IMapper mapper, IDeserializator deserializator)
        {
            _repository = repository;
            _mapper = mapper;
            _deserializator = deserializator;
        }


        [HttpPost("auto")]
        public ActionResult<UserAutoAnswerDto> AutoriseUser(UserAutoRequestDto userAutoRequestDto)
        {
            var item = _repository.AutoriseUser(userAutoRequestDto);
            return Ok(item);
        }

        [HttpGet("groups")]
        public ActionResult<IEnumerable<Models.Group>> GetGroups()
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

        [HttpGet("startdes")]
        public ActionResult RunDeserializer(int code)
        {
            if (code == 1111)
            {
                _deserializator.ShedulerDeserializator();
                return Ok();
            }
            else return BadRequest("А ну, супостат, отведуй силушки богатырской!!!");
        }
    }
}
