using Microsoft.EntityFrameworkCore;
using PrestamoApp.Core;
using PrestamoApp.Data;

var builder = WebApplication.CreateBuilder(args);

// Obtener la cadena de conexión desde las variables de entorno
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

if (string.IsNullOrWhiteSpace(connectionString))
{
    throw new InvalidOperationException("La cadena de conexión no está configurada. Verifica la variable de entorno ConnectionStrings__DefaultConnection.");
}

builder.Services.AddDbContext<PrestamoDbContext>(options =>
    options.UseSqlServer(connectionString));

var app = builder.Build();

app.MapPost("/api/cambiar-clave", async (PrestamoDbContext db, CambiarClaveRequest req) =>
{
    if (string.IsNullOrWhiteSpace(req.Token) || string.IsNullOrWhiteSpace(req.NuevaClave))
    {
        return Results.BadRequest(new { error = "Token y nueva clave son obligatorios." });
    }

    var usuario = await db.Usuario.FirstOrDefaultAsync(u => u.Token == req.Token && u.TokenExpira > DateTime.UtcNow);
    if (usuario == null)
    {
        return Results.BadRequest(new { error = "Token inválido o expirado." });
    }

    usuario.Clave = req.NuevaClave;
    usuario.Token = null;
    usuario.TokenExpira = null;

    await db.SaveChangesAsync();

    return Results.Ok(new { message = "Contraseña actualizada correctamente." });
});

app.Run();

record CambiarClaveRequest(string Token, string NuevaClave);
