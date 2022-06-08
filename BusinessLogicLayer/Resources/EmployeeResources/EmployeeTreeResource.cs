using System.Collections.Generic;

namespace BusinessLogicLayer.Resources.EmployeeResources
{
    public class EmployeeTreeResource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public IEnumerable<EmployeeResource> Subordinates { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }
}