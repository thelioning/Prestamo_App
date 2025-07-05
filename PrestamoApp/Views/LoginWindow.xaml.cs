using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PrestamoApp.Views
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        private void BtnCerrar_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;  // Asegura que App sepa que el login fue cancelado
            this.Close();
        }

        private async void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            string correo = txtCorreo.Text.Trim();
            string contrasena = txtContrasena.Password;

            if (string.IsNullOrWhiteSpace(correo) || string.IsNullOrWhiteSpace(contrasena))
            {
                MessageBox.Show("Por favor, complete todos los campos.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                bool loginExitoso = await ValidarCredencialesAsync(correo, contrasena);
                var loginWindow = new LoginWindow();

                if (loginExitoso)
                {
                    var menuPrincipal = new MenuPrincipal();
                    this.DialogResult = true;
                    this.Close();

                }
                else
                {
                    MessageBox.Show("Correo o contraseña incorrectos.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error durante el inicio de sesión.\nDetalles: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task<bool> ValidarCredencialesAsync(string correo, string contrasena)
        {
            var usuario = await App.DbContext!.Usuario
                .FirstOrDefaultAsync(u => u.Correo == correo && u.Clave == contrasena);

            return usuario != null;
        }

        private void RecuperarContrasena_Click(object sender, RoutedEventArgs e)
        {
            var dialogo = new RecuperarDialog { Owner = this };
            var resultado = dialogo.ShowDialog();

            if (resultado == true)
            {
                MessageBox.Show("Se ha enviado un correo de recuperación si el correo existe en el sistema.",
                                "Recuperación enviada", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}


