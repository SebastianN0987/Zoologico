using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;
using System.Text.Json.Serialization;
// Necesario para usar UseNpgsql
using Npgsql.EntityFrameworkCore.PostgreSQL;
// Asume que tu DbContext está accesible, si no, necesitarás el using para tu carpeta Data.

namespace Zoologico.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<ZoologicoAPIContext>(options =>
                // Comentado: Proveedor de SQL Server
                //options.UseSqlServer(builder.Configuration.GetConnectionString("ZoologicoAPIContext.sqlserver") ?? throw new InvalidOperationException("Connection string 'ZoologicoAPIContext' not found."))

                // DESCOMENTADA Y ACTIVADA: Proveedor de PostgreSQL
                options.UseNpgsql(builder.Configuration.GetConnectionString("ZoologicoAPIContext.postgresql") ?? throw new InvalidOperationException("Connection string 'ZoologicoAPIContext' not found."))

            // Comentado: Proveedor de Oracle
            //options.UseOracle(builder.Configuration.GetConnectionString("ZoologicoAPIContext.oracle") ?? throw new InvalidOperationException("Connection string 'ZoologicoAPIContext' not found."))

            // Comentado: Proveedor de MariaDB/MySQL (CAUSABA EL ERROR)
            //options.UseMySql(
            //    builder.Configuration.GetConnectionString("ZoologicoAPIContext.mariadb") ?? throw new InvalidOperationException("Connection string 'ZoologicoAPIContext' not found."),
            //    Microsoft.EntityFrameworkCore.ServerVersion.Parse("12.0.2-MariaDB")
            //)
            );

            // Add services to the container.

            // ... (el resto del código de servicios permanece igual)

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Configure JSON options
            builder.Services
                .AddControllers()
                .AddNewtonsoftJson(
                    options =>
                    options.SerializerSettings.ReferenceLoopHandling
                    = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                );

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
}