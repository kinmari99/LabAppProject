using LabApp.Data;
using LabApp.Dtos;
using LabApp.Models;
using Microsoft.EntityFrameworkCore;

namespace LabApp.Services
{
    public class PatientService : IPatientService
    {
        private readonly ApplicationDbContext _context;

        public PatientService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<PatientDto>> GetAllAsync()
        {
            return await _context.Patients
                .Select(p => new PatientDto
                {
                    Id = p.Id,
                    PESEL = p.PESEL,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    Email = p.Email,
                    PhoneNumber = p.PhoneNumber
                })
                .ToListAsync();
        }

        public async Task<PatientDto?> GetByIdAsync(int id)
        {
            var p = await _context.Patients.FindAsync(id);
            if (p == null) return null;

            return new PatientDto
            {
                Id = p.Id,
                PESEL = p.PESEL,
                FirstName = p.FirstName,
                LastName = p.LastName,
                Email = p.Email,
                PhoneNumber = p.PhoneNumber
            };
        }

        public async Task<PatientDto> CreateAsync(CreatePatientDto dto)
        {
            var patient = new Patient
            {
                PESEL = dto.PESEL,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber
            };

            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();

            return new PatientDto
            {
                Id = patient.Id,
                PESEL = patient.PESEL,
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                Email = patient.Email,
                PhoneNumber = patient.PhoneNumber
            };
        }

        public async Task<bool> UpdateAsync(int id, UpdatePatientDto dto)
        {
            var p = await _context.Patients.FindAsync(id);
            if (p == null) return false;

            p.LastName = dto.LastName;
            p.PhoneNumber = dto.PhoneNumber;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var p = await _context.Patients.FindAsync(id);
            if (p == null) return false;

            _context.Patients.Remove(p);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
