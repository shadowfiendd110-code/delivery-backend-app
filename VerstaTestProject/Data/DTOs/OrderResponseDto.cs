namespace VerstaTestProject.Data.DTOs
{
    /// <summary>
    /// DTO для ответа с данными заказа.
    /// </summary>
    public class OrderResponseDto
    {
        /// <summary>
        /// Уникальный идентификатор заказа.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Уникальный номер заказа.
        /// </summary>
        public string OrderNumber { get; set; } = string.Empty;

        /// <summary>
        /// Город отправителя.
        /// </summary>
        public string SenderCity { get; set; } = string.Empty;

        /// <summary>
        /// Адрес отправителя.
        /// </summary>
        public string SenderAddress { get; set; } = string.Empty;

        /// <summary>
        /// Город получателя.
        /// </summary>
        public string RecipientCity { get; set; } = string.Empty;

        /// <summary>
        /// Адрес получателя.
        /// </summary>
        public string RecipientAddress { get; set; } = string.Empty;

        /// <summary>
        /// Вес груза в килограммах.
        /// </summary>
        public decimal Weight { get; set; }

        /// <summary>
        /// Дата забора груза.
        /// </summary>
        public DateTime PickupDate { get; set; }

        /// <summary>
        /// Дата и время создания заказа.
        /// </summary>
        public DateTime CreatedAt { get; set; }
    }
}
