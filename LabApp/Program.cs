using LabApp.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using LabApp.Services;
using LabApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using LabApp.Middleware;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.OpenApi.Any;

namespace LabApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase("LabDb"));

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddGrpc();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Wpisz 'Bearer <token>' bez cudzys³owu",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
                options.OperationFilter<AddClientHeaderFilter>();
            });
            builder.Services.AddScoped<IPatientService, PatientService>();
            builder.Services.AddScoped<IOrderService, OrderService>();
            builder.Services.AddScoped<IExaminationService, ExaminationService>();
            builder.Services.AddScoped<IResultService, ResultService>();
            builder.Services.AddScoped<IDiagnosticianService, DiagnosticianService>();
            builder.Services.AddScoped<IDeviceService, DeviceService>();
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
            var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>();
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidateAudience = true,
                    ValidAudience = jwtSettings.Audience,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey)),
                };
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                       
                        var token = context.Request.Cookies["jwt"];
                        if (!string.IsNullOrEmpty(token))
                        {
                            context.Token = token;
                        }
                        return Task.CompletedTask;
                    }
                };
            });

            var app = builder.Build();
            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                context.Devices.AddRange(
                    new Device { Name = "Mikroskop", Model = "MX-200", SerialNumber = "ABC123", IsOperational = true },
                    new Device { Name = "Wirowka", Model = "CF-500", SerialNumber = "XYZ789", IsOperational = false }
                );

                context.Diagnosticians.AddRange(
                    new Diagnostician { FirstName = "Anna", LastName = "Kowalska", PWZDL = "12345", Email = "akowal@labo.pl", Specialization = "Mikrobiologia" },
                    new Diagnostician { FirstName = "Piotr", LastName = "Nowak", PWZDL = "23456", Email = "pnowak@labo.pl", Specialization = "Chemia kliniczna" }
                    );

                context.SaveChanges();
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
           app.UseHeaderCheck();

            app.UseAuthentication();

            app.UseAuthorization();


            app.MapControllers();

            app.MapGrpcService<GrpcDeviceService>();
            app.MapGrpcService<GrpcDiagnosticianService>();
            app.MapGet("/", () => "gRPC dzia³a. U¿yj klienta gRPC.");


            app.Run();
        }
    }
}