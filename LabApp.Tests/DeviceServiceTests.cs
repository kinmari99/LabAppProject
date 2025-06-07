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
    public class DeviceServiceTests
    {
        private async Task<ApplicationDbContext> GetDbContextAsync()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: $"LabDb_{System.Guid.NewGuid()}")
                .Options;

            var context = new ApplicationDbContext(options);
            context.Database.EnsureCreated();

            return context;
        }

        [Fact]
        public async Task AddDevice_ShouldAddDeviceToDatabase()
        {
            
            var dbContext = await GetDbContextAsync();
            var service = new DeviceService(dbContext);
            var device = new Device
            {
                Name = "TestDevice",
                Model = "Model123",
                SerialNumber = "SN123",
                IsOperational = true
            };


            await service.CreateAsync(new CreateDeviceDto
            {
                Name = "TestDevice",
                Model = "Model123",
                SerialNumber = "SN123"
                
            });


            dbContext.Devices.Should().ContainSingle(d => d.Name == "TestDevice");
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateDeviceOperationalStatus()
        {
            
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "UpdateDeviceTest")
                .Options;

            using var context = new ApplicationDbContext(options);
            var service = new DeviceService(context);

            var device = new Device
            {
                Name = "TestDevice",
                Model = "ModelX",
                SerialNumber = "SN-001",
                IsOperational = false
            };
            context.Devices.Add(device);
            await context.SaveChangesAsync();

            
            var result = await service.UpdateAsync(device.Id, new UpdateDeviceDto
            {
                IsOperational = true
            });

            var updatedDevice = await context.Devices.FindAsync(device.Id);

     
            Assert.True(result); 
            Assert.NotNull(updatedDevice);
            Assert.True(updatedDevice.IsOperational); 
        }
    }
}
       
    
