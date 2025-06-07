using Grpc.Core;
using LabApp.Data;
using LabApp.gRPC;
using LabApp.Models;
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
        public override async Task<DevicesReply> GetAllDevices(EmptyRequest request, ServerCallContext context)
        {
            var devices = await _context.Devices.ToListAsync();

            var reply = new DevicesReply();
            reply.Devices.AddRange(devices.Select(d => new DeviceReply
            {
                Id = d.Id,
                Name = d.Name,
                Model = d.Model,
                SerialNumber = d.SerialNumber,
                IsOperational = d.IsOperational
            }));

            return reply;
        }
        public override async Task<DeviceReply> AddDevice(DeviceCreateRequest request, ServerCallContext context)
        {
            var device = new Device
            {
                Name = request.Name,
                Model = request.Model,
                SerialNumber = request.SerialNumber,
                IsOperational = request.IsOperational
            };

            _context.Devices.Add(device);
            await _context.SaveChangesAsync();

            return new DeviceReply
            {
                Id = device.Id,
                Name = device.Name,
                Model = device.Model,
                SerialNumber = device.SerialNumber,
                IsOperational = device.IsOperational
            };
        }
        public override async Task<DeviceReply> UpdateDevice(DeviceUpdateRequest request, ServerCallContext context)
        {
            var device = await _context.Devices.FirstOrDefaultAsync(d => d.Id == request.Id);

            if (device == null)
                throw new RpcException(new Status(StatusCode.NotFound, $"Device {request.Id} not found"));

            device.Name = request.Name;
            device.Model = request.Model;
            device.SerialNumber = request.SerialNumber;
            device.IsOperational = request.IsOperational;

            await _context.SaveChangesAsync();

            return new DeviceReply
            {
                Id = device.Id,
                Name = device.Name,
                Model = device.Model,
                SerialNumber = device.SerialNumber,
                IsOperational = device.IsOperational
            };
        }
        public override async Task<EmptyReply> DeleteDevice(DeviceRequest request, ServerCallContext context)
        {
            var device = await _context.Devices.FirstOrDefaultAsync(d => d.Id == request.Id);

            if (device == null)
                throw new RpcException(new Status(StatusCode.NotFound, $"Device {request.Id} not found"));

            _context.Devices.Remove(device);
            await _context.SaveChangesAsync();

            return new EmptyReply();
        }



    }
}
