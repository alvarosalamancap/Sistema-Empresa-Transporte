using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MantencionCamiones.Models;

namespace bodega.Models
{
    public partial class viaje
    {
        public int id { get; set; }

        [Required]
        [Column("camion_id")]
        public int camion_id { get; set; }

        [Required]
        public DateTime fecha { get; set; }

        [Required]
        public string destino { get; set; }

        [Required]
        public string estado { get; set; }

        public virtual Camione camion { get; set; }
    }
}
