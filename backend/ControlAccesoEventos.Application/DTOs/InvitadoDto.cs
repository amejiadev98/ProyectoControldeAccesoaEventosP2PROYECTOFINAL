namespace ControlAccesoEventos.Application.DTOs
{
    public class InvitadoDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Cedula { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
    }
}
