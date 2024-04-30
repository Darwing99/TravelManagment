using Entities.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DataAccess
{
    public class TravelDetailDao:ConnectionToSql
    {
        public TravelDetailDao() { }
        public async Task<List<TravelDetail>> GetDetailTravelAsync(int CarrierId,DateTime Init,DateTime end )
        {
            try
            {
                string sql = @"select E.Name as NameEmployee, T.Date, Td.DistanceTraveled,U.Name as CreatedBy, 
                                 c.Name,c.RatePerKilometer,
                                (c.RatePerKilometer*Td.DistanceTraveled) as Total from Trip as T 
                                INNER JOIN TripDetail AS td ON t.Code=tD.TripCode 
                                inner join Carrier as C on C.CarrierId=T.CarrierID
                                INNER JOIN Employee as E on E.EmployeeID=Td.EmployeeID
                                INNER JOIN Users as U on U.Id = T.UserID where c.CarrierID = @CarrierId 
                                and c.Status=1 and Convert(date,T.Date)>=@Init and  Convert(date,T.Date)<=@End;";
                SqlParameter[] sqlParameters = [
                    new("@Init",Init.Date),
                    new("@End", end.Date),
                    new("@CarrierId",CarrierId)

                ];
                var result = await ExecuteQuerySelect(sql, reader => {
                    return new TravelDetail()
                    {
                        NameEmployee = reader["NameEmployee"]!=DBNull.Value? reader["NameEmployee"].ToString() :"",
                         CreatedBy= reader["CreatedBy"] != DBNull.Value ? reader["CreatedBy"].ToString() : "",
                          Name= reader["Name"] != DBNull.Value ? reader["Name"].ToString() : "",
                        Total = (decimal)reader["total"],
                        RatePerKilometer = (decimal)reader["RatePerKilometer"],
                        DistanceTraveled = (decimal)reader["DistanceTraveled"],
                        Date = (DateTime)reader["Date"],
                        

                    }; }, sqlParameters);
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        } 
    }
}
