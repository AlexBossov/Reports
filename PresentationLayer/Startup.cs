using System;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Resources.EmployeeResources;
using BusinessLogicLayer.Resources.ProblemResources;
using BusinessLogicLayer.Resources.ReportResources;
using BusinessLogicLayer.Resources.TokenResources;
using BusinessLogicLayer.Security.Hashing;
using BusinessLogicLayer.Security.Tokens;
using BusinessLogicLayer.Services;
using DataAccessLayer;
using DataAccessLayer.EF;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using TokenHandler = BusinessLogicLayer.Security.Tokens.TokenHandler;
using TokenOptions = BusinessLogicLayer.Security.Tokens.TokenOptions;

namespace PresentationLayer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        
        public void ConfigureServices(IServiceCollection services)
        {
            string connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddControllersWithViews();
            
            services.AddDbContext<ReportsDbContext>(options => options.UseSqlServer(connection));
            
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IProblemRepository, ProblemRepository>();
            services.AddScoped<IReportRepository, ReportRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddSingleton<IPasswordHasher, PasswordHasher>();
            services.AddSingleton<ITokenHandler, TokenHandler>();
            services.AddScoped<IProblemService, ProblemService>();
            services.AddScoped<IReportService, ReportService>();

            services.AddControllers();
            services.AddAutoMapper(typeof(EmployeeResource));
            services.AddAutoMapper(typeof(EmployeeTreeResource));
            services.AddAutoMapper(typeof(AccessToken));
            services.AddAutoMapper(typeof(AccessTokenResource));
            services.AddAutoMapper(typeof(Problem));
            services.AddAutoMapper(typeof(ProblemResource));
            services.AddAutoMapper(typeof(Report));
            services.AddAutoMapper(typeof(ReportResource));
            services.Configure<TokenOptions>(Configuration.GetSection("TokenOptions"));
            TokenOptions tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>();

            var signingConfigurations = new SigningConfigurations(tokenOptions.Secret);
            services.AddSingleton(signingConfigurations);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(jwtBearerOptions =>
                {
                    jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = tokenOptions.Issuer,
                        ValidAudience = tokenOptions.Audience,
                        IssuerSigningKey = signingConfigurations.SecurityKey,
                        ClockSkew = TimeSpan.Zero
                    };
                });
            services.AddAutoMapper(GetType().Assembly);
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            
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