namespace ControlAccesEvents.Blazor.DTOs;

public class ValidacionDto
{
    public int Id { get; set; }
    public string EntradaCodigo { get; set; } = string.Empty;
    public string FechaHora { get; set; } = string.Empty;
    public string Estado { get; set; } = string.Empty;
}
