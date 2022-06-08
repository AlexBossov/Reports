using BusinessLogicLayer.Resources.EmployeeResources;

namespace BusinessLogicLayer.Services.Communication
{
    public class EmployeeResponse : BaseResponse
    {
        public EmployeeResource EmployeeResource { get; private set; }

        private EmployeeResponse(bool success, string message, EmployeeResource employee) 
            : base(success, message)
        {
            EmployeeResource = employee;
        }

        public EmployeeResponse(EmployeeResource employeeResource)
            : this(true, string.Empty, employeeResource)
        {
        }

        public EmployeeResponse(string message)
            : this(false, message, null)
        {
        }
    }
}