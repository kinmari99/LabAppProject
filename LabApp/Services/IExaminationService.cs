using LabApp.Dtos;

namespace LabApp.Services
{
    public interface IExaminationService
    {
        Task<List<ExaminationDto>> GetAllAsync();
        Task<ExaminationDto?> GetByIdAsync(int id);
        Task<ExaminationDto> CreateAsync(CreateExaminationDto dto);
        Task<bool> UpdateAsync(int id, UpdateExaminationDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
