using TodoListForMee.Components;
using TodoListForMee.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddTransient<ITaskApiClient, TaskApiClient>();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:5002") });
//builder.Services.AddScoped(sp => new HttpClient
//{
//    BaseAddress = new Uri(builder.Configuration["BackendApiUrl"])
//});

builder.Services.AddCors(options =>
{    
    options.AddPolicy("Policy1",
        policy => policy
        .SetIsOriginAllowed((host) => true)
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials());
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.UseCors("Policy1");

app.Run();
