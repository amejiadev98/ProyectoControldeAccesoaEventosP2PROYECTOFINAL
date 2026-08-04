using System;

namespace ControlAccesoEventos.Domain.Entities
{
    public class Invitado : Persona
    {
        public string Email { get; private set; }
        public string Telefono { get; private set; }

        public Invitado() { }

        public Invitado(int id, string nombre, string apellidos, string documentoIdentidad, string email, string telefono)
            : base(id, nombre, apellidos, documentoIdentidad)
        {
            Email = email;
            Telefono = telefono;
        }

        public override string ObtenerIdentificador()
        {
            return !string.IsNullOrWhiteSpace(DocumentoIdentidad) ? DocumentoIdentidad : Email;
        }
    }
}
