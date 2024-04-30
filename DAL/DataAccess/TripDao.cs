using Entities.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DataAccess
{
    public class TripDao:ConnectionToSql
    {
        public TripDao() { }
        public int CountTravelByEmployee(int employeeId)
        {
            try
            {
                string sql = @"SELECT count (*) as Count FROM Trip AS T 
                                INNER JOIN TripDetail as TD on T.Code=TD.TripCode 
                                where TD.EmployeeID=@EmployeeId and Convert(date,T.Date) = @Date ";

                DateTime dateTime = DateTime.Now.Date;

                SqlParameter[] sp = [
                      new SqlParameter("@Date", SqlDbType.Date) { Value = dateTime },
                    new SqlParameter("@EmployeeId", SqlDbType.Int) { Value = employeeId }
                ];
                var result=  ExecuteQuerySelectSingle(sql, reader => reader["Count"],sp);
                return Convert.ToInt32(result);
            }
            catch (Exception)
            {
                return 0;
                
            }
        }
        public async Task<int> SaveTripAsync(Trip trip)
        {
            try
            {
                string sql = @"INSERT INTO [dbo].[Trip]
                               ([Code]
                               ,[Date]
                               ,[BranchID]
                               ,[CarrierID]
                               ,[UserID]
                               ,[Status])
                         VALUES
                               (@Code
                                ,@Date
                               ,@BranchID
                               ,@CarrierID
                               ,@UserID
                               ,@Status)";
                SqlParameter[] sqlParameters = [
                    new("@Code", trip.Code),
                    new("@Date",trip.Date),
                    new("@BranchID",trip.BranchId),
                    new("@CarrierID",trip.CarrierId),
                    new("@UserID",trip.UserId),
                    new("@Status",trip.Status)
                    ]; 
                var result= await ExecuteQuery(sql, sqlParameters);
                return result;
            }
            catch (Exception)
            {
                return 0;
                throw;
            }
        }

        public async Task<int> SaveTripDetailAsync(TripDetail tripDetail)
        {
            try
            {
                string sql = @"INSERT INTO [dbo].[TripDetail]
                               ([TripCode]
                               ,[EmployeeID]
                               ,[DistanceTraveled]
                             
                               ,[Status])
                         VALUES
                               (@TripCode
                               ,@EmployeeID
                               ,@DistanceTraveled
                              
                               ,@Status)";
                SqlParameter[] sqlParameters = [
                    new("@TripCode",tripDetail.TripCode),
                    new("@EmployeeID",tripDetail.EmployeeId),
                    new("@DistanceTraveled",tripDetail.DistanceTraveled),                  
                    new("@Status", tripDetail.CountEmployees),
                ];
                var result= await ExecuteQuery(sql, sqlParameters);
                return result;
            }
            catch (Exception)
            {
                return 0;
                throw;
            }
        }
    }
}
