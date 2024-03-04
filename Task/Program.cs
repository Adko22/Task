using BusinessLogic.ProjectLogic;
using BusinessLogic.TimeLogLogic;
using BusinessLogic.UserLogic;
using DataAccess.Context;
using DataAccess.Repositories.ProjectRepository;
using DataAccess.Repositories.TimeLogRepository;
using DataAccess.Repositories.UserRepository;
using Microsoft.EntityFrameworkCore;

namespace Task
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages().AddRazorRuntimeCompilation();


            builder.Services.AddDbContext<TaskContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });


            //DAL
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<ITimeLogRepository, TimeLogRepository>();
            builder.Services.AddScoped<IProjectRepository, ProjectRepository>();

            //BL
            builder.Services.AddScoped<IUserManager, UserManager>();
            builder.Services.AddScoped<IProjectManager, ProjectManager>();
            builder.Services.AddScoped<ITimeLogManager, TimeLogManager>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }
    }
}
