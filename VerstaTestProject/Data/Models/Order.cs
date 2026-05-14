using System.ComponentModel.DataAnnotations;

namespace VerstaTestProject.Data.Models
{
    /// <summary>
    /// Модель заказа на доставку.
    /// </summary>
    public class Order
    {
        /// <summary>
        /// Уникальный идентификатор заказа.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Город отправителя.
        /// </summary>
        [Required(ErrorMessage = "Город отправителя обязателен.")]
        [MaxLength(100, ErrorMessage = "Город не может быть длиннее 100 символов.")]
        public string SenderCity { get; set; } = string.Empty;

        /// <summary>
        /// Адрес отправителя.
        /// </summary>
        [Required(ErrorMessage = "Адрес отправителя обязателен.")]
        [MaxLength(200, ErrorMessage = "Адрес не может быть длиннее 200 символов.")]
        public string SenderAddress { get; set; } = string.Empty;

        /// <summary>
        /// Город получателя.
        /// </summary>
        [Required(ErrorMessage = "Город получателя обязателен.")]
        [MaxLength(100, ErrorMessage = "Город не может быть длиннее 100 символов.")]
        public string RecipientCity { get; set; } = string.Empty;

        /// <summary>
        /// Адрес получателя.
        /// </summary>
        [Required(ErrorMessage = "Адрес получателя обязателен.")]
        [MaxLength(200, ErrorMessage = "Адрес не может быть длиннее 200 символов.")]
        public string RecipientAddress { get; set; } = string.Empty;

        /// <summary>
        /// Вес груза в килограммах.
        /// </summary>
        [Required(ErrorMessage = "Вес груза обязателен.")]
        [Range(0.01, 1000, ErrorMessage = "Вес должен быть от 0.01 до 1000 кг.")]
        public decimal Weight { get; set; }

        /// <summary>
        /// Дата забора груза.
        /// </summary>
        [Required(ErrorMessage = "Дата забора обязательна.")]
        public DateTime PickupDate { get; set; }

        /// <summary>
        /// Уникальный номер заказа (генерируется автоматически).
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string OrderNumber { get; set; } = string.Empty;

        /// <summary>
        /// Дата и время создания заказа в UTC.
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
