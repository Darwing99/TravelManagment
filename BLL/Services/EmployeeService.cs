using DAL.DataAccess;
using Entities.Models;


namespace BLL.Services
{
    public class EmployeeService
    {
        private readonly EmployeeDao _employeeDao;
        public EmployeeService(EmployeeDao employeeDao) =>_employeeDao=employeeDao;
        public async Task<List<Employee>> GetEmployeesAsync() => await _employeeDao.GetEmployees();
        public async Task<List<EmployeeByBranch>> GetEmployeesByBranchId(int BranchId)=> await _employeeDao.GetEmployeesByBranchId(BranchId);   

        public async Task<bool> CreateAsync(Employee employee) => await _employeeDao.CreateAsync(employee);
    }
}
