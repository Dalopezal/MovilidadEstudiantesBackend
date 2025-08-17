using Apis.Extensions;
using AccesoDatos.Extensions;
using ModeloDatos.IModelos;
using System.Text;
using ModeloDatos.Utilidades;
using Microsoft.EntityFrameworkCore;
using AccesoDatos;
using AccesoDatos.Movilidad;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ModeloDatos.Modelos.DbMovilidadContext>(options =>
{
    if (builder.Environment.IsProduction())
    {
        options.UseSqlServer(Conexion.ConexionSqlServerBd);
    }
    else if (builder.Environment.IsDevelopment())
    {
        options.UseSqlServer(Conexion.ConexionSqlServerBd);
    }
});

builder.Services.AddControllers();
builder.Services.AddTransient<IBeneficios, AccesoDatos.Movilidad.Beneficios>();
builder.Services.AddTransient<ICategoriaMovilidad, AccesoDatos.Movilidad.CategoriaMovilidad>();
builder.Services.AddTransient<ICiudad, AccesoDatos.Movilidad.Ciudad>();
builder.Services.AddTransient<IClasificacionConvenio, AccesoDatos.Movilidad.ClasificacionConvenio>();
builder.Services.AddTransient<ICondicionesConvocatoria, AccesoDatos.Movilidad.CondicionesConvocatoria>();
builder.Services.AddTransient<ICondicion, AccesoDatos.Movilidad.Condicion>();
builder.Services.AddTransient<IModalidad, AccesoDatos.Movilidad.Modalidad>();
builder.Services.AddTransient<ITipoMovilidad, AccesoDatos.Movilidad.TipoMovilidad>();
builder.Services.AddTransient<IConvenio, AccesoDatos.Movilidad.Convenios>();
builder.Services.AddTransient<ITipoActividad, AccesoDatos.Movilidad.TipoActividad>();
builder.Services.AddTransient<ITipoConvenio, AccesoDatos.Movilidad.TipoConvenio>();

builder.Services.AddAutoMapper(config => {
    config.RegisterApiMappings();
    config.RegisterAccessMappings();
});


builder.Services.AddControllers()
    .AddJsonOptions(opt =>
    {
        opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        opt.JsonSerializerOptions.WriteIndented = true;
    });


// Add services to the container.

builder.Services.AddControllers()
.AddJsonOptions(opciones => opciones.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.Never);

builder.Services.AddCors(options =>
{
    options.AddPolicy("Development", policy =>
    {
        policy.AllowAnyOrigin();
        policy.AllowAnyMethod();
        policy.AllowAnyHeader();
    });

    options.AddPolicy("Production", policy =>
    {
        policy.AllowAnyOrigin();
        policy.AllowAnyMethod();
        policy.AllowAnyHeader();
    });
});

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");



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
