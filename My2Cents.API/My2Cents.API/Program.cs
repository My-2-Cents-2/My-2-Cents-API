using My2Cents.DataInfrastructure;
using Microsoft.EntityFrameworkCore;
using My2Cents.DatabaseManagement;
using My2Cents.DatabaseManagement.Interfaces;
using My2Cents.DatabaseManagement.Implements;
using My2Cents.Logic.Implements;
using My2Cents.Logic.Interfaces;

var builder = WebApplication.CreateBuilder(args);

string connectionString = builder.Configuration.GetConnectionString("connectionString");

Console.WriteLine(builder.Configuration.GetSection("Version").GetSection("Number").Value);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<My2CentsContext>(options =>
{
    // logging to console is on by default
    options.UseSqlServer(connectionString, b => b.MigrationsAssembly("My2Cents.API"));
});


builder.Services.AddScoped<IRepository, EfRepository>();
builder.Services.AddScoped<IStockPortfolioManagementDL, StockPortfolioManagementDL>();

builder.Services.AddScoped<IStockPortfolioManagementBL, StockPortfolioManagementBL>();


builder.Services.AddCors(options =>
{
    // here you put all the origins that websites making requests to this API via JS are hosted at
    options.AddDefaultPolicy(builder =>
        builder
            .WithOrigins("http://localhost:4200", "https://my2centsui.azurewebsites.net/")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials());
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.MapControllers();


app.Run();
