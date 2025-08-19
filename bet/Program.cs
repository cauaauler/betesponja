using Microsoft.EntityFrameworkCore;
using FutebolSimplesBetsHub.Data;
using FutebolSimplesBetsHub.Services;
using System.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews()
    .AddRazorRuntimeCompilation();

// Add DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add Services
builder.Services.AddScoped<IMatchService, MatchService>();
builder.Services.AddScoped<IBetService, BetService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IStatisticsService, StatisticsService>();
builder.Services.AddScoped<IOddsService, OddsService>();

// HttpClient global
builder.Services.AddHttpClient();

// HttpClient tipado para OddsService com headers corretos
builder.Services.AddHttpClient<IOddsService, OddsService>(client =>
{
    client.BaseAddress = new Uri("https://v3.football.api-sports.io/");
    var apiKey = builder.Configuration["FootballApi:ApiKey"] ?? string.Empty;
    if (client.DefaultRequestHeaders.Contains("x-apisports-key"))
        client.DefaultRequestHeaders.Remove("x-apisports-key");
    client.DefaultRequestHeaders.Add("x-apisports-key", apiKey);
    client.DefaultRequestHeaders.Accept.Clear();
    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    client.DefaultRequestHeaders.UserAgent.ParseAdd("BetSponge/1.0");
});

// Add Session
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

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
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Configure URLs
app.Urls.Clear();
app.Urls.Add("http://localhost:5001");
app.Urls.Add("https://localhost:5002");

app.Run(); 