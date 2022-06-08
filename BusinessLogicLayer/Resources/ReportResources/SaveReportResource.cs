using System.ComponentModel.DataAnnotations;

namespace BusinessLogicLayer.Resources.ReportResources
{
    public class SaveReportResource
    {
        [Required]
        [MaxLength(500)]
        public string Description { get; set; }
        public string EReportState { get; set; }
        public int EmployeeId { get; set; }
    }
}