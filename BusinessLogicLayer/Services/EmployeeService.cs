using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Resources.EmployeeResources;
using BusinessLogicLayer.Services.Communication;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;

namespace BusinessLogicLayer.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        
        public EmployeeService(IEmployeeRepository employeeRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        
        public async Task<IEnumerable<EmployeeResource>> ListAsync(EmployeeParameters employeeParameters)
        {
            IEnumerable<Employee> employees = await _employeeRepository.ListAsync(employeeParameters);
            IEnumerable<EmployeeResource> resources = 
                _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeResource>>(employees);
            return resources;
        }

        public async Task<IEnumerable<EmployeeTreeResource>> FindByIdAsync()
        {
            IEnumerable<Employee> employees = await _employeeRepository.GetEmployeeTreeAsync();
            IEnumerable<EmployeeTreeResource> employeeTreeResources = 
                _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeTreeResource>>(employees);
            return employeeTreeResources;
        }

        public async Task<EmployeeResponse> SaveAsync(SaveEmployeeResource saveEmployeeResource)
        {
            Employee employee = _mapper.Map<SaveEmployeeResource, Employee>(saveEmployeeResource);
            Employee existingEmployee = await FindByEmailAsync(employee.Email);
            if (existingEmployee != null)
                return new EmployeeResponse("Employee with the email already exist");
            
            try
            {
                await _employeeRepository.AddAsync(employee);
                await _unitOfWork.CompleteAsync();
                EmployeeResource resource = _mapper.Map<Employee, EmployeeResource>(employee);
                return new EmployeeResponse(resource);
            }
            catch (Exception ex)
            {
                return new EmployeeResponse($"An error occurred when saving the employee: {ex.Message}");
            }
        }

        public async Task<EmployeeResponse> UpdateAsync(int id, SaveEmployeeResource saveEmployeeResource)
        {
            Employee employee = _mapper.Map<SaveEmployeeResource, Employee>(saveEmployeeResource);
            Employee existingEmployee = await _employeeRepository.FindByIdAsync(id);

            if (existingEmployee == null)
                return new EmployeeResponse("Employee not found.");

            existingEmployee.Name = employee.Name;
            existingEmployee.Email = employee.Email;
            existingEmployee.HeadId = saveEmployeeResource.HeadId;
            
            try
            {
                _employeeRepository.Update(existingEmployee);
                await _unitOfWork.CompleteAsync();
                EmployeeResource resource = _mapper.Map<Employee, EmployeeResource>(employee);
                return new EmployeeResponse(resource);
            }
            catch (Exception ex)
            {
                return new EmployeeResponse($"An error occurred when updating the employee: {ex.Message}");
            }
        }

        public async Task<EmployeeResponse> DeleteAsync(int id)
        {
            Employee existingEmployee = await _employeeRepository.FindByIdAsync(id);

            if (existingEmployee == null)
                return new EmployeeResponse("Employee not found.");
            foreach (Employee existingEmployeeSubordinate in existingEmployee.Subordinates)
                await DeleteAsync(existingEmployeeSubordinate.Id);
            
            try
            {
                _employeeRepository.Remove(existingEmployee);
                await _unitOfWork.CompleteAsync();
                EmployeeResource resource = _mapper.Map<Employee, EmployeeResource>(existingEmployee);
                return new EmployeeResponse(resource);
            }
            catch (Exception ex)
            {
                return new EmployeeResponse($"An error occurred when deleting the employee: {ex.Message}");
            }
        }

        public async Task<Employee> FindByEmailAsync(string email)
        {
            return await _employeeRepository.FindByEmailAsync(email);
        }
    }
}