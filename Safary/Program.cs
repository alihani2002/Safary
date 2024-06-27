using AutoMapper;
using Safary.Seeds;
using Domain.Entities;
using Domain.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Persistence.Data;
using Safary.Mapping;
using System.Text;
using Domain.Repositories;
using Persistence.Repositories;
using Service.Abstractions;
using Safary.Repository;
using Persistence.Configurations;
using Sieve.Services;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Domain.Consts;
using Service.Implementations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddServicesRegistration(builder.Configuration);


builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.Configure<MailSettings>(builder.Configuration.GetSection(nameof(MailSettings)));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
 .AddEntityFrameworkStores<ApplicationDbContext>()
 .AddDefaultTokenProviders();

builder.Services.Configure<JWT>(builder.Configuration.GetSection(nameof(JWT)));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
// Registering the ReviewService with its interface
builder.Services.AddScoped<IReviewService, ReviewService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddScoped<ISieveProcessor, SieveProcessor>();
builder.Services.AddScoped<ISieveConfiguration, SieveConfiguration>();
builder.Services.AddScoped<ITourGuideRepository, TourGuideRepository>();
builder.Services.AddScoped<ITourRepository, TourRepository>();

builder.Services.AddCors();

builder.Services.AddAuthentication(option =>
{
	option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
	option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(option =>
{
	option.SaveToken = true;
	option.RequireHttpsMetadata = false;
	option.TokenValidationParameters = new TokenValidationParameters()
	{
		ValidateIssuer = true,
		ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
		ValidateAudience = true,
		ValidAudience = builder.Configuration["JWT:ValidAudiance"],
		IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"])),
	};
});

builder.Services.AddAuthorization(options =>
{
	options.AddPolicy("AdminPolicy", policy =>
		policy.RequireClaim("Role", AppRoles.Admin));
	options.AddPolicy("TourGuidePolicy", policy =>
		policy.RequireClaim("Role", AppRoles.TourGuide));
	options.AddPolicy("UserPolicy", policy =>
		policy.RequireClaim("Role", AppRoles.User));
});

builder.Services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

builder.Services.AddScoped(x =>
{
	var actionContext = x.GetRequiredService<IActionContextAccessor>().ActionContext;
	var factory = x.GetRequiredService<IUrlHelperFactory>();
	return factory.GetUrlHelper(actionContext!);
});

builder.Services.AddSwaggerGen(c =>
{
	c.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo", Version = "v1" });
});
builder.Services.AddSwaggerGen(swagger =>
{
	//This is to generate the Default UI of Swagger Documentation    
	swagger.SwaggerDoc("v2", new OpenApiInfo
	{
		Version = "v1",
		Title = "ASP.NET 8 Web API",
		Description = " ITI Projrcy"
	});

	// To Enable authorization using Swagger (JWT)    
	swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
	{
		Name = "Authorization",
		Type = SecuritySchemeType.ApiKey,
		Scheme = "Bearer",
		BearerFormat = "JWT",
		In = ParameterLocation.Header,
		Description = "Enter 'Bearer' [space] and then your valid token in the text input below.\r\n\r\nExample: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9\"",
	});
	swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
				{
					{
					   new OpenApiSecurityScheme
					{
					   Reference = new OpenApiReference
					{
					   Type = ReferenceType.SecurityScheme,
					   Id = "Bearer"
					}

					},
					   new string[] {}
					}
				});
});
var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
	app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseCors(c => c.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());

app.UseAuthentication();
app.UseAuthorization();

var scopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();

using var scope = scopeFactory.CreateScope();

var roleManger = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
var userManger = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

await DefaultRoles.SeedAsync(roleManger);
await DefaultUsers.SeedAdminUserAsync(userManger);

app.MapControllers();

app.Run();
