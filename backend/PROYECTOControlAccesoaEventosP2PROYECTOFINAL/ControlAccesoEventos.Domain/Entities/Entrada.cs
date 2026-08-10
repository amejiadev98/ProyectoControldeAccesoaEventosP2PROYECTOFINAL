using System;

namespace ControlAccesoEventos.Domain.Entities
{
    public enum EstadoEntrada
    {
        Emitida,
        Usada,
        Anulada
    }

    public class Entrada
    {
        public int Id { get; private set; }
        public string CodigoAcceso { get; private set; }
        public EstadoEntrada Estado { get; private set; }
        public int EventoId { get; private set; }
        public int InvitadoId { get; private set; }
        public DateTime FechaEmision { get; private set; }
        public Evento Evento { get; private set; }
        public Invitado Invitado { get; private set; }
        public ICollection<Validacion> Validaciones { get; private set; } = new List<Validacion>();

        public Entrada() { }

        public Entrada(int id, string codigoAcceso, int eventoId, int invitadoId)
        {
            Id = id;
            CodigoAcceso = codigoAcceso;
            Estado = EstadoEntrada.Emitida;
            EventoId = eventoId;
            InvitadoId = invitadoId;
            FechaEmision = DateTime.UtcNow;
        }

        public bool MarcarComoUsada()
        {
            if (Estado != EstadoEntrada.Emitida) return false;
            Estado = EstadoEntrada.Usada;
            return true;
        }

        public void Anular()
        {
            Estado = EstadoEntrada.Anulada;
        }
    }
}
