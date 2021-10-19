using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using ProcurementManagementSystemServices.Providers;
using ProcurementManagementSystemServices.Models;

namespace ProcurementManagmentSystemAPIs
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //DTO Mapper
            services.AddAutoMapper(typeof(Startup));

            //Auth
            var tokenKey = Configuration.GetValue<string>("TokenKey");
            var key = Encoding.ASCII.GetBytes(tokenKey);
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
            services.AddSingleton<IJWTAuthenticationManager>(new JWTAuthenticationManager(tokenKey));

            //Authorizaion
            services.AddAuthorization(options =>
            {
                options.AddPolicy("OnlySiteSupervisor",
                     policy => policy.RequireRole("SITE_SUPERVISOR"));
                options.AddPolicy("OnlyProcurementOfficer",
                     policy => policy.RequireRole("PROCUREMENT_OFFICER"));
                options.AddPolicy("OnlyFinanceOfficer",
                     policy => policy.RequireRole("FINANCE_OFFICER"));
                options.AddPolicy("OnlyAdmin",
                     policy => policy.RequireRole("ADMIN"));
            });

            //DB Context injection
            string dbCon = Configuration.GetConnectionString("MySQLConnection");
            services.AddDbContext<ProcurementManagmentContext>(
                options => options.UseMySql(dbCon, MySqlServerVersion.LatestSupportedServerVersion)
            );

            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ProcurementManagmentSystemData", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ProcurementManagmentSystemData v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
