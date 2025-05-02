using System.ComponentModel.DataAnnotations;

namespace ZimojiAssessmentProject.Models
{
    public class AvailableTasks
    {
        [Key]
        public int Task_Id { get; set; }
        [Required]
        public string Task_Name { get; set; }
        [Required]
        public string Task_Description { get; set; }
        
        //public virtual ICollection<UserTasks> UserTasks { get; set; }

    }
}
