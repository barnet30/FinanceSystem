using System.Reflection;
using System.Text;
using Authorization;
using Authorization.Configuration;
using Authorization.Interfaces;
using FinanceSystem.Data.Extensions;
using FinanceSystem.Extensions;
using FinanceSystem.Filters;
using FinanceSystem.Services.MapperProfiles;
using FinanceSystem.Services.Resolver;
using FinanceSystem.Validation.Payments;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.Configure<AuthOptions>(builder.Configuration.GetSection(nameof(AuthOptions)));

builder.Services.RegisterRepositories();
builder.Services.RegisterServices(builder.Configuration.GetValue<string>("SearchGrpcService"));

builder.Services
    .AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.Converters.Add(new StringEnumConverter());
    });

builder.Services
    .AddScoped<IAuthManager, AuthManager>()
    .AddDataAccess(builder.Configuration.GetConnectionString("DatabaseConnectionString"))
    .AddScoped(typeof(ValidationActionFilter<>));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
    {
        options.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "Finance system API",
            Version = "v1"
        });
        
        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

        options.IncludeXmlComments(xmlPath);
        options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "FinanceSystem.Abstractions.xml"));

        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Description = "Insert Bearer jwt token",
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer",
            BearerFormat = "JWT"
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
                new string[] { }
            }
        });
    })
    .AddAutoMapper(
        typeof(UserProfile).Assembly,
        typeof(ReferenceItemMapResolver<>).Assembly)
    .AddValidatorsFromAssemblyContaining<PaymentPostDtoValidator>()
    .AddSwaggerGenNewtonsoftSupport()
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        var jwtOptions = builder.Configuration.GetSection(nameof(AuthOptions)).Get<AuthOptions>();
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidIssuer = jwtOptions.Issuer,
            ValidAudience = jwtOptions.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Key)),
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = false,
            ValidateIssuerSigningKey = true
        };
    });

var app = builder.Build();

MigrationToolExtension.Execute(app.Services);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection()
    .UseAuthentication()
    .UseAuthorization()
    .UseExceptionHandler(b => b.Run(async httpContext => await HandleError(httpContext, app.Environment)));

app.MapControllers();

app.Run();

Task HandleError(HttpContext httpContext, IHostEnvironment env)
{
    var exceptionHandlerPathFeature = httpContext.Features.Get<IExceptionHandlerPathFeature>();

    var exception = exceptionHandlerPathFeature?.Error;

    var response = exception.ToProblemDetail(env);

    httpContext.Response.StatusCode = response.Status ?? 500;
    return httpContext.Response.WriteAsJsonAsync(response);
}