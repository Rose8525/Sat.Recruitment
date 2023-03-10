using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sat.Recruitment.Api.Middleware;
using Sat.Recruitment.Application.Interfaces;
using Sat.Recruitment.Application.Mapper;
using Sat.Recruitment.Application.MoneyCalculator;
using Sat.Recruitment.Application.Services;
using Sat.Recruitment.Application.Validators;
using Sat.Recruitment.Infraestructure.Persistence.EF;
using Sat.Recruitment.Infraestructure.Persistence.File;
using System.IO;
using Microsoft.AspNetCore.Mvc.Versioning;
using Newtonsoft.Json.Serialization;

namespace Sat.Recruitment.Api
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
            services.AddControllers().AddNewtonsoftJson(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver()); 
            services.AddSwaggerGen();

            services.AddApiVersioning(opt =>
            {
                opt.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(0, 0);
                opt.AssumeDefaultVersionWhenUnspecified = true;
                opt.ReportApiVersions = true;
                opt.ApiVersionReader = new UrlSegmentApiVersionReader();
            });

            services.AddValidatorsFromAssemblyContaining<UserValidator>();
            services.AddScoped<IUserService, UserService>();
            services.AddSingleton<IMoneyCalculatorFactory, MoneyCalculatorFactory>();

            var conn = new SqliteConnection(Configuration.GetConnectionString("UserDatabase"));
            services.AddDbContext<UserDbContext>(options => 
                options.UseSqlite(conn));
            
            services.AddScoped<IUserRepository>(sp =>
            {
                if (Configuration["PersistenceMode"] == "file")
                {
                    return new FileRepository(Path.Combine(Directory.GetCurrentDirectory(), "Files", Configuration.GetConnectionString("UserFile")));
                }
                else
                {
                    return new DbRepository(sp.GetRequiredService<UserDbContext>());
                }
            });

            // Auto Mapper Configurations
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //SQlite 
                var serviceScope = app.ApplicationServices.CreateScope();
                serviceScope.ServiceProvider.GetRequiredService<UserDbContext>().Database.EnsureCreated();
            }
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
            app.UseMiddleware<ErrorHandlerMiddleware>();
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
