using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TimetableAPI.Dtos;
using TimetableAPI.Repos;

namespace TimetableAPI.Controllers
{
    [Route("api/[controller]")]
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


        [HttpPost]
        public ActionResult<UserAutoAnswerDto> AutoriseUser(UserAutoRequestDto userAutoRequestDto)
        {
            var item = _repository.AutoriseUser(userAutoRequestDto.Login, userAutoRequestDto.Password);
            return Ok(item);
        }
    }
}
