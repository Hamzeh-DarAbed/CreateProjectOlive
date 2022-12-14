using CreateProjectOlive.Models;
using CreateProjectOlive.Services;
using CreateProjectOlive.UnitOfWork;

var builder = WebApplication.CreateBuilder(args);

var mongoDbSettings = builder.Configuration.GetSection("MongoDb").Get<DataBaseConfig>();

// Add services to the container.
builder.Services.AddSingleton(typeof(IUnitOfWork), typeof(UnitOfWork));
builder.Services.AddSingleton(typeof(IProjectService), typeof(ProjectService));

builder.Services.AddIdentity<ApplicationUser, ApplicationRole>()
        .AddMongoDbStores<ApplicationUser, ApplicationRole, Guid>(
            mongoDbSettings.ConnectionString, mongoDbSettings.Database
        );

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();


builder.Services.Configure<DataBaseConfig>(builder.Configuration.GetSection("MongoDb"));
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }
