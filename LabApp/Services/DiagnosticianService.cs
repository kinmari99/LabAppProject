using LabApp.Data;
using LabApp.Dtos;
using LabApp.Models;
using Microsoft.EntityFrameworkCore;

namespace LabApp.Services
{
    public class DiagnosticianService : IDiagnosticianService
    {
        private readonly ApplicationDbContext _context;

        public DiagnosticianService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<DiagnosticianDto>> GetAllAsync()
        {
            return await _context.Diagnosticians
                .Select(d => new DiagnosticianDto
                {
                    Id = d.Id,
                    FirstName = d.FirstName,
                    LastName = d.LastName,
                    PWZDL = d.PWZDL,
                    Email = d.Email,
                    Specialization = d.Specialization
                })
                .ToListAsync();
        }

        public async Task<DiagnosticianDto?> GetByIdAsync(int id)
        {
            var d = await _context.Diagnosticians.FindAsync(id);
            if (d == null) return null;

            return new DiagnosticianDto
            {
                Id = d.Id,
                FirstName = d.FirstName,
                LastName = d.LastName,
                PWZDL = d.PWZDL,
                Email = d.Email,
                Specialization = d.Specialization
            };
        }

        public async Task<DiagnosticianDto> CreateAsync(CreateDiagnosticianDto dto)
        {
            var diagnostician = new Diagnostician
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                PWZDL = dto.PWZDL,
                Email = dto.Email,
                Specialization = dto.Specialization
            };

            _context.Diagnosticians.Add(diagnostician);
            await _context.SaveChangesAsync();

            return new DiagnosticianDto
            {
                Id = diagnostician.Id,
                FirstName = diagnostician.FirstName,
                LastName = diagnostician.LastName,
                PWZDL = diagnostician.PWZDL,
                Email = diagnostician.Email,
                Specialization = diagnostician.Specialization
            };
        }

        public async Task<bool> UpdateAsync(int id, UpdateDiagnosticianDto dto)
        {
            var d = await _context.Diagnosticians.FindAsync(id);
            if (d == null) return false;

            d.LastName = dto.LastName;
            d.PWZDL = dto.PWZDL;
            d.Email = dto.Email;
            d.Specialization = dto.Specialization;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var d = await _context.Diagnosticians.FindAsync(id);
            if (d == null) return false;

            _context.Diagnosticians.Remove(d);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
