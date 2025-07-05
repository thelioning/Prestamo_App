using PrestamoApp.Models;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace PrestamoApp.Views
{
    public partial class ClienteDialog : Window
    {
        public Cliente Cliente { get; private set; } = new Cliente();

        public ClienteDialog(Cliente? cliente = null)
        {
            InitializeComponent();

            if (cliente != null)
            {
                Cliente = cliente;
                txtNroDocumento.Text = Cliente.NroDocumento;
                txtNombre.Text = Cliente.Nombre;
                txtApellido.Text = Cliente.Apellido;
                txtCorreo.Text = Cliente.Correo;
                txtTelefono.Text = Cliente.Telefono;
            }
        }

        private void BarraSuperior_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ButtonState == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void Cerrar_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private async void Guardar_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidarCampos())
                return;

            Cliente.NroDocumento = txtNroDocumento.Text.Trim();
            Cliente.Nombre = txtNombre.Text.Trim();
            Cliente.Apellido = txtApellido.Text.Trim();
            Cliente.Correo = txtCorreo.Text.Trim();
            Cliente.Telefono = txtTelefono.Text.Trim();

            var context = App.DbContext!;  // Accede directamente al miembro estático

            try
            {
                if (Cliente.IdCliente == 0)
                {
                    context.Cliente.Add(Cliente);
                }
                else
                {
                    context.Cliente.Update(Cliente);
                }

                await context.SaveChangesAsync();

                MessageBox.Show("Cliente guardado correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar el cliente.\nDetalles: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(txtNroDocumento.Text))
            {
                MessageBox.Show("Ingrese el Nro. Documento.", "Validación", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("Ingrese el Nombre.", "Validación", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (!string.IsNullOrWhiteSpace(txtCorreo.Text) &&
                !Regex.IsMatch(txtCorreo.Text, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                MessageBox.Show("El correo no tiene un formato válido.", "Validación", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            return true;
        }
    }
}



