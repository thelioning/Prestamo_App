using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Windows;

namespace PrestamoApp.Views
{
    public partial class RecuperarDialog : Window
    {
        public RecuperarDialog()
        {
            InitializeComponent();
        }

        private void Cancelar_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private async void Recuperar_Click(object sender, RoutedEventArgs e)
        {
            string correo = txtCorreo.Text.Trim();

            if (string.IsNullOrEmpty(correo))
            {
                MessageBox.Show("Por favor ingrese un correo electrónico.", "Validación", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!EsCorreoValido(correo))
            {
                MessageBox.Show("El correo electrónico no tiene un formato válido.", "Validación", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                var context = App.DbContext!;
                var usuario = await context.Usuario.FirstOrDefaultAsync(u => u.Correo == correo);

                if (usuario == null)
                {
                    MessageBox.Show("El correo electrónico no está registrado.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                string token = GenerarTokenSeguro();
                usuario.Token = token;
                usuario.TokenExpira = DateTime.UtcNow.AddHours(1);
                await context.SaveChangesAsync();

                string urlRecuperacion = $"https://prestamo-recuperacion.netlify.app/cambiar.html?token={token}";

                await EnviarEmailRecuperacionAsync(correo, urlRecuperacion);

                MessageBox.Show("Se ha enviado un correo con instrucciones para recuperar la contraseña.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);

                // Cierra el diálogo después de notificar
                this.DialogResult = true;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error inesperado:\n{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool EsCorreoValido(string correo)
        {
            return Regex.IsMatch(correo, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }

        private string GenerarTokenSeguro(int longitud = 64)
        {
            using var rng = RandomNumberGenerator.Create();
            byte[] bytes = new byte[longitud];
            rng.GetBytes(bytes);
            return Convert.ToBase64String(bytes)
                          .Replace("+", "")
                          .Replace("/", "")
                          .Replace("=", "");
        }

        private async Task EnviarEmailRecuperacionAsync(string correoDestino, string urlRecuperacion)
        {
            var smtpHost = "smtp.gmail.com";
            int smtpPort = 587;
            var remitente = "theliondjprodutions@gmail.com";
            var clave = "xeqjlfvfjrkkcahc"; // ⚠ Esto idealmente debería venir de config/variable de entorno

            var mensaje = new MailMessage
            {
                From = new MailAddress(remitente, "PrestamoApp Soporte"),
                Subject = "Recuperación de contraseña - PrestamoApp",
                IsBodyHtml = true,
                Body = $@"
                    <h3>Solicitud de recuperación de contraseña</h3>
                    <p>Para recuperar tu contraseña, haz clic en el siguiente enlace:</p>
                    <p><a href='{urlRecuperacion}'>Recuperar contraseña</a></p>
                    <p>Este enlace expirará en 1 hora.</p>
                    <br/>
                    <p>Si no solicitaste esta acción, puedes ignorar este correo.</p>"
            };

            mensaje.To.Add(correoDestino);

            using var clienteSmtp = new SmtpClient(smtpHost, smtpPort)
            {
                Credentials = new NetworkCredential(remitente, clave),
                EnableSsl = true,
            };

            await clienteSmtp.SendMailAsync(mensaje);
        }
    }
}



