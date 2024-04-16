using CarRental.APIs.Extensions;
using CarRental.APIs.Helper;
using CarRental.Core;
using CarRental.Core.Entities;
using CarRental.Core.Repositories;
using CarRental.Repository;
using CarRental.Repository.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System;

namespace CarRental.APIs
{
    public class Program
    {
        public static void Main(string[] args)
        {
            #region Configure Services
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            // DB Of CarRental
            builder.Services.AddDbContext<CarRentalContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddScoped<ICarRepository, CarRepository>();
            //builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            // We Call Method Which Contain All Services of Identity
            builder.Services.AddIdentityServices(builder.Configuration);

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Api Demo", Version = "v1" });

                var securitySchema = new OpenApiSecurityScheme
                {
                    Description = "JWT Auth Bearer Scheme",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    Reference = new OpenApiReference
                    {
                        Id = "Bearer",
                        Type = ReferenceType.SecurityScheme
                    }
                };

                c.AddSecurityDefinition("Bearer", securitySchema);

                var securityRequirment = new OpenApiSecurityRequirement
                {
                    {
                        securitySchema, new string[]{"Bearer"}
                    }
                };

                c.AddSecurityRequirement(securityRequirment);
            });
            #endregion

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
