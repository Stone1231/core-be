using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using Backend.Models;
using Backend.Repositories;
using Backend.Services;

var builder = WebApplication.CreateBuilder(args);

// 配置 Kestrel
builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.ListenAnyIP(8000); // HTTP
    serverOptions.ListenAnyIP(8001, listenOptions => // HTTPS
    {
        listenOptions.UseHttps();
    });
});

// Add DbContext
builder.Services.AddDbContext<MyContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add Repositories
builder.Services.AddScoped<DeptRepository, DeptRepository>();
builder.Services.AddScoped<ProjRepository, ProjRepository>();
builder.Services.AddScoped<UserRepository, UserRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Add Services
builder.Services.AddScoped<DeptService, DeptService>();
builder.Services.AddScoped<ProjService, ProjService>();
builder.Services.AddScoped<UserService, UserService>();
builder.Services.AddScoped<FileService, FileService>();
builder.Services.AddScoped<InitService, InitService>();

// Add Controllers
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        //options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase; // 使用駝峰命名
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull; // 忽略 null 值
    });

// Add Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Backend API", Version = "v1" });
});

// Add CORS with specific policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

// Configure the HTTP request pipeline.
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Backend API v1"));
}

//app.UseHttpsRedirection();

// 確保 CORS 在路由之前
app.UseCors("AllowAll");

app.UseAuthorization();
app.MapControllers();

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new Microsoft.Extensions.FileProviders.PhysicalFileProvider(
        Path.Combine(Directory.GetCurrentDirectory(), "Static")),
    RequestPath = "/static"
});

app.Run();
