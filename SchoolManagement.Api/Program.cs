using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using SchoolManagement.Domain.Interfaces.Repositories;
using SchoolManagement.Infrastructure.DbContext;
using SchoolManagement.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SchoolManagement.Application.Extensions;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Application.Features.Rooms.Service;
using SchoolManagement.Infrastructure.Seeder;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var Configuration = builder.Configuration;
// injecting the MediatR
// builder.Services.AddMediatR(config => 
//     config.RegisterServicesFromAssemblies(typeof(Program).Assembly));

builder.Services.AddApplicationServices();

#region Configure JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidIssuer = Configuration["JWT:ValidIssuer"],
        ValidateAudience = true,
        ValidAudience = Configuration["JWT:ValidAudience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Secret"])),

    };
});
#endregion

#region injecting interfaces and their implementations

// builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IStudentRepository, StudentRepository>();

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(/*option => option.SignIn.RequireConfirmedAccount = true*/)
    .AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddScoped<AgoraTokenService>();

#endregion

#region DataBase Config
        
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options
        .UseSqlServer(connectionString)
        .LogTo(Console.WriteLine, LogLevel.Information);
});

#endregion


var app = builder.Build();

 #region RolesSeeder

using var scope = app.Services.CreateScope();
try
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    await DefaultRoles.SeedAsync(roleManager);
}
catch (Exception exception)
{
    Console.WriteLine(exception);
    throw;
}


#endregion

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
