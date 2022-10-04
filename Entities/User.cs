using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SundownBoulevard.Enums;

namespace SundownBoulevard.Entities
{
    public class User
    {
        [Key] 
        public Guid UserId { get; } = Guid.NewGuid();
        
        public String Email { get; set; }
        
        public string  Password { get; set; }

        public DateTime? CreatedAt { get; set; }
        
        public DateTime? UpdatedAt { get; set; }
        
        public DateTime? DeletedAt { get; set; }
        
        public UserRoles UserRole { get; set; } 

        public List<Booking> Bookings { get; set; }
        
        
    }
}