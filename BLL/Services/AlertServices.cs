using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazorToastify;
using CurrieTechnologies.Razor.SweetAlert2;


namespace BLL.Services
{
    public class AlertServices(IToastService toastService, SweetAlertService sweetAlert)

    {
        private readonly IToastService _toastService = toastService;
        private readonly SweetAlertService _sweetAlert = sweetAlert;

        public async Task ShowToast(string type, string message, string animation, int timer)
        {
            await _toastService.AddToastAsync(
                 message: message,
                 type: type,
                 animation: animation,
                 autoClose: timer
                 );
        }
        public async Task SweetAlertMessage(string title, string message, SweetAlertIcon icon, decimal timer)
        {
            await _sweetAlert.FireAsync(new SweetAlertOptions
            {
                Title = title,
                Text = message,
                Icon = icon,
                Timer = timer

            });
        }

    }
}
