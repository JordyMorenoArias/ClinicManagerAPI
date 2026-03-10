using ClinicManagerAPI.Authorization.Handlers;
using ClinicManagerAPI.Authorization.Requirements;
using ClinicManagerAPI.AutoMapper;
using ClinicManagerAPI.Constants;
using ClinicManagerAPI.Data;
using ClinicManagerAPI.Middlewares;
using ClinicManagerAPI.Models.Entities;
using ClinicManagerAPI.Repositories;
using ClinicManagerAPI.Repositories.Interfaces;
using ClinicManagerAPI.Services.Allergy;
using ClinicManagerAPI.Services.Allergy.Interfaces;
using ClinicManagerAPI.Services.Appointment;
using ClinicManagerAPI.Services.Appointment.Interfaces;
using ClinicManagerAPI.Services.Auth;
using ClinicManagerAPI.Services.Auth.Interfaces;
using ClinicManagerAPI.Services.DoctorProfile;
using ClinicManagerAPI.Services.DoctorProfile.Interfaces;
using ClinicManagerAPI.Services.MedicalRecord;
using ClinicManagerAPI.Services.MedicalRecord.Interfaces;
using ClinicManagerAPI.Services.Patient;
using ClinicManagerAPI.Services.Patient.Interfaces;
using ClinicManagerAPI.Services.PatientAllergy;
using ClinicManagerAPI.Services.PatientAllergy.Interfaces;
using ClinicManagerAPI.Services.Report;
using ClinicManagerAPI.Services.Report.Interfaces;
using ClinicManagerAPI.Services.Security;
using ClinicManagerAPI.Services.Security.Interfaces;
using ClinicManagerAPI.Services.User;
using ClinicManagerAPI.Services.User.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Configure DbContext with SQL Server
builder.Services.AddDbContext<ClinicManagerContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.")));

// Configure AutoMapper
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<AutoMapping>();
});

// Repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IPatientRepository, PatientRepository>();
builder.Services.AddScoped<IMedicalRecordRepository, MedicalRecordRepository>();
builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();
builder.Services.AddScoped<IDoctorProfileRepository, DoctorProfileRepository>();
builder.Services.AddScoped<IAllergyRepository, AllergyRepository>();
builder.Services.AddScoped<IPatientAllergyRepository, PatientAllergyRepository>();

// Services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IPatientService, PatientService>();
builder.Services.AddScoped<IMedicalRecordService, MedicalRecordService>();
builder.Services.AddScoped<IAppointmentService, AppointmentService>();
builder.Services.AddScoped<IReportService, ReportService>();
builder.Services.AddScoped<IDoctorProfileService, DoctorProfileService>();
builder.Services.AddScoped<IAllergyService, AllergyService>();
builder.Services.AddScoped<IPatientAllergyService, PatientAllergyService>();

// Infrastructure Services
builder.Services.AddSingleton<IJwtService, JwtService>();
builder.Services.AddScoped<IPasswordHasher<UserEntity>, PasswordHasher<UserEntity>>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Enter your JWT in the format: Bearer {token}"
    });

    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

// JWT Authentication
var jwtKey = builder.Configuration["JWT:KEY"];

if (string.IsNullOrEmpty(jwtKey))
{
    throw new Exception("JWT:KEY is not configured");
}

var jwtIssuer = builder.Configuration["JWT:ISSUER"];

if (string.IsNullOrEmpty(jwtIssuer))
{
    throw new Exception("JWT:ISSUER is not configured");
}

var jwtAudience = builder.Configuration["JWT:AUDIENCE"];

