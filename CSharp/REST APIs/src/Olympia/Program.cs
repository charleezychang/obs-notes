using Microsoft.EntityFrameworkCore;

using Olympia.DependencyInjection;
using Olympia.Persistence;
using Olympia.RequestPipeline;

var builder = WebApplication.CreateBuilder(args);
{
    //builder.Services.AddScoped<ProductsService>();
    builder.Services
        .AddPersistence(builder.Configuration)
        .AddServices()
        .AddControllers();
    builder.Services.AddSwaggerGen();
}
var app = builder.Build();
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        app.InitializeDatabase();
    }
    app.MapControllers();

}
app.Run();

// dotnet run --project src/Olympia