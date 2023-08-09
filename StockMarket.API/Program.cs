using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using StockMarket.Entities.Concrete;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using StockMarket.Business.Concrete;
using StockMarket.Business.Abstract;
using Microsoft.AspNetCore.Identity;
using StockMarket.DataAccess.Abstract;
using StockMarket.DataAccess.Concrete;

using StockMarket.DataAccess.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<Context>(options =>
    options.UseSqlServer("Server=DERBEDEK;Database=StockMarketDB;User Id=DBTest;Password=112233;TrustServerCertificate=True;"));


builder.Services.AddIdentity<AppUser, AppRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 6;
    options.User.RequireUniqueEmail = false;
    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
})
    .AddEntityFrameworkStores<Context>()
    .AddDefaultTokenProviders()
    .AddUserManager<UserManager<AppUser>>() // CustomUserManager yerine UserManager<AppUser> kullanıldı
    .AddRoles<AppRole>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserManager>();
builder.Services.AddScoped<IBalanceRepository, BalanceRepository>();
builder.Services.AddScoped<IBalanceService, BalanceManager>();
builder.Services.AddScoped<IPortfolioRepository, PortfolioRepository>();
builder.Services.AddScoped<IPortfolioService, PortfolioManager>();
builder.Services.AddScoped<ISystemBalanceService, SystemBalanceManager>();     
builder.Services.AddScoped<ISystemBalanceRepository, SystemBalanceRepository>();   


builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API Name", Version = "v1" });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer
    (options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Token:Issuer"],
            ValidAudience = builder.Configuration["Token:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"])),
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policy =>
    {
        policy.RequireRole("Admin");
        policy.RequireClaim("CreateUser");
        policy.RequireClaim("DeleteUser");
        // Diğer yetkileri burada tanımlayabiliriz.
    });
});

builder.Services.AddControllers();

// Configure the HTTP request pipeline.
var app = builder.Build();

using (var scope = app.Services.CreateScope())//admin oluşturduk.
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

if (!await roleManager.RoleExistsAsync("Admin"))
{
    await roleManager.CreateAsync(new IdentityRole("Admin"));
}

var adminUser = new IdentityUser
{
    UserName = "admin@example.com",
    Email = "admin@example.com"
};
await userManager.CreateAsync(adminUser, "Admin123!");
await userManager.AddToRoleAsync(adminUser, "Admin");

}


if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}


app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Your API Name v1");
});
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();


// Admin kullanıcısını oluşturma işlemini burada yapabilirsiniz
