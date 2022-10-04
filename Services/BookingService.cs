using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SundownBoulevard.Constants;
using SundownBoulevard.Data;
using SundownBoulevard.DTO;
using SundownBoulevard.Entities;
using SundownBoulevard.Models;

namespace SundownBoulevard.Services
{

    public interface IBookingService
    {
        Task<Meal> GetADish();
        Task<List<Drink>> GetDrinksMenu(Pagination paginationModel);
        Booking Create(BookingDTO model, HttpContext userContext);
        Booking Update(BookingDTO model, Guid id);
        Booking Delete(Guid id);
    }
    
    
    public class BookingService : IBookingService
    {

        private readonly HttpClient _httpClient;

        private readonly Uri BaseAddress1 = new Uri("https://www.themealdb.com/");
        private readonly Uri BaseAddress2 = new Uri("https://api.punkapi.com/");
        private readonly SundownDbContext _context;
        private readonly IMapper _mapper;
        
        
        public BookingService(HttpClient httpClient, IMapper mapper, SundownDbContext context)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = BaseAddress1;
            _mapper = mapper;
            _context = context;
        }

        public async Task<Meal> GetADish()
        {
            var APIURL = "api/json/v1/1/random.php";
            var response = await _httpClient.GetAsync(APIURL);

            var jsonResult = await response.Content.ReadAsStringAsync();

            var mealDTO = JsonSerializer.Deserialize<MealDTO>(jsonResult);
            
            var meal = new Meal(mealDTO);
            
            return meal;
        }


        public async Task<List<Drink>> GetDrinksMenu(Pagination paginationModel)
        {
            _httpClient.BaseAddress = BaseAddress2;
            var APIURL = $"v2/beers?page={paginationModel.Page}&per_page={paginationModel.PerPage}";
            var response = await _httpClient.GetAsync(APIURL);

            var jsonResult = await response.Content.ReadAsStringAsync();
            
            var drinksMenu = JsonSerializer.Deserialize<List<Drink>>(jsonResult);
            
            return drinksMenu;
        }

        
        public Booking Create(BookingDTO model, HttpContext userContext)
        {
            var httpContext = userContext;
            //var jwt = httpContext.Request.Headers["Authorization"];
            var userIdString = httpContext.User?.Claims.First(x => x.Type == ClaimTypes.UserData).Value;
            var userId = Guid.Parse(userIdString);
            
            var booking = _mapper.Map<Booking>(model);
            booking.CreatedAt = DateTime.UtcNow;
            booking.UpdatedAt = DateTime.UtcNow;
            booking.UserId = userId;

            _context.Bookings.Add(booking);
            _context.SaveChanges();

            return booking;
        }

        public Booking Update(BookingDTO model, Guid id)
        {
            var updatedBooking = _context.Bookings.SingleOrDefault(x => x.BookingId == id);

            updatedBooking.Comment = model.Comment;
            updatedBooking.DishId = model.DishId;
            updatedBooking.DrinkId = model.DrinkId;
            updatedBooking.UpdatedAt = DateTime.UtcNow;

            _context.Bookings.Update(updatedBooking);
            _context.SaveChanges();

            return updatedBooking;
        }

        public Booking Delete(Guid id)
        {
            var bookingToDelete = _context.Bookings.Find(id);
            
            bookingToDelete.DeletedAt = DateTime.UtcNow;

            _context.SaveChanges();

            return bookingToDelete;
        }


    }
}