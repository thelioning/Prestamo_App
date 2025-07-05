using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Windows;
using System.Windows.Controls;

namespace PrestamoApp.Views
{
    public partial class MenuPrincipal : Window
    {
        public MenuPrincipal()
        {
            InitializeComponent();
        }

        private void CargarVista(UserControl control)
        {
            try
            {
                ContenidoFrame.Content = control;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar el módulo.\nDetalles: {ex.Message}",
                                "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            /*try
             {
                 ContenidoFrame.Content = control;
             }
             catch (Exception ex)
             {
                 MessageBox.Show($"Error al cargar el módulo.\nDetalles: {ex.Message}",
                                 "Error", MessageBoxButton.OK, MessageBoxImage.Error);
             }
            try
            {
                MessageBox.Show($"Intentando cargar: {control.GetType().Name}");
                ContenidoFrame.Content = control;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar el módulo.\nDetalles: {ex.Message}",
                                "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }*/
        }

        private void Clientes_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var vista = new ClientesView();  // Asume que tienes un UserControl ClientesView
                CargarVista(vista);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al abrir Clientes.\nDetalles: {ex.Message}",
                                "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Monedas_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var vista = new MonedasView();  // Asume que tienes un UserControl MonedasView
                CargarVista(vista);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al abrir Monedas.\nDetalles: {ex.Message}",
                                "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Prestamos_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var vista = new PrestamosView();  // Asume que tienes un UserControl PrestamosView
                CargarVista(vista);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al abrir Préstamos.\nDetalles: {ex.Message}",
                                "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DetallesPrestamos_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var vista = new PrestamoDetallesView();  // Asume que tienes un UserControl PrestamoDetallesView
                CargarVista(vista);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al abrir Detalles Préstamos.\nDetalles: {ex.Message}",
                                "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Resumen_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var vista = new ReportesView();  // Asume que tienes un UserControl ReportesView
                CargarVista(vista);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al abrir Resumen.\nDetalles: {ex.Message}",
                                "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Usuarios_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var vista = new UsuariosView();  // Asume que tienes un UserControl UsuariosView
                CargarVista(vista);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al abrir Usuarios.\nDetalles: {ex.Message}",
                                "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CerrarSesion_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (MessageBox.Show("¿Seguro que desea cerrar sesión?", "Confirmar", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    var login = new LoginWindow();
                    login.Show();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cerrar sesión.\nDetalles: {ex.Message}",
                                "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
