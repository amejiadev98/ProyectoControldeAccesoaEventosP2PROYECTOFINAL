using ControlAccesoEventos.Application.DTOs;
using ControlAccesoEventos.Application.Interfaces;
using ControlAccesoEventos.Domain.Entities;
using ControlAccesoEventos.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlAccesoEventos.Application.Services
{
    public class EventoService : IEventoService
    {
        private readonly IRepository<Evento> _repo;

        public EventoService(IRepository<Evento> repo)
        {
            _repo = repo;
        }

        public async Task<EventoDto> CreateAsync(EventoDto dto)
        {
            var entity = new Evento(dto.Nombre, dto.Fecha, dto.Lugar);
            var created = await _repo.AddAsync(entity);
            dto.Id = created.Id;
            return dto;
        }

        public async Task DeleteAsync(int id)
        {
            await _repo.DeleteAsync(id);
        }

        public async Task<IEnumerable<EventoDto>> GetAllAsync()
        {
            var list = await _repo.GetAllAsync();
            return list.Select(e => new EventoDto { Id = e.Id, Nombre = e.Nombre, Fecha = e.Fecha, Lugar = e.Lugar });
        }

        public async Task<EventoDto?> GetByIdAsync(int id)
        {
            var e = await _repo.GetByIdAsync(id);
            if (e == null) return null;
            return new EventoDto { Id = e.Id, Nombre = e.Nombre, Fecha = e.Fecha, Lugar = e.Lugar };
        }

        public async Task UpdateAsync(int id, EventoDto dto)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null) return;
            entity.Nombre = dto.Nombre;
            entity.Fecha = dto.Fecha;
            entity.Lugar = dto.Lugar;
            await _repo.UpdateAsync(entity);
        }
    }
}
