using Microsoft.Azure.Mobile.Server;
using System.ComponentModel.DataAnnotations.Schema;

namespace MobileApps.MobileAppService.DataObjects
{
    [Table("boletas")]
    public class Boletas : EntityData
    {
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