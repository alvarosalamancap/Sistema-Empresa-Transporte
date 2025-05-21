using bodega.Models;
using MantencionCamiones.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class RegistrarViajeModel : PageModel
{
    private readonly TransporteContext _context;

    public RegistrarViajeModel(TransporteContext context)
    {
        _context = context;
    }

    [BindProperty]
    public viaje viaje { get; set; }

    public List<SelectListItem> CamionesDisponibles { get; set; } = new List<SelectListItem>();

    public async Task<IActionResult> OnGetAsync()
    {
        Console.WriteLine("GET: Cargando camiones disponibles");
        await CargarCamionesDisponiblesAsync();
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        Console.WriteLine("POST: Enviando formulario de registro de viaje");

        if (viaje == null)
        {
            Console.WriteLine("ERROR: El objeto viaje es null");
            ModelState.AddModelError(string.Empty, "Datos del viaje no válidos.");
            await CargarCamionesDisponiblesAsync();
            return Page();
        }

        Console.WriteLine($"Valores recibidos - camionId: {viaje.camion_id}, destino: {viaje.destino}, fecha: {viaje.fecha}");

        if (!ModelState.IsValid)
        {
            Console.WriteLine("ERROR: ModelState no es válido");
            await CargarCamionesDisponiblesAsync();
            return Page();
        }

        var camion = await _context.Camiones
            .FirstOrDefaultAsync(c => c.Id == viaje.camion_id && c.Estado == "Disponible");

        if (camion == null)
        {
            Console.WriteLine("ERROR: No se encontró un camión disponible con el ID indicado.");
            ModelState.AddModelError(string.Empty, "El camión no está disponible.");
            await CargarCamionesDisponiblesAsync();
            return Page();
        }

        viaje.estado = "Iniciado";

        Console.WriteLine("Agregando viaje a la base de datos...");
        _context.viaje.Add(viaje);

        camion.Estado = "EnViaje";
        Console.WriteLine($"Cambiando estado del camión {camion.Id} a 'EnViaje'");

        await _context.SaveChangesAsync();
        Console.WriteLine("Viaje registrado exitosamente.");

        return RedirectToPage("ListarViajes");
    }

    private async Task CargarCamionesDisponiblesAsync()
    {
        Console.WriteLine("Cargando lista de camiones disponibles...");
        CamionesDisponibles = await _context.Camiones
            .Where(c => c.Estado == "Disponible")
            .Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = $"{c.Marca} {c.Modelo}"
            })
            .ToListAsync();
        Console.WriteLine($"Camiones disponibles encontrados: {CamionesDisponibles.Count}");
    }
}
