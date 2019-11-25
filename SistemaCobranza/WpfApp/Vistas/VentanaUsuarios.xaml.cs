using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Entidades;
using AccesoADatos;

namespace WpfApp.Vistas
{
    /// <summary>
    /// Lógica de interacción para VentanaUsuarios.xaml
    /// </summary>
    public partial class VentanaUsuarios : Window
    {
        public VentanaUsuarios()
        {
            InitializeComponent();
        }

        private void btnBuscarUsuarios_Click(object sender, RoutedEventArgs e)
        {
            lbUsuarios.ItemsSource = UsuarioABM.UsuarioPorNombre(txbBuscarUsuarios.Text);

        }

        private void btnAltaUsuario_Click(object sender, RoutedEventArgs e)
        {
            VentanaUsuarioAM AltaUsuario = new VentanaUsuarioAM();
            AltaUsuario.ShowDialog();
        }

        private void btnModUsuario_Click(object sender, RoutedEventArgs e)
        {
            if (lbUsuarios.SelectedItem != null)
            {
                VentanaUsuarioAM ModUsuario = new VentanaUsuarioAM((Usuario)lbUsuarios.SelectedItem);
                ModUsuario.ShowDialog();
            }
        }

        private void btnBorrarUsuario_Click(object sender, RoutedEventArgs e)
        {
            if (lbUsuarios.SelectedItem != null)
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("Esta seguro que desea eliminar el Usuario?", "Confirmacion Borrar", System.Windows.MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    UsuarioABM.BorrarUsuario((Usuario)lbUsuarios.SelectedItem);
                    lbUsuarios.Items.Refresh();
                }
            }
            else MessageBox.Show("Debe seleccionar un usuario.");
            
            
        }
    }
}
