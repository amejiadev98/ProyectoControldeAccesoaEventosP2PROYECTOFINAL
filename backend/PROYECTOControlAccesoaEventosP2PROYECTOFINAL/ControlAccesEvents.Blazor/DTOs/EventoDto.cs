namespace ControlAccesEvents.Blazor.DTOs;

public class EventoDto
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Fecha { get; set; } = string.Empty;
    public string Lugar { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
}
