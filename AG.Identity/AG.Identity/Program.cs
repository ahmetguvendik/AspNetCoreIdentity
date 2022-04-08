using AG.Identity.Context;
using AG.Identity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AGContext>(opt =>
{
    opt.UseSqlServer("server = DESKTOP-A523NCQ\\MSSQLSERVER01; database = AG.Identity; integrated security = true");
}
  );


builder.Services.AddIdentity<AppUser, AppRole>(opt =>
{
    opt.Lockout.MaxFailedAccessAttempts = 3;
}).AddEntityFrameworkStores<AGContext>();

builder.Services.ConfigureApplicationCookie(opt =>
{
    opt.Cookie.HttpOnly = true;
    opt.Cookie.SameSite =  SameSiteMode.Strict; //Ýlgili Domainden kullanýlýr sadece
    opt.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest; //HTTP ile gelirse HTTP , HTTPS ile gelirse HTTPS ile çalýþýr.
    opt.Cookie.Name = "AGCookie";
    opt.ExpireTimeSpan = TimeSpan.FromDays(20); //Ýlgili Cookieler 20 gün saklanýr.
    opt.LoginPath = new PathString("/Home/SignIn"); //Yetkisiz giriþ oluþunca gideceði yer
    opt.AccessDeniedPath = new PathString("/Home/AccessDenied");
}
);



var app = builder.Build();

app.UseStaticFiles();
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine
    (Directory.GetCurrentDirectory(), "node_modules"))
    ,
    RequestPath = "/node_modules"
});

app.UseAuthentication();
app.UseAuthorization();

app.MapDefaultControllerRoute();

app.Run();
