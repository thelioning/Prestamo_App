using Microsoft.EntityFrameworkCore;
using PrestamoApp.Core;
using PrestamoApp.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<PrestamoDbContext>(options =>
    options.UseSqlServer("Server=tcp:prestamoappsql.database.windows.net,1433;Initial Catalog=DBPrestamo;Persist Security Info=False;User ID=PrestamoApp;Password=Perlapaola1972;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"));

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
        return Results.BadRequest(new { error = "Token inv�lido o expirado." });
    }

    usuario.Clave = req.NuevaClave;
    usuario.Token = null;
    usuario.TokenExpira = null;

    await db.SaveChangesAsync();

    return Results.Ok(new { message = "Contrase�a actualizada correctamente." });
});

app.Run();

record CambiarClaveRequest(string Token, string NuevaClave);