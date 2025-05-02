using System.Data.Entity;
using ZimojiAssessmentProject.DataAccessLayer;
using ZimojiAssessmentProject.DTO;
using ZimojiAssessmentProject.Interfaces;
using ZimojiAssessmentProject.Models;

namespace ZimojiAssessmentProject.InterfaceImplementations
{
    public class UserTaskImplementations : IUserTaskActions
    {
        private readonly UserTaskContext userTaskContext;
        public UserTaskImplementations(UserTaskContext context)
        {
            this.userTaskContext = context;
        }

        public async Task<IEnumerable<AvailableTasks>> GetAllAvailableTasksForUserAsync(int id)
        {
            var userTasks = (from task in this.userTaskContext.AvailableTasks
                             where !(from ut in this.userTaskContext.UserTasks
                                     where ut.User_ID == id
                                     select ut.Task_Id)
                                     .Contains(task.Task_Id)
                             select task);
            return await Task.FromResult(userTasks.ToList());
        }

        public async Task<IEnumerable<Users>> GetAllUsersAsync()
        {
            var users = userTaskContext.Users.ToList();
            return await Task.FromResult( users.ToList());
        }


        public Task<IEnumerable<UsetTaskDTO>> GetAllUserTask()
        {
            var result = (from task in userTaskContext.UserTasks
                          join user in userTaskContext.Users on task.User_ID equals user.User_Id
                          join t in userTaskContext.AvailableTasks on task.Task_Id equals t.Task_Id
                          select new UsetTaskDTO
                          {
                              Task_Id = task.Task_Id,
                              UserTask_Id = task.UserTask_Id,
                              User_ID = task.User_ID,
                              Task_Name = t.Task_Name,
                              Task_Description = t.Task_Description,
                              User_Name = user.User_Name,
                              User_Email = user.User_Email
                          }).ToList();

            return Task.FromResult<IEnumerable<UsetTaskDTO>>(result);
        }




        public async Task<IEnumerable<AvailableTasks>> GetAvailableTaskByIdAsync(int id)
        {
            var tasks= (from task  in this.userTaskContext.AvailableTasks
                        where  task.Task_Id == id
                        select task).ToList();
            return await Task.FromResult(tasks.ToList());
        }

    }
}
