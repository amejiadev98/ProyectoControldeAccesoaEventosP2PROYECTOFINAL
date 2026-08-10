using System;

namespace ControlAccesoEventos.Domain.Entities
{
    public class Validacion
    {
        public int Id { get; set; }
        public int EntradaId { get; set; }
        public DateTime FechaValidacion { get; set; }
        public bool AccesoPermitido { get; set; }

       
        public Entrada? Entrada { get; set; }

        public Validacion()
        {
            FechaValidacion = DateTime.UtcNow;
        }

        public Validacion(int entradaId, bool accesoPermitido)
        {
            EntradaId = entradaId;
            AccesoPermitido = accesoPermitido;
            FechaValidacion = DateTime.UtcNow;
        }
    }
}
