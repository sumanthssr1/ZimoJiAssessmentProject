using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ZimojiAssessmentProject.DataAccessLayer;
using ZimojiAssessmentProject.InterfaceImplementations;
using ZimojiAssessmentProject.Interfaces;
using ZimojiAssessmentProject.Models;

namespace ZimojiAssessmentProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles ="User")]
   // [Authorize(Roles ="Admin")]
    public class UserController : ControllerBase
    { 

       // private readonly UserTaskImplementations userTaskImplementations;
        private readonly IUserTaskActions userTaskActions;
        private readonly UserTaskContext userTaskContext;
        public  UserController( IUserTaskActions Iuta, UserTaskContext utc)
        {
          
            this.userTaskActions = Iuta;
            this.userTaskContext = utc;
            
        }
        
        [HttpGet("TaskByuserID/{UserID}")]
        public async Task<IActionResult> GetTasksByUserID(int userid)
        {
            var Tasks=await userTaskActions.GetAllAvailableTasksForUserAsync(userid);
            return Ok(Tasks);
        }


        [HttpGet("taskById/{TaskID}")]
        public async Task<IActionResult> GetTaskByID(int taskid)
        {

            var availabltask=await userTaskActions.GetAvailableTaskByIdAsync(taskid);
            Console.WriteLine(availabltask.ToList());
            return Ok(availabltask);

        }
       
    }
}
