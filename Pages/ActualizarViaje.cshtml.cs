using bodega.Models;
using MantencionCamiones.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

public class ActualizarViajeModel : PageModel
{
    private readonly TransporteContext _context;

    public ActualizarViajeModel(TransporteContext context)
    {
        _context = context;
    }

    [BindProperty]
    public viaje viaje { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        viaje = await _context.viaje
            .Include(v => v.camion)
            .FirstOrDefaultAsync(v => v.id == id);

        if (viaje == null)
        {
            return NotFound();
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var viajeExistente = await _context.viaje
            .Include(v => v.camion)
            .FirstOrDefaultAsync(v => v.id == viaje.id);

        if (viajeExistente == null)
        {
            return NotFound();
        }

        viajeExistente.estado = viaje.estado;

        if (viaje.estado == "Terminado" && viajeExistente.camion != null)
        {
            viajeExistente.camion.Estado = "Disponible";
            _context.Update(viajeExistente.camion);
        }

        _context.Update(viajeExistente);
        await _context.SaveChangesAsync();

        return RedirectToPage("/ListarViajes");
    }
}
