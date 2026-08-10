using System;

namespace ControlAccesoEventos.Domain.Entities
{
    public class Evento
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public DateTime Fecha { get; set; }
        public string Lugar { get; set; } = string.Empty;

        public Evento()
        {
        }

        public Evento(string nombre, DateTime fecha, string lugar)
        {
            Nombre = nombre;
            Fecha = fecha;
            Lugar = lugar;
        }
    }
}
