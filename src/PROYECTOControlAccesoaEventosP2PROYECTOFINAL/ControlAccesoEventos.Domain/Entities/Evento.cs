using System;

namespace ControlAccesoEventos.Domain.Entities
{
    public class Evento
    {
        public int Id { get; private set; }
        public string Nombre { get; private set; }
        public DateTime Fecha { get; private set; }
        public string Lugar { get; private set; }
        public int Capacidad { get; private set; }
        public string Descripcion { get; private set; }

        public Evento() { }

        public Evento(int id, string nombre, DateTime fecha, string lugar, int capacidad, string descripcion = null)
        {
            Id = id;
            Nombre = nombre;
            Fecha = fecha;
            Lugar = lugar;
            Capacidad = capacidad;
            Descripcion = descripcion;
        }

        public bool TieneCupo(int asistentesActuales)
        {
            return asistentesActuales < Capacidad;
        }
    }
}
