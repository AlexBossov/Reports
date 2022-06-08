using System;

namespace BusinessLogicLayer.Resources.ProblemResources
{
    public class ProblemResource
    {
        public int ProblemId { get; set; }
        public string Description { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime ChangeTime { get; set; }
        public string EProblemState { get; set; }
        public int EmployeeId { get; set; }
    }
}