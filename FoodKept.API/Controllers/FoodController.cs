using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using FoodKept.Models;
using System.Web;

using System.Linq;
using Microsoft.AspNetCore.Http;
using System;

namespace FoodKept.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodController : ControllerBase
    {
        private readonly IFoodRepository _repository;
        private readonly ITokenRepository _userRepository;
        private readonly IMapper _mapper;
        public FoodController(IFoodRepository repository, ITokenRepository userRepository, IMapper mapper)
        {
            _repository = repository;
            _userRepository = userRepository;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetFoodList()
        {
            string token = HttpContext.Request.Headers["Authorization"];

            if(!_userRepository.Authorize(token)) {
                return Unauthorized();
            }
            return Ok(_repository.GetAllFood());
        }

        //GET api/food/{id}
        [HttpGet("{id}", Name = "GetFoodById")]
        public IActionResult GetFoodById(int id)
        {
            string token = HttpContext.Request.Headers["Authorization"];

            if(!_userRepository.Authorize(token)) {
                return Unauthorized();
            }
            var foodModelFromRepo = _repository.GetFood(id);
            if (foodModelFromRepo != null)
            {
                 return Ok(foodModelFromRepo);
            }
            return NotFound();
        }    

        //POST api/food
        [HttpPost]
        public ActionResult <Food> CreateNewFood(Food foodCreate)
        {
            string token = HttpContext.Request.Headers["Authorization"];

            if(!_userRepository.Authorize(token)) {
                return Unauthorized();
            }
            _repository.Add(foodCreate);

            return CreatedAtRoute (nameof(GetFoodById), new {Id = foodCreate.ID}, foodCreate);
        }

        //PUT api/food/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateFood(int id, Food food)
        {
            string token = HttpContext.Request.Headers["Authorization"];

            if(!_userRepository.Authorize(token)) {
                return Unauthorized();
            }
            var foodFromRepo = _repository.GetFood(id);
            if (foodFromRepo == null)
            {
                return NotFound();
            }
            foodFromRepo.FoodName = food.FoodName;
            foodFromRepo.FoodCategory = food.FoodCategory;
            foodFromRepo.Price = food.Price;
            _repository.Update(foodFromRepo);

            return NoContent();
        }

        //DELETE api/food/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteFood(int id)
        {
            string token = HttpContext.Request.Headers["Authorization"];

            if(!_userRepository.Authorize(token)) {
                return Unauthorized();
            }
            var response =  _repository.Delete(id);

            return Ok(response);
        }
    }
}
