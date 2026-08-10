using System;

namespace ControlAccesoEventos.Domain.Entities
{
    public class Entrada
    {
        public int Id { get; set; }
        public int EventoId { get; set; }
        public int InvitadoId { get; set; }
        public string CodigoQR { get; set; } = string.Empty;
        public bool Usada { get; set; }

        
        public Evento? Evento { get; set; }
        public Invitado? Invitado { get; set; }

        public Entrada()
        {
        }

        public Entrada(int eventoId, int invitadoId, string codigoQR)
        {
            EventoId = eventoId;
            InvitadoId = invitadoId;
            CodigoQR = codigoQR;
            Usada = false;
        }
    }
}
