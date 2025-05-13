using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;
using WebAPITesting.Config;
using WebAPITesting.Data;
using WebAPITesting.IRepository;
using WebAPITesting.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("HotelsDbConnectionString");
builder.Services.AddDbContext<HotelsDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle


builder.Services.AddIdentityCore<UserAccount>()
    .AddRoles<IdentityRole>()
    .AddTokenProvider<DataProtectorTokenProvider<UserAccount>>("HotelsAPI")
    .AddEntityFrameworkStores<HotelsDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.IncludeErrorDetails = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        
        ValidateIssuerSigningKey = true,
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero,
        ValidIssuer = builder.Configuration["JwSettings:Issuer"],
        ValidAudience = builder.Configuration["JwSettings:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwSettings:Key"]))
        
    };
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        b => b.AllowAnyHeader()
        .AllowAnyOrigin()
        .AllowAnyMethod());
});

builder.Host.UseSerilog((context, config) => 
{ 
    config.WriteTo.Console().ReadFrom.Configuration(context.Configuration);
});

builder.Services.AddAutoMapper(config => 
{
    config.AddProfile<MapperConfig>();
});

builder.Services.AddScoped<ICountriesRepository, CountriesRepositoryImpl>();
builder.Services.AddScoped<IHotelsRepository,  HotelsRepositoryImpl>();
builder.Services.AddScoped<IUserAuthentication, UserAuthenticationImpl>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
