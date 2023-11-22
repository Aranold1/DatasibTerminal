using DataSibTerminal.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDataProtection();

builder.Services.AddControllersWithViews();
//this is test db, we gonna remove connection string latter (i promise) 
builder.Services.AddDbContext<postgresContext>(options => options.UseNpgsql("Server=158.101.194.79;Port=3066;User Id=postgres;Password=3g0rvt3m3;Database=postgres"));
builder.Services.AddAuthorization(options=>
{
    options.AddPolicy("cookie", policy => 
    {
        policy.RequireAuthenticatedUser(); 
    });
});
builder.Services.AddAuthentication("cookie").AddCookie("cookie");
var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseCookiePolicy();

app.UseStatusCodePages();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapDefaultControllerRoute();
app.Run();
