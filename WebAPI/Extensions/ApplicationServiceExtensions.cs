using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebAPI.Data;
using WebAPI.Helpers;
using WebAPI.Interfaces;
using WebAPI.Services;

namespace WebAPI.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(
            this IServiceCollection services, IConfiguration config)   
        {
            services.AddScoped<ITokenService, TokenService>(); 
            // Inject UserRepository
            services.AddScoped<IUserRepository, UserRepository>();
            // Inject Automapper
            services.AddAutoMapper(typeof(AuthoMapperProfiles).Assembly);
            // Setting up SQLite Provider
            services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlite(config.GetConnectionString("DefaultConnection"));
            });   

            return services;
        }
    }
}