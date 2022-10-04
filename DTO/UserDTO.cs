using System;

namespace SundownBoulevard.DTO
{
    public class UserDTO
    {
        public string Email { get; set; }
        
        public string  Password { get; set; }

        public int UserRole { get; set; }
    }
}