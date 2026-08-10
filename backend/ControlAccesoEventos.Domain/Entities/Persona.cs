using System;

namespace ControlAccesoEventos.Domain.Entities
{
    public abstract class Persona
    {
        public int Id { get; protected set; }
        public string Nombre { get; protected set; }

        protected Persona()
        {
            Nombre = string.Empty;
        }

        protected Persona(int id, string nombre)
        {
            Id = id;
            Nombre = nombre;
        }

        protected Persona(string nombre)
        {
            Nombre = nombre;
        }

        
        public virtual string GetDisplayName()
        {
            return Nombre;
        }

        public virtual string GetDisplayName(bool includeId)
        {
            return includeId ? $"{Id} - {Nombre}" : Nombre;
        }
    }
}
