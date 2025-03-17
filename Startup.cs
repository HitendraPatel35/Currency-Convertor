using CurrencyConverterAPI.Extensions;
using CurrencyConverterAPI.Middleware;
using CurrencyConverterAPI.Providers;
using CurrencyConverterAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using OpenTelemetry.Trace;
using Serilog;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CurrencyConverterAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; set; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Currency Converter API", Version = "v1" });
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.GetSection("JWT:Key").Value)),
                ClockSkew = TimeSpan.Zero
            }
        );
            services.AddAuthorization();
            //services.AddAuthorization(options =>
            //{
            //    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
            //});
            services.AddLogging(loggingBuilder =>
            loggingBuilder.AddSerilog(dispose: true));

            services.AddOpenTelemetry()
            .WithTracing(builder =>
            {
                builder.AddAspNetCoreInstrumentation()
                       .AddHttpClientInstrumentation()
                       .AddConsoleExporter();
            });

            services.AddScoped<ICurrencyService, CurrencyService>();
            services.AddSingleton<ICurrencyProviderFactory, CurrencyProviderFactory>();
            services.AddSingleton<CurrencyServiceFactory>();

            // Register currency providers
            services.AddTransient<ICurrencyProvider, CurrencyProviderA>();
            services.AddTransient<ICurrencyProvider, CurrencyProviderB>();

            services.AddTransient<ICurrencyRateProvider, CurrencyProviderEUR>();
            services.AddTransient<ICurrencyRateProvider, CurrencyProviderUSD>();

            services.AddTransient<CurrencyProviderA>();
            services.AddTransient<CurrencyProviderB>();

            services.AddTransient<CurrencyProviderEUR>();
            services.AddTransient<CurrencyProviderUSD>();

            services.AddPolicies();
            services.AddDistributedTracing();
            services.AddThrottling();

            services.AddHttpContextAccessor();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseMiddleware<JwtMiddleware>();
            app.UseThrottlingMiddleware(requestLimit: 50, timeWindow: TimeSpan.FromMinutes(1));

            app.Use(async (context, next) =>
            {
                if (context.User.Identity.IsAuthenticated)
                {
                    Log.Information("User is authenticated");
                }
                else
                {
                    Log.Warning("User is not authenticated");
                }

                var clientIp = context.Connection.RemoteIpAddress?.ToString();
                //var clientId = context.User.FindFirst("Email")?.Value;
                var clientId = context.User?.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")?.Value;
                //var clientId = context.User.FindFirst(JwtRegisteredClaimNames.Email)?.Value ?? "Email not available"; // Retrieve the email claim
                var httpMethod = context.Request.Method;
                var endpoint = context.Request.Path;
                var startTime = DateTime.UtcNow;

                try
                {
                    await next.Invoke();
                }
                catch (Exception ex)
                {
                    //Log.Error(ex, "An unhandled exception occurred while processing the request.");
                    //throw; // Re-throw the exception after logging it
                }

                var responseCode = context.Response.StatusCode;
                var responseTime = DateTime.UtcNow - startTime;

                Log.Information("Request details: Client IP: {ClientIp}, Client ID: {ClientId}, HTTP Method: {HttpMethod}, Endpoint: {Endpoint}, Response Code: {ResponseCode}, Response Time: {ResponseTime}ms",
                    clientIp, clientId, httpMethod, endpoint, responseCode, responseTime.TotalMilliseconds);
            });
            
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Currency Converter API V1");
                c.RoutePrefix = string.Empty; // Serve Swagger UI at the app's root
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
    
