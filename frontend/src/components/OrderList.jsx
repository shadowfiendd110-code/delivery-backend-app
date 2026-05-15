/**
 * Компонент для отображения списка заказов в виде таблицы.
 */

import { useState, useEffect } from 'react';
import { getAllOrders } from '../services/api';

/// <summary>
/// Компонент списка заказов.
/// </summary>
/// <param name="onSelectOrder">Колбэк, вызываемый при выборе заказа.</param>
function OrderList({ onSelectOrder }) {
  /// <summary>
  /// Список заказов.
  /// </summary>
  const [orders, setOrders] = useState([]);

  /// <summary>
  /// Состояние загрузки.
  /// </summary>
  const [loading, setLoading] = useState(true);

  /// <summary>
  /// Состояние ошибки.
  /// </summary>
  const [error, setError] = useState(null);

  /// <summary>
  /// Загружает заказы с API.
  /// </summary>
  const loadOrders = async () => {
    try {
      setLoading(true);
      setError(null);
      const data = await getAllOrders();
      setOrders(data);
    } catch (err) {
      setError('Не удалось загрузить заказы. Проверьте соединение с бэкендом.');
      console.error(err);
    } finally {
      setLoading(false);
    }
  };

  /// <summary>
  /// Загружает заказы при монтировании компонента.
  /// </summary>
  useEffect(() => {
    loadOrders();
  }, []);

  /// <summary>
  /// Обновляет список заказов.
  /// </summary>
  const refresh = () => {
    loadOrders();
  };

  if (typeof window !== 'undefined') {
    window.refreshOrderList = refresh;
  }

  if (loading) return <div className="loading">⏳ Загрузка заказов...</div>;
  if (error) return <div className="error">{error}</div>;

  if (orders.length === 0) {
    return <div className="empty">📭 Пока нет ни одного заказа. Создайте первый!</div>;
  }

  return (
    <div className="order-list">
      <div className="list-header">
        <h2>📋 Список заказов</h2>
        <button onClick={refresh} className="btn-refresh">🔄 Обновить</button>
      </div>

      <table className="orders-table">
        <thead>
          <tr>
            <th>№ заказа</th>
            <th>Откуда</th>
            <th>Куда</th>
            <th>Вес (кг)</th>
            <th>Дата забора</th>
            <th></th>
          </tr>
        </thead>
        <tbody>
          {orders.map((order) => (
            <tr key={order.id} onClick={() => onSelectOrder(order)}>
              <td><strong>{order.orderNumber}</strong></td>
              <td>{order.senderCity}, {order.senderAddress.substring(0, 40)}</td>
              <td>{order.recipientCity}, {order.recipientAddress.substring(0, 40)}</td>
              <td>{order.weight}</td>
              <td>{new Date(order.pickupDate).toLocaleDateString('ru-RU')}</td>
              <td>
                <button
                  className="btn-details"
                  onClick={(e) => {
                    e.stopPropagation();
                    onSelectOrder(order);
                  }}
                >
                  Подробнее
                </button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}

export default OrderList;