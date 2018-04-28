using System;

using MobileApps.Models;

namespace MobileApps.ViewModels
{
    public class BoletaDetailViewModel : BaseViewModel
    {
        public Boleta Boleta { get; set; }
        public BoletaDetailViewModel(Boleta boleta = null)
        {
            Title = boleta?.descripcion;
            Boleta = boleta;
        }
    }
}
