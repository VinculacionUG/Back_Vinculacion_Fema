
using Back_Vinculacion_Fema.Interface;
using Back_Vinculacion_Fema.Models.DbModels;
using Back_Vinculacion_Fema.Models.Utilidades;
using Back_Vinculacion_Fema.Service;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

#region Swagger services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
#endregion

#region BD services
builder.Services.AddDbContext<vinculacionfemaContext>(options =>
//options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionTestRobles")));
//options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionTest")));
//options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionTest")));
//options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
#endregion

builder.Services.AddScoped<IFemaDos, FemaDos>();
builder.Services.AddScoped<IFemaTres, FemaTres>();
builder.Services.AddScoped<IFemaCinco, FemaCinco>();
builder.Services.AddScoped<IFemaCuatro, FemaCuatro>();
builder.Services.AddScoped<IListarUsuariosSuper, UserSuperService>();
builder.Services.AddScoped<IListarUsuariosInsp, UserInspService>();
builder.Services.AddScoped<IDetalleUsuarios, DetalleUsuariosService>();
builder.Services.AddScoped<IEliminarUsuario, EliminarUsuarioService>();
builder.Services.AddScoped<IActualizarDatosUsuario, ActualizarUsuarioService>();

#region JWT services
builder.Services.AddAuthorization();
Token.AddJwtAuthentication(builder.Services);
#endregion

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "API",
                      builder =>
                      {
                          builder.WithHeaders("*");
                          builder.WithOrigins("*");
                          builder.WithMethods("*");
                          builder.AllowAnyOrigin();

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

app.UseAuthentication();
app.UseAuthorization();
app.UseCors("API");

app.MapControllers();

app.Run();
