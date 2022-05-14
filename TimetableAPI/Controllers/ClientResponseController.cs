using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TimetableAPI.Dtos;
using TimetableAPI.Repos;

namespace TimetableAPI.Controllers
{
    [Route("api/crc")]
    [ApiController]
    public class ClientResponseController : ControllerBase
    {

        private readonly IClientResponceRepo _repository;
        private readonly IMapper _mapper;

        public ClientResponseController(IClientResponceRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        [HttpPost("auto")]
        public ActionResult<UserAutoAnswerDto> AutoriseUser(UserAutoRequestDto userAutoRequestDto)
        {
            var item = _repository.AutoriseUser(userAutoRequestDto);
            return Ok(item);
        }

        [HttpGet("groups")]
        public ActionResult<IEnumerable<Group>> GetGroups()
        {
            var item = _repository.GetGroups();

            return Ok(_mapper.Map<IEnumerable<GroupReadDto>>(item));
        }

        [HttpGet]
        public ActionResult<IEnumerable<TimetableReadAnswerDto>> GetSchedulers(TimetableReadRequestDto timetableReadRequestDto)
        {
            var item = _repository.GetSchedulers(timetableReadRequestDto);
            if(item != null)
            {
                return Ok();
            }
            else
            {
                return BadRequest("Атрибуты не указаны или указаны одновременно!");
            }
        }
    }
}
