using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SundownBoulevard.Data;
using SundownBoulevard.DTO;
using SundownBoulevard.Entities;
using SundownBoulevard.Enums;
using SundownBoulevard.Models;

namespace SundownBoulevard.Services
{

    public interface IUserService
    {
        AuthenticationResponse Authenticate(UserDTO model);
        User Create(UserDTO model);
        IEnumerable<User> GetAll();
        User GetById(Guid id);
        User Update(UserDTO model, Guid id);
        User Delete(Guid id);
    }
    
    
    public class UserService : IUserService
    {

        private readonly SundownDbContext _context;
        private readonly IMapper _mapper;
        private readonly AppSettings _appSettings;
        private readonly IValidator<UserDTO> _validator;

        public UserService(SundownDbContext context, IMapper mapper,
            IOptions<AppSettings> appSettings, IValidator<UserDTO> validator)
        {
            _context = context;
            _mapper = mapper;
            _appSettings = appSettings.Value;
            _validator = validator;
        }

        public AuthenticationResponse Authenticate(UserDTO model)
        {
            foreach (var userItem in _context.Users)

            {
                if (userItem.Email == model.Email)
                {
                    var veryfiedPassword = VerifyPassword(userItem.Password, model.Password);
                    if (veryfiedPassword == true)
                    {
                        model.Password = userItem.Password;
                    }
                }
            }

            var user = _context.Users.SingleOrDefault(x => x.Email == model.Email && x.Password == model.Password);
            
            // authentication successful so generate jwt token
            var token = GenerateJwtToken(user);
            
            
            return new AuthenticationResponse(user, token);
        }
        
        
        public User Create(UserDTO model)
        {
            var result = _validator.Validate(model);

            if (!result.IsValid)
            {
                throw new ValidationException("Validation failed");
            }
            
            var user = _mapper.Map<User>(model);
            user.Password = HashPassword(model.Password);
            user.CreatedAt = DateTime.UtcNow;
            user.UpdatedAt = DateTime.UtcNow;

            _context.Users.Add(user);
            _context.SaveChanges();
            
            return user;
        }


        public IEnumerable<User> GetAll()
        {
            return _context.Users.ToList();
        }

        public User GetById(Guid id)
        {
            var user = _context.Users.SingleOrDefault(x => x.UserId == id);
        
            return user;
        }

        public User Update(UserDTO model, Guid id)
        {
            var result = _validator.Validate(model);

            if (!result.IsValid)
            {
                throw new ValidationException("Validation failed");
            }
            
            var updatedUser = _context.Users.SingleOrDefault(x => x.UserId == id);
            
            updatedUser.Email = model.Email;
            updatedUser.Password = model.Password;
            updatedUser.UpdatedAt = DateTime.UtcNow;

            _context.Users.Update(updatedUser);
            _context.SaveChanges();

            return updatedUser;
        }

        public User Delete(Guid id)
        {
            var userToDelete = _context.Users.Find(id);
            
            userToDelete.DeletedAt = DateTime.UtcNow;
            
            _context.SaveChanges();

            return userToDelete;
        }
        
        
        
        
        
        
        // Helper methods

        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.UserData, user.UserId.ToString()),
                    new Claim(ClaimTypes.Role,  Enum.GetName(typeof(UserRoles), user.UserRole) ?? string.Empty)
                }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }


        public string HashPassword(string password, byte[] salt = null, bool needsOnlyHash = false)
        {
            if (salt == null || salt.Length != 16)
            {
                // generate a 128-bit salt using a secure PRNG
                salt = new byte[128 / 8];
                using (var rng = RandomNumberGenerator.Create())
                {
                    rng.GetBytes(salt);
                }
            }

            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            if (needsOnlyHash) return hashed;
            // password will be concatenated with salt using ':'
            return $"{hashed}:{Convert.ToBase64String(salt)}";
        }

        


        private bool VerifyPassword(string hashedPasswordWithSalt, string passwordToCheck)
        {
            // retrieve both salt and password from 'hashedPasswordWithSalt'
            var passwordAndHash = hashedPasswordWithSalt.Split(':');
            if (passwordAndHash == null || passwordAndHash.Length != 2)
                return false;
            var salt = Convert.FromBase64String(passwordAndHash[1]);
            if (salt == null)
                return false;
            // hash the given password
            var hashOfpasswordToCheck = HashPassword(passwordToCheck, salt, true);
            // compare both hashes
            if (String.Compare(passwordAndHash[0], hashOfpasswordToCheck) == 0)
            {
                return true;
            }
            return false;
        }


    }
}