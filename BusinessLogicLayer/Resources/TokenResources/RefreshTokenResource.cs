using System.ComponentModel.DataAnnotations;

namespace BusinessLogicLayer.Resources.TokenResources
{
    public class RefreshTokenResource
    {
        [Required]
        public string Token { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [StringLength(255)]
        public string EmployeeEmail { get; set; }
    }
}