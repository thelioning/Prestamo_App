using PrestamoApp.Data;
using PrestamoApp.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PrestamoApp.Views
{
    public partial class ClientesView : UserControl
    {
        private int _paginaActual = 1;
        private int _registrosPorPagina = 10;
        private int _totalPaginas = 1;

        public ClientesView()
        {
            InitializeComponent();
            Loaded += ClientesView_Loaded;
        }

        private async void ClientesView_Loaded(object sender, RoutedEventArgs e)
        {
            await CargarClientesAsync();
        }

        private async Task CargarClientesAsync()
        {
            try
            {
                string filtro = txtBuscar.Text.Trim();

                var query = App.DbContext!.Cliente.AsQueryable();

                if (!string.IsNullOrWhiteSpace(filtro))
                {
                    query = query.Where(c =>
                        (c.NroDocumento ?? "").Contains(filtro) ||
                        (c.Nombre ?? "").Contains(filtro) ||
                        (c.Apellido ?? "").Contains(filtro) ||
                        (c.Correo ?? "").Contains(filtro) ||
                        (c.Telefono ?? "").Contains(filtro));
                }

                int totalRegistros = await Task.Run(() => query.Count());
                _totalPaginas = (int)Math.Ceiling((double)totalRegistros / _registrosPorPagina);

                var clientes = await Task.Run(() => query
                    .OrderBy(c => c.Nombre)
                    .Skip((_paginaActual - 1) * _registrosPorPagina)
                    .Take(_registrosPorPagina)
                    .ToList());

                dgClientes.ItemsSource = clientes;

                int inicio = totalRegistros == 0 ? 0 : ((_paginaActual - 1) * _registrosPorPagina) + 1;
                int fin = inicio + clientes.Count - 1;

                txtPaginacion.Text = $"Mostrando {inicio} a {fin} de {totalRegistros} registros";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar los clientes.\nDetalles: {ex.Message}",
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void TxtBuscar_KeyUp(object sender, KeyEventArgs e)
        {
            _paginaActual = 1;
            await CargarClientesAsync();
        }

        private async void Primero_Click(object sender, RoutedEventArgs e)
        {
            _paginaActual = 1;
            await CargarClientesAsync();
        }

        private async void Anterior_Click(object sender, RoutedEventArgs e)
        {
            if (_paginaActual > 1)
            {
                _paginaActual--;
                await CargarClientesAsync();
            }
        }

        private async void Siguiente_Click(object sender, RoutedEventArgs e)
        {
            if (_paginaActual < _totalPaginas)
            {
                _paginaActual++;
                await CargarClientesAsync();
            }
        }

        private async void Ultimo_Click(object sender, RoutedEventArgs e)
        {
            _paginaActual = _totalPaginas;
            await CargarClientesAsync();
        }

        private async void NuevoCliente_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new ClienteDialog();
            dialog.Owner = Window.GetWindow(this);

            if (dialog.Owner != null)
            {
                dialog.WindowStartupLocation = WindowStartupLocation.Manual;
                double menuWidth = 200;
                dialog.Left = dialog.Owner.Left + menuWidth + ((dialog.Owner.Width - menuWidth - dialog.Width) / 2);
                dialog.Top = dialog.Owner.Top + (dialog.Owner.Height - dialog.Height) / 2;
            }

            if (dialog.ShowDialog() == true)
            {
                await CargarClientesAsync();
            }
        }

        private async void EditarCliente_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Tag is Cliente cliente)
            {
                var dialog = new ClienteDialog(cliente);
                dialog.Owner = Window.GetWindow(this);
                if (dialog.ShowDialog() == true)
                {
                    await CargarClientesAsync();
                }
            }
        }

        private async void EliminarCliente_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Tag is Cliente cliente)
            {
                var confirm = new ConfirmarEliminarDialog();
                confirm.Owner = Window.GetWindow(this);
                if (confirm.ShowDialog() == true && confirm.Confirmado)
                {
                    App.DbContext!.Cliente.Remove(cliente);
                    await App.DbContext.SaveChangesAsync();
                    await CargarClientesAsync();
                    MessageBox.Show("Cliente eliminado correctamente.", "Eliminado", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }
    }
}