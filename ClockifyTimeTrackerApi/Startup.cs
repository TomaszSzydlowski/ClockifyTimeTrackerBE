using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AutoMapper;
using Microsoft.OpenApi.Models;
using System;
using ClockifyTimeTrackerBE.Controllers.Config;
using ClockifyTimeTrackerBE.Domain.Repositories;
using ClockifyTimeTrackerBE.Domain.Services;
using ClockifyTimeTrackerBE.Persistence.Contexts;
using ClockifyTimeTrackerBE.Persistence.Repository;
using ClockifyTimeTrackerBE.Services;
using Microsoft.Extensions.Azure;
using Microsoft.Identity.Web;

namespace ClockifyTimeTrackerBE
{
    public class Startup
    {
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                    builder =>
                    {
                        builder.WithOrigins("https://localhost:3000")
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            });

            services.AddControllers()
                .ConfigureApiBehaviorOptions(option =>
                {
                    option.InvalidModelStateResponseFactory = InvalidModelStateResponseFactory.ProduceErrorResponse;
                });

            services.AddMicrosoftIdentityWebApiAuthentication(Configuration);

            services.AddMvc();
            services.Configure<Settings>(options =>
            {
                options.ConnectionString
                    = Configuration.GetConnectionString("DefaultConnection");
                options.Database
                    = Configuration.GetSection("MongoSettings:Database").Value;
            });
            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "ClockifyTimeTrackerAPI API",
                    Version = "v1",
                    Contact = new OpenApiContact
                    {
                        Name = "Tomasz Szydlowski",
                        Url = new Uri("https://github.com/TomaszSzydlowski/ClockifyTimeTrackerBE"),
                        Email = "Tomasz.Piotr.Szydlowski@gmail.com"
                    },
                    License = new OpenApiLicense
                    {
                        Name = "MIT"
                    }
                });
            });
            services.AddAzureClients(builder =>
            {
                builder.AddBlobServiceClient(Configuration.GetSection("Storage:ConnectionString").Value);
            });
            services.AddAutoMapper(typeof(Startup));
            services.AddScoped<IAppDbContext, AppDbContext>();
            services.AddTransient<IBlobService, BlobService>();
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<IReceiptRepository, ReceiptRepository>();
            services.AddScoped<IReceiptService, ReceiptService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseSwagger();

            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Students Demo Template"); });

            app.UseRouting();
            app.UseCors(MyAllowSpecificOrigins);

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}