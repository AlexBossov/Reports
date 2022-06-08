using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLayer.EF;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories
{
    public class EmployeeRepository : BaseRepository, IEmployeeRepository
    {
        public EmployeeRepository(ReportsDbContext context)
            : base(context)
        {
        }

        public async Task<IEnumerable<Employee>> ListAsync(EmployeeParameters employeeParameters)
        {
            return await Context.Employees
                .Include(e => e.EmployeeRoles)
                .ThenInclude(e => e.Role)
                .Include(e => e.Problems)
                .Where(e => employeeParameters.Name == string.Empty || e.Name == employeeParameters.Name)
                .OrderBy(e => e.Id)
                .Skip((employeeParameters.PageNumber - 1) * employeeParameters.PageSize)
                .Take(employeeParameters.PageSize)
                .ToListAsync();
        }

        public async Task<IEnumerable<Employee>> GetEmployeeTreeAsync()
        {
            return await Context.Employees
                .Include(e => e.EmployeeRoles)
                .ThenInclude(e => e.Role)
                .Include(e => e.Problems)
                .ToListAsync();
        }

        public async Task AddAsync(Employee employee)
        {
            await Context.Employees.AddAsync(employee);
        }

        public async Task<Employee> FindByIdAsync(int id)
        {
            return await Context.Employees.FirstOrDefaultAsync();
        }

        public void Update(Employee employee)
        {
            Context.Employees.Update(employee);
        }

        public void Remove(Employee employee)
        {
            Context.Employees.Remove(employee);
        }

        public async Task<Employee> FindByEmailAsync(string email)
        {
            return await Context.Employees
                .Include(u => u.EmployeeRoles)
                .ThenInclude(ur => ur.Role)
                .SingleOrDefaultAsync(u => u.Email == email);
        }
    }
}