using SundownBoulevard.Entities;

namespace SundownBoulevard.Models
{
    public class AuthenticationResponse
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public string JwtToken { get; set; }



        public AuthenticationResponse(User user, string jwtToken)
        {
            Email = user.Email;
            Password = user.Password;
            JwtToken = jwtToken;
        }
    }
    
    
    
    
    
}