using MantencionCamiones.Models;// o MantencionCamiones.Models, según como tengas tu proyecto
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;

namespace bodega.Pages
{
    public class ListadoCamionesModel : PageModel
    {
        public List<Camione> ListaCamiones { get; set; } = new List<Camione>();

        public void OnGet()
        {
            using var context = new TransporteContext();
            ListaCamiones = context.Camiones.OrderBy(x => x.Marca).ToList();
        }
    }
}
