using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using FoodKept.Models;


using System.Linq;


namespace FoodKept.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ITokenRepository _repository;
        private readonly IMapper _mapper;
        public AuthController(ITokenRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        //POST api/auth
        [HttpPost]
        public ActionResult<string> Login(Login login)
        {
            var token = _repository.Login(login.Username, login.Password);
            if(token == null){
                return BadRequest();
            }
            return Ok(token);
        }


    }
}
