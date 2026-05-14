using VerstaTestProject.Data.DTOs;

namespace VerstaTestProject.Interfaces
{
    /// <summary>
    /// Интерфейс сервиса для работы с заказами.
    /// </summary>
    public interface IOrderService
    {
        /// <summary>
        /// Получает все заказы в виде DTO.
        /// </summary>
        /// <returns>Список DTO заказов.</returns>
        Task<IEnumerable<OrderResponseDto>> GetAllOrdersAsync();

        /// <summary>
        /// Получает заказ по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор заказа.</param>
        /// <returns>DTO заказа, если найден, иначе null.</returns>
        Task<OrderResponseDto?> GetOrderByIdAsync(int id);

        /// <summary>
        /// Создаёт новый заказ.
        /// </summary>
        /// <param name="createOrderDto">DTO с данными для создания заказа.</param>
        /// <returns>DTO созданного заказа.</returns>
        /// <exception cref="ArgumentException">Выбрасывается, если дата забора в прошлом.</exception>
        Task<OrderResponseDto> CreateOrderAsync(CreateOrderDto createOrderDto);

        /// <summary>
        /// Проверяет, что дата забора не в прошлом.
        /// </summary>
        /// <param name="pickupDate">Дата забора.</param>
        /// <returns>true, если дата корректна.</returns>
        bool ValidatePickupDate(DateTime pickupDate);
    }
}
