using MantencionCamiones.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;

namespace MantencionCamiones.Pages
{
    public class EditarCamionModel : PageModel
    {
        private readonly TransporteContext _context;

        public EditarCamionModel(TransporteContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Camione Camion { get; set; } = null!;

        public IActionResult OnGet(int id)
        {
            Camion = _context.Camiones.FirstOrDefault(c => c.Id == id);

            if (Camion == null)
            {
                return NotFound();
            }

            return Page();
        }

        public IActionResult OnPost(int id)
        {
            var camionExistente = _context.Camiones.FirstOrDefault(c => c.Id == id);

            if (camionExistente == null)
            {
                return NotFound();
            }

            camionExistente.Estado = Camion.Estado;
            camionExistente.Kilometraje = Camion.Kilometraje;

            _context.SaveChanges();

            return RedirectToPage("ListadoCamiones");
        }
    }
}
