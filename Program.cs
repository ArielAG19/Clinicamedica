using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Clinicamedica.Data;
using Microsoft.AspNetCore.Identity.UI.Services;
using Clinicamedica.Services; // Asegúrate de usar el espacio de nombres correcto para EmailSender

var builder = WebApplication.CreateBuilder(args);

// Configura el DbContext para usar Identity
builder.Services.AddDbContext<ClinicamedicaContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ClinicamedicaContext") ?? throw new InvalidOperationException("Connection string 'ClinicamedicaContext' not found.")));

// Configura Identity
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = true;
    options.Password.RequiredUniqueChars = 1;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;
    options.User.RequireUniqueEmail = false; // Cambiado a false para no requerir email único
})
.AddEntityFrameworkStores<ClinicamedicaContext>()
.AddDefaultTokenProviders();

// Configura el servicio de correo electrónico
builder.Services.AddSingleton<IEmailSender>(provider =>
{
    var configuration = provider.GetRequiredService<IConfiguration>();
    var host = configuration["Email:Host"] ?? throw new InvalidOperationException("Email host not configured.");
    var port = configuration.GetValue<int>("Email:Port");
    var username = configuration["Email:Username"] ?? throw new InvalidOperationException("Email username not configured.");
    var password = configuration["Email:Password"] ?? throw new InvalidOperationException("Email password not configured.");
    var fromAddress = configuration["Email:FromAddress"] ?? throw new InvalidOperationException("Email from address not configured.");

    return new EmailSender(host, port, username, password, fromAddress);
});

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add Razor Pages support
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Añade autenticación y autorización
app.UseAuthentication();
app.UseAuthorization();

// Configura las rutas de Identity
app.MapRazorPages(); // Esto es necesario para las páginas de Identity

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
