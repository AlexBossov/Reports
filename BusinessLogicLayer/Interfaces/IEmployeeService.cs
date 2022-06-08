using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLogicLayer.Resources.EmployeeResources;
using BusinessLogicLayer.Services.Communication;
using DataAccessLayer.Entities;

namespace BusinessLogicLayer.Interfaces
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeResource>> ListAsync(EmployeeParameters employeeParameters);
        Task<IEnumerable<EmployeeTreeResource>> FindByIdAsync();
        Task<EmployeeResponse> SaveAsync(SaveEmployeeResource saveEmployeeResource);
        Task<EmployeeResponse> UpdateAsync(int id, SaveEmployeeResource saveEmployeeResource);
        Task<EmployeeResponse> DeleteAsync(int id);
        Task<Employee> FindByEmailAsync(string email);
    }
}