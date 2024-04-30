using Entities.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DataAccess
{
    public class UserDao:ConnectionToSql
    {
        public UserDao() { }    
        public async Task<bool> CreateUserAsync(User user)
        {
            try
            {
                var sql = @"INSERT INTO [dbo].[Users]
                           ([Code]
                           ,[Name]
                           ,[Username]
                           ,[IdentityNumber]
                           ,[Email]
                           ,[Password]
                           ,[CreateBy]
                           ,[ModDate]
                           ,[CrDate]
                           ,[Birthday]
                           ,[Status]
                           ,[RolId])
                     VALUES
                           (@Code
                           ,@Name
                           ,@Username
                           ,@IdentityNumber                           
                           ,@Email
                           ,@Password
                           ,@CreateBy
                           ,@ModDate                        
                           ,@CrDate 
                           ,@Birthday                    
                           ,@Status                  
                           ,@RolId)";
                SqlParameter[] parameters =[
                    new("@Code",user.Code),
                    new("@Name",user.Name),
                    new("@Username",user.Username),
                    new("@IdentityNumber",user.IdentityNumber),
                    new ("@Email",user.Email),
                    new("@Password",user.Password),
                    new("@CreateBy",user.CreateBy),
                    new("@ModDate",user.ModDate),
                    new("@CrDate",user.CrDate),
                    new("@Birthday",user.Birthday),
                    new("@Status",user.Status),
                    new("@RolId",user.RolId)


                    ];
                var result = await ExecuteQuery(sql, parameters);
                return result > 0;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }
        public async Task<User> GetUser(User user)
        {
            try
            {
                var sql = $"SELECT* FROM Users where Username=@Username and Password=@Password and Status=1";
                SqlParameter[] sqlParameters = [
                    new("@Username",user.Username),
                    new ("@Password",user.Password)
                    ]; 
                var result=await ExecuteQuerySelectSingle(sql, 
                    reader => { 
                        return new User {
                            Id = reader["Id"]!=DBNull.Value ? Convert.ToInt32(reader["Id"]) : 0,
                            Username = reader["Username"]!=DBNull.Value ? reader["Username"].ToString() : "",
                            Name = reader["Name"] != DBNull.Value ? reader["Name"].ToString() : "",
                            IdentityNumber = reader["IdentityNumber"]!=DBNull.Value ? reader["IdentityNumber"].ToString() : "",
                            Code = reader["Code"]!=DBNull.Value ? (Guid)reader["Code"]:  Guid.NewGuid(),
                            RolId = reader["RolId"]!=DBNull.Value ? Convert.ToInt32(reader["RolId"]) : 0,
                        }; 
                    }, 
                    sqlParameters
                    );
                return result;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
       
    }
}
