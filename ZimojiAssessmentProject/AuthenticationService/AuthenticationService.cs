using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using ZimojiAssessmentProject.DataAccessLayer;
using ZimojiAssessmentProject.Models;

namespace ZimojiAssessmentProject.AuthenticationService
{
    public class AuthenticationService : IAuthenticationService

    {
        private readonly UserTaskContext _context;
        private readonly IConfiguration _configuration;

        public AuthenticationService(UserTaskContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public Task<bool> Authenticate(string username, string password)
        {

            var user = _context.Users.FirstOrDefault(x => x.User_Email == username);
            if (user == null)
            {
                return Task.FromResult(false);
            }
            var hashedPassword = GetSha256Hash(password);
            bool isPasswordValid = VerifyPassword(hashedPassword, user.User_Password);
            if (!isPasswordValid)
            {
                // Invalid password
                return Task.FromResult(false);
            }

            return Task.FromResult(true);

        }
        public bool VerifyPassword(string password, string storedHash)
        {

            return password == storedHash;
        }
        private string GetSha256Hash(string input)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public async Task<string> GenerateJwtToken(Users user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            // Ensure the key is of appropriate length (256 bits = 32 bytes for HMACSHA256)
            var key = Encoding.ASCII.GetBytes(_configuration["jwt:key"]);

            // Create claims based on the user's roles and ID
            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, user.User_Name),
        new Claim("User_Id", user.User_Id.ToString())
    };

            // Add roles based on the user's flags
            if (user.Role == UserRole.Admin)
            {
                claims.Add(new Claim(ClaimTypes.Role, "Admin"));
            }
            if (user.Role == UserRole.User)
            {
                claims.Add(new Claim(ClaimTypes.Role, "User"));
            }

            // Set token expiration and signing credentials
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(10),  // You can adjust the expiration time as needed
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            // Create the token
            var token = tokenHandler.CreateToken(tokenDescriptor);

            // Return the token as a string
            return await Task.FromResult(tokenHandler.WriteToken(token));
        }


    
}
}
