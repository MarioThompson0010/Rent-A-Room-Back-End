using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RentARoom.Models;
using RentARoom.Models.Clients;
using RentARoom.Models.Reservations;
using RentARoom.Models.Rooms;
using Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore;
//using System.Configuration;
//using Microsoft.Extensions.Configuration.;
//public IConfiguration Configuration { get; }

var builder = WebApplication.CreateBuilder(args);

//var connectionString = builder.Configuration.GetConnectionString("AppDbContextConnection") ?? throw new InvalidOperationException("Connection string 'AppDbContextConnection' not found.");
IConfiguration Configuration = builder.Configuration;

builder.Services.AddDbContext<AppDbContext>(options =>
options.UseSqlServer(Configuration.GetConnectionString("MyConnDBConnection"))
);

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<AppDbContext>();
builder.Services.AddRazorPages();
//var connectionString = builder.Configuration.GetConnectionString("AppDbContextConnection") ?? throw new InvalidOperationException("Connection string 'AppDbContextConnection' not found.");

// Add services to the container.

builder.Services.AddControllers();



//builder.Services.AddDefaultIdentity<IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
//    .AddEntityFrameworkStores<AppDbContext>();

//builder.Services.AddDefaultIdentity<SignInManager<IdentityUser>>(options => options.SignIn.RequireConfirmedAccount = true)
//    .AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddScoped<IMyClientRepository, MyClientRepository>();
builder.Services.AddScoped<IMyRoomRepository, MyRoomRepository>();
builder.Services.AddScoped<IMyReservationRepository, MyReservationRepository>();
builder.Services.AddScoped<IMyClientOutputSPRepository, MyClientOutputSPRepository>();


//builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>();



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<IdentityOptions>(options =>
{
    // Password settings.
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;

    // Lockout settings.
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;

    // User settings.
    options.User.AllowedUserNameCharacters =
    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = false;
});

builder.Services.ConfigureApplicationCookie(options =>
{
    // Cookie settings
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

    options.LoginPath = "/Identity/Account/Login";
    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
    options.SlidingExpiration = true;
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

    //app.UseMigrationsEndPoint();
    app.UseSwagger();
    app.UseSwaggerUI();

}

//!!!!
app.UseStaticFiles();
app.UseAuthentication();


app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllers();
app.MapRazorPages();

app.Run();
