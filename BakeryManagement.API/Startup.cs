using InterviewChallenge.API.Application.Interfaces;
using InterviewChallenge.API.Application.Services;
using InterviewChallenge.API.Common;
using InterviewChallenge.API.Infrastructure.Authentication;
using InterviewChallenge.API.Infrastructure.Data;
using InterviewChallenge.API.Infrastructure.Data.Data.Repository;
using Microsoft.AspNetCore.Authentication;
using Microsoft.OpenApi.Models;
namespace InterviewChallenge.API
{
    internal class Startup
    {
        readonly string AllowSpecificOrigins = "_allowSpecificOrigins";
        public IConfiguration Configuration { get; }
        public IWebHostEnvironment HostingEnvironment { get; }
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            HostingEnvironment = env;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    builder => builder
                        .WithOrigins("https://localhost:7110/") // Replace with your frontend URL
                        .AllowAnyHeader()
                        .AllowAnyMethod());
            });

            services.AddScoped<IApiKeyValidator, ApiKeyValidator>();
            services.AddAuthentication(Constants.ApiKeyAuthentication).AddScheme<AuthenticationSchemeOptions, ApiKeyAuthenticationHandler>(Constants.ApiKeyAuthentication, null);
            services.AddRouting(options =>
            {
                options.LowercaseUrls = true;
            });
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

                // Define the security scheme
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    },
                    Scheme = "oauth2",
                    Name = "Bearer",
                    In = ParameterLocation.Header,
                },
                new List<string>()
            }
        });
            });
            services.AddControllers();
            services.AddSingleton<ILiteDBService, LiteDBService>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IBakeryItemRepository, BakeryItemRepository>();
            services.AddScoped<IBakeryItemService, BakeryItemService>();

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseRouting();

            app.UseCors(AllowSpecificOrigins);

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
