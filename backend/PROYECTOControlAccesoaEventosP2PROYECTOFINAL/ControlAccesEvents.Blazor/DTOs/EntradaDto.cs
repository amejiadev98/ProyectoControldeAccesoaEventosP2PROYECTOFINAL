namespace ControlAccesEvents.Blazor.DTOs;

public class EntradaDto
{
    public int Id { get; set; }
    public string Codigo { get; set; } = string.Empty;
    public int EventoId { get; set; }
    public int InvitadoId { get; set; }
}
