using System.Collections.Generic;

namespace DataAccessLayer.Entities
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Report Report { get; set; }
        public int? HeadId { get; set; }
        public Employee Head { get; set; }
        public IList<Employee> Subordinates { get; set; } = new List<Employee>();
        public IList<Problem> Problems { get; set; } = new List<Problem>();
        public IList<EmployeeRole> EmployeeRoles { get; set; } = new List<EmployeeRole>();
    }
}