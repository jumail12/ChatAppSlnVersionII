
using ChatAppSlnVersionII.Application.ConfigService;
using ChatAppSlnVersionII.Infrastructure.ConfigService;
using ChatAppSlnVersionII.Infrastructure.SignalR.Hubs;
using ChatAppSlnVersionII.Middlewares;
using ChatAppSlnVersionII.Shared.ApiResponses;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Text.Json;
namespace ChatAppSlnVersionII
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "ChatApp API", Version = "v1" });
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,   // ✅ Important
                    Scheme = "bearer",                // ✅ lowercase
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Paste your JWT token only (without typing Bearer)"
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
            });

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                  .AddJwtBearer(options =>
                  {
                      var jwtKey = builder.Configuration["JwtSettings:Key"];
                      var issuer = builder.Configuration["JwtSettings:Issuer"];
                      var audience = builder.Configuration["JwtSettings:Audience"];

                      if (string.IsNullOrEmpty(jwtKey))
                          throw new Exception("JWT Key is missing in configuration.");

                      options.TokenValidationParameters = new TokenValidationParameters
                      {
                          ValidateIssuerSigningKey = true,
                          IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),

                          ValidateIssuer = false,      // ✅ turn off
                          ValidateAudience = false,    // ✅ turn off

                          ValidateLifetime = true,
                          ClockSkew = TimeSpan.Zero
                      };

                      options.Events = new JwtBearerEvents
                      {
                          OnChallenge = context =>
                          {
                              context.HandleResponse();

                              context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                              context.Response.ContentType = "application/json";

                              var result = new 
                              {
                                  resultType = ResultType.UnAuthorized,
                                  message = "Unauthorized",
                                  error = "Token is missing or invalid",
                              };

                              return context.Response.WriteAsync(
                                  JsonSerializer.Serialize(result)
                              );
                          },

                          OnForbidden = context =>
                          {
                              context.Response.StatusCode = StatusCodes.Status403Forbidden;
                              context.Response.ContentType = "application/json";

                              var result = new 
                              {
                                  resultType = ResultType.Forbidden,
                                  message = "Access denied",
                                  error = "You do not have permission to access this resource",
                              };

                              return context.Response.WriteAsync(
                                  JsonSerializer.Serialize(result)
                              );
                          }
                      };
                  });

            var corsOrigins = builder.Configuration.GetSection("CorsOrigins").Get<string[]>();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.WithOrigins(corsOrigins!)
                          .AllowAnyHeader()
                          .AllowAnyMethod()
                           .AllowCredentials(); // 🔥 must;
                });
            });

            builder.Services.AddInfrastructure(builder.Configuration);
            builder.Services.ApplicationRegisterService(builder.Configuration);

            var app = builder.Build();
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseCors("AllowAll");
            app.UseHttpsRedirection();
            app.UseMiddleware<ExceptionMiddleware>();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseMiddleware<AuthUserMiddleware>();
            app.MapHub<ChatHub>("/chatHub");
            app.MapControllers();
            app.Run();
        }
    }
}
