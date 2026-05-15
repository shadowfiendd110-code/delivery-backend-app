/**
 * Компонент формы создания нового заказа на доставку.
 * Содержит валидацию полей и отправку данных на API.
 */

import { useState } from 'react';
import { createOrder } from '../services/api';

/**
 * @param {Object} props - Свойства компонента.
 * @param {Function} props.onOrderCreated - Колбэк, вызываемый после успешного создания заказа.
 */
function OrderForm({ onOrderCreated }) {
  /** Состояние данных формы */
  const [formData, setFormData] = useState({
    senderCity: '',
    senderAddress: '',
    recipientCity: '',
    recipientAddress: '',
    weight: '',
    pickupDate: '',
  });

  /** Состояние ошибок валидации */
  const [errors, setErrors] = useState({});
  /** Состояние загрузки (отправки запроса) */
  const [isLoading, setIsLoading] = useState(false);
  /** Сообщение об успешном создании заказа */
  const [successMessage, setSuccessMessage] = useState('');

  /**
   * Обработчик изменения значений полей формы.
   * @param {Event} e - Событие изменения.
   */
  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData((prev) => ({ ...prev, [name]: value }));
    
    // Очищаем ошибку для поля, которое пользователь начал исправлять
    if (errors[name]) {
      setErrors((prev) => ({ ...prev, [name]: '' }));
    }
  };

  /**
   * Валидация всех полей формы.
   * @returns {boolean} true, если все поля валидны.
   */
  const validate = () => {
    const newErrors = {};

    if (!formData.senderCity.trim()) {
      newErrors.senderCity = 'Укажите город отправителя';
    }
    if (!formData.senderAddress.trim()) {
      newErrors.senderAddress = 'Укажите адрес отправителя';
    }
    if (!formData.recipientCity.trim()) {
      newErrors.recipientCity = 'Укажите город получателя';
    }
    if (!formData.recipientAddress.trim()) {
      newErrors.recipientAddress = 'Укажите адрес получателя';
    }

    const weightNum = parseFloat(formData.weight);
    if (!formData.weight || isNaN(weightNum) || weightNum <= 0 || weightNum > 1000) {
      newErrors.weight = 'Вес должен быть от 0.01 до 1000 кг';
    }

    if (!formData.pickupDate) {
      newErrors.pickupDate = 'Выберите дату забора';
    } else {
      const selected = new Date(formData.pickupDate);
      const today = new Date();
      today.setHours(0, 0, 0, 0);
      if (selected < today) {
        newErrors.pickupDate = 'Дата не может быть в прошлом';
      }
    }

    setErrors(newErrors);
    return Object.keys(newErrors).length === 0;
  };

  /**
   * Обработчик отправки формы.
   * @param {Event} e - Событие отправки.
   */
  const handleSubmit = async (e) => {
    e.preventDefault();
    if (!validate()) return;

    setIsLoading(true);
    setSuccessMessage('');

    try {
      const payload = {
        ...formData,
        weight: parseFloat(formData.weight),
      };

      const newOrder = await createOrder(payload);
      setSuccessMessage(`✅ Заказ №${newOrder.orderNumber} создан!`);

      // Очищаем форму
      setFormData({
        senderCity: '',
        senderAddress: '',
        recipientCity: '',
        recipientAddress: '',
        weight: '',
        pickupDate: '',
      });

      // Уведомляем родительский компонент об успешном создании
      if (onOrderCreated) onOrderCreated();

      // Скрываем сообщение через 3 секунды
      setTimeout(() => setSuccessMessage(''), 3000);
    } catch (err) {
      alert('Ошибка при создании заказа. Проверьте подключение к серверу.');
    } finally {
      setIsLoading(false);
    }
  };

  return (
    <div className="order-form">
      <h2>📦 Новый заказ на доставку</h2>

      {successMessage && <div className="success-message">{successMessage}</div>}

      <form onSubmit={handleSubmit}>
        {/* Поле: Город отправителя */}
        <div className="form-group">
          <label>Город отправителя *</label>
          <input
            type="text"
            name="senderCity"
            value={formData.senderCity}
            onChange={handleChange}
            className={errors.senderCity ? 'error' : ''}
          />
          {errors.senderCity && <span className="error-text">{errors.senderCity}</span>}
        </div>

        {/* Поле: Адрес отправителя */}
        <div className="form-group">
          <label>Адрес отправителя *</label>
          <input
            type="text"
            name="senderAddress"
            value={formData.senderAddress}
            onChange={handleChange}
            className={errors.senderAddress ? 'error' : ''}
          />
          {errors.senderAddress && <span className="error-text">{errors.senderAddress}</span>}
        </div>

        {/* Поле: Город получателя */}
        <div className="form-group">
          <label>Город получателя *</label>
          <input
            type="text"
            name="recipientCity"
            value={formData.recipientCity}
            onChange={handleChange}
            className={errors.recipientCity ? 'error' : ''}
          />
          {errors.recipientCity && <span className="error-text">{errors.recipientCity}</span>}
        </div>

        {/* Поле: Адрес получателя */}
        <div className="form-group">
          <label>Адрес получателя *</label>
          <input
            type="text"
            name="recipientAddress"
            value={formData.recipientAddress}
            onChange={handleChange}
            className={errors.recipientAddress ? 'error' : ''}
          />
          {errors.recipientAddress && <span className="error-text">{errors.recipientAddress}</span>}
        </div>

        <div className="form-row">
          {/* Поле: Вес груза */}
          <div className="form-group">
            <label>Вес (кг) *</label>
            <input
              type="number"
              step="0.01"
              name="weight"
              value={formData.weight}
              onChange={handleChange}
              className={errors.weight ? 'error' : ''}
            />
            {errors.weight && <span className="error-text">{errors.weight}</span>}
          </div>

          {/* Поле: Дата забора */}
          <div className="form-group">
            <label>Дата забора *</label>
            <input
              type="date"
              name="pickupDate"
              value={formData.pickupDate}
              onChange={handleChange}
              className={errors.pickupDate ? 'error' : ''}
            />
            {errors.pickupDate && <span className="error-text">{errors.pickupDate}</span>}
          </div>
        </div>

        <button type="submit" disabled={isLoading} className="btn-submit">
          {isLoading ? 'Отправка...' : '🚀 Создать заказ'}
        </button>
      </form>
    </div>
  );
}

export default OrderForm;