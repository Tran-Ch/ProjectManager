using Microsoft.Data.SqlClient;
using ProjectManager.ASPMVC.Handlers;
using ProjectManager.Common.Repositories;

namespace ProjectManager.ASPMVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            string connectionString = builder.Configuration.GetConnectionString("ProjectManagerDb")!;

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // Services nécessaires aux sessions
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddScoped<UserSessionManager>();

            builder.Services.AddDistributedMemoryCache();

            builder.Services.AddSession(options =>
            {
                options.Cookie.Name = "ProjectManager.Session";
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
                options.IdleTimeout = TimeSpan.FromMinutes(30);
            });

            // Injection de dépendance SqlConnection
            builder.Services.AddScoped<SqlConnection>(serviceProvider =>
                new SqlConnection(connectionString));

            // DAL
            builder.Services.AddScoped<IEmployeeRepository<ProjectManager.DAL.Entities.Employee>, ProjectManager.DAL.Services.EmployeeService>();
            builder.Services.AddScoped<IPostRepository<ProjectManager.DAL.Entities.Post>, ProjectManager.DAL.Services.PostService>();
            builder.Services.AddScoped<IProjectRepository<ProjectManager.DAL.Entities.Project>, ProjectManager.DAL.Services.ProjectService>();
            builder.Services.AddScoped<IUserRepository<ProjectManager.DAL.Entities.User>, ProjectManager.DAL.Services.UserService>();

            // BLL
            builder.Services.AddScoped<IEmployeeRepository<ProjectManager.BLL.Entities.Employee>, ProjectManager.BLL.Services.EmployeeService>();
            builder.Services.AddScoped<IPostRepository<ProjectManager.BLL.Entities.Post>, ProjectManager.BLL.Services.PostService>();
            builder.Services.AddScoped<IProjectRepository<ProjectManager.BLL.Entities.Project>, ProjectManager.BLL.Services.ProjectService>();
            builder.Services.AddScoped<IUserRepository<ProjectManager.BLL.Entities.User>, ProjectManager.BLL.Services.UserService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseSession();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
