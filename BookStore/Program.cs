using BookStore.DataAccess.Data;
using BookStore.Models;
using BookStore.Repository;
using BookStore.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDBContext>(option =>
    option.UseSqlServer(builder.Configuration.GetConnectionString("DBCon"))
);

/* If you are using Repository Pattern
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();  
*/

/* If you are using Unit of Work Pattern */
builder.Services.AddScoped<IUnitOfWorks, UnitOfWorks>();  



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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");

app.Run();