if (string.IsNullOrEmpty(jwtAudience))
{
    throw new Exception("JWT:AUDIENCE is not configured");
}

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        ValidIssuer = jwtIssuer,
        ValidAudience = jwtAudience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
    };

    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            // Retrieve token from cookie
            if (context.Request.Cookies.ContainsKey("access_token"))
            {
                context.Token = context.Request.Cookies["access_token"];
            }
            return Task.CompletedTask;
        },
        OnAuthenticationFailed = context =>
        {
            var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<Program>>();
            logger.LogError($"Authentication failed: {context.Exception.Message}");
            return Task.CompletedTask;
        },
        OnTokenValidated = context =>
        {
            var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<Program>>();
            logger.LogInformation("Token validated successfully.");
            return Task.CompletedTask;
        }
    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("canManageAllergies", policy =>
    {
        policy.RequireRole(Roles.AdminAndDoctor);
    });

    options.AddPolicy("canManageAppointments", policy =>
    {
        policy.RequireRole(Roles.AdminDoctorAndAssistant);
    });

    options.AddPolicy("canManageDoctorProfiles", policy =>
    {
        policy.RequireRole(Roles.Admin);
    });

    options.AddPolicy("canManageMedicalRecord", policy =>
    {
        policy.RequireRole(Roles.Doctor);
    });

    options.AddPolicy("canManagePatientAllergies", policy =>
    {
        policy.RequireRole(Roles.Doctor);
    });

    options.AddPolicy("canManagePatients", policy =>
    {
        policy.RequireRole(Roles.AdminDoctorAndAssistant);
    });

    options.AddPolicy("canManageReports", policy =>
    {
        policy.RequireRole(Roles.Admin);
    });

    options.AddPolicy("canUpdateUser", policy =>
    {
        policy.Requirements.Add(new UpdateUserRequirement());
    });

});

builder.Services.AddScoped<IAuthorizationHandler, UpdateUserHandler>();
builder.Services.AddHttpContextAccessor();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });

var permittedOrigins = builder.Configuration
    .GetValue<string>("PermittedOrigins")!
    .Split(";", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);


builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins(permittedOrigins)
               .AllowAnyHeader()
               .AllowAnyMethod()
               .AllowCredentials();
    });
});

builder.Services.AddProblemDetails(options =>
{
    options.CustomizeProblemDetails = context =>
    {
        var exception = context.HttpContext.Features
            .Get<IExceptionHandlerFeature>()?.Error;

        if (exception == null)
            return;

        var problem = context.ProblemDetails;

        problem.Extensions["traceId"] = context.HttpContext.TraceIdentifier;
        problem.Extensions["timestamp"] = DateTime.UtcNow;

        switch (exception)
        {
            case KeyNotFoundException:
                problem.Status = StatusCodes.Status404NotFound;
                problem.Title = "Resource not found";
                problem.Type = "https://httpstatuses.com/404";
                break;

            case ArgumentException:
                problem.Status = StatusCodes.Status400BadRequest;
                problem.Title = "Invalid request";
                problem.Type = "https://httpstatuses.com/400";
                break;

            case FormatException:
                problem.Status = StatusCodes.Status400BadRequest;
                problem.Title = "Invalid format";
                problem.Type = "https://httpstatuses.com/400";
                break;

            case UnauthorizedAccessException:
                problem.Status = StatusCodes.Status401Unauthorized;
                problem.Title = "Unauthorized";
                problem.Type = "https://httpstatuses.com/401";
                break;

            case InvalidOperationException:
                problem.Status = StatusCodes.Status409Conflict;
                problem.Title = "Invalid operation";
                problem.Type = "https://httpstatuses.com/409";
                break;

            case TimeoutException:
                problem.Status = StatusCodes.Status408RequestTimeout;
                problem.Title = "Request timeout";
                problem.Type = "https://httpstatuses.com/408";
                break;

            case NotImplementedException:
                problem.Status = StatusCodes.Status501NotImplemented;
                problem.Title = "Feature not implemented";
                problem.Type = "https://httpstatuses.com/501";
                break;

            default:
                problem.Status = StatusCodes.Status500InternalServerError;
                problem.Title = "An unexpected error occurred";
                problem.Type = "https://httpstatuses.com/500";
                break;
        }

        problem.Detail = exception.Message;
    };
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ClinicManagerContext>();
    dbContext.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(opciones =>
    {
        opciones.EnablePersistAuthorization();
    });
}

app.UseCors();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

//app.UseMiddleware<ErrorHandlerMiddleware>();

app.UseExceptionHandler();

app.Run();