using System;

namespace MobileApps.Models
{
    public class Boleta
    {
        public string Id { get; set; }
        public string idBoleta { get; set; }
        public string coordx { get; set; }
        public string coordy { get; set; }
        public string tecnicoId { get; set; }
        public string descripcion { get; set; }
        public string foto1Url { get; set; }
        public string foto2Url { get; set; }
        public string firmaUrl { get; set; }
    }
}