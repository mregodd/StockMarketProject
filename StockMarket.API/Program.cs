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
using System.Security.Claims;
using System.Text.Json.Serialization;
using StockMarket.API.BackgroundServices;

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
    .AddUserManager<UserManager<AppUser>>() // CustomUserManager yerine UserManager<AppUser> kullanýldý
    .AddRoles<AppRole>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserManager>();
builder.Services.AddScoped<IBalanceRepository, BalanceRepository>();
builder.Services.AddScoped<IBalanceService, BalanceManager>();
builder.Services.AddScoped<IPortfolioRepository, PortfolioRepository>();
builder.Services.AddScoped<IPortfolioService, PortfolioManager>();
builder.Services.AddScoped<ISystemBalanceService, SystemBalanceManager>();     
builder.Services.AddScoped<ISystemBalanceRepository, SystemBalanceRepository>();
builder.Services.AddScoped<IStockDataRepository, StockDataRepository>();
builder.Services.AddScoped<IStockDataService, StockDataManager>();
builder.Services.AddScoped<IStockDataFetcher, StockDataFetcher>();

builder.Services.AddHostedService<StockUpdateService>();

builder.Services.AddHttpClient();

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.WriteIndented = true;
});


builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API Name", Version = "v1" });
    c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
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
    options.AddPolicy("AdminOnly", policy =>
    {
        policy.RequireRole("Admin");
        policy.RequireClaim("CreateUser");
        policy.RequireClaim("DeleteUser");
        // Diðer yetkileri burada tanýmlayabiliriz.
    });
});

builder.Services.AddControllers();

// Configure the HTTP request pipeline.
var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();
    var roleManager = serviceProvider.GetRequiredService<RoleManager<AppRole>>();
    var balanceRepository = serviceProvider.GetRequiredService<IBalanceRepository>();
    var portfolioRepository = serviceProvider.GetRequiredService<IPortfolioRepository>();

    var adminRoleExist = roleManager.RoleExistsAsync("Admin").Result;
    if (!adminRoleExist)
    {
        var adminRole = new AppRole
        {
            Name = "Admin"
        };
        var result = roleManager.CreateAsync(adminRole).Result;
    }
    
    
    var roleExists = roleManager.RoleExistsAsync("User").Result;

    if (!roleExists)
    {
        var UserRole = new AppRole
        {
            Name = "User"
        };
        var result = roleManager.CreateAsync(UserRole).Result;
    }

    var adminUser = new AppUser
    {
        UserName = "ADMIN",
        Name = "Admin",
        Surname = "Admin",
        City = "AdminCity",
        District = "AdminDistrict",
    };

    var adminResult = await userManager.CreateAsync(adminUser, "1q2w3e4REE!"); // Admin kullanýcýsýnýn þifresi burada belirleniyor

    if (adminResult.Succeeded)
    {
            if (!await userManager.IsInRoleAsync(adminUser, "Admin"))
            {
                await userManager.AddToRoleAsync(adminUser, "Admin"); // Admin kullanýcýsýna "Admin" rolü atanýyor
            }

            var adminBalance = new UserBalance
            {
                Balance = 10000,
                AppUser = adminUser
            };
            balanceRepository.AddUserBalance(adminBalance); // Admin kullanýcýsýnýn bakiyesi sisteme ekleniyor

            var adminPortfolio = new UserPortfolio
            {
                StockName = "Example Stock",
                Quantity = 500,
                Value = 5000,
                AppUser = adminUser
            };
            portfolioRepository.AddPortfolio(adminPortfolio); // Admin kullanýcýsýnýn portfolyosu sisteme ekleniyor

            // Admin kullanýcýsýnýn sahip olduðu yetkileri eklemek için claimler tanýmlanýr
            var adminClaims = new List<Claim>
            {
                new Claim("CreateUser", "true"),
                new Claim("DeleteUser", "true"),
                new Claim(ClaimTypes.Name, adminUser.UserName)
            };

        await userManager.AddClaimsAsync(adminUser, adminClaims);
    }

}//rol oluþturucumuz

    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Your API Name v1");
    });
    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();

    app.Run();

