using ZimojiAssessmentProject.Models;

namespace ZimojiAssessmentProject.AuthenticationService
{
    public interface IAuthenticationService
    {
        Task<bool> Authenticate(string username, string password);
        public Task<string> GenerateJwtToken(Users user);
    }
}
