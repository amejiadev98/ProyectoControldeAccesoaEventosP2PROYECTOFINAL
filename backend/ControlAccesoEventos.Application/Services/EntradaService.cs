using ControlAccesoEventos.Application.DTOs;
using ControlAccesoEventos.Application.Interfaces;
using ControlAccesoEventos.Domain.Entities;
using ControlAccesoEventos.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlAccesoEventos.Application.Services
{
    public class EntradaService : IEntradaService
    {
        private readonly IRepository<Entrada> _repo;

        public EntradaService(IRepository<Entrada> repo)
        {
            _repo = repo;
        }

        public async Task<EntradaDto> CreateAsync(EntradaDto dto)
        {
            var entity = new Entrada(dto.EventoId, dto.InvitadoId, dto.CodigoQR) { Usada = dto.Usada };
            var created = await _repo.AddAsync(entity);
            dto.Id = created.Id;
            return dto;
        }

        public async Task DeleteAsync(int id)
        {
            await _repo.DeleteAsync(id);
        }

        public async Task<IEnumerable<EntradaDto>> GetAllAsync()
        {
            var list = await _repo.GetAllAsync();
            return list.Select(e => new EntradaDto { Id = e.Id, EventoId = e.EventoId, InvitadoId = e.InvitadoId, CodigoQR = e.CodigoQR, Usada = e.Usada });
        }

        public async Task<EntradaDto?> GetByIdAsync(int id)
        {
            var e = await _repo.GetByIdAsync(id);
            if (e == null) return null;
            return new EntradaDto { Id = e.Id, EventoId = e.EventoId, InvitadoId = e.InvitadoId, CodigoQR = e.CodigoQR, Usada = e.Usada };
        }

        public async Task UpdateAsync(int id, EntradaDto dto)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null) return;
            entity.EventoId = dto.EventoId;
            entity.InvitadoId = dto.InvitadoId;
            entity.CodigoQR = dto.CodigoQR;
            entity.Usada = dto.Usada;
            await _repo.UpdateAsync(entity);
        }
    }
}
