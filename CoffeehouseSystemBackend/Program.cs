using CoffeehouseLibrary;
using CoffeehouseLibrary.Models;
using Microsoft.EntityFrameworkCore;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddSwaggerGen();

        builder.Services.AddDbContext<CoffeehouseSystemContext>(
            option => option.UseSqlServer(
                builder.Configuration.GetConnectionString("CoffeehouseCS")
                )
            );

        builder.Services.AddAutoMapper(typeof(MapperProfile));

        builder.Services.AddCors();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseCors();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}