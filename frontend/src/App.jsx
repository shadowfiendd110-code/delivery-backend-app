/**
 * Главный компонент приложения.
 * Управляет вкладками и модальным окном деталей заказа.
 */

import { useState } from 'react';
import OrderForm from './components/OrderForm';
import OrderList from './components/OrderList';
import OrderDetails from './components/OrderDetails';
import './App.css';

/// <summary>
/// Главный компонент приложения.
/// </summary>
function App() {
  /// <summary>
  /// Текущая активная вкладка ('create' или 'list').
  /// </summary>
  const [activeTab, setActiveTab] = useState('create');

  /// <summary>
  /// Выбранный заказ для отображения в модальном окне.
  /// </summary>
  const [selectedOrder, setSelectedOrder] = useState(null);

  /// <summary>
  /// Обработчик успешного создания заказа.
  /// </summary>
  const handleOrderCreated = () => {
    if (window.refreshOrderList) {
      window.refreshOrderList();
    }
    setActiveTab('list');
  };

  return (
    <div className="app">
      <h1>🚚 Система приёма заказов на доставку</h1>

      <div className="tabs">
        <button
          className={activeTab === 'create' ? 'tab active' : 'tab'}
          onClick={() => setActiveTab('create')}
        >
          ➕ Создать заказ
        </button>
        <button
          className={activeTab === 'list' ? 'tab active' : 'tab'}
          onClick={() => setActiveTab('list')}
        >
          📋 Список заказов
        </button>
      </div>

      <div className="tab-content">
        {activeTab === 'create' && (
          <OrderForm onOrderCreated={handleOrderCreated} />
        )}
        {activeTab === 'list' && (
          <OrderList onSelectOrder={setSelectedOrder} />
        )}
      </div>

      {selectedOrder && (
        <OrderDetails order={selectedOrder} onClose={() => setSelectedOrder(null)} />
      )}
    </div>
  );
}

export default App;