using System.Collections.Generic;
using System.Threading.Tasks;
using DataAccessLayer.Entities;

namespace DataAccessLayer.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> ListAsync(EmployeeParameters employeeParameters);
        Task<IEnumerable<Employee>> GetEmployeeTreeAsync();
        Task AddAsync(Employee employee);
        Task<Employee> FindByIdAsync(int id);
        void Update(Employee employee);
        void Remove(Employee employee);
        Task<Employee> FindByEmailAsync(string email);
    }
}