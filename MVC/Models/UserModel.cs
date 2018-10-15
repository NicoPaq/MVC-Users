using System.ComponentModel.DataAnnotations;

namespace MVC.Models
{
    public class UserModel
    {

        public int Id { get; set; }

        [Display(Name = "Firstname")]
        [Required(ErrorMessage = "The field Firstname is required")]

        public string Firstname { get; set; }

        [Display(Name = "Lastname")]
        [Required(ErrorMessage = "The field Lastname is required")]
        
        public string Lastname { get; set; }

        [Display(Name = "E-Mail")]
        [Required(ErrorMessage =  "The field Email is required ")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

    }
}