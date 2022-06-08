using System;
using System.Collections.Generic;
using DataAccessLayer.EStates;

namespace DataAccessLayer.Entities
{
    public class Problem
    {
        public int ProblemId { get; set; }
        public string Description { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime ChangeTime { get; set; }
        public EProblemState EProblemState { get; set; }
        public int? EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public int? ReportId { get; set; }
        public Report Report { get; set; }
        public IList<Comment> Comments { get; set; } = new List<Comment>();
    }
}