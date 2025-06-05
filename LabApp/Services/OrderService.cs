using LabApp.Dtos;

namespace LabApp.Services
{
    public class OrderService: IOrderService
    {
        private readonly List<OrderDto> _orders = new();
        private int _idCounter = 1;

        public Task<List<OrderDto>> GetAllAsync()
        {
            return Task.FromResult(_orders);
        }

        public Task<OrderDto?> GetByIdAsync(int id)
        {
            var order = _orders.FirstOrDefault(o => o.Id == id);
            return Task.FromResult(order);
        }

        public Task<OrderDto> CreateAsync(CreateOrderDto dto)
        {
            var newOrder = new OrderDto
            {
                Id = _idCounter++,
                PatientId = dto.PatientId,
                OrderedAt = DateTime.UtcNow,
                Tests = dto.Tests
            };

            _orders.Add(newOrder);
            return Task.FromResult(newOrder);
        }

        public Task<bool> UpdateAsync(int id, UpdateOrderDto dto)
        {
            var order = _orders.FirstOrDefault(o => o.Id == id);
            if (order == null)
                return Task.FromResult(false);

            order.Tests = dto.Tests;
            return Task.FromResult(true);
        }

        public Task<bool> DeleteAsync(int id)
        {
            var order = _orders.FirstOrDefault(o => o.Id == id);
            if (order == null)
                return Task.FromResult(false);

            _orders.Remove(order);
            return Task.FromResult(true);
        }
    }
}
