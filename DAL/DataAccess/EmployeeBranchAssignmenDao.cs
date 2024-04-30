using Entities.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DataAccess
{
    public class EmployeeBranchAssignmenDao : ConnectionToSql
    {
        public EmployeeBranchAssignmenDao() { }
        public  int CountBranchByEmployee(int EmployeeID,int BranchID)
        {
            try
            {
                string sql = @"SELECT COUNT(*) as count FROM EmployeeBranchAssignment where EmployeeID = @EmployeeID and BranchID=@BranchID and Status=1";
                SqlParameter[] sp = [
                    new("@EmployeeID",EmployeeID),
                    new("@BranchID",BranchID)
                    ];
                var result =  ExecuteQuerySelectSingle(sql, reader => reader["count"]  ,sp);
                return Convert.ToInt32(result);
            }
            catch (Exception)
            {
                return 0;
                throw;
            }
          
        
        }
        public async Task<bool> AddAsync(EmployeeBranchAssignmen employeeBranchAssignmen)
        {
            try
            {
                string sql = @"INSERT INTO [dbo].[EmployeeBranchAssignment]
                               ([EmployeeID]
                               ,[BranchID]
                               ,[DistanceToHome]
                               ,[Status])
                         VALUES
                               (@EmployeeID
                               ,@BranchID
                               ,@DistanceToHome
                               ,@Status)";

                SqlParameter[] sqlParameters = [
                    new("@EmployeeID",employeeBranchAssignmen.EmployeeId),
                    new("@BranchID",employeeBranchAssignmen.BranchId),
                    new("@DistanceToHome",employeeBranchAssignmen.DistanceToHome),
                    new("@Status",employeeBranchAssignmen.Status)
                    ];
                var result= await ExecuteQuery(sql, sqlParameters);
                return result > 0;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }
    }
}
