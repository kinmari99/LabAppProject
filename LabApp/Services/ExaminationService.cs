using LabApp.Data;
using LabApp.Dtos;
using LabApp.Models;
using Microsoft.EntityFrameworkCore;

namespace LabApp.Services
{
    public class ExaminationService : IExaminationService
    {
        private readonly ApplicationDbContext _context;

        public ExaminationService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<ExaminationDto>> GetAllAsync()
        {
            return await _context.Examinations
                .Select(e => new ExaminationDto
                {
                    Id = e.Id,
                    Name = e.Name,
                    Description = e.Description,
                    Unit = e.Unit,
                    LowerRange = e.LowerRange,
                    UpperRange = e.UpperRange,
                    Price = e.Price
                })
                .ToListAsync();
        }

        public async Task<ExaminationDto?> GetByIdAsync(int id)
        {
            var e = await _context.Examinations.FindAsync(id);
            if (e == null) return null;

            return new ExaminationDto
            {
                Id = e.Id,
                Name = e.Name,
                Description = e.Description,
                Unit = e.Unit,
                LowerRange = e.LowerRange,
                UpperRange = e.UpperRange,
                Price = e.Price
            };
        }

        public async Task<ExaminationDto> CreateAsync(CreateExaminationDto dto)
        {
            var exam = new Examination
            {
                Name = dto.Name,
                Description = dto.Description,
                Unit = dto.Unit,
                LowerRange = dto.LowerRange,
                UpperRange = dto.UpperRange,
                Price = dto.Price,
                OrderId = dto.OrderId,
                DeviceId = dto.DeviceId
            };

            _context.Examinations.Add(exam);
            await _context.SaveChangesAsync();

            return new ExaminationDto
            {
                Id = exam.Id,
                Name = exam.Name,
                Description = exam.Description,
                Unit = exam.Unit,
                LowerRange = exam.LowerRange,
                UpperRange = exam.UpperRange,
                Price = exam.Price
            };
        }

        public async Task<bool> UpdateAsync(int id, UpdateExaminationDto dto)
        {
            var e = await _context.Examinations.FindAsync(id);
            if (e == null) return false;

            e.Name = dto.Name;
            e.Description = dto.Description;
            e.Unit = dto.Unit;
            e.LowerRange = dto.LowerRange;
            e.UpperRange = dto.UpperRange;
            e.Price = dto.Price;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var e = await _context.Examinations.FindAsync(id);
            if (e == null) return false;

            _context.Examinations.Remove(e);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
