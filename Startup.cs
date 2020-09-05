
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using evoting.Persistence.Contexts;
using evoting.Services;
using evoting.Domain.Models;

namespace evoting
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration; 
        }

        public void ConfigureServices(IServiceCollection services)
        {
           
            services.AddControllers();

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("sql"));
            });
            AppDBCalls.SetDBConnect();

           
            
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITestingService, TestingServices>();
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<IRegistrationService, RegistrationService>();
            services.AddScoped<IGenerateEVENTService, GenerateEVENTService>();
            services.AddScoped<IAgreementUploadService, AgreementUploadService>(); 
            services.AddScoped<IROMUploadService, ROMUploadService>();
            services.AddScoped<IFileUploadService, FileUploadService>();
            services.AddScoped<IPrivateListService, PrivateListService>();
            services.AddScoped<IListService, ListService>();
             services.AddScoped<IEventListService, EventListService>();


            
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}