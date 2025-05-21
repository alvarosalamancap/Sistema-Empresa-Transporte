using MantencionCamiones.Models;

public partial class Mantencione
{
    public int Id { get; set; }
    public int CamionId { get; set; }
    public DateTime Fecha { get; set; }
    public string? Observacion { get; set; }
    public virtual Camione Camion { get; set; } = null!;
}
