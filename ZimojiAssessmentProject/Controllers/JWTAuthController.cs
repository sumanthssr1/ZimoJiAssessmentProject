using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ZimojiAssessmentProject.DataAccessLayer;
using ZimojiAssessmentProject.Exceptions;
using ZimojiAssessmentProject.AuthenticationService;
using IAuthenticationService = ZimojiAssessmentProject.AuthenticationService.IAuthenticationService;
namespace ZimojiAssessmentProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JWTAuthController : ControllerBase
    {
       
        private readonly IAuthenticationService _authService;
        private readonly UserTaskContext _Context;

        public JWTAuthController(IAuthenticationService authService, UserTaskContext context)
        {
            _authService = authService;
            _Context = context;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            if (model == null || string.IsNullOrEmpty(model.Username) || string.IsNullOrEmpty(model.Password))
            {
                throw new UserNotFoundException("UserName and Password should needed.... Please enter Username and password!!!...");
            }

            var user = _Context.Users.FirstOrDefault(x => x.User_Email == model.Username);
            if (user == null)
            {
                return Unauthorized("Invalid UserName and Password... Please try again");
            }

            var result = await _authService.Authenticate(model.Username, model.Password);
            if (!result)
            {
                return Unauthorized("Invalid password.");
            }
            var token = _authService.GenerateJwtToken(user);

            return Ok(token);
        }

    }

    public class LoginDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    // here are the sample user_email and password details to enter to get JWT code
    /*       As the password has been hashed and stored in Database.
     *       {
   "username": "p_suman_s",
   "password": "P_Suman@2025"
 }


 Student example object
             {
   "username": "suman_sadham",
   "password": "Sumanth@12345"
 }
 */
}
