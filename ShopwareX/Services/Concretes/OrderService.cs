using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShopwareX.Dtos.Order;
using ShopwareX.Entities;
using ShopwareX.Repositories.Abstracts;
using ShopwareX.Services.Abstracts;

namespace ShopwareX.Services.Concretes
{
    public class OrderService(IMapper mapper, IOrderRepository orderRepository) : IOrderService
    {
        private readonly IMapper _mapper = mapper;
        private readonly IOrderRepository _orderRepository = orderRepository;

        public async Task<OrderResponseDto> AddOrderAsync(OrderCreateDto dto)
        {
            var order = _mapper.Map<Order>(dto);
            await _orderRepository.AddAsync(order);
            await _orderRepository.SaveAsync();
            return _mapper.Map<OrderResponseDto>(order);
        }

        public async Task<OrderResponseDto> DeleteOrderByIdAsync(long id)
        {
            var existOrder = await _orderRepository.GetOrderByIdAsync(id);

            if (existOrder is not null)
            {
                existOrder.IsDeleted = true;
                existOrder.UpdatedAt = DateTime.Now;

                existOrder.Items
                    .ToList()
                    .ForEach(i =>
                    {
                        i.IsDeleted = true;
                        i.UpdatedAt = DateTime.Now;
                    });

                await _orderRepository.SaveAsync();
            }

            return _mapper.Map<OrderResponseDto>(existOrder);
        }

        public async Task<IEnumerable<OrderResponseDto>> GetAllOrdersAsync()
        {
            var orders = await _orderRepository.GetAllOrders().ToListAsync();
            return _mapper.Map<IEnumerable<OrderResponseDto>>(orders);
        }

        public async Task<OrderResponseDto> GetOrderByIdAsync(long id)
        {
            var existOrder = await _orderRepository.GetOrderByIdAsync(id);
            return _mapper.Map<OrderResponseDto>(existOrder);
        }
    }
}
