using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SundownBoulevard.DTO;
using SundownBoulevard.Entities;
using SundownBoulevard.Models;
using SundownBoulevard.Services;

namespace SundownBoulevard.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : Controller
    {

        private readonly IBookingService _bookingService;

        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }
        
        [HttpGet("randomdish")]
        public async Task<IActionResult> GetADish()
        {
            var response = await _bookingService.GetADish();

            return Ok(response);
        }

        
        [HttpGet("drinksmenu")]
        public async Task<IActionResult> GetDrinksMenu([FromQuery]Pagination paginationModel)
        {
            var response = await _bookingService.GetDrinksMenu(paginationModel);

            return Ok(response);
        }

        [Authorize]
        [HttpPost("create")]
        public IActionResult Create(BookingDTO model)
        {
            var httpContext = HttpContext;
            
            var response = _bookingService.Create(model, httpContext);

            return Ok(response);
        }


        [HttpPost("update/{id}")]
        public IActionResult Update(BookingDTO model, Guid id)
        {
            var response = _bookingService.Update(model, id);

            return Ok(response);
        }

        [HttpDelete("delete/{id}")]
        public IActionResult Delete(Guid id)
        {
            var response = _bookingService.Delete(id);

            return Ok(response);
        }
        
        
        
        
        
        
        
        
        
        
        
        
    }
}