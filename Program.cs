using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MicroBill.Data;

var builder = WebApplication.CreateBuilder(args);

//Athirai----
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
//-----

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add services ----
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();
//-----

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// Middleware------
app.UseStaticFiles();
app.UseRouting();
app.UseSession();
//-----------

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();
