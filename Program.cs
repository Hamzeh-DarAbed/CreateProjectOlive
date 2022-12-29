using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text;
using CreateProjectOlive.Models;
using CreateProjectOlive.UnitOfWorks;
// using CreateProjectOlive.SeedMiddleware;
using CreateProjectOlive.Context;
using CreateProjectOlive.Services;
using AutoMapper;
using CreateProjectOlive.Mapping;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddDbContext<EF_DbContext>(options =>
     options.UseLazyLoadingProxies().UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));




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
        builder.Services.AddScoped(typeof(IService<>), typeof(Service<>));

        builder.Services.AddAutoMapper(typeof(ProjectProfile));

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSingleton(typeof(IService<>), typeof(Service<>));

        builder.Services.AddSwaggerGen();
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
    }
}