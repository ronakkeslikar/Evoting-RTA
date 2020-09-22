
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
        //readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration; 
        }

        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddCors(options =>
            //{
            //    options.AddPolicy(name: MyAllowSpecificOrigins,
            //                      builder =>
            //                      {
            //                          builder.WithOrigins("https://slimlink.io",
            //                                              "http://localhost:4200");
            //                      });
            //});
            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));
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
            services.AddScoped<IDocumentDownloadService, DocumentDownloadService>();
            services.AddScoped<IDocumentUploadService, DocumentUploadService>();
            services.AddScoped<IApproveEventService, ApproveEventService>();
            services.AddScoped<IEventListService, EventListService>();
            services.AddScoped<IListService, ListService>();

            services.AddScoped<IROMUploadService, ROMUploadService>();
            services.AddScoped<IFileUploadService, FileUploadService>();
            services.AddScoped<IPrivateListService, PrivateListService>();
            services.AddScoped<IListService, ListService>();
             services.AddScoped<IEventListService, EventListService>();
             services.AddScoped<IShareHolderService, ShareHolderService>();
             services.AddScoped<IReportsService, ReportsService>();



            
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors("MyPolicy");
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


            app.UseHttpsRedirection();
            app.UseStaticFiles();
            

            //app.UseCors(MyAllowSpecificOrigins);

        }
    }
}