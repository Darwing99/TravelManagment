using DAL.DataAccess;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class BranchService
    {
        private readonly BranchDao branchDao;
        public BranchService(BranchDao branchDao)
        {
            this.branchDao = branchDao;
        }
        
        public async Task<List<Branch>> GetBranchesAsync() => await branchDao.GetBranchAllAsync();
        public async Task<bool> SaveBranchAsync(Branch branch) => await branchDao.SaveBranch(branch);
    }
}
