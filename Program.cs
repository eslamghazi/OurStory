// Get WebApplicationBuilder.
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ourStory.Helpers;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Get ConnectionString From appsettings.json.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Use The Created DbContext That Inherit From "IdentityDbContext<ApplicationUser>".
// Require Microsoft.EntityFrameworkCore and Microsoft.EntityFrameworkCore.SqlServer and Microsoft.EntityFrameworkCore.Tools packages.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(connectionString);
});

// Add services to the container.
builder.Services.AddControllers();

// Add Swagger/OpenAPI configuration with JWT support.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Our Story API", Version = "v1" });

    // Add a security definition for the JWT Bearer token
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    // Add a requirement for using the Bearer token in all secured endpoints
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
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

// Add Dependency Injection For Blogs Service.
builder.Services.AddTransient<IBlogs, BlogService>();

// Add Dependency Injection For Lovers Service.
builder.Services.AddTransient<ILovers, LoverService>();

// Add JwtHelper.
builder.Services.AddScoped<JwtHelper>();

// Configure JWT Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

builder.Services.AddAuthorization();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();
builder.Services.AddHttpContextAccessor();

// Add Cors.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyHeader()
               .AllowAnyMethod()
               .AllowAnyOrigin();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Our Story API v1"));
}

// Use Exception Handling Middleware
app.UseExceptionHandler("/error");

app.UseHttpsRedirection();

// UseCors For FrontEnd Requests.
app.UseCors("AllowAll");

// Use routing before authentication and authorization
app.UseRouting();

// Use authentication and authorization middleware
app.UseAuthentication();
app.UseAuthorization();

app.UseStaticFiles();  // Enable serving static files

app.MapControllers();

app.Run();
