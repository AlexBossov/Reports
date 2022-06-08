using System.ComponentModel.DataAnnotations;

namespace BusinessLogicLayer.Resources.EmployeeResources
{
    public class EmployeeCredentials
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        [MaxLength(255)]
        public string Email { get; set; }

        [Required]
        [MaxLength(100)]
        public string Password { get; set; }
    }
}