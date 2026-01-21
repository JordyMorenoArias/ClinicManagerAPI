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
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });

var permittedOrigins = builder.Configuration.GetValue<string>("PermittedOrigins")!.Split(";");

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

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.UseMiddleware<ErrorHandlerMiddleware>();

app.UseCors();

app.Run();