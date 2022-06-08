using System.Collections.Generic;
using DataAccessLayer.EStates;

namespace DataAccessLayer.Entities
{
    public class Report
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public EReportState EReportState { get; set; }
        public IList<Problem> Problems { get; set; } = new List<Problem>();
        public int? EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }
}