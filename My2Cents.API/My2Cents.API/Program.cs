using My2Cents.DataInfrastructure;
using Microsoft.EntityFrameworkCore;
using My2Cents.DatabaseManagement;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using My2Cents.API.AuthenticationService.Interfaces;
using My2Cents.API.AuthenticationService.Implements;
using My2Cents.API.Middlewares.Implements;

var builder = WebApplication.CreateBuilder(args);

string connectionString = builder.Configuration.GetConnectionString("connectionString");
var key = builder.Configuration["Token:Key"];

Console.WriteLine(builder.Configuration.GetSection("Version").GetSection("Number").Value);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IAccessTokenManager, AccessTokenManager>();
builder.Services.AddDistributedMemoryCache();

//Identity Role
builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(x =>
{
    x.User.RequireUniqueEmail = true;
})
.AddEntityFrameworkStores<My2CentsContext>();

//Authentication
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.ASCII.GetBytes(key)),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

builder.Services.AddDbContext<My2CentsContext>(options =>
{
    // logging to console is on by default
    options.UseSqlServer(connectionString, b => b.MigrationsAssembly("My2Cents.API"));
});

builder.Services.AddScoped<IRepository, EfRepository>();


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

//Apply middlewares
app.UseTokenManagerMiddleware();

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();


app.Run();
