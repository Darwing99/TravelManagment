using DAL.DataAccess;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class CarrierService(CarrierDao carrierDao)
    {
        private readonly CarrierDao _carrierDao = carrierDao;

        public async Task<List<Carrier>> GetCarriersAsync()=> await _carrierDao.GetCarriersAsync();
        public async Task<bool> SaveCarrierAsync(Carrier carrier) => await _carrierDao.SaveCarrierAsync(carrier);
    }
}
