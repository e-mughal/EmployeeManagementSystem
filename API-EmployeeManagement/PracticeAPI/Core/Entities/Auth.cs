using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace EmployeeManagementAPI.Core.Entities
{
    public class Auth
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [PasswordPropertyText]
        [MinLength(5)]
        public string Password { get; set; }
    }
}
