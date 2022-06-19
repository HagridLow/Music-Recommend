using System.Text;
using API;
using API.Contexts;
using API.Entities;
using API.Helpers;
using API.Services;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
IConfiguration configuration = builder.Configuration;
IServiceCollection collections = builder.Services;

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<IdentityContext>(options =>{
    options.UseSqlite(builder.Configuration.GetConnectionString("IdentityConnection"));
});
builder.Services.AddDbContext<DataContext>(options =>{
    options.UseSqlite(builder.Configuration.GetConnectionString("DataConnection"));
});
builder.Services.AddIdentityCore<AppUser>(opt =>
{
    opt.Password.RequireNonAlphanumeric = false;
    opt.Password.RequiredLength = 10;
    opt.Password.RequireUppercase = true;
})
.AddEntityFrameworkStores<IdentityContext>()
.AddSignInManager<SignInManager<AppUser>>();

var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("bQeThWmZq3t6w9z$"));
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opt =>
    {
        opt.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = key,
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });
builder.Services.AddScoped<TokenService>();
builder.Services.AddScoped<SearchHelper>();




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

using (var scope = app.Services.CreateScope())
{
    var loggerFactory = scope.ServiceProvider.GetRequiredService<ILoggerFactory>();
    var dataContext = scope.ServiceProvider.GetRequiredService<DataContext>();
    await DataContextSeed.SeedAsync(dataContext, loggerFactory);
}

app.UseAuthorization();

app.MapControllers();

app.Run();
