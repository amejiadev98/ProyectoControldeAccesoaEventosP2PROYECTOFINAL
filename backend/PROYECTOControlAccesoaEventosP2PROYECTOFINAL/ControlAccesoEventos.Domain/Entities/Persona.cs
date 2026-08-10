using System;

namespace ControlAccesoEventos.Domain.Entities
{
    public abstract class Persona
    {
        public int Id { get; protected set; }
        public string Nombre { get; protected set; }
        public string Apellidos { get; protected set; }
        public string DocumentoIdentidad { get; protected set; }

        protected Persona() { }

        protected Persona(int id, string nombre, string apellidos, string documentoIdentidad)
        {
            Id = id;
            Nombre = nombre;
            Apellidos = apellidos;
            DocumentoIdentidad = documentoIdentidad;
        }

        public abstract string ObtenerIdentificador();
    }
}
