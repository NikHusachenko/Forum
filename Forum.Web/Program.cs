using FluentValidation;
using FluentValidation.AspNetCore;
using Forum.EntityFramework;
using Forum.EntityFramework.Repositories;
using Forum.Services.AuthenticationServices;
using Forum.Services.AuthenticationServices.Models;
using Forum.Services.CurrentUserContext;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var configuration = builder.Configuration;
var services = builder.Services;

services.AddControllersWithViews();

services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.RequireAuthenticatedSignIn = false;
})
    .AddCookie(options =>
    {
        options.AccessDeniedPath = "/Authentication/SignIn";
        options.LogoutPath = "/Home/Index";
        options.LoginPath = "/Home/Index";
        options.ExpireTimeSpan = TimeSpan.FromDays(3);
        options.SlidingExpiration = true;
    });

services.AddFluentValidationAutoValidation()
    .AddFluentValidationClientsideAdapters();

services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
services.AddTransient<IAuthenticationService, AuthenticationService>();
services.AddScoped<ICurrentUserContext, CurrentUserContext>();

services.AddTransient<IValidator<SignInViewModel>, SignInViewModelValidator>();
services.AddTransient<IValidator<SignUpViewModel>, SignUpViewModelValidator>();

var app = builder.Build();

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
