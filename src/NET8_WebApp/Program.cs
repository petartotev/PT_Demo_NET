using NET8_WebApp.Services;
using NET8_WebApp.Services.Interfaces;

namespace NET8_WebApp;

// .NET8 uses C#12
public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        // https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-8#keyed-di-services
        builder.Services.AddKeyedSingleton<IKeyedMinuteService, KeyedEvenMinuteService>("even");
        builder.Services.AddKeyedSingleton<IKeyedMinuteService, KeyedOddMinuteService>("odd");

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}