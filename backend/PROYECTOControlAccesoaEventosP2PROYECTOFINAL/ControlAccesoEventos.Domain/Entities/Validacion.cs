using System;

namespace ControlAccesoEventos.Domain.Entities
{
    public enum ResultadoValidacion
    {
        Aceptado,
        Rechazado
    }

    public class Validacion
    {
        public int Id { get; private set; }
        public int EntradaId { get; private set; }
        public DateTime FechaHora { get; private set; }
        public ResultadoValidacion Resultado { get; private set; }
        public string Observacion { get; private set; }
        public string Ubicacion { get; private set; }
        public Entrada Entrada { get; private set; }

        public Validacion() { }

        public Validacion(int id, int entradaId, ResultadoValidacion resultado, string ubicacion = null, string observacion = null)
        {
            Id = id;
            EntradaId = entradaId;
            FechaHora = DateTime.UtcNow;
            Resultado = resultado;
            Ubicacion = ubicacion;
            Observacion = observacion;
        }
    }
}
