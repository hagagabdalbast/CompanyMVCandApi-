using Demo.Models;
using Demo.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Net;

namespace Demo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container "Register".
            //1-Built in service and already register in IOC Container "IConfiguration"
            //2-Built in service but not register in IOC Container "AddSession"

            //for use the pipline filters to implement the pipline on all the application

            builder.Services.AddControllersWithViews();
                //conf => conf.Filters.Add(Authorization)) ;


            //register the ITIEntity to the service
            builder.Services.AddDbContext<ITIEntity>(OptionsBuilder => {
                OptionsBuilder.UseSqlServer(builder.Configuration.GetConnectionString("constr"));
                });

            //register the UserManger and the RoleManager in the service

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<ITIEntity>();

            //3-Custom service that we will use to register the interfaces and classes repository
            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();

            var app = builder.Build();
            
            // Configure the HTTP request pipeline.

            
            /*//custom middleware "inline component"

            app.Use(async (httpContext, next) =>
            {
                //write on response
                await httpContext.Response.WriteAsync("1)MiddleWare 1\n");
                //call next middleware
                await next.Invoke();
            });

            app.Use(async (httpContext, next) =>
            {
                //write on response
                await httpContext.Response.WriteAsync("2)MiddleWare 2\n");
                //call next middleware
                await next.Invoke();
            });

            app.Run(async (httpContext) =>
            {
                //write on response and terminate
                await httpContext.Response.WriteAsync("3)Terminate \n");
                
                
            });*/


            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();//for the Authorize Filter "cookie"

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}