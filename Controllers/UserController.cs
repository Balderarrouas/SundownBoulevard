using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SundownBoulevard.DTO;
using SundownBoulevard.Services;

namespace SundownBoulevard.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : Controller
    {

        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate(UserDTO model)
        {
            var response = _userService.Authenticate(model);

            return Ok(response);
        }
        
        [Authorize(Roles="Guest")]
        [HttpPost("create")]
        public IActionResult Create(UserDTO model)
        {

            var response = _userService.Create(model);
            
            return Ok(response);
        }
        [Authorize(Roles="Admin")]
        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var response = _userService.GetAll();

            return Ok(response);
        }

        [Authorize(Roles="Admin")]
        [HttpGet("getbyid/{id}")]
        public IActionResult GetById(Guid id)
        {
            var response = _userService.GetById(id);
        
            return Ok(response);
        }

        [Authorize]
        [HttpPut("update/{id}")]
        public IActionResult Update(UserDTO model, Guid id)
        {
            var response = _userService.Update(model, id);

            return Ok(response);
        }

        [Authorize(Roles="Admin")]
        [HttpDelete("delete/{id}")]
        public IActionResult Delete(Guid id)
        {
            var response = _userService.Delete(id);

            return Ok(response);
        }

    }
}