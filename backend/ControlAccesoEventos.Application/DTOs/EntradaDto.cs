namespace ControlAccesoEventos.Application.DTOs
{
    public class EntradaDto
    {
        public int Id { get; set; }
        public int EventoId { get; set; }
        public int InvitadoId { get; set; }
        public string CodigoQR { get; set; } = string.Empty;
        public bool Usada { get; set; }
    }
}
