using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using LabApp.Models;
using LabApp.Data;
using LabApp.Services;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Xunit;
using LabApp.Dtos;

namespace LabApp.Tests
{
    public class DiagnosticianServiceTests
    {
        [Fact]
        public async Task AddAsync_ShouldAddDiagnosticianToDatabase()
        {
           
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "AddDiagnosticianTest")
                .Options;

            using var context = new ApplicationDbContext(options);
            var service = new DiagnosticianService(context);

            var dto = new CreateDiagnosticianDto
            {
                FirstName = "Anna",
                LastName = "Nowak",
                Email = "anna@lab.com",
                PWZDL = "123456",
                Specialization = "Immunologia"
            };

          
            var result = await service.CreateAsync(dto);
            var diagnosticianInDb = await context.Diagnosticians.FirstOrDefaultAsync(d => d.Email == dto.Email);

           
            Assert.NotNull(result);
            Assert.Equal(dto.FirstName, result.FirstName);
            Assert.Equal(dto.LastName, result.LastName);
            Assert.Equal(dto.Email, result.Email);
            Assert.NotNull(diagnosticianInDb);
        }
        [Fact]
        public async Task DeleteAsync_ShouldRemoveDiagnostician_WhenExists()
        {
           
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("DeleteDiagnosticianTest")
                .Options;

            using var context = new ApplicationDbContext(options);

            var diagnostician = new Diagnostician
            {
                FirstName = "Jan",
                LastName = "Kowalski",
                Email = "jan@lab.com",
                PWZDL = "999888",
                Specialization = "Patomorfologia"
            };

            context.Diagnosticians.Add(diagnostician);
            await context.SaveChangesAsync();

            var service = new DiagnosticianService(context);

          
            var result = await service.DeleteAsync(diagnostician.Id);
            var deleted = await context.Diagnosticians.FindAsync(diagnostician.Id);

           
            Assert.True(result);
            Assert.Null(deleted);
        }

        [Fact]
        public async Task DeleteAsync_ShouldReturnFalse_WhenDiagnosticianDoesNotExist()
        {
           
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("DeleteNonExistingDiagnosticianTest")
                .Options;

            using var context = new ApplicationDbContext(options);
            var service = new DiagnosticianService(context);

           
            var result = await service.DeleteAsync(999); 

          
            Assert.False(result);
        }
    }
}

