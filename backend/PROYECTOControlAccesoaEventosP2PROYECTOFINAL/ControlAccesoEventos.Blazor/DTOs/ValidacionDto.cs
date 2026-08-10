using System;

namespace ControlAccesoEventos.Blazor.DTOs
{
    public class ValidacionDto
    {
        public int Id { get; set; }
        public int EntradaId { get; set; }
        public DateTime Fecha { get; set; }
        public bool Valido { get; set; }
    }
}

