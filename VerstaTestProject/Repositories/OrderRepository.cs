using Microsoft.EntityFrameworkCore;
using VerstaTestProject.Data;
using VerstaTestProject.Data.Models;
using VerstaTestProject.Interfaces;

namespace VerstaTestProject.Repositories
{
    /// <summary>
    /// Репозиторий для работы с заказами.
    /// </summary>
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Создаёт экземпляр репозитория.
        /// </summary>
        /// <param name="context">Контекст базы данных.</param>
        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Получает все заказы, отсортированные по дате создания (новые сверху).
        /// </summary>
        /// <returns>Список всех заказов.</returns>
        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            return await _context.Orders
                .OrderByDescending(o => o.CreatedAt)
                .ToListAsync();
        }

        /// <summary>
        /// Получает заказ по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор заказа.</param>
        /// <returns>Заказ, если найден, иначе null.</returns>
        public async Task<Order?> GetByIdAsync(int id)
        {
            return await _context.Orders.FindAsync(id);
        }

        /// <summary>
        /// Создаёт новый заказ в базе данных.
        /// </summary>
        /// <param name="order">Объект заказа для создания.</param>
        /// <returns>Созданный заказ с заполненным Id.</returns>
        public async Task<Order> CreateAsync(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return order;
        }

        /// <summary>
        /// Сохраняет изменения в базе данных.
        /// </summary>
        /// <returns>true, если изменения сохранены успешно, иначе false.</returns>
        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
