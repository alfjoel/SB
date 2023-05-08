using SB.Fiscal;
using SB.Fiscal.Services;
using SB.Infrastructure;
using SB.Infrastructure.Entity;
using SB.LiteDb;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Configuration.AddJsonFile("config.json");
builder.Services.Configure<Config>(builder.Configuration);
builder.Services.AddSingleton<ILiteDbSettings>(_ => new LiteDbSetting
    { ConnectionString = Common.ToApplicationPath("storecore.dll") });

builder.Services.AddHostedService<PrinterServer>();

var app = builder.Build();

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