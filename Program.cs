using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;
using System.Text;

using CreateProjectOlive.Models;
using CreateProjectOlive.Services;
using CreateProjectOlive.UnitOfWork;
using CreateProjectOlive.Middleware;

var builder = WebApplication.CreateBuilder(args);

var mongoDbSettings = builder.Configuration.GetSection("MongoDb").Get<DataBaseConfig>();

// Add services to the container.
builder.Services.AddSingleton(typeof(IUnitOfWork), typeof(UnitOfWork));
builder.Services.AddSingleton(typeof(IProjectService), typeof(ProjectService));

builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(option =>
{
    option.Password = new PasswordOptions
    {
        RequiredLength = 8,
        RequireDigit = true,
        RequireLowercase = true,
        RequireUppercase = true,
        RequireNonAlphanumeric = false
    };
}).AddMongoDbStores<ApplicationUser, ApplicationRole, Guid>(
            mongoDbSettings.ConnectionString, mongoDbSettings.Database
        );


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();


builder.Services.Configure<DataBaseConfig>(builder.Configuration.GetSection("MongoDb"));
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


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

// CreateProjectOlive.Helper.SeedToDBHelper.SeedToDataToDb();

app.UseCustomMiddleware();

app.Run();

public partial class Program { }
