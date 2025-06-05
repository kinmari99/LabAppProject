using LabApp.Dtos;

namespace LabApp.Services
{
    public class DeviceService:IDeviceService
    {
        private readonly List<DeviceDto> _devices = new();
        private int _idCounter = 1;

        public Task<List<DeviceDto>> GetAllAsync()
        {
            return Task.FromResult(_devices);
        }

        public Task<DeviceDto?> GetByIdAsync(int id)
        {
            var device = _devices.FirstOrDefault(d => d.Id == id);
            return Task.FromResult(device);
        }

        public Task<DeviceDto> CreateAsync(CreateDeviceDto dto)
        {
            var device = new DeviceDto
            {
                Id = _idCounter++,
                Name = dto.Name,
                Model = dto.Model,
                SerialNumber = dto.SerialNumber,
                IsOperational = true
            };
            _devices.Add(device);
            return Task.FromResult(device);
        }

        public Task<bool> UpdateAsync(int id, UpdateDeviceDto dto)
        {
            var device = _devices.FirstOrDefault(d => d.Id == id);
            if (device == null)
                return Task.FromResult(false);

            device.IsOperational = dto.IsOperational;
            return Task.FromResult(true);
        }

        public Task<bool> DeleteAsync(int id)
        {
            var device = _devices.FirstOrDefault(d => d.Id == id);
            if (device == null)
                return Task.FromResult(false);

            _devices.Remove(device);
            return Task.FromResult(true);
        }
    }
}
