using LabApp.Dtos;

namespace LabApp.Services
{
    public class ExaminationService:IExaminationService
    {
        private readonly List<ExaminationDto> _examinations = new();
        private int _idCounter = 1;

        public Task<List<ExaminationDto>> GetAllAsync()
        {
            return Task.FromResult(_examinations);
        }

        public Task<ExaminationDto?> GetByIdAsync(int id)
        {
            var exam = _examinations.FirstOrDefault(e => e.Id == id);
            return Task.FromResult(exam);
        }

        public Task<ExaminationDto> CreateAsync(CreateExaminationDto dto)
        {
            var exam = new ExaminationDto
            {
                Id = _idCounter++,
                Name = dto.Name,
                Description = dto.Description,
                Unit = dto.Unit,
                LowerRange = dto.LowerRange,
                UpperRange = dto.UpperRange,
                Price = dto.Price
            };
            _examinations.Add(exam);
            return Task.FromResult(exam);
        }

        public Task<bool> UpdateAsync(int id, UpdateExaminationDto dto)
        {
            var exam = _examinations.FirstOrDefault(e => e.Id == id);
            if (exam == null)
                return Task.FromResult(false);

            exam.Name = dto.Name;
            exam.Description = dto.Description;
            exam.Unit = dto.Unit;
            exam.LowerRange = dto.LowerRange;
            exam.UpperRange = dto.UpperRange;
            exam.Price = dto.Price;
            return Task.FromResult(true);
        }

        public Task<bool> DeleteAsync(int id)
        {
            var exam = _examinations.FirstOrDefault(e => e.Id == id);
            if (exam == null)
                return Task.FromResult(false);

            _examinations.Remove(exam);
            return Task.FromResult(true);
        }
    }
}
