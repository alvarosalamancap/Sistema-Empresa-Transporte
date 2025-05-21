using MantencionCamiones.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Linq;

namespace MantencionCamiones.Pages
{
    public class AgregarMantencionModel : PageModel
    {
        private readonly TransporteContext _context;

        public AgregarMantencionModel(TransporteContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Mantencione Mantencion { get; set; } = new Mantencione();

        public Camione Camion { get; set; } = null!;

        [TempData]
        public string? MensajeExito { get; set; }

        public IActionResult OnGet(int camionId)
        {
            Camion = _context.Camiones.FirstOrDefault(c => c.Id == camionId);
            if (Camion == null)
            {
                return NotFound();
            }

            Mantencion.CamionId = camionId;
            Mantencion.Fecha = DateTime.Today;

            return Page();
        }

        public IActionResult OnPost()
        {
            Console.WriteLine($"[DEBUG] Intentando guardar mantención con CamionId: {Mantencion.CamionId}");

            if (!ModelState.IsValid)
            {
                Console.WriteLine("[DEBUG] ModelState inválido.");
                foreach (var key in ModelState.Keys)
                {
                    var errors = ModelState[key].Errors;
                    foreach (var error in errors)
                    {
                        Console.WriteLine($"[ERROR] Campo: {key}, Error: {error.ErrorMessage}");
                    }
                }

                Camion = _context.Camiones.FirstOrDefault(c => c.Id == Mantencion.CamionId);
                return Page();
            }

            var camionExistente = _context.Camiones.FirstOrDefault(c => c.Id == Mantencion.CamionId);
            if (camionExistente == null)
            {
                Console.WriteLine("[DEBUG] No se encontró el camión.");
                return NotFound();
            }

            _context.Mantenciones.Add(Mantencion);
            _context.SaveChanges();

            MensajeExito = "¡Mantención guardada con éxito!";
            return RedirectToPage("ListarMantenciones", new { camionId = Mantencion.CamionId });
        }
    }
}

