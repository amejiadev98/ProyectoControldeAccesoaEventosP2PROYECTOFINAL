using System;
using System.ComponentModel.DataAnnotations;

namespace ControlAccesoEventos.Blazor.DTOs
{
    public class EntradaDto
    // DTO for Entrada
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string CodigoAcceso { get; set; }

        public string Estado { get; set; }

        [Required]
        public int EventoId { get; set; }

        [Required]
        public int InvitadoId { get; set; }

        public DateTime FechaEmision { get; set; }
    }
}
