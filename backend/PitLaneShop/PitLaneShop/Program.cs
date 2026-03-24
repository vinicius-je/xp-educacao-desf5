using PitLaneShop.Model.Repositories;
using PitLaneShop.Persistence;
using PitLaneShop.Persistence.Repositories;
using PitLaneShop.Services.Features.Cliente.Implementation;
using PitLaneShop.Services.Features.Cliente.Interfaces;
using PitLaneShop.Services.Features.CodigoPromocional.Implementation;
using PitLaneShop.Services.Features.CodigoPromocional.Interfaces;
using PitLaneShop.Services.Features.Pedido.Implementation;
using PitLaneShop.Services.Features.Pedido.Interfaces;
using PitLaneShop.Services.Features.Produto.Implementation;
using PitLaneShop.Services.Features.Produto.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<PitLaneShopDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<PitLaneShopDbContext>());

builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IPedidoRepository, PedidoRepository>();
builder.Services.AddScoped<IItemPedidoRepository, ItemPedidoRepository>();
builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();
builder.Services.AddScoped<ICodigoPromocionalRepository, CodigoPromocionalRepository>();

builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddScoped<IPedidoService, PedidoService>();
builder.Services.AddScoped<IProdutoService, ProdutoService>();
builder.Services.AddScoped<ICodigoPromocionalService, CodigoPromocionalService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllClientForTest", policy =>
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod());
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

EnsureDatabaseAndApplyMigrations(app);

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

    if (db.Produtos.Any())
        return;

    var assembly = typeof(PitLaneShopDbContext).Assembly;
    using var stream = assembly.GetManifestResourceStream("PitLaneShop.Persistence.Scripts.seed-data.sql")
        ?? throw new InvalidOperationException("Embedded resource 'seed-data.sql' not found.");

    using var reader = new StreamReader(stream);
    var sql = reader.ReadToEnd();
    db.Database.ExecuteSqlRaw(sql);
}

app.Run();
