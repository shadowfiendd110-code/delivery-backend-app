/**
 * Компонент для отображения деталей заказа в модальном окне.
 */

/// <summary>
/// Компонент деталей заказа.
/// </summary>
/// <param name="order">Объект заказа для отображения.</param>
/// <param name="onClose">Колбэк, вызываемый при закрытии модального окна.</param>
function OrderDetails({ order, onClose }) {
  if (!order) return null;

  return (
    <div className="modal-overlay" onClick={onClose}>
      <div className="modal-content" onClick={(e) => e.stopPropagation()}>
        <div className="modal-header">
          <h2>🔍 Детали заказа</h2>
          <button className="modal-close" onClick={onClose}>✖</button>
        </div>
        <div className="modal-body">
          <div className="detail-row">
            <span className="label">Номер заказа:</span>
            <span className="value"><strong>{order.orderNumber}</strong></span>
          </div>
          <div className="detail-row">
            <span className="label">Город отправителя:</span>
            <span className="value">{order.senderCity}</span>
          </div>
          <div className="detail-row">
            <span className="label">Адрес отправителя:</span>
            <span className="value">{order.senderAddress}</span>
          </div>
          <div className="detail-row">
            <span className="label">Город получателя:</span>
            <span className="value">{order.recipientCity}</span>
          </div>
          <div className="detail-row">
            <span className="label">Адрес получателя:</span>
            <span className="value">{order.recipientAddress}</span>
          </div>
          <div className="detail-row">
            <span className="label">Вес груза:</span>
            <span className="value">{order.weight} кг</span>
          </div>
          <div className="detail-row">
            <span className="label">Дата забора:</span>
            <span className="value">{new Date(order.pickupDate).toLocaleDateString('ru-RU')}</span>
          </div>
          <div className="detail-row">
            <span className="label">Дата создания:</span>
            <span className="value">{new Date(order.createdAt).toLocaleString('ru-RU')}</span>
          </div>
        </div>
      </div>
    </div>
  );
}

export default OrderDetails;