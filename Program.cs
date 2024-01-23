using Microsoft.EntityFrameworkCore;
using SchoolApi.Repositories;
using SchoolApi.Services;
using SchoolApi.Interfaces;
using SchoolApi.Data;
using SchoolApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddTransient<Seed>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<AlumnoInterface, AlumnoRepository>();
builder.Services.AddScoped<ExamenInterface, ExamenRepository>();
builder.Services.AddScoped<ProfesorInterface, ProfesorRepository>();
builder.Services.AddScoped<AlumnosProfesInterface, AlumnosProfesRepository>();
builder.Services.AddScoped<Service>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataContext>(options =>
{ //Add-Migration InitialCreate    Update-Database
    if (!options.IsConfigured)
        options.UseSqlServer(builder.Configuration
            .GetConnectionString("DefaultConnection"));
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<DataContext>();
    dbContext.Database.Migrate();
}

SeedData(app);
void SeedData(IHost app)
{
    var scopedFactory = app.Services.GetService<IServiceScopeFactory>();

    using (var scope = scopedFactory.CreateScope())
    {
        var service = scope.ServiceProvider.GetService<Seed>();
        service.SeedDataContext();
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Eliminar la base de datos al final (no funciona)
app.Lifetime.ApplicationStopping.Register(() =>
{
    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<DataContext>();
        dbContext.Database.EnsureDeleted();
    }
});

app.Run();
