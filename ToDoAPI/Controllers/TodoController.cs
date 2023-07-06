using ToDoAPI.Models;
using ToDoAPI.Repositories;
using ToDoAPI.Specifications;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDoAPI.Services.Hobby;

namespace ToDoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly IHobbyService _hobbyService;

        public TodoController(IHobbyService service)
        {
            _hobbyService = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllHobbys()
        {
            try
            {
                return Ok(await _hobbyService.GetAllHobbys());
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetHobbyById(int id)
        {
            var hobby = await _hobbyService.GetHobbyById(id);
            //var idSpecification = new IDSpecification(id);
            //var hobbys = await _hobbyService.GetAllHobbys();
            //var hobby = hobbys.Where(idSpecification.IsSatified); 
            return hobby == null ? NotFound() : Ok(hobby);
        }

        [HttpPost]
        public async Task<IActionResult> AddNewHobby(HobbyModel model)
        {
            try
            {
                var newHobby = await _hobbyService.AddNewHobby(model);
                return newHobby == null ? NotFound() : Ok(newHobby);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateHobby(int id, HobbyModel model)
        {
            try
            {
                if(id != model.Id)
                {
                    return NotFound();
                }
                var hobbys = await _hobbyService.UpdateHobby(id, model);
                return Ok(hobbys);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHobby(int id)
        {
            try
            {
                var hobbys = await _hobbyService.DeleteHobby(id);
                return Ok(hobbys);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
