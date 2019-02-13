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
using PageCheckerAPI.Services.Interfaces;
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
                opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddAutoMapper();
            services.AddHangfire(conf =>
            {
                conf.UseSqlServerStorage(Configuration.GetConnectionString("DefaultConnection"));
                // conf.UseSqlServerStorage("Server=(localdb)\\MSSQLLocalDB;Integrated Security=True");
            });

            services.AddScoped<IPageRepositoryAsync, PageRepositoryAsync>();
            services.AddScoped<IPageService, PageService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPageBackgroundService, PageBackgroundService>();
            services.AddScoped<IWebsiteService, WebsiteService>();
            services.AddScoped<IEmailNotificationService, EmailNotificationService>();
            services.AddScoped<IHtmlDifferenceService, HtmlDifferenceService>();
            services.AddScoped<IFacebookService, FacebookService>();
            services.AddScoped<ITokenService, TokenService>();

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

            app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin().AllowCredentials());
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
