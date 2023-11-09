<<<<<<< HEAD
=======
using Microsoft.AspNetCore.Identity;
>>>>>>> 82cd00eff8fa092f92ccd1f5cf3af4bb2e90c670
using Microsoft.EntityFrameworkCore;
using TicketEase.Application.Interfaces.Repositories;
using TicketEase.Application.Interfaces.Services;
using TicketEase.Application.ServicesImplementation;
<<<<<<< HEAD
using TicketEase.Configurations;
=======
using TicketEase.Common.Utilities;
using TicketEase.Configurations;
using TicketEase.Domain.Entities;
using TicketEase.Mapper;
>>>>>>> 82cd00eff8fa092f92ccd1f5cf3af4bb2e90c670
using TicketEase.Persistence.Context;
using TicketEase.Persistence.Extensions;
using TicketEase.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var configuration = builder.Configuration;
var services = builder.Services;
var env = builder.Environment;


<<<<<<< HEAD
// Add services to the container.
//builder.Services.AddHttpClient();
//builder.Services.AddCloudinaryService(config);
builder.Services.AddMailService(builder.Configuration);

// Authentication configuration
=======
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IBoardServices, BoardServices>();
builder.Services.AddScoped<ITicketService, TicketService>();
builder.Services.AddScoped<ITicketRepository, TicketRepository>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddAutoMapper(typeof(MapperProfile));

>>>>>>> 82cd00eff8fa092f92ccd1f5cf3af4bb2e90c670
builder.Services.AddAuthentication();
builder.Services.AuthenticationConfiguration(configuration);

// Identity  configuration
builder.Services.IdentityConfiguration();
builder.Services.AddLoggingConfiguration(builder.Configuration);
<<<<<<< HEAD
builder.Services.AddLogging(builder => builder.AddConsole());


=======
>>>>>>> 82cd00eff8fa092f92ccd1f5cf3af4bb2e90c670

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<TicketEaseDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddSwagger();

<<<<<<< HEAD
builder.Services.AddDbContext<TicketEaseDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("TicketEase"));
});

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<JwtTokenGeneratorService>();
//builder.Services.AddScoped<IEmailServices, EmailServices>();
=======
builder.Services.AddIdentity<AppUser, IdentityRole>()
			   .AddEntityFrameworkStores<TicketEaseDbContext>()
			   .AddDefaultTokenProviders();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
>>>>>>> 82cd00eff8fa092f92ccd1f5cf3af4bb2e90c670

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();

    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Ticket Ease v1"));
}
using (var scope = app.Services.CreateScope())
{
	var serviceprovider = scope.ServiceProvider;
	Seeder.SeedRolesAndSuperAdmin(serviceprovider);
}
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

