using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZimojiAssessmentProject.DataAccessLayer;
using ZimojiAssessmentProject.Models;

namespace ZimojiAssessmentProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(policy:"AdminPolicy")]
    public class AvailableTasksController : ControllerBase
    {
        private readonly UserTaskContext _context;

        public AvailableTasksController(UserTaskContext context)
        {
            _context = context;
        }

        // GET: api/AvailableTasks
        
        // POST: api/AvailableTasks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AvailableTasks>> PostAvailableTasks(AvailableTasks availableTasks)
        {
            _context.AvailableTasks.Add(availableTasks);
            await _context.SaveChangesAsync();

            return Ok(availableTasks);
        }

      
    }
}
