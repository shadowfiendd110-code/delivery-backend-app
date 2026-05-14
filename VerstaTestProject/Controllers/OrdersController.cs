using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VerstaTestProject.Data.DTOs;
using VerstaTestProject.Interfaces;

namespace VerstaTestProject.Controllers
{
    /// <summary>
    /// Контроллер для управления заказами на доставку.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly ILogger<OrdersController> _logger;

        /// <summary>
        /// Создаёт экземпляр контроллера.
        /// </summary>
        /// <param name="orderService">Сервис для работы с заказами.</param>
        /// <param name="logger">Логгер контроллера.</param>
        public OrdersController(IOrderService orderService, ILogger<OrdersController> logger)
        {
            _orderService = orderService;
            _logger = logger;
        }

        /// <summary>
        /// Получает список всех заказов.
        /// </summary>
        /// <returns>Список DTO заказов.</returns>
        /// <response code="200">Успешное получение списка заказов.</response>
        /// <response code="500">Внутренняя ошибка сервера.</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderResponseDto>>> GetAllOrders()
        {
            try
            {
                _logger.LogInformation("Запрос на получение всех заказов");
                var orders = await _orderService.GetAllOrdersAsync();
                return Ok(orders);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при получении всех заказов");
                return StatusCode(500, "Внутренняя ошибка сервера");
            }
        }

        /// <summary>
        /// Получает заказ по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор заказа.</param>
        /// <returns>DTO заказа.</returns>
        /// <response code="200">Успешное получение заказа.</response>
        /// <response code="404">Заказ с указанным ID не найден.</response>
        /// <response code="500">Внутренняя ошибка сервера.</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderResponseDto>> GetOrderById(int id)
        {
            try
            {
                _logger.LogInformation("Запрос на получение заказа с ID {OrderId}", id);
                var order = await _orderService.GetOrderByIdAsync(id);

                if (order == null)
                {
                    _logger.LogWarning("Заказ с ID {OrderId} не найден", id);
                    return NotFound($"Заказ с ID {id} не найден");
                }

                return Ok(order);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при получении заказа с ID {OrderId}", id);
                return StatusCode(500, "Внутренняя ошибка сервера");
            }
        }

        /// <summary>
        /// Создаёт новый заказ на доставку.
        /// </summary>
        /// <param name="createOrderDto">DTO с данными для создания заказа.</param>
        /// <returns>DTO созданного заказа.</returns>
        /// <response code="201">Заказ успешно создан.</response>
        /// <response code="400">Некорректные данные запроса.</response>
        /// <response code="500">Внутренняя ошибка сервера.</response>
        [HttpPost]
        public async Task<ActionResult<OrderResponseDto>> CreateOrder([FromBody] CreateOrderDto createOrderDto)
        {
            try
            {
                _logger.LogInformation("Запрос на создание заказа: Отправитель {SenderCity}, Получатель {RecipientCity}",
                    createOrderDto.SenderCity, createOrderDto.RecipientCity);

                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("Невалидные данные при создании заказа");
                    return BadRequest(ModelState);
                }

                if (!_orderService.ValidatePickupDate(createOrderDto.PickupDate))
                {
                    _logger.LogWarning("Попытка создать заказ с датой забора в прошлом: {PickupDate}",
                        createOrderDto.PickupDate);
                    return BadRequest(new { error = "Дата забора не может быть в прошлом" });
                }

                var createdOrder = await _orderService.CreateOrderAsync(createOrderDto);

                _logger.LogInformation("Заказ успешно создан. Номер заказа: {OrderNumber}, ID: {OrderId}",
                    createdOrder.OrderNumber, createdOrder.Id);

                return CreatedAtAction(nameof(GetOrderById), new { id = createdOrder.Id }, createdOrder);
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "Ошибка валидации при создании заказа");
                return BadRequest(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при создании заказа");
                return StatusCode(500, "Внутренняя ошибка сервера");
            }
        }
    }
}
