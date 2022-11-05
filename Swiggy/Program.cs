using DevExpress.Xpo;
using FluentAssertions.Common;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Swiggy.Core.IRepository;
using Swiggy.Core.Repository;
using Swiggy.Data;
using Microsoft.AspNetCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<SwiggyDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("SwiggyConnectionString")));

//builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IUserRepository ,UserRepository>();
builder.Services.AddScoped<OrderRepository>();
//builder.Services.AddSingleton<IUserRepository, UserRepository>();

//builder.Services.AddDefaultIdentity<UserRepository>()
//                .AddRoles<IdentityRole>()
//                .AddEntityFrameworkStores<SwiggyDbContext>();

builder.Services.AddScoped<ProductRepository>(sp => {
    // Build your Context Options
    DbContextOptionsBuilder<SwiggyDbContext> optsBuilder = new DbContextOptionsBuilder<SwiggyDbContext>();
    optsBuilder.UseSqlServer(builder.Configuration.GetConnectionString(("SwiggyConnectionString")));
    // Build your context (using the options from the builder)
    SwiggyDbContext ctx = new SwiggyDbContext(optsBuilder.Options);
    // Build your unit of work (and pass in the context)
    // Build your service (and pass in the unit of work)
    ProductRepository svc = new ProductRepository(ctx);
    // Return your Svc
    return svc;
});

//builder.Services.AddScoped<UserRepository>(sp1 =>
//{
//    DbContextOptionsBuilder<SwiggyDbContext> optsBuild = new DbContextOptionsBuilder<SwiggyDbContext>();
//    optsBuild.UseSqlServer(builder.Configuration.GetConnectionString(("SwiggyConnectionString")));
//    SwiggyDbContext cox = new SwiggyDbContext(optsBuild.Options);
//    UserRepository svc = new UserRepository(cox);
//    return svc;
//});


builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.ExpireTimeSpan = new TimeSpan(0, 10, 0);
        options.Events.OnRedirectToLogin = (context) =>
        {
            context.Response.StatusCode = 401;
            return Task.CompletedTask;
        };
    }
    );





var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
