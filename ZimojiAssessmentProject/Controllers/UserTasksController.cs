using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZimojiAssessmentProject.DataAccessLayer;
using ZimojiAssessmentProject.DTO;
using ZimojiAssessmentProject.Interfaces;
using ZimojiAssessmentProject.Models;

namespace ZimojiAssessmentProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="Admin")]
    public class UserTasksController : ControllerBase
    {
        private readonly UserTaskContext _context;
        IUserTaskActions uTaskActions;
        public UserTasksController(UserTaskContext context,IUserTaskActions userTaskActions)
        {
            this.uTaskActions=userTaskActions;
            this._context = context;
        }
        [HttpGet("GetAllUserTasks")]
        public async Task<IActionResult> GetAllUserTasks()
        {
            var usertasksdetails = await uTaskActions.GetAllUserTask(); // ✅ add await
            return Ok(usertasksdetails);
        }


      
        // POST: api/UserTasks to Add new task to user(mapping new task to user profile so that user subscribed to Particular task)
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<UserTasks>> PostUserTasks(UserTasks userTasks)
        {
            _context.UserTasks.Add(userTasks);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserTasks", new { id = userTasks.UserTask_Id }, userTasks);
        }

      
    }
}
