using Microsoft.EntityFrameworkCore;
using ZimojiAssessmentProject.Models;

namespace ZimojiAssessmentProject.DataAccessLayer
{
    public class UserTaskContext : DbContext
    {
        public DbSet<Users> Users { get; set; }
        public DbSet<AvailableTasks> AvailableTasks { get; set; }
        public DbSet<UserTasks> UserTasks { get; set; }
        public UserTaskContext(DbContextOptions options) : base(options)
        {
        }

        protected UserTaskContext()
        {
        }
    }
}
