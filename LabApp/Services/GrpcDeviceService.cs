using Grpc.Core;
using LabApp.Data;
using LabApp.gRPC;
using Microsoft.EntityFrameworkCore;

namespace LabApp.Services
{
    public class GrpcDeviceService:DeviceServiceGrpc.DeviceServiceGrpcBase
    {
        private readonly ApplicationDbContext _context;

        public GrpcDeviceService(ApplicationDbContext context)
        {
            _context = context;
        }

        public override async Task<DeviceReply> GetDeviceById(DeviceRequest request, ServerCallContext context)
        {
            var device = await _context.Devices.FirstOrDefaultAsync(d => d.Id == request.Id);

            if (device == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"Device ID {request.Id} not found"));
            }

            return new DeviceReply
            {
                Id = device.Id,
                Name = device.Name,
                Model = device.Model,
                SerialNumber = device.SerialNumber,
                IsOperational = device.IsOperational
            };
        }
    }
}
