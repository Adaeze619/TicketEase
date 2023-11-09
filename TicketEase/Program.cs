using CloudinaryDotNet;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TicketEase.Common.Utilities;
using TicketEase.Configurations;
using TicketEase.Domain.Entities;
using TicketEase.Mapper;
using TicketEase.Persistence.Context;
using TicketEase.Persistence.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var configuration = builder.Configuration;
var services = builder.Services;
var env = builder.Environment;


// Add services to the container.
//builder.Services.AddHttpClient();
//builder.Services.AddCloudinaryService(config);
//builder.Services.AddMailService(config);

builder.Services.AddDependencies(builder.Configuration);

// Authentication configuration
builder.Services.AddAuthentication();
builder.Services.AuthenticationConfiguration(configuration);

// Identity  configuration
builder.Services.IdentityConfiguration();
builder.Services.AddLoggingConfiguration(builder.Configuration);

builder.Services.AddSingleton(provider =>
{
    var cloudinarySettings = provider.GetRequiredService<IOptions<CloudinarySetting>>().Value;

    Account cloudinaryAccount = new Account(
        cloudinarySettings.CloudName,
        cloudinarySettings.APIKey,
        cloudinarySettings.APISecret);

    return new Cloudinary(cloudinaryAccount);
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddDbContext<TicketEaseDbContext>(options =>
//options.UseSqlServer(builder.Configuration.GetConnectionString("DBConnectionStrings")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<TicketEaseDbContext>(options => 
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddSwagger();
builder.Services.AddAutoMapper(typeof(MapperProfile));

builder.Services.AddDbContext<TicketEaseDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseDeveloperExceptionPage();

	app.UseSwagger();
	app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Ticket Ease v1"));
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();