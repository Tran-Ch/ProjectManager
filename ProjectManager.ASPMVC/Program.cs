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

            builder.Services.AddHttpContextAccessor();
            builder.Services.AddSession();

            builder.Services.AddScoped<SqlConnection>( option =>
                new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ProjectManager;Integrated Security=True;Connect Timeout=60;Encrypt=True;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False"));

            builder.Services.AddScoped<IEmployeeRepository<ProjectManager.DAL.Entities.Employee>, ProjectManager.DAL.Services.EmployeeService>();
            builder.Services.AddScoped<IPostRepository<ProjectManager.DAL.Entities.Post>, ProjectManager.DAL.Services.PostService>();
            builder.Services.AddScoped<IProjectRepository<ProjectManager.DAL.Entities.Project>, ProjectManager.DAL.Services.ProjectService>();
            builder.Services.AddScoped<IUserRepository<ProjectManager.DAL.Entities.User>, ProjectManager.DAL.Services.UserService>();

            builder.Services.AddScoped<IEmployeeRepository<ProjectManager.BLL.Entities.Employee>, ProjectManager.BLL.Services.EmployeeService>();
            builder.Services.AddScoped<IPostRepository<ProjectManager.BLL.Entities.Post>, ProjectManager.BLL.Services.PostService>();
            builder.Services.AddScoped<IProjectRepository<ProjectManager.BLL.Entities.Project>, ProjectManager.BLL.Services.ProjectService>();
            builder.Services.AddScoped<IUserRepository<ProjectManager.BLL.Entities.User>, ProjectManager.BLL.Services.UserService>();

            builder.Services.AddScoped<UserSessionManager>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseStaticFiles();
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
