namespace VerstaTestProject.Helpers
{
    /// <summary>
    /// Вспомогательный класс для генерации уникальных номеров заказов.
    /// </summary>
    public static class OrderNumberGenerator
    {
        /// <summary>
        /// Генерирует уникальный номер заказа.
        /// </summary>
        /// <returns>Уникальный номер заказа в формате ORD-{ticks}-{random}.</returns>
        public static string Generate()
        {
            var ticks = DateTime.UtcNow.Ticks.ToString();
            var random = new Random().Next(1000, 9999);
            return $"ORD-{ticks}-{random}";
        }
    }
}
