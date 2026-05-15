using OrderService.Application.Interfaces;
using OrderService.Domain;

namespace OrderService.Application.Services
{
    public class OrderService
    {
        private readonly IOrderRepository _repository;

        public OrderService(IOrderRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Order>> GetAllOrders()
        {
            return await _repository.GetAllAsync();
        }

        public async Task CreateOrder(Order order)
        {
            await _repository.AddAsync(order);
            await _repository.SaveAsync();
        }
    }
}
