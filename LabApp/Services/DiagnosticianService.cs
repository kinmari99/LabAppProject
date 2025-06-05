using LabApp.Dtos;

namespace LabApp.Services
{
    public class DiagnosticianService:IDiagnosticianService
    {
        private readonly List<DiagnosticianDto> _diagnosticians = new();
        private int _idCounter = 1;

        public Task<List<DiagnosticianDto>> GetAllAsync()
        {
            return Task.FromResult(_diagnosticians);
        }

        public Task<DiagnosticianDto?> GetByIdAsync(int id)
        {
            var diagnostician = _diagnosticians.FirstOrDefault(d => d.Id == id);
            return Task.FromResult(diagnostician);
        }

        public Task<DiagnosticianDto> CreateAsync(CreateDiagnosticianDto dto)
        {
            var diagnostician = new DiagnosticianDto
            {
                Id = _idCounter++,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                PWZDL=dto.PWZDL,
                Email = dto.Email,
                Specialization = dto.Specialization
            };
            _diagnosticians.Add(diagnostician);
            return Task.FromResult(diagnostician);
        }

        public Task<bool> UpdateAsync(int id, UpdateDiagnosticianDto dto)
        {
            var diagnostician = _diagnosticians.FirstOrDefault(d => d.Id == id);
            if (diagnostician == null)
                return Task.FromResult(false);

            diagnostician.LastName= dto.LastName;
            diagnostician.PWZDL= dto.PWZDL;
            diagnostician.Email = dto.Email;
            diagnostician.Specialization = dto.Specialization;
            return Task.FromResult(true);
        }

        public Task<bool> DeleteAsync(int id)
        {
            var diagnostician = _diagnosticians.FirstOrDefault(d => d.Id == id);
            if (diagnostician == null)
                return Task.FromResult(false);

            _diagnosticians.Remove(diagnostician);
            return Task.FromResult(true);
        }
    }
}
