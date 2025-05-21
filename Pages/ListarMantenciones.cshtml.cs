using MantencionCamiones.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace MantencionCamiones.Pages
{
    public class ListarMantencionesModel : PageModel
    {
        private readonly TransporteContext _context;

        public ListarMantencionesModel(TransporteContext context)
        {
            _context = context;
        }

        public List<Mantencione> Mantenciones { get; set; } = new();
        public Camione Camion { get; set; } = null!;

        public IActionResult OnGet(int camionId)
        {
            // Buscar el camión por ID
            Camion = _context.Camiones.FirstOrDefault(c => c.Id == camionId);
            if (Camion == null)
            {
                return NotFound();
            }

            // Cargar todas las mantenciones del camión
            Mantenciones = _context.Mantenciones
                .Where(m => m.CamionId == camionId)
                .OrderByDescending(m => m.Fecha)
                .ToList();

            return Page();
        }
    }
}
