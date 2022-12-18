using BankManagementApi.Data;
using BankManagementApi.Interfaces;
using BankManagementApi.Repository;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IMasterRepository, MasterRepository>();
builder.Services.AddScoped<IBalanceInfoRepository, BalanceInfoRepository>();
builder.Services.AddScoped<ILoyaltyPointsRepository, LoyaltyPointsRepository>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
	builder.Configuration.GetConnectionString("DefaultConnection")
	));
builder.Services.AddControllers().AddNewtonsoftJson();

var myCors = "appCors";
builder.Services.AddCors(options =>
{
	options.AddPolicy(myCors, policy =>
	{
		policy.WithOrigins("http://localhost:3000").AllowAnyHeader().AllowAnyMethod();
	});
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
