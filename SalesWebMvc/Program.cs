using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SalesWebMvc.Data;
using SalesWebMvc.Services;

var builder = WebApplication.CreateBuilder(args);
//adicionando dependencia no projeto
builder.Services.AddDbContext<SalesWebMvcContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("SalesWebMvcContext") ?? throw new InvalidOperationException("Connection string 'SalesWebMvcContext' not found."), 
    new MySqlServerVersion(new Version(8, 0, 29)), 
    builder => 
    builder.MigrationsAssembly("SalesWebMvc")));

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<SeedingService>();
builder.Services.AddScoped<SellerService>();
builder.Services.AddScoped<DepartamentService>();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    // Chamar o serviço de seeding
    using (var scope = app.Services.CreateScope())
    {
        var seedingService = scope.ServiceProvider.GetRequiredService<SeedingService>();
        seedingService.Seed();
    }
}
// Configure the HTTP request pipeline.
else 
{
    //Pagina de erro da aplica��o
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
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
