using ControlAccesoEventos.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControlAccesoEventos.Application.Interfaces
{
    public interface IInvitadoService
    {
        Task<IEnumerable<InvitadoDto>> GetAllAsync();
        Task<InvitadoDto?> GetByIdAsync(int id);
        Task<InvitadoDto> CreateAsync(InvitadoDto dto);
        Task UpdateAsync(int id, InvitadoDto dto);
        Task DeleteAsync(int id);
    }
}
