using LabApp.Dtos;

namespace LabApp.Services
{
    public interface IDiagnosticianService
    {
        Task<List<DiagnosticianDto>> GetAllAsync();
        Task<DiagnosticianDto?> GetByIdAsync(int id);
        Task<DiagnosticianDto> CreateAsync(CreateDiagnosticianDto dto);
        Task<bool> UpdateAsync(int id, UpdateDiagnosticianDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
