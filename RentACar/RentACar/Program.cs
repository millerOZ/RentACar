using Microsoft.EntityFrameworkCore;
using RentACar.Data;
using RentACar.Helpers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<RentCarContext>(o =>
{
    o.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
  


builder.Services.AddScoped<ICombosHelper, CombosHelper>();//Combos
builder.Services.AddScoped<IBlobHelper, BlobHelper>(); //Conexión al blob Azure
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();// Permite hacer cambios en caliente



var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/error/{0}");
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
