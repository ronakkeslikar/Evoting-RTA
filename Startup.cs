
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using evoting.Persistence.Contexts;
using evoting.Services;
using evoting.Domain.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using evoting.Utility;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using System.Web.Http;


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
            JWT_SetupServices(services);
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
                       .AllowAnyHeader()
                       
                       ;
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
            services.AddScoped<IVote_InvestorService, Vote_InvestorService>();
            services.AddScoped<IRegisterSpeakerService, RegisterSpeakerService>();
            services.AddScoped<ICustodianROMUploadService, CustodianROMUploadService>();
            services.AddScoped<IAccountSearchService, AccountSearchService>();
            services.AddScoped<ILockEventService, LockEventService>();
            services.AddScoped<IVideoConfService, VideoConfService>();
            services.AddScoped<ISpeakerListService, SpeakerListService>();
            services.AddScoped<IPaneListService, PaneListService>();

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors("MyPolicy");
            //app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);

            app.UseAuthentication();
            app.UseAuthorization();

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

        public void JWT_SetupServices(IServiceCollection _services)
        {
            var issuer = "https://evoting.bigshareonline.com";
            _services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
          .AddJwtBearer(options =>
          {
              options.TokenValidationParameters = new TokenValidationParameters
              {
                  ValidateIssuer = true,
                  ValidateAudience = true,
                  ValidateIssuerSigningKey = true,
                  ValidIssuer = issuer,
                  ValidAudience = issuer,
                  IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Token_Handling.key))
              };

              options.Events = new JwtBearerEvents
              {
                  OnAuthenticationFailed = context =>
                  {
                      if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                      {
                          context.Response.Headers.Add("Token-Expired", "true");
                      }
                      return Task.CompletedTask;
                  }
              };
          });
        }

        
    }
}
