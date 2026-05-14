using VerstaTestProject.Data.Models;

namespace VerstaTestProject.Interfaces
{
    /// <summary>
    /// Интерфейс репозитория для работы с заказами.
    /// </summary>
    public interface IOrderRepository
    {
        /// <summary>
        /// Получает все заказы из базы данных.
        /// </summary>
        /// <returns>Список всех заказов.</returns>
        Task<IEnumerable<Order>> GetAllAsync();

        /// <summary>
        /// Получает заказ по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор заказа.</param>
        /// <returns>Заказ, если найден, иначе null.</returns>
        Task<Order?> GetByIdAsync(int id);

        /// <summary>
        /// Создаёт новый заказ в базе данных.
        /// </summary>
        /// <param name="order">Объект заказа для создания.</param>
        /// <returns>Созданный заказ с заполненным Id.</returns>
        Task<Order> CreateAsync(Order order);

        /// <summary>
        /// Сохраняет изменения в базе данных.
        /// </summary>
        /// <returns>true, если изменения сохранены успешно.</returns>
        Task<bool> SaveChangesAsync();
    }
}
