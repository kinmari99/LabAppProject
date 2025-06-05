using LabApp.Data;
using LabApp.Dtos;
using LabApp.Models;
using LabApp.Services;
using Microsoft.EntityFrameworkCore;

public class DeviceService : IDeviceService
{
    private readonly ApplicationDbContext _context;

    public DeviceService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<DeviceDto>> GetAllAsync()
    {
        return await _context.Devices
            .Select(d => new DeviceDto
            {
                Id = d.Id,
                Name = d.Name,
                Model = d.Model,
                SerialNumber = d.SerialNumber,
                IsOperational = d.IsOperational
            })
            .ToListAsync();
    }

    public async Task<DeviceDto?> GetByIdAsync(int id)
    {
        var device = await _context.Devices.FindAsync(id);
        if (device == null) return null;

        return new DeviceDto
        {
            Id = device.Id,
            Name = device.Name,
            Model = device.Model,
            SerialNumber = device.SerialNumber,
            IsOperational = device.IsOperational
        };
    }

    public async Task<DeviceDto> CreateAsync(CreateDeviceDto dto)
    {
        var device = new Device
        {
            Name = dto.Name,
            Model = dto.Model,
            SerialNumber = dto.SerialNumber,
            IsOperational = true
        };

        _context.Devices.Add(device);
        await _context.SaveChangesAsync();

        return new DeviceDto
        {
            Id = device.Id,
            Name = device.Name,
            Model = device.Model,
            SerialNumber = device.SerialNumber,
            IsOperational = device.IsOperational
        };
    }

    public async Task<bool> UpdateAsync(int id, UpdateDeviceDto dto)
    {
        var device = await _context.Devices.FindAsync(id);
        if (device == null) return false;

        device.IsOperational = dto.IsOperational;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var device = await _context.Devices.FindAsync(id);
        if (device == null) return false;

        _context.Devices.Remove(device);
        await _context.SaveChangesAsync();
        return true;
    }
}
