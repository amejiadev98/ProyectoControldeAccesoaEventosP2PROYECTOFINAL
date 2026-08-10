using ControlAccesoEventos.Application.DTOs;
using ControlAccesoEventos.Application.Interfaces;
using ControlAccesoEventos.Domain.Entities;
using ControlAccesoEventos.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlAccesoEventos.Application.Services
{
    public class InvitadoService : IInvitadoService
    {
        private readonly IRepository<Invitado> _repo;

        public InvitadoService(IRepository<Invitado> repo)
        {
            _repo = repo;
        }

        public async Task<InvitadoDto> CreateAsync(InvitadoDto dto)
        {
            var entity = new Invitado(dto.Nombre, dto.Cedula, dto.Correo);
            var created = await _repo.AddAsync(entity);
            dto.Id = created.Id;
            return dto;
        }

        public async Task DeleteAsync(int id)
        {
            await _repo.DeleteAsync(id);
        }

        public async Task<IEnumerable<InvitadoDto>> GetAllAsync()
        {
            var list = await _repo.GetAllAsync();
            return list.Select(i => new InvitadoDto { Id = i.Id, Nombre = i.Nombre, Cedula = i.Cedula, Correo = i.Correo });
        }

        public async Task<InvitadoDto?> GetByIdAsync(int id)
        {
            var i = await _repo.GetByIdAsync(id);
            if (i == null) return null;
            return new InvitadoDto { Id = i.Id, Nombre = i.Nombre, Cedula = i.Cedula, Correo = i.Correo };
        }

        public async Task UpdateAsync(int id, InvitadoDto dto)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null) return;
            // Persona.Nombre is protected set, can't set directly; recreate or use reflection - we'll create a new instance to replace
            entity = new Invitado(id, dto.Nombre, dto.Cedula, dto.Correo);
            await _repo.UpdateAsync(entity);
        }
    }
}
