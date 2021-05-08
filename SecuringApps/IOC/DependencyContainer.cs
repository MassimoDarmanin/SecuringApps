using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SecuringApps.Data;
using SecuringApps.Repository;
using SecuringApps.Services;
using ShoppingCart.Application.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecuringApps.IOC
{
    public class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection services, string connectionString)
        {

            services.AddDbContext<SecuringAppDbContext>(options =>
            {
                options.UseSqlServer(
                 connectionString);//.UseLazyLoadingProxies();
            }
                );

            services.AddScoped<ITaskRepository, TaskRepository>();
            services.AddScoped<ITaskServices, TaskService>();

            services.AddScoped<IFileRepository, FileRepository>();
            services.AddScoped<IFileServices, FileServices>();

            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<ICommentServices, CommentServices>();

            services.AddAutoMapper(typeof(AutoMapperConfiguration));
            AutoMapperConfiguration.RegisterMappings();

        }
    }
}
