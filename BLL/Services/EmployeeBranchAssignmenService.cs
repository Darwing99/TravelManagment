using DAL.DataAccess;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class EmployeeBranchAssignmenService(EmployeeBranchAssignmenDao employeeBranchAssignmenDao)
    {
        private readonly EmployeeBranchAssignmenDao employeeBranchAssignmenDao = employeeBranchAssignmenDao;

        public int CountBranchesByEmployee(int EmployeeID, int BranchID)
            => employeeBranchAssignmenDao.CountBranchByEmployee(EmployeeID, BranchID);
        public async Task<bool> AddAsync(EmployeeBranchAssignmen employeeBranchAssignmen) 
            => await employeeBranchAssignmenDao.AddAsync(employeeBranchAssignmen);
    }
}
