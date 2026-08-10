using System;
using System.ComponentModel.DataAnnotations;

namespace ControlAccesoEventos.Blazor.DTOs {
    public class EventoDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        public DateTime Fecha { get; set; }

        [Required]
        [StringLength(200)]
        public string Lugar { get; set; } = string.Empty;

        [Range(1, int.MaxValue)]
        public int Capacidad { get; set; }

        [StringLength(1000)]
        public string Descripcion { get; set; } = string.Empty;
    }
}
