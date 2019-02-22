using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PageCheckerAPI.DataAccess;
using AutoMapper;
using Hangfire;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using PageCheckerAPI.Repositories;
using PageCheckerAPI.Repositories.Interfaces;
using PageCheckerAPI.Services;
using PageCheckerAPI.Services.EmailService;
using PageCheckerAPI.Services.FacebookService;
using PageCheckerAPI.Services.HtmlDifferenceService;
using PageCheckerAPI.Services.HtmlDifferenceService.DifferenceServicesFactory;
using PageCheckerAPI.Services.PageBackgroundService;
using PageCheckerAPI.Services.PageService;
using PageCheckerAPI.Services.TokenService;
using PageCheckerAPI.Services.UserService;
using PageCheckerAPI.Services.WebsiteService;
using Swashbuckle.AspNetCore.Swagger;

namespace PageCheckerAPI
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
            services.AddSwaggerGen(c => c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo {Title = "PageChecker", Version = "v1"}));
            services.AddMvc();
            services.AddCors();
            services.AddDbContext<ApplicationDbContext>(opt =>
                opt.UseSqlServer(Configuration.GetConnectionString("AzurePageChecker")));
            services.AddAutoMapper();
            services.AddHangfire(conf =>
            {
                conf.UseSqlServerStorage(Configuration.GetConnectionString("AzurePageChecker"));
                // conf.UseSqlServerStorage("Server=(localdb)\\MSSQLLocalDB;Integrated Security=True");
            });

            services.AddScoped<IPageRepositoryAsync, PageRepositoryAsync>();
            services.AddScoped<IPageService, PageService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPageBackgroundService, PageBackgroundService>();
            services.AddScoped<IWebsiteService, WebsiteService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IHtmlDifferenceService, HtmlDifferenceService>();
            services.AddScoped<IFacebookService, FacebookService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IDifferenceServicesFactory, DifferenceServicesFactory>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Configuration["Jwt:Issuer"],
                        ValidAudience = Configuration["Jwt:Issuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                    };
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1"));

            app.UseHangfireServer();
            app.UseHangfireDashboard();
            

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin().AllowCredentials());

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
