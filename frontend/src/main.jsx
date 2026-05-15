/**
 * Точка входа React-приложения.
 * Выполняет рендер корневого компонента App в DOM-элемент root.
 */

import React from 'react';
import ReactDOM from 'react-dom/client';
import App from './App';
import './index.css';

/// <summary>
/// Создаёт корневой узел React и рендерит приложение.
/// </summary>
ReactDOM.createRoot(document.getElementById('root')).render(
  <React.StrictMode>
    <App />
  </React.StrictMode>
);