using System.Collections.Generic;
using System.Reflection.Emit;
using VerstaTestProject.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace VerstaTestProject.Data
{
    /// <summary>
    /// Контекст базы данных для работы с заказами.
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        /// <summary>
        /// Создаёт экземпляр контекста базы данных.
        /// </summary>
        /// <param name="options">Настройки контекста.</param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Набор заказов в базе данных.
        /// </summary>
        public DbSet<Order> Orders { get; set; }

        /// <summary>
        /// Настраивает модель базы данных.
        /// </summary>
        /// <param name="modelBuilder">Построитель модели.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.OrderNumber).IsRequired().HasMaxLength(50);
                entity.HasIndex(e => e.OrderNumber).IsUnique();
                entity.Property(e => e.Weight).HasPrecision(18, 2);
            });
        }
    }
}
