using Microsoft.EntityFrameworkCore;
using PrestamoApp.Data;
using PrestamoApp.Views;
using System;
using System.Windows;

namespace PrestamoApp
{
    public partial class App : Application
    {
        // DbContext estático para toda la app, inicializado en OnStartup
        public static PrestamoDbContext DbContext { get; private set; } = null!;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            try
            {
                // Configura las opciones del DbContext (conexión a SQL Server)
                var options = new DbContextOptionsBuilder<PrestamoDbContext>()
                    .UseSqlServer(@"Server=DESKTOP-ELMACHO\SQLEXPRESS;Database=DBPrestamo;Trusted_Connection=True;Encrypt=False;")
                    .Options;

                DbContext = new PrestamoDbContext(options);

                // Muestra ventana Login como diálogo modal
                var loginWindow = new LoginWindow();
                bool? result = loginWindow.ShowDialog();

                if (result == true)
                {
                   //Login exitoso: abre ventana principal
                   var menuPrincipal = new MenuPrincipal();
                   menuPrincipal.Show();
                }
                else
                {
                    // Login cancelado o fallido: cierra la app
                    Shutdown();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error crítico al iniciar la aplicación.\n\nDetalle completo:\n{ex.ToString()}",
                    "Error crítico",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);

                Shutdown();

            }
        }
    }
}
