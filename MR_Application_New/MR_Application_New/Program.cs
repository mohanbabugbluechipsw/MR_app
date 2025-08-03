using DAL;
using DAL.IRepositories;
using DAL.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Model_New.Models;
using Serilog;
using System.Text.Json.Serialization;
using System.Text.Json;
using Microsoft.AspNetCore.Http.Features;


var builder = WebApplication.CreateBuilder(args);



Log.Logger = new LoggerConfiguration()
    .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day)  // Log file configuration
    .CreateLogger();
builder.Host.UseSerilog();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login/AdminDashboard"; // Set your login path
        options.AccessDeniedPath = "/Account/AccessDenied"; // Set your access denied path
    });

builder.Services.AddDbContext<MrAppDbNewContext
    >(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));
builder.Services.AddScoped<IQuestionsService, QuestionsService>();


builder.Services.AddScoped(typeof(UnitOfWork<MrAppDbNewContext>));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder => builder.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });


builder.Services.AddDistributedMemoryCache();



builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 104857600; // 100MB limit (adjust as needed)
});

builder.Services.AddHttpContextAccessor();





builder.Services.AddControllersWithViews();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Set timeout to 1 minute
    options.Cookie.HttpOnly = true; // Make the session cookie HTTP-only
    options.Cookie.IsEssential = true; // Make the session cookie essential
});

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage(); // Display detailed error pages in development
}
else
{
    app.UseExceptionHandler("/Home/Error"); // Use a generic error page for production
    app.UseHsts(); // Use HTTP Strict Transport Security
}


app.UseHttpsRedirection();
app.UseStaticFiles(); // Enable serving static files

app.UseCors("AllowAll");

app.UseRouting();

app.UseSession();



app.UseAuthorization(); // Enable authorization middleware

// Configure endpoint routing
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}"); // Set default route to Home controller
});

app.Run();


