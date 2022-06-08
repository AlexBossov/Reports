using System.ComponentModel.DataAnnotations;

namespace BusinessLogicLayer.Resources.ProblemResources
{
    public class SaveProblemResource
    {
         [Required]
         [MaxLength(100)]
         public string Description { get; set; }
         [MaxLength(500)]
         public string Comments { get; set; }
         // [Required]
         // public string EProblemState { get; set; }
         [Required]
         public int EmployeeId { get; set; }
    }
}