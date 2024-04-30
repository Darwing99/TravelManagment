using Entities.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DataAccess
{
    public class BranchDao:ConnectionToSql
    {
        public BranchDao() { }
        public async Task<List<Branch>> GetBranchAllAsync() {
			try
			{
				string sql = @"SELECT*FROM Branches where Status=1";
				var result = await ExecuteQuerySelect(sql, reader =>
				{
					return new Branch() {
						BranchId = reader["BranchId"]!=DBNull.Value? Convert.ToInt32(reader["BranchId"]) : 0,
						Address = reader["Address"]!=DBNull.Value ? reader["Address"].ToString() : "",
						Name = reader["Name"]!=DBNull.Value ? reader["Name"].ToString() : "",
					};
				});
				return result;
			}
			catch (Exception)
			{

				throw;
			}
        }
		public async Task<bool> SaveBranch(Branch branch)
		{
			try
			{
				string sql = @"INSERT INTO [dbo].[Branches]
							   ([Name]
							   ,[Address]
							   ,[Status])
						 VALUES
							   (@Name,@Address,@Status)";
				SqlParameter[] sqlParameters = [
					new("@Name",branch.Name),
					new("@Address",branch.Address),
					new("@Status",branch.Status)
					];
				var result = await ExecuteQuery(sql, sqlParameters);
				return result > 0;
			}
			catch (Exception)
			{

				throw;
			}
		}
    }
}
