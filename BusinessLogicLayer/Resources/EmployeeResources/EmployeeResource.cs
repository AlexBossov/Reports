using System.Collections.Generic;

namespace BusinessLogicLayer.Resources.EmployeeResources
{
    public class EmployeeResource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int? HeadId { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }
}