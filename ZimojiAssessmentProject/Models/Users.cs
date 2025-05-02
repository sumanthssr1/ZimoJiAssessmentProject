using System.ComponentModel.DataAnnotations;

namespace ZimojiAssessmentProject.Models
{
    public class Users
    {
        [Key]
        public int User_Id { get; set; }

        [Required]
        public string User_Name { get; set; }

        [Required]
        public string User_Email { get; set; }

        [Required]
        public string User_Password { get; set; }

        [Required]
        public string User_PhoneNumber { get; set; }

        [Required]
        public string User_Address { get; set; }

        [Required]
        public UserRole Role { get; set; }  // 👈 Enum-based Role

        public virtual ICollection<UserTasks> UserTasks { get; set; }
    }
    public enum UserRole
    {
        Admin = 1,
        User = 2
    }



}
