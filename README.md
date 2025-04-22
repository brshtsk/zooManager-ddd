# Zoo Management System

## Запуск

Рекомендую запускать в JetBrains Rider. После запуска удобно работать с API в http://localhost:5000/swagger/index.html (или другой адрес хоста).

## Функционал

**Реализован весь требуемый функционал:**

- Добавление, удаление и перемещения животных
- Добавление и удаление вольеров
- Изменение расписания кормления
- Получение статистики

Данные функции реализованы через REST API (модуль `Presentation`) и сервисы (модуль `Application`). Данные хранятся in-memory, без подключения к БД.

Используются доменные события: `AnimalMovedEvent` и `FeedingTimeEvent`.

## Архитектура

```text
[ Presentation (API) ]
          ↓
[ Infrastructure ]
          ↓
[ Application ]
          ↓
     [ Domain ]
```

**Domain-Driven Design (DDD):**

- Бизнес-логика и сущности находятся в `Domain`
- Используются Value Object’ы (например, `AnimalFood`, `FeedingTime`)
- Бизнес-правила инкапсулированы в моделях и сервисах

**Clean Architecture:**

- Проект разделён на 4 уровня: `Domain`, `Application`, `Infrastructure`, `Presentation`
- Зависимости направлены внутрь
- Логика приложения отделена от хранения данных и внешнего взаимодействия
- Для каждого слоя есть свой csproj
