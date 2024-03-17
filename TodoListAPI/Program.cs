using Microsoft.EntityFrameworkCore;
using TodoListAPI.Data;
using Microsoft.Extensions.DependencyInjection;
using TodoListAPI.Extensions;
using TodoListAPI.Repositories;
using Microsoft.AspNetCore.Builder;
using TodoListAPI.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Add DbContext
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<TodoListDbContext>(x => x.UseSqlServer(connectionString));

builder.Services.AddScoped<DbInitializer>();

//builder.Services.Configure<PositionOptions>(
//    builder.Configuration.GetSection(PositionOptions.Position));
//builder.Services.Configure<ColorOptions>(
//    builder.Configuration.GetSection(ColorOptions.Color));
builder.Services
    //.AddConfig(builder.Configuration)
    .AddMyDependencyGroup();

//builder.Services.AddScoped<IMyDependency, MyDependency>();
//builder.Services.AddScoped<IMyDependency, MyDependency2>();

//builder.Services.AddTransient<IOperationTransient, Operation>();
//builder.Services.AddScoped<IOperationScoped, Operation>();
//builder.Services.AddSingleton<IOperationSingleton, Operation>();
builder.Services.AddTransient<ITaskRepository, TaskRepository>();

var app = builder.Build();

using (var serviceScope = app.Services.CreateScope())
{
    var services = serviceScope.ServiceProvider;

    var myDependency = services.GetRequiredService<IMyDependency>();
    myDependency.WriteMessage("Call services from main");
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseItToSeedSqlServer();
    app.UseSwagger();
    app.UseSwaggerUI();
}


//app.UseMyMiddleware();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
