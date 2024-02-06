using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Schematix.Api.Middleware;
using Schematix.Core.Entities;
using Schematix.Core.Interfaces;
using Schematix.Core.Mappers;
using Schematix.Infrastructure.Context;
using Schematix.Infrastructure.Repositories;
using static Schematix.Core.Mappers.IEmployeeMapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(p => p.AddPolicy("corspolicy", build =>
{
    build.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader();
}));

builder.Services.AddTransient<GlobalExceptionMiddleware>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IEmployeeMapper, EmployeeMapper>();

builder.Services.AddScoped<IBranchRepository, BranchRepository>();
builder.Services.AddScoped<IBranchMapper, BranchMapper>();

builder.Services.AddScoped<IShiftRepository, ShiftRepository>();
builder.Services.AddScoped<IShiftMapper, ShiftMapper>();

builder.Services.AddScoped<IWorkTaskMapper, WorkTaskMapper>();

builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddAuthorization();

builder.Services.AddIdentityApiEndpoints<Employee>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<DataContext>()
    .AddDefaultTokenProviders();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors("corspolicy");

app.MapControllers();

app.UseMiddleware<GlobalExceptionMiddleware>();

app.MapMyIdentityApi<Employee>();

app.Run();
