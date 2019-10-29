using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AutoMapper;
using WeightTracker.Api.Repositories;
using WeightTracker.Api.Models;
using WeightTracker.Api.Migrations;
using System.IdentityModel.Tokens.Jwt;
using WeightTracker.Api.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using WeightTracker.Api.Services;

namespace WeightTracker.Api
{
    public class Startup
    {
        public IConfiguration Config { get; }

        public Startup(IConfiguration Config)
        {
            this.Config = Config;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // JWT Service
            var jwtService = new JwtService(new JwtSecurityTokenHandler(), Config);
            services.AddSingleton(jwtService);

            // JWT Authentication
            services.AddAuthentication(a =>
            {
                a.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                a.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(j =>
            {
                j.RequireHttpsMetadata = false;
                j.SaveToken = true;
                j.TokenValidationParameters = jwtService.GetTokenValidationParameters();
            });

            /*
            services.AddAuthentication(a =>
            {
                a.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                a.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(j =>
            {
                j.RequireHttpsMetadata = false;
                j.SaveToken = true;
                /*
                j.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(jwtKeyInBytes),
                    ValidateIssuer = false,
                    ValidateAudience = false
                }; */
                /*
                j.Audience = Config.GetValue<string>("Jwt:Issuer");
                j.ClaimsIssuer = Config.GetValue<string>("Jwt:Issuer");
                j.TokenValidationParameters = jwtHelper.GetTokenValidationParameters();

                j.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Query["token"];
                        /*
                        // If the request is for our hub...
                        var path = context.HttpContext.Request.Path;
                        if (!string.IsNullOrEmpty(accessToken) &&
                            (path.StartsWithSegments("/hubs/chat")))
                        {
                            // Read the token out of the query string
                            context.Token = accessToken;
                        } */
                        /*
                        return Task.CompletedTask;
                    }
                }; 
            });*/

            // AutoMapper
            var mappingConfig = new MapperConfiguration(mappingConfig =>
            {
                mappingConfig.AddProfile(new MappingProfile());
            });
            services.AddSingleton(mappingConfig.CreateMapper());

            // Dependencies
            services.AddSingleton<IConfiguration>(Config);
            services.AddDbContext<DataContext>();
            //services.AddAutoMapper(typeof(Startup));

            // Converters
            var unitConverter = new UnitConverter();
            services.AddSingleton(unitConverter);
            services.AddSingleton(new UserUnitConverter(unitConverter));

            // Repositories
            services.AddScoped<Repository<UnitModel>, UnitRepository>();
            services.AddScoped<Repository<UserModel>, UserRepository>();
            services.AddScoped<Repository<WeighInModel>, WeighInRepository>();

            services.AddControllers();
            services.AddMvc().AddNewtonsoftJson();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseCors(c => c.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
