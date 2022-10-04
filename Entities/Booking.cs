using System;
using System.ComponentModel.DataAnnotations;

namespace SundownBoulevard.Entities
{
    public class Booking
    {
        [Key]
        public Guid BookingId { get; } = Guid.NewGuid();

        public string Comment { get; set; }

        public int DrinkId { get; set; }
        
        public int DishId { get; set; }

        public DateTime? CreatedAt { get; set; }
        
        public DateTime? UpdatedAt { get; set; }
        
        public DateTime? DeletedAt { get; set; }

        public Guid UserId { get; set; }

        public User User { get; set; }
    }
}