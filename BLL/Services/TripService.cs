using DAL.DataAccess;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class TripService
    {
        private readonly TripDao    _tripDao;
        public TripService(TripDao tripDao)
        {
            _tripDao = tripDao;
        }
        public int CountTravelByEmployee(int employeeId) => _tripDao.CountTravelByEmployee(employeeId);
        public async Task<int> SaveTripAsync(Trip trip)=> await _tripDao.SaveTripAsync(trip);
        public async Task<int> SaveTripDetailAsync(TripDetail tripDetail) => await _tripDao.SaveTripDetailAsync(tripDetail);
    }
}
