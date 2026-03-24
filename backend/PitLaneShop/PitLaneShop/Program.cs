using PitLaneShop.Model.Repositories;
using PitLaneShop.Persistence;
using PitLaneShop.Persistence.Repositories;
using PitLaneShop.Services.Features.Carro.Implementation;
using PitLaneShop.Services.Features.Carro.Interfaces;
using PitLaneShop.Services.Features.Cliente.Implementation;
using PitLaneShop.Services.Features.Cliente.Interfaces;
using PitLaneShop.Services.Features.RealizarAluguel.Implementation;
using PitLaneShop.Services.Features.RealizarAluguel.Interfaces;
using PitLaneShop.Services.Features.VisualizarVeiculos.Implementation;
using PitLaneShop.Services.Features.VisualizarVeiculos.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<PitLaneShopDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<PitLaneShopDbContext>());

builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IVeiculoModeloRepository, VeiculoModeloRepository>();
builder.Services.AddScoped<ICarroRepository, CarroRepository>();
builder.Services.AddScoped<ITarifaDiariaRepository, TarifaDiariaRepository>();
builder.Services.AddScoped<IAluguelRepository, AluguelRepository>();

builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddScoped<ICarroService, CarroService>();
builder.Services.AddScoped<IVisualizarVeiculosService, VisualizarVeiculosService>();
builder.Services.AddScoped<IRealizarAluguelService, RealizarAluguelService>();

// Add services to the container.

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllClientForTest", policy =>
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod());
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

EnsureDatabaseAndApplyMigrations(app);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAllClientForTest");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

static void EnsureDatabaseAndApplyMigrations(WebApplication app)
{
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<PitLaneShopDbContext>();
    db.Database.Migrate();

    if (db.VeiculosModelo.Any())
        return;

    var assembly = typeof(PitLaneShopDbContext).Assembly;
    using var stream = assembly.GetManifestResourceStream("PitLaneShop.Persistence.Scripts.seed-data.sql")
        ?? throw new InvalidOperationException("Embedded resource 'seed-data.sql' not found.");

    using var reader = new StreamReader(stream);
    var sql = reader.ReadToEnd();
    db.Database.ExecuteSqlRaw(sql);
}

app.Run();
