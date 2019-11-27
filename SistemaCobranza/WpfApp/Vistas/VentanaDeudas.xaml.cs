using AccesoADatos;
using Entidades;
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

namespace WpfApp.Vistas
{
    /// <summary>
    /// Lógica de interacción para VentanaDeudas.xaml
    /// </summary>
    public partial class VentanaDeudas : Window
    {
        public VentanaDeudas(Usuario usuarioRecibido)
        {
            InitializeComponent();
            if(usuarioRecibido.Nivel == 2)
            {
                btnBorrarDeuda.Visibility = Visibility.Collapsed;
            }
            else if (usuarioRecibido.Nivel == 3)
            {
                btnAltaDeuda.Visibility = Visibility.Collapsed;
                btnBorrarDeuda.Visibility = Visibility.Collapsed;
                btnModDeuda.Visibility = Visibility.Collapsed;
            }
            Refresh();
        }
        private void Refresh()
        {
            lbDeudas.ItemsSource = DeudaABM.ListadeDeudas();
        }
        private void btnBuscarDeudas_Click(object sender, RoutedEventArgs e)
        {
            var radio = rdbDni.IsChecked.Value ? "dni" : "cuit ";
            
            lbDeudas.ItemsSource = DeudaABM.deudasPorAtributo(radio, txbBuscarDeudas.Text);
            Refresh();
        }

        private void btnAltaDeuda_Click(object sender, RoutedEventArgs e)
        {
            VentanaDeudaAM Altadeuda = new VentanaDeudaAM();
            Altadeuda.ShowDialog();
            Refresh();
        }
        private void btnModDeuda_Click(object sender, RoutedEventArgs e)
        {
            if (lbDeudas.SelectedItem != null)
            {
                VentanaDeudaAM deudaMod = new VentanaDeudaAM((Deuda)lbDeudas.SelectedItem);
                deudaMod.ShowDialog();
                Refresh();
            }
            else MessageBox.Show("Debes escoger una deuda de la lista para modificarla.");
        }
        private void btnBorrarDeuda_Click(object sender, RoutedEventArgs e)
        {
            if (lbDeudas.SelectedItem != null)
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("Esta seguro que desea eliminar esta deuda?", "Confirmacion Borrar", System.Windows.MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    DeudaABM.BorrarDeuda((Deuda)lbDeudas.SelectedItem);
            Refresh();
                }
            }
            else MessageBox.Show("Debe seleccionar un deudor.");

        }
 
    }
}
