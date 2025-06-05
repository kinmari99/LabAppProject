using LabApp.Dtos;

namespace LabApp.Services
{
    public class PatientService:IPatientService
    {
        private readonly List<PatientDto> _patients = new();
        private int _idCounter = 1;

        public Task<List<PatientDto>> GetAllAsync()
        {
            return Task.FromResult(_patients);
        }

        public Task<PatientDto?> GetByIdAsync(int id)
        {
            var patient = _patients.FirstOrDefault(p => p.Id == id);
            return Task.FromResult(patient);
        }

        public Task<PatientDto> CreateAsync(CreatePatientDto dto)
        {
            var patient = new PatientDto
            {
                Id = _idCounter++,
                PESEL = dto.PESEL,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber
            };
            _patients.Add(patient);
            return Task.FromResult(patient);
        }

        public Task<bool> UpdateAsync(int id, UpdatePatientDto dto)
        {
            var patient = _patients.FirstOrDefault(p => p.Id == id);
            if (patient == null)
                return Task.FromResult(false);

            patient.LastName = dto.LastName;
            patient.PhoneNumber = dto.PhoneNumber;
            return Task.FromResult(true);
        }

        public Task<bool> DeleteAsync(int id)
        {
            var patient = _patients.FirstOrDefault(p => p.Id == id);
            if (patient == null)
                return Task.FromResult(false);

            _patients.Remove(patient);
            return Task.FromResult(true);
        }
    }
}
