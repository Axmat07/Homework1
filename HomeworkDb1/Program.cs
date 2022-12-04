using HomeworkDb1;
using HomeworkDb1.Abstractions;
using HomeworkDb1.Domain;
using HomeworkDb1.Domain.Repos;
using HomeworkDb1.HomeworkTasks;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataContext>(c =>
{
    c.UseNpgsql(builder.Configuration.GetConnectionString("db"));
    c.UseSnakeCaseNamingConvention();
});

builder.Services.AddScoped<DbContext, DataContext>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
builder.Services.AddScoped<IHomeworkDoingService, HomeworkDoingService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var buildTable = scope.ServiceProvider.GetRequiredService<IHomeworkDoingService>();
    await buildTable.FillTable();
    await buildTable.PrintTables();
    await buildTable.AddToTable();
}

app.Run();
