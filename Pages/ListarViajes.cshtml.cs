using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using bodega.Models;
using MantencionCamiones.Models;

public class ListarViajesModel : PageModel
{
    private readonly TransporteContext _context;

    public ListarViajesModel(TransporteContext context)
    {
        _context = context;
    }

    public List<viaje> Viajes { get; set; }

    public async Task OnGetAsync()
    {
        Viajes = await _context.viaje
            .Include(v => v.camion)
            .ToListAsync();
    }
}
