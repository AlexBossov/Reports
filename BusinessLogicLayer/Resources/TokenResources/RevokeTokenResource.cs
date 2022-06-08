using System.ComponentModel.DataAnnotations;

namespace BusinessLogicLayer.Resources.TokenResources
{
    public class RevokeTokenResource
    {
        [Required]
        public string Token { get; set; }
    }
}