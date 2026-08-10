using ControlAccesoEventos.Application.DTOs;
using ControlAccesoEventos.Application.Interfaces;
using ControlAccesoEventos.Domain.Entities;
using ControlAccesoEventos.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlAccesoEventos.Application.Services
{
    public class ValidacionService : IValidacionService
    {
        private readonly IRepository<Validacion> _repo;

        public ValidacionService(IRepository<Validacion> repo)
        {
            _repo = repo;
        }

        public async Task<ValidacionDto> CreateAsync(ValidacionDto dto)
        {
            var entity = new Validacion(dto.EntradaId, dto.AccesoPermitido) { FechaValidacion = dto.FechaValidacion };
            var created = await _repo.AddAsync(entity);
            dto.Id = created.Id;
            return dto;
        }

        public async Task DeleteAsync(int id)
        {
            await _repo.DeleteAsync(id);
        }

        public async Task<IEnumerable<ValidacionDto>> GetAllAsync()
        {
            var list = await _repo.GetAllAsync();
            return list.Select(v => new ValidacionDto { Id = v.Id, EntradaId = v.EntradaId, FechaValidacion = v.FechaValidacion, AccesoPermitido = v.AccesoPermitido });
        }

        public async Task<ValidacionDto?> GetByIdAsync(int id)
        {
            var v = await _repo.GetByIdAsync(id);
            if (v == null) return null;
            return new ValidacionDto { Id = v.Id, EntradaId = v.EntradaId, FechaValidacion = v.FechaValidacion, AccesoPermitido = v.AccesoPermitido };
        }

        public async Task UpdateAsync(int id, ValidacionDto dto)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null) return;
            entity.EntradaId = dto.EntradaId;
            entity.FechaValidacion = dto.FechaValidacion;
            entity.AccesoPermitido = dto.AccesoPermitido;
            await _repo.UpdateAsync(entity);
        }
    }
}
