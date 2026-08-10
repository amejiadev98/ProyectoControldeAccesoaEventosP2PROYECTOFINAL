using System;

namespace ControlAccesoEventos.Domain.Entities
{
    public class Invitado : Persona
    {
        public string Cedula { get; private set; }
        public string Correo { get; private set; }

        public Invitado() : base()
        {
            Cedula = string.Empty;
            Correo = string.Empty;
        }

        public Invitado(int id, string nombre, string cedula, string correo) : base(id, nombre)
        {
            Cedula = cedula;
            Correo = correo;
        }

        public Invitado(string nombre, string cedula, string correo) : base(nombre)
        {
            Cedula = cedula;
            Correo = correo;
        }

       
        public void UpdateContact(string correo)
        {
            Correo = correo;
        }

        public void UpdateContact(string correo, string cedula)
        {
            Correo = correo;
            Cedula = cedula;
        }
    }
}
