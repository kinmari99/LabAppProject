using LabApp.Data;
using LabApp.Dtos;
using LabApp.Models;
using Microsoft.EntityFrameworkCore;

namespace LabApp.Services
{
    public class ResultService : IResultService
    {
        private readonly ApplicationDbContext _context;

        public ResultService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<ResultDto>> GetAllAsync()
        {
            return await _context.Results
                .Include(r => r.Examination)
                .Select(r => new ResultDto
                {
                    Id = r.Id,
                    ExaminationId = r.ExaminationId,
                    ExaminationName = r.Examination.Name,
                    Value = r.Value,
                    Unit = r.Examination.Unit,
                    LowerRange = r.Examination.LowerRange,
                    UpperRange = r.Examination.UpperRange,
                    PerformedAt = r.PerformedAt
                })
                .ToListAsync();
        }

        public async Task<ResultDto?> GetByIdAsync(int id)
        {
            var result = await _context.Results
                .Include(r => r.Examination)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (result == null) return null;

            return new ResultDto
            {
                Id = result.Id,
                ExaminationId = result.ExaminationId,
                ExaminationName = result.Examination.Name,
                Value = result.Value,
                Unit = result.Examination.Unit,
                LowerRange = result.Examination.LowerRange,
                UpperRange = result.Examination.UpperRange,
                PerformedAt = result.PerformedAt
            };
        }

        public async Task<List<ResultDto>> GetByOrderIdAsync(int orderId)
        {
            return await _context.Results
                .Include(r => r.Examination)
                .Where(r => r.Examination.OrderId == orderId)
                .Select(r => new ResultDto
                {
                    Id = r.Id,
                    ExaminationId = r.ExaminationId,
                    ExaminationName = r.Examination.Name,
                    Value = r.Value,
                    Unit = r.Examination.Unit,
                    LowerRange = r.Examination.LowerRange,
                    UpperRange = r.Examination.UpperRange,
                    PerformedAt = r.PerformedAt
                })
                .ToListAsync();
        }

        public async Task<ResultDto> CreateAsync(CreateResultDto dto)
        {
            var result = new Result
            {
                ExaminationId = dto.ExaminationId,
                Value = dto.Value,
                PerformedAt = DateTime.UtcNow
            };

            _context.Results.Add(result);
            await _context.SaveChangesAsync();

            var exam = await _context.Examinations.FindAsync(dto.ExaminationId);

            return new ResultDto
            {
                Id = result.Id,
                ExaminationId = result.ExaminationId,
                ExaminationName = exam?.Name,
                Value = result.Value,
                Unit = exam?.Unit,
                LowerRange = exam?.LowerRange,
                UpperRange = exam?.UpperRange,
                PerformedAt = result.PerformedAt
            };
        }

        public async Task<bool> UpdateAsync(int id, UpdateResultDto dto)
        {
            var result = await _context.Results.FindAsync(id);
            if (result == null) return false;

            result.Value = dto.Value;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var result = await _context.Results.FindAsync(id);
            if (result == null) return false;

            _context.Results.Remove(result);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
