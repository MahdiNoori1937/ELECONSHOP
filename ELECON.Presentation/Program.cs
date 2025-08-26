using ELECON.Application.Feature.User.Validators;
using Elecon.Infrastructure.Context;
using ELECON.IOC;
using EleconShop.Domain.Dtos;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.IOC();
// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ShopContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ELECON_SHOPConnectionStrings"));
});
builder.Services.AddMediatR(option =>
{
    option.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
});
builder.Services.AddValidatorsFromAssemblyContaining<UserRegisterValidator>();
builder.Services.Configure<MeliPayamak>(builder.Configuration.GetSection("MeliPayamak"));

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(option =>
{
    option.LoginPath = "/Login/PasswordLogin";
    option.LogoutPath = "login/Logout";
    option.ExpireTimeSpan = TimeSpan.FromDays(7);
    option.AccessDeniedPath = "/Login/AccessDenied";
});



WebApplication app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Home}/{id?}");

app.Run();