using FluentValidation;
using SundownBoulevard.DTO;

namespace SundownBoulevard.Validation
{
    public class UserDTOValidator : AbstractValidator<UserDTO>
    {

        public UserDTOValidator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Password).NotEmpty();
        }
        
        
    }
}