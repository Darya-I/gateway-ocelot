# API Getaway ocelot
![https://img.shields.io/badge/Status-WIP-blue](https://img.shields.io/badge/Status-WIP-blue)
 
 В примере используетя всего один веб-API CRUD, связанный с однотабличной БД, так как мы разбираемся только с API шлюзом

<details>
  
  <summary>Подробнее про веб-API ...</summary>
  
  ## WebApplication3
### Версия .NET и используемые библиотеки

Проект разработан на платформе .NET 6.0.

Для работы с базой данных и ORM использованы библиотеки Entity Framework Core. В проекте используются следующие версии библиотек Entity Framework Core:
- Microsoft.EntityFrameworkCore версии 7.0.0
- Microsoft.EntityFrameworkCore.SqlServer версии 7.0.0
- Microsoft.EntityFrameworkCore.Tools версии 6.0.23

Библиотека для генерации открытой документации API (Swagger & OpenAPI):
- Swashbuckle.AspNetCore версии 6.2.3
- Swashbuckle.AspNetCore.Annotations версии 6.5.0


### /Project.cs
Модель Project представляет собой сущность, используемую для хранения информации о проектах в рамках системы.

### /Data/AppDbContext.cs

Класс AppDbContext представляет собой контекст базы данных для микросервиса "Project", обеспечивающий доступ к базе данных и представляющий сеанс работы с ней. Данный контекст отвечает за взаимодействие между приложением и базой данных, определение структуры базы данных и выполнение операций с данными.
> Для взаимодействия с базой данных, связанной с микросервисом "Project", используйте класс AppDbContext для выполнения операций с данными, таких как запросы, добавление, обновление и удаление данных.

- **AppDbContext(DbContextOptions<AppDbContext> options)**: Конструктор класса AppDbContext, принимающий параметры для настройки контекста.
  > options: Экземпляр DbContextOptions, представляющий параметры для данного контекста.
  > Поведение: Проверяет параметр options и инициализирует контекст базы данных.

- **DbSet<Project> Projects**: Свойство, представляющее коллекцию сущностей Project в базе данных.
  > Использование: Используйте это свойство для запросов и выполнения операций с данными сущностей Project в базе данных.


### /Controllers/ProjectsController.cs

Контроллер ProjectsController обрабатывает HTTP-запросы для выполнения операций CRUD (Create, Read, Update, Delete) с данными о проектах.

#### Маршруты
Все методы контроллера взаимодействуют с веб-приложением через следующий маршрут: /api/projects

#### Методы контроллера
  **GET: /api/projects**: Получение всех проектов.
  - Возвращает: Список всех проектов в базе данных.
  - Статусы ответа: 200 - Успешное выполнение запроса, 404 - Проекты не найдены.

  **GET: /api/projects/{id}**: Получение конкретного проекта по идентификатору.
   - Возвращает: Информацию о проекте с указанным идентификатором.
   - Статусы ответа: 200 - Успешное выполнение запроса, 404 - Проект не найден.

  **PUT: /api/projects/{id}**: Обновление проекта по идентификатору.
  - Возвращает: Статус No Content в случае успешного обновления, BadRequest - в случае неверных входных данных, NotFound - если проект не найден.

  **POST: /api/projects**: Создание нового проекта.
  - Возвращает: Информацию о созданном проекте.
  - Статусы ответа: 201 - Успешное создание, 400 - Неверные входные данные, 500 - Ошибка сервера.

  **DELETE: /api/projects/{id}**: Удаление проекта по идентификатору.
  - Возвращает: Статус No Content в случае успешного удаления, NotFound - если проект не найден.

> Использование: Для взаимодействия с данными о проектах в системе, отправляйте HTTP-запросы на соответствующие маршруты, используя методы запроса (GET, POST, PUT, DELETE).

### /Program.cs
Файл Program.cs представляет собой точку входа в приложение ASP.NET Core. Здесь происходит конфигурирование и настройка окружения, подключение к базе данных, добавление контроллеров, генерация документации Swagger и обработка HTTP-запросов.

  **ConfigureServices**: добавление контекста базы данных AppDbContext и настройка подключения к базе данных SQL Server через Entity Framework Core.

#### Генерация документации Swagger
  **AddEndpointsApiExplorer()**: Добавляет генерацию информации об эндпоинтах (конечных точках) вашего API. Это позволяет Swagger узнавать обо всех конечных точках, доступных в вашем веб-приложении.
  
  **AddSwaggerGen()**: Добавляет генерацию документации Swagger. Здесь можно указать информацию о API, такую как название, версия и другие метаданные. 
  > В данном примере SwaggerDoc("v1", ...) устанавливает версию документации "v1" с информацией, включающей название "Project" и версию "v1".
</details>

 ## API_v2

### Ocelot
Описание: Ocelot - это библиотека для создания API-шлюзов в .NET. Она предназначена для перенаправления запросов на различные микросервисы. Ocelot обеспечивает такие функции, как маршрутизация, авторизация, расширяемость и многое другое. Она помогает упростить архитектуру микросервисов.

Версия: 18.0.0

[Ocelot GitHub репозиторий](https://github.com/ThreeMammals/Ocelot)

### ocelot.json
Описание конфигурации для API-шлюза Ocelot.

#### Глобальная конфигурация

 - BaseUrl (Базовый URL)
- **Описание:** Базовый URL для API-шлюза.
- **Значение:** https://localhost:7066

#### Маршруты

#### Route 1

- **Upstream Path Template** (Шаблон входящего пути): /
- **Upstream Http Method** (HTTP-методы входящего запроса): GET
- **Downstream Path Template** (Шаблон исходящего пути): /
- **Downstream Scheme** (Протокол исходящего запроса): https
- **Downstream Host and Ports** (Хост и порт исходящего запроса):
  - **Host:** localhost
  - **Port:** 7066

#### Route 2

- **Upstream Path Template** (Шаблон входящего пути): /gateway/projects
- **Upstream Http Method** (HTTP-методы входящего запроса): GET, POST
- **Downstream Path Template** (Шаблон исходящего пути): /api/projects
- **Downstream Scheme** (Протокол исходящего запроса): https
- **Downstream Host and Ports** (Хост и порт исходящего запроса):
  - **Host (Хост):** localhost
  - **Port (Порт):** 7001

#### Route 3

- **Upstream Path Template** (Шаблон входящего пути): /gateway/projects/{Id}
- **Upstream Http Method** (HTTP-методы входящего запроса): GET, PUT, DELETE
- **Downstream Path Template** (Шаблон исходящего пути): /api/projects/{Id}
- **Downstream Scheme** (Протокол исходящего запроса): https
- **Downstream Host and Ports** (Хост и порт исходящего запроса):
  - **Host (Хост):** localhost
  - **Port (Порт):** 7001
 
> Каждый маршрут указывает шаблоны входящих и исходящих путей, методы HTTP и детали удаленного хоста.


## Program.cs

- **new ConfigurationBuilder()**: создает экземпляр ConfigurationBuilder, который представляет собой строитель конфигурации.
  > .AddJsonFile("ocelot.json"): обавляет файл ocelot.json как источник конфигурационных данных. Это означает, что данные конфигурации будут загружены из этого JSON-файла.
  > После этого кода переменная configuration содержит все данные, загруженные из файла ocelot.json

- **UseRouting()**: устанавливает соответствие маршрутов запросов в приложении к соответствующим обработчикам запросов.

- **UseEndpoints()**: этот метод используется для настройки точек входа приложения. Точки входа представляют собой определение того, как обрабатывать входящие HTTP-запросы.
  -  В блоке endpoints.MapGet("/") определяется обработчик для HTTP GET запросов, направленных на корневой URL. В данном случае, при обращении к корневому URL приложения, будет вызван указанный обработчик.
  -  async context => { await context.Response.WriteAsync("Gateway is working"); } - это асинхронная функция обратного вызова, которая будет выполнена при получении GET запроса на корневой URL. В данном случае, приложение просто отправляет строку "Gateway is working" в ответ на такие запросы.
 
> Данный код устанавливает обработчик для GET запросов к корневому URL приложения, который просто отправляет строку в ответ на такие запросы. Требовалось только для проверки работы в процессе разработки. 

- **app.UseOcelot()**: этот метод добавляет промежуточное ПО Ocelot в конвейер обработки запросов приложения. Это позволяет Ocelot перехватывать входящие запросы и обрабатывать их согласно определенным правилам и настройкам, указанным в файле конфигурации.
  - Wait() - этот вызов блокирует выполнение текущего потока до тех пор, пока задача, возвращаемая методом app.UseOcelot(), не завершится. В данном случае, это используется для ожидания завершения настройки Ocelot перед запуском приложения.
