using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.IdentityModel.Tokens.Jwt;
using WeightTracker.Api.Repositories;
using WeightTracker.Api.Models;
using WeightTracker.Api.Migrations;
using WeightTracker.Api.Helpers;
using WeightTracker.Api.Services;
using System;
using System.Reflection;
using System.IO;
using System.Collections.Generic;
using Microsoft.OpenApi.Models;

namespace WeightTracker.Api
{
    #pragma warning disable CS1591
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

            // AutoMapper
            var mappingConfig = new MapperConfiguration(mappingConfig =>
            {
                mappingConfig.AddProfile(new MappingProfile());
            });
            services.AddSingleton(mappingConfig.CreateMapper());

            // Swagger
            services.AddSwaggerGen(setupAction =>
            {
                // Setup document
                setupAction.SwaggerDoc(
                    "WeightTrackerAPI",
                    new Microsoft.OpenApi.Models.OpenApiInfo()
                    {
                        Title = "Weight Tracker API",
                        Description = "Through this API you can access authors and their books.",
                        Contact = new Microsoft.OpenApi.Models.OpenApiContact()
                        {
                            Email = "harmeshuppal@gmail.com",
                            Name = "Mesh Uppal",
                            Url = new Uri("https://www.github.com/meshu-dev")
                        },
                        License = new Microsoft.OpenApi.Models.OpenApiLicense()
                        {
                            Name = "MIT License",
                            Url = new Uri("https://opensource.org/licenses/MIT")
                        }
                    });
                
                setupAction.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                    {
                        Description = "JWT Authorization header using the Bearer scheme.",
                        Type = SecuritySchemeType.Http, //We set the scheme type to http since we're using bearer authentication
                        Scheme = "bearer" //The name of the HTTP Authorization scheme to be used in the Authorization header. In this case "bearer".
                    }
                );
                
                setupAction.AddSecurityRequirement(new OpenApiSecurityRequirement{
                    {
                        new OpenApiSecurityScheme{
                            Reference = new OpenApiReference{
                                Id = "Bearer", //The name of the previously defined security scheme.
                                Type = ReferenceType.SecurityScheme
                            }
                        },
                        new List<string>()
                    }
                });

                /*
                // Adds authorize button to Swagger UI Docs
                setupAction.AddSecurityDefinition("basicAuth", new OpenApiSecurityScheme()
                {
                    Type = SecuritySchemeType.Http,
                    Scheme = "basic",
                    Description = "Input your username and password to access this API"
                });

                // Add locks to each endpoint in Swagger UI
                setupAction.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference {
                                Type = ReferenceType.SecurityScheme,
                                Id = "basicAuth" }
                        }, new List<string>() }
                });

                setupAction.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = "header",
                    Type = "apiKey"
                }); */

                /*
                // Adds authorize button to Swagger UI Docs
                setupAction.AddSecurityDefinition("basicAuth", new OpenApiSecurityScheme()
                {
                    Type = SecuritySchemeType.Http,
                    Scheme = "basic",
                    Description = "Input your username and password to access this API"
                });

                // Add locks to each endpoint in Swagger UI
                setupAction.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference {
                                Type = ReferenceType.SecurityScheme,
                                Id = "basicAuth" }
                        }, new List<string>() }
                });

                // Run actions dependant on API Version selected in top right dropdown on Swagger UI
                /*
                setupAction.DocInclusionPredicate((documentName, apiDescription) =>
                {
                    //var actionApiVersionModel = apiDescription.ActionDescriptor
                    //.GetApiVersionModel(ApiVersionMapping.Explicit | ApiVersionMapping.Implicit);

                    if (actionApiVersionModel == null)
                    {
                        return true;
                    }

                    if (actionApiVersionModel.DeclaredApiVersions.Any())
                    {
                        return actionApiVersionModel.DeclaredApiVersions.Any(v =>
                        $"LibraryOpenAPISpecificationv{v.ToString()}" == documentName);
                    }
                    return actionApiVersionModel.ImplementedApiVersions.Any(v =>
                        $"LibraryOpenAPISpecificationv{v.ToString()}" == documentName);
                }); */

                // Required to include method XML comments in Swagger UI
                var xmlCommentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentsFile);

                setupAction.IncludeXmlComments(xmlCommentsFullPath);
            });

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

            app.UseSwagger();
            app.UseSwaggerUI(s =>
            {
                s.SwaggerEndpoint(
                    "/swagger/WeightTrackerAPI/swagger.json",
                    "Weight Tracker API"
                );
                s.RoutePrefix = "swagger";
            });

            app.UseCors(c => c.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
        #pragma warning restore CS1591
    }
}
