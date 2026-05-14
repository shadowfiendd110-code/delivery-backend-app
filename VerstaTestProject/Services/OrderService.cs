using VerstaTestProject.Data.DTOs;
using VerstaTestProject.Data.Models;
using VerstaTestProject.Interfaces;

namespace VerstaTestProject.Services
{
    /// <summary>
    /// Сервис для работы с заказами.
    /// </summary>
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        /// <summary>
        /// Создаёт экземпляр сервиса.
        /// </summary>
        /// <param name="orderRepository">Репозиторий заказов.</param>
        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        /// <summary>
        /// Получает все заказы в виде DTO.
        /// </summary>
        /// <returns>Список DTO заказов.</returns>
        public async Task<IEnumerable<OrderResponseDto>> GetAllOrdersAsync()
        {
            var orders = await _orderRepository.GetAllAsync();
            return orders.Select(o => MapToResponseDto(o));
        }

        /// <summary>
        /// Получает заказ по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор заказа.</param>
        /// <returns>DTO заказа, если найден, иначе null.</returns>
        public async Task<OrderResponseDto?> GetOrderByIdAsync(int id)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            return order != null ? MapToResponseDto(order) : null;
        }

        /// <summary>
        /// Создаёт новый заказ.
        /// </summary>
        /// <param name="createOrderDto">DTO с данными для создания заказа.</param>
        /// <returns>DTO созданного заказа.</returns>
        /// <exception cref="ArgumentException">Выбрасывается, если дата забора в прошлом.</exception>
        public async Task<OrderResponseDto> CreateOrderAsync(CreateOrderDto createOrderDto)
        {
            if (!ValidatePickupDate(createOrderDto.PickupDate))
            {
                throw new ArgumentException("Дата забора не может быть в прошлом");
            }

            var order = new Order
            {
                SenderCity = createOrderDto.SenderCity,
                SenderAddress = createOrderDto.SenderAddress,
                RecipientCity = createOrderDto.RecipientCity,
                RecipientAddress = createOrderDto.RecipientAddress,
                Weight = createOrderDto.Weight,
                PickupDate = createOrderDto.PickupDate,
                OrderNumber = Helpers.OrderNumberGenerator.Generate(),
                CreatedAt = DateTime.UtcNow
            };

            var createdOrder = await _orderRepository.CreateAsync(order);
            return MapToResponseDto(createdOrder);
        }

        /// <summary>
        /// Проверяет, что дата забора не в прошлом.
        /// </summary>
        /// <param name="pickupDate">Дата забора.</param>
        /// <returns>true, если дата сегодня или позже.</returns>
        public bool ValidatePickupDate(DateTime pickupDate)
        {
            return pickupDate.Date >= DateTime.UtcNow.Date;
        }

        /// <summary>
        /// Преобразует модель Order в DTO OrderResponseDto.
        /// </summary>
        /// <param name="order">Модель заказа.</param>
        /// <returns>DTO заказа.</returns>
        private OrderResponseDto MapToResponseDto(Order order)
        {
            return new OrderResponseDto
            {
                Id = order.Id,
                OrderNumber = order.OrderNumber,
                SenderCity = order.SenderCity,
                SenderAddress = order.SenderAddress,
                RecipientCity = order.RecipientCity,
                RecipientAddress = order.RecipientAddress,
                Weight = order.Weight,
                PickupDate = order.PickupDate,
                CreatedAt = order.CreatedAt
            };
        }
    }
}
