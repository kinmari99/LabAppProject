using LabApp.Dtos;

namespace LabApp.Services
{
    public interface IResultService
    {
        Task<List<ResultDto>> GetAllAsync();
        Task<ResultDto?> GetByIdAsync(int id);
        Task<List<ResultDto>> GetByOrderIdAsync(int orderId);
        Task<ResultDto> CreateAsync(CreateResultDto dto);
        Task<bool> UpdateAsync(int id, UpdateResultDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
