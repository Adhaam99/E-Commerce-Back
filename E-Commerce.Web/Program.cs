using System.Threading.Tasks;
using AutoMapper;
using DomainLayer.Contracts;
using E_Commerce.Web.CustomMiddleWares;
using E_Commerce.Web.Extensions;
using E_Commerce.Web.Factories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Presentation.Data;
using Presistence;
using Presistence.Repositories;
using Service;
using Service.Profiles;
using ServiceAbstraction;
using Shared.ErrorModels;

namespace E_Commerce.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            #region Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddSwaggerServices();

            builder.Services.AddInfrastructureServices(builder.Configuration);
            
            builder.Services.AddApplicationServices();

            builder.Services.AddWebApplicationServices();

            #endregion

            var app = builder.Build();

            await app.SeedDataBaseAsync();

            // Configure the HTTP request pipeline.

            app.UseCustomExceptionMiddleWares();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwaggerMiddleWares();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            //app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
