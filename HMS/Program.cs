using FluentValidation.AspNetCore;
using HMS.Business.Services.Implementations;
using HMS.Business.Services.Interfaces;
using HMS.Business.Validators;
using HMS.Core.Abstracts;
using HMS.Core.Entities;
using HMS.Data.DAL;
using HMS.Data.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

#region Logger

builder.Host.UseSerilog((hostContext, services, configuration) =>
{
    configuration.MinimumLevel.Override("Microsoft", LogEventLevel.Information)
        .Enrich.FromLogContext()
        .WriteTo.File(
            Path.Combine("Logs", "Log.txt"),
            rollingInterval: RollingInterval.Day,
            fileSizeLimitBytes: 10 * 1024 * 1024,
            retainedFileCountLimit: 30,
            rollOnFileSizeLimit: true,
            shared: true,
            flushToDiskInterval: TimeSpan.FromSeconds(2))
        .WriteTo.Console();
});

#endregion

// Add services to the container.
builder.Services.AddControllersWithViews();


#region DataBase

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

#endregion

#region Identity

builder.Services.AddIdentity<AppUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();


builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Lockout.MaxFailedAccessAttempts = 15;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMilliseconds(5);
    options.Lockout.AllowedForNewUsers = true;
    options.User.RequireUniqueEmail = true;
    options.SignIn.RequireConfirmedEmail = false;
    options.SignIn.RequireConfirmedPhoneNumber = false;
});

#endregion

#region Cooike

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
        options.SlidingExpiration = true;
        options.AccessDeniedPath = "/Forbidden/";
    });
// builder.Services.ConfigureApplicationCookie(opt =>
// {
//     opt.LoginPath = new PathString("/User/Login");
//     opt.Cookie = new CookieBuilder
//     {
//         Name = "AspNetCoreIdentityCookie", //Olu?turulacak Cookie'yi isimlendiriyoruz.
//         HttpOnly = false, //K�t� niyetli insanlar?n client-side taraf?ndan Cookie'ye eri?mesini engelliyoruz.
//         Expiration = TimeSpan.FromMinutes(2), //Olu?turulacak Cookie'nin vadesini belirliyoruz.
//         SameSite = SameSiteMode
//             .Lax, //Top level navigasyonlara sebep olmayan requestlere Cookie'nin g�nderilmemesini belirtiyoruz.
//         SecurePolicy = CookieSecurePolicy.Always //HTTPS �zerinden eri?ilebilir yap?yoruz.
//     };
//     opt.SlidingExpiration =
//         true; //Expiration s�resinin yar?s? kadar s�re zarf?nda istekte bulunulursa e?er geri kalan yar?s?n? tekrar s?f?rlayarak ilk ayarlanan s�reyi tazeleyecektir.
//     opt.ExpireTimeSpan =
//         TimeSpan.FromMinutes(2); //CookieBuilder nesnesinde tan?mlanan Expiration de?erinin varsay?lan de?erlerle ezilme ihtimaline kar??n tekrardan Cookie vadesi burada da belirtiliyor.)
// });

#endregion

#region FluentValidator

builder.Services.AddFluentValidation(p =>
    p.RegisterValidatorsFromAssemblyContaining<UserRegisterVMValidator>());

#endregion

#region UnitOfWork

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IUnitOfWorkService, UnitOfWorkService>();

#endregion

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