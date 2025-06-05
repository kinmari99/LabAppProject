using LabApp.Dtos;

namespace LabApp.Services
{
    public class ResultService:IResultService
    {
        private readonly List<ResultDto> _results = new();
        private int _idCounter = 1;

        public Task<List<ResultDto>> GetAllAsync()
        {
            return Task.FromResult(_results);
        }

        public Task<ResultDto?> GetByIdAsync(int id)
        {
            var result = _results.FirstOrDefault(r => r.Id == id);
            return Task.FromResult(result);
        }

        public Task<List<ResultDto>> GetByOrderIdAsync(int orderId)
        {
            var results = _results.Where(r => r.OrderId == orderId).ToList();
            return Task.FromResult(results);
        }

        public Task<ResultDto> CreateAsync(CreateResultDto dto)
        {
            var result = new ResultDto
            {
                Id = _idCounter++,
                OrderId = dto.OrderId,
                ExaminationName = dto.ExaminationName,
                Value = dto.Value,
                Unit = dto.Unit,
                LowerRange = dto.LowerRange,
                UpperRange = dto.UpperRange,
                PerformedAt = DateTime.UtcNow
            };

            _results.Add(result);
            return Task.FromResult(result);
        }

        public Task<bool> UpdateAsync(int id, UpdateResultDto dto)
        {
            var result = _results.FirstOrDefault(r => r.Id == id);
            if (result == null)
                return Task.FromResult(false);

            result.Value = dto.Value;
            result.Unit = dto.Unit;
            return Task.FromResult(true);
        }

        public Task<bool> DeleteAsync(int id)
        {
            var result = _results.FirstOrDefault(r => r.Id == id);
            if (result == null)
                return Task.FromResult(false);

            _results.Remove(result);
            return Task.FromResult(true);
        }
    }
}
