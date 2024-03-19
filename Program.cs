using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RentARoom.Models;
//using System.Configuration;
//using Microsoft.Extensions.Configuration.;
//public IConfiguration Configuration { get; }

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
IConfiguration Configuration = builder.Configuration;

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options =>
options.UseSqlServer(Configuration.GetConnectionString("MyConnDBConnection"))
) ;

builder.Services.AddScoped<IMyClientRepository, MyClientRepository>();
//builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();

//builder.Services.AddDbContext<AirBbContext>(
//        options => options.UseSqlServer(Configuration.GetConnectionString("MyConnDBConnection")));
//builder.Services.AddDbContext<MyClientOutputSPContext>(
//        options => options.UseSqlServer(Configuration.GetConnectionString("MyConnDBConnection")));

//builder.Services.AddDbContext<MakeReservationOutputSPContext>(
//        options => options.UseSqlServer(Configuration.GetConnectionString("MyConnDBConnection")));

//builder.Services.AddDbContext<DeleteReservationOutputSPContext>(
//        options => options.UseSqlServer(Configuration.GetConnectionString("MyConnDBConnection")));


//builder.Services.AddDbContext<MyRoomContext>(
//        options => options.UseSqlServer(Configuration.GetConnectionString("MyConnDBConnection")));

//builder.Services.AddDbContext<MyReservationContext>(
//        options => options.UseSqlServer(Configuration.GetConnectionString("MyConnDBConnection")));




// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//!!!!
//builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<AirBbContext>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//!!!!
//app.UseAuthentication();


app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllers();

app.Run();
