using bodega.Models;
using System;
using System.Collections.Generic;

namespace MantencionCamiones.Models
{
    public partial class Camione
    {
        public int Id { get; set; }
        public string Marca { get; set; } = null!;
        public string Modelo { get; set; } = null!;
        public int Anio { get; set; }
        public int Kilometraje { get; set; }
        public string Estado { get; set; } = null!;
        public virtual ICollection<Mantencione> Mantenciones { get; set; } = new List<Mantencione>();
        public virtual ICollection<viaje> Viajes { get; set; } = new List<viaje>();
    }
}
