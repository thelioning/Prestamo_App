using System.Windows;

namespace PrestamoApp.Views
{
    public partial class ConfirmarEliminarDialog : Window
    {
        public bool Confirmado { get; private set; } = false;

        public ConfirmarEliminarDialog()
        {
            InitializeComponent();
        }

        private void Eliminar_Click(object sender, RoutedEventArgs e)
        {
            Confirmado = true;
            DialogResult = true;
            Close();
        }

        private void Retornar_Click(object sender, RoutedEventArgs e)
        {
            Confirmado = false;
            DialogResult = false;
            Close();
        }
    }
}
