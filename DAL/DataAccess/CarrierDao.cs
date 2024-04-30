using Entities.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DAL.DataAccess
{
    public class CarrierDao:ConnectionToSql
    {
        public CarrierDao() { }
        public async Task<List<Carrier>> GetCarriersAsync()
        {
            try
            {
                string sql = "SELECT* FROM Carrier where Status=1";
                var result = await ExecuteQuerySelect(sql, reader => { return new Carrier() {
                    CarrierId= reader.GetInt32(0),
                    Name = reader["Name"] != DBNull.Value ? reader["Name"].ToString() : "",
                    RatePerKilometer = reader["RatePerKilometer"] != DBNull.Value ? (decimal)reader["RatePerKilometer"] : 0,
                 
                }; 
                });
                return result;  
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> SaveCarrierAsync(Carrier carrier)
        {
            try
            {
                try
                {
                    string sql = @"INSERT INTO [dbo].[Carrier]
							   ([Name]
                               ,[RatePerKilometer]
                               ,[Status])
						 VALUES
							   (@Name,@RatePerKilometer,@Status)";
                    SqlParameter[] sqlParameters = [
                        new("@Name", carrier.Name),
                        new("@RatePerKilometer", carrier.RatePerKilometer),
                        new("@Status", carrier.Status)
                        ];
                    var result = await ExecuteQuery(sql, sqlParameters);
                    return result > 0;
                }
                catch (Exception)
                {

                    throw;
                }
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }
    }
}
