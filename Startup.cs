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

namespace WeightTracker.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IConfiguration>(Configuration);
            services.AddDbContext<DataContext>();
            //services.AddAutoMapper(typeof(Startup));

            // AutoMapper
            var mappingConfig = new MapperConfiguration(mappingConfig =>
            {
                mappingConfig.AddProfile(new MappingProfile());
            });
            services.AddSingleton(mappingConfig.CreateMapper());

            // JWT Helper
            var jwtHelper = new JwtHelper(new JwtSecurityTokenHandler(), Configuration);
            services.AddSingleton(jwtHelper);

            services.AddScoped<Repository<UnitModel>, UnitRepository>();
            services.AddScoped<Repository<UserModel>, UserRepository>();
            services.AddScoped<Repository<WeighInModel>, WeighInRepository>();

            services.AddControllers();
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
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
