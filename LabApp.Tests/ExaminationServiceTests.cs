using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LabApp.Models;
using LabApp.Data;
using LabApp.Services;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Xunit;
using LabApp.Dtos;
using FluentAssertions;

namespace LabApp.Tests
{
    public class ExaminationServiceTests
    {
        [Fact]
        public async Task CreateAsync_ShouldAddNewExamination()
        {
            
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using var context = new ApplicationDbContext(options);

            var device = new Device { Name = "Test Device", Model = "123", SerialNumber = "XYZ", IsOperational = true };
            var order = new Order { PatientId = 1, DiagnosticianId = 1 };

            context.Devices.Add(device);
            context.Orders.Add(order);
            await context.SaveChangesAsync();

            var service = new ExaminationService(context);

            var dto = new CreateExaminationDto
            {
                Name = "Blood Test",
                Description = "Check glucose",
                Unit = "mg/dL",
                LowerRange = "70",
                UpperRange = "110",
                Price = 99,
                OrderId = order.Id,
                DeviceId = device.Id
            };

            
            var result = await service.CreateAsync(dto);

           Assert.NotNull(result);
            Assert.Equal(dto.Name, result.Name);
            Assert.Equal(dto.Price, result.Price);
            Assert.Equal(dto.Unit, result.Unit);
         
          
        }
        [Fact]
        public async Task GetAllAsync_ShouldReturnAllExaminations()
        {

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
        .UseInMemoryDatabase(Guid.NewGuid().ToString())
        .Options;

            using var context = new ApplicationDbContext(options);

            var device = new Device { Name = "X-Ray", Model = "X123", SerialNumber = "SN001", IsOperational = true };
            var order = new Order { PatientId = 1, DiagnosticianId = 1 };

            context.Devices.Add(device);
            context.Orders.Add(order);
            await context.SaveChangesAsync();

            context.Examinations.Add(new Examination
            {
                Name = "MRI",
                Description = "Scan",
                Unit = "cm",
                LowerRange = "1",
                UpperRange = "5",
                Price = 500,
                DeviceId = device.Id,
                OrderId = order.Id
            });

            await context.SaveChangesAsync();

            var service = new ExaminationService(context);

          
            var result = await service.GetAllAsync();

            Assert.NotNull(result);
            Assert.Equal(1, result.Count);
      
        }
    }
}
