using System;

namespace ControlAccesoEventos.Application.DTOs
{
    public class ValidacionDto
    {
        public int Id { get; set; }
        public int EntradaId { get; set; }
        public DateTime FechaValidacion { get; set; }
        public bool AccesoPermitido { get; set; }
    }
}
