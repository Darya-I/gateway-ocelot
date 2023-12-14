using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Services;

var builder = WebApplication.CreateBuilder(args);

// Настройка подключения к базе данных
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Добавление сервиса и его реализации в контейнер служб
builder.Services.AddScoped<IProjectService, ProjectService>();

// Добавление контроллеров
builder.Services.AddControllers();

// Добавление генерации документации Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Project", Version = "v1" });
});



var app = builder.Build();

// Enable middleware to serve generated Swagger as a JSON endpoint.
app.UseSwagger();

// Настройка конвейера обработки HTTP-запросо
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Project V1");
    });

}

// Обработка запросов HTTPS
app.UseHttpsRedirection();

// Определение маршрутов для контроллеров
app.UseAuthorization();
app.MapControllers();

app.Run();
