using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HrOffice.Models
{
    public class Subscription
    {
        [Key]
        public int SubscriptionId { get; set; }

        [Required(ErrorMessage = "User Name is required field")]
        [DataType(DataType.EmailAddress)]
        [MaxLength(50)]
        [DisplayName("User Name")]
        public string UserName { get; set; }


        [Required(ErrorMessage = "Company is required field")]
        [DataType(DataType.Text)]
        [MaxLength(50)]
        [DisplayName("Company Name")]
        public string CompanyName { get; set; }


        [Required(ErrorMessage = "DBName is required field")]
        [DataType(DataType.Text)]
        [MaxLength(10)]
        [DisplayName("DB Name")]
        public string DBName { get; set; }


        [Required(ErrorMessage = "Email is required field")]
        [DataType(DataType.EmailAddress)]
        [MaxLength(20)]
        [DisplayName("Email")]
        public string EmailId { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [MaxLength(20)]
        [DisplayName("User Password")]
        public string UserPassword { get; set; }


        [Required(ErrorMessage = "Role is required field from (Admin,User)")]
        [DataType(DataType.Text)]
        [MaxLength(10)]
        [DisplayName("Role")]
        public string Role { get; set; }
    }
}
