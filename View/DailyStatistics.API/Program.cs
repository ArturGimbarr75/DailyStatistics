using DailyStatistics.Application.Services;
using DailyStatistics.Application.Services.Interfaces;
using DailyStatistics.Persistence;
using DailyStatistics.Persistence.Models;
using DailyStatistics.Persistence.Repositories;
using DailyStatistics.Persistence.Repositories.EF;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddSwaggerGen(c =>
{
	var securityScheme = new OpenApiSecurityScheme
	{
		Name = "Authorization",
		Description = "Enter JWT Bearer token **_only_**",
		In = ParameterLocation.Header,
		Type = SecuritySchemeType.Http,
		Scheme = JwtBearerDefaults.AuthenticationScheme,
		BearerFormat = "JWT",
		Reference = new OpenApiReference
		{
			Id = JwtBearerDefaults.AuthenticationScheme,
			Type = ReferenceType.SecurityScheme
		}
	};

	var scheme = new OpenApiSecurityScheme
	{
		Reference = new OpenApiReference
		{
			Type = ReferenceType.SecurityScheme,
			Id = JwtBearerDefaults.AuthenticationScheme
		}
	};

	c.AddSecurityRequirement(new OpenApiSecurityRequirement()
	{
		{
			scheme, new string[] { }
		}
	});

	c.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();
	c.OperationFilter<SecurityRequirementsOperationFilter>(true, JwtBearerDefaults.AuthenticationScheme);

	c.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, securityScheme);
});

// TODO: Add validation
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
	.AddJwtBearer(options =>
	{
		options.RequireHttpsMetadata = true;
		options.TokenValidationParameters = new TokenValidationParameters
		{
			ValidateIssuer = false,
			ValidateAudience = false,
			ValidateIssuerSigningKey = true,
			ValidateLifetime = true,
			IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Auth:Jwt:Key"]!)),
			ClockSkew = TimeSpan.Zero
		};
	});

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddIdentity<User, IdentityRole>(options =>
{
	//options.SignIn.RequireConfirmedAccount = true; // temporary removed

	options.Password.RequireDigit = true;
	options.Password.RequireLowercase = true;
	options.Password.RequireNonAlphanumeric = true;
	options.Password.RequireUppercase = true;
	options.Password.RequiredLength = 8;
	options.Password.RequiredUniqueChars = 1;

	// Lockout settings.
	options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
	options.Lockout.MaxFailedAccessAttempts = 5;
	options.Lockout.AllowedForNewUsers = true;

	// User settings.
	options.User.AllowedUserNameCharacters =
	"abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
	options.User.RequireUniqueEmail = false;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
builder.Services.AddScoped<ITrackingActivityKindRepository, TrackingActivityKindRepository>();
builder.Services.AddScoped<ITrackingActivityRecordRepository, TrackingActivityRecordRepository>();
builder.Services.AddScoped<IDayRecordRepository, DayRecordRepository>();
builder.Services.AddScoped<IProfileImageRepository, ProfileImageRepository>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserManagerFacade, UserManagerFacade>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IActivityKindService, ActivityKindService>();
builder.Services.AddScoped<IActivityRecordService, ActivityRecordService>();
builder.Services.AddScoped<IProfileImageService, ProfileImageService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
