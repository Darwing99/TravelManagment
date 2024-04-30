using Entities.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DataAccess
{
    public class EmployeeDao:ConnectionToSql
    {
        public EmployeeDao() { }
        public async Task<List<Employee>> GetEmployees()
        {
            try
            {
                string sql = "SELECT*FROM Employee WHERE Status=1";
                var result = await ExecuteQuerySelect(sql, reader => {
                    return new Employee() {
                         EmployeeId = reader["EmployeeID"] != DBNull.Value ? Convert.ToInt32(reader["EmployeeID"]) : 0,
                         Name = reader["Name"]!=DBNull.Value ? reader["Name"].ToString() : "",
                         Address = reader["Address"]!=DBNull.Value ? reader["Address"].ToString() : "",                   
                }; 
                });
                return result;
            }
            catch (Exception)
            {
                return null;
                throw;
            }
        }
     
        public async Task<List<EmployeeByBranch>> GetEmployeesByBranchId(int BranchId)
        {
            try
            {
                string sql = @"SELECT EM.EmployeeID AS EmployeeID, 
                        EM.Name as Name, EM.Address, EBA.DistanceToHome AS DistanceToHome FROM EmployeeBranchAssignment  as EBA  
                        INNER JOIN Employee AS EM on EBA.EmployeeID=EM.EmployeeID  
                        INNER JOIN Branches AS Br on EBA.BranchID=Br.BranchId 
                        WHERE em.Status=1 AND Br.Status=1 and  BR.BranchId=@BranchId";
                SqlParameter[] sqlParameters = [
                    new("@BranchId",BranchId)
                    ];
                var result = await ExecuteQuerySelect(sql, reader => {
                    return new EmployeeByBranch()
                    {
                        EmployeeId = reader["EmployeeID"] != DBNull.Value ? Convert.ToInt32(reader["EmployeeID"]) : 0,
                        Name = reader["Name"] != DBNull.Value ? reader["Name"].ToString() : "",
                        Address = reader["Address"] != DBNull.Value ? reader["Address"].ToString() : "",
                        DistanceToHome = reader["DistanceToHome"]!=DBNull.Value ? (decimal)reader["DistanceToHome"] :0,
                    };
                },sqlParameters);
                return result;
            }
            catch (Exception)
            {
                return null;
                throw;
            }
        }

        public async Task<bool> CreateAsync(Employee employee)
        {
            try
            {
                string sql = @"INSERT INTO [dbo].[Employee]
                               ([Name]
                               ,[Address]
                               ,[Status])
                         VALUES
                               (@Name
                               ,@Address
                               ,@Status)";
                SqlParameter[] sqlParameters = [
                    new("@Name",employee.Name),
                    new("@Address", employee.Address),
                    new("@Status", employee.Status),

                ];
                var result = await ExecuteQuery(sql, sqlParameters);
                return result>0;
            }
            catch (Exception)
            {
                return false;
                
            }
        }
    }
}
