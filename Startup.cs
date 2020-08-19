
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
            FJC_ForgotPassword fJC_Forgot=new FJC_ForgotPassword();

            if(fJC_Forgot.TypeOfUser!='I'|| fJC_Forgot.TypeOfUpdate =='E')
            {
                fJC_Forgot.PAN_ID="XXXXXXXX";  
            }

            FJC_Registration  fJC_Registration=new FJC_Registration();  
             if(fJC_Registration.REG_TYPE_ID==1 || fJC_Registration.REG_TYPE_ID==2 )
                {
                  fJC_Registration.PANID="XXXXXXXX";  
                } 
        }

        public void ConfigureServices(IServiceCollection services)
        {
           
            services.AddControllers();

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("sql"));
            });

           
            
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITestingService, TestingServices>();
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<IRegistrationService, RegistrationService>();
            services.AddScoped<IGenerateEVENTService, GenerateEVENTService>();
            
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