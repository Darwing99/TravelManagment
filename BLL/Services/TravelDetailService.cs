using DAL.DataAccess;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class TravelDetailService
    {
        private readonly TravelDetailDao travelDetailDao;
        public TravelDetailService(TravelDetailDao travelDetailDao)
        {
            this.travelDetailDao = travelDetailDao;
        }   
        public async Task<List<TravelDetail>> GetTravelDetailsAsync(int CarrierId,DateTime Init, DateTime end) 
            => await travelDetailDao.GetDetailTravelAsync(CarrierId, Init, end);
    }
}
