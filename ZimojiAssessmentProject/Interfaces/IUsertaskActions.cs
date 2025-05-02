using ZimojiAssessmentProject.DTO;
using ZimojiAssessmentProject.Models;

namespace ZimojiAssessmentProject.Interfaces
{
    public interface IUserTaskActions
    {
        Task<IEnumerable<Users>> GetAllUsersAsync();
        Task<IEnumerable<AvailableTasks>> GetAllAvailableTasksForUserAsync(int id);
        Task<IEnumerable<AvailableTasks>> GetAvailableTaskByIdAsync(int id);
      
        Task<IEnumerable<UsetTaskDTO>> GetAllUserTask();


    }
}
