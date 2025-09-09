using Apis.Extensions;
using AccesoDatos.Extensions;
using ModeloDatos.IModelos;
using ModeloDatos.Utilidades;
using Microsoft.EntityFrameworkCore;
using AccesoDatos;
using AccesoDatos.Movilidad;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Configuración de DbContext
builder.Services.AddDbContext<ModeloDatos.Modelos.MovilidadDesarrolloContext>(options =>
{
    options.UseSqlServer(Conexion.ConexionSqlServerBd);
});

// Inyección de dependencias 
builder.Services.AddTransient<IBeneficios, AccesoDatos.Movilidad.Beneficios>();
builder.Services.AddTransient<ICumplimientoCondiciones, AccesoDatos.Movilidad.CumplimientoCondicion>();
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
builder.Services.AddTransient<IInstitucion, AccesoDatos.Movilidad.Institucion>();
builder.Services.AddTransient<IEntregable, AccesoDatos.Movilidad.Entregable>();
builder.Services.AddTransient<IPais, AccesoDatos.Movilidad.Pais>();
builder.Services.AddTransient<IConvocatoria, AccesoDatos.Movilidad.Convocatoria>();
builder.Services.AddTransient<IPostulaciones, AccesoDatos.Movilidad.Postulacion>();
builder.Services.AddTransient<IFinanciacionUCM, AccesoDatos.Movilidad.FinanciacionUCM>();
builder.Services.AddTransient<ITipoFinanciacion, AccesoDatos.Movilidad.TipoFinanciacion>();
builder.Services.AddTransient<IFinanciacionExterna, AccesoDatos.Movilidad.FinanciacionExterna>();
builder.Services.AddTransient<IEstadosPostulacion, AccesoDatos.Movilidad.EstadosPostulacion>();
builder.Services.AddTransient<IBeneficiosPostulacion, AccesoDatos.Movilidad.BeneficiosPostulacion>();
builder.Services.AddTransient<ICumplimientoCondiciones, AccesoDatos.Movilidad.CumplimientoCondicion>();

builder.Services.AddTransient<IConsultaCategoriaMovilidad, AccesoDatos.Movilidad.ConsultaCategoriaModalidad>();
builder.Services.AddTransient<IEntregablePostulacion, AccesoDatos.Movilidad.EntregablePostulacion>();
builder.Services.AddTransient<INotificaciones, AccesoDatos.Movilidad.Notificaciones>();
builder.Services.AddTransient<IInstitucionConvenio, AccesoDatos.Movilidad.InstitucionConvenio>();

// AutoMapper
builder.Services.AddAutoMapper(config =>
{
    config.RegisterApiMappings();
    config.RegisterAccessMappings();
});

// Configuración de JSON
builder.Services.AddControllers()
    .AddJsonOptions(opt =>
    {
        opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        opt.JsonSerializerOptions.WriteIndented = true;
    });

// --------------------- CORS: definir una única política ---------------------
var allowedOrigins = new[]
{
    "http://localhost:4200",      // tu frontend durante desarrollo (ajusta si usas otro puerto)
    "http://localhost:5173",      // si usas Vite (opcional)
    "https://tudominio-frontend"  // producción (ajusta o añade el dominio real)
};

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", policy =>
    {
        policy.WithOrigins(allowedOrigins)
              .AllowAnyHeader()
              .AllowAnyMethod()
              // .AllowCredentials() // descomenta solo si necesitas cookies/credenciales y recuerda usar orígenes específicos (no "*")
              ;
    });
});
// ---------------------------------------------------------------------------

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Swagger en desarrollo
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

// APLICAR LA POLÍTICA CORS UNA SOLA VEZ
app.UseCors("CorsPolicy");

// Middlewares de autenticación/autorización si aplica
app.UseAuthorization();

app.MapControllers();

app.Run();