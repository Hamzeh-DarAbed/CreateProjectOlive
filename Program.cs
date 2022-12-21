using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text;

using CreateProjectOlive.Models;
using CreateProjectOlive.UnitOfWorks;
// using CreateProjectOlive.SeedMiddleware;
using CreateProjectOlive.Context;
using CreateProjectOlive.SeedMiddleware;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddDbContext<EF_DbContext>(
        o => o.UseNpgsql(builder.Configuration.GetConnectionString("Ef_Postgres_Db"))
    );


builder.Services.AddIdentity<User, IdentityRole>(option =>
{

    option.Password = new PasswordOptions
    {
        RequiredLength = 8,
        RequireDigit = true,
        RequireLowercase = true,
        RequireUppercase = true,
        RequireNonAlphanumeric = false
    };

    option.User = new UserOptions
    {
        RequireUniqueEmail = true,
    };


}).AddEntityFrameworkStores<EF_DbContext>();



builder.Services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidAudience = builder.Configuration["Jwt:Audience"],
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),

    };
});

builder.Services.AddAuthorization();
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// //Authentication & Authorization
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

// await app.UseSeedMiddleware();

app.MapControllers();

app.Run();
