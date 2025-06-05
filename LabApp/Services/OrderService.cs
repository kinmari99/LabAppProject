using LabApp.Data;
using LabApp.Dtos;
using LabApp.Models;
using Microsoft.EntityFrameworkCore;

namespace LabApp.Services
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _context;

        public OrderService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<OrderDto>> GetAllAsync()
        {
            return await _context.Orders
                .Select(o => new OrderDto
                {
                    Id = o.Id,
                    PatientId = o.PatientId,
                    DiagnosticianId = o.DiagnosticianId,
                    DeviceId = o.DeviceId,
                    OrderedAt = o.OrderedAt
                })
                .ToListAsync();
        }

        public async Task<OrderDto?> GetByIdAsync(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null) return null;

            return new OrderDto
            {
                Id = order.Id,
                PatientId = order.PatientId,
                DiagnosticianId = order.DiagnosticianId,
                DeviceId = order.DeviceId,
                OrderedAt = order.OrderedAt
            };
        }

        public async Task<OrderDto> CreateAsync(CreateOrderDto dto)
        {
            var order = new Order
            {
                PatientId = dto.PatientId,
                DiagnosticianId = dto.DiagnosticianId,
                DeviceId = dto.DeviceId,
                OrderedAt = DateTime.UtcNow
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return new OrderDto
            {
                Id = order.Id,
                PatientId = order.PatientId,
                DiagnosticianId = order.DiagnosticianId,
                DeviceId = order.DeviceId,
                OrderedAt = order.OrderedAt
            };
        }

        public async Task<bool> UpdateAsync(int id, UpdateOrderDto dto)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null) return false;

            
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null) return false;

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
