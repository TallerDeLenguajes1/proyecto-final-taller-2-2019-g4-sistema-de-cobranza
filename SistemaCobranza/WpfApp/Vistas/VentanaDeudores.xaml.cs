using System;
using AccesoADatos;
using Entidades;
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
    /// Lógica de interacción para Deudores.xaml
    /// </summary>
    public partial class Deudores : Window
    {
        public Deudores(Usuario usuarioactual)
        {
            InitializeComponent();
            if(usuarioactual.Nivel == 3)
            {
                btnBorrarDeudores.Visibility = Visibility.Collapsed;
                btnModDeudores.Visibility = Visibility.Collapsed;
            }
            else if(usuarioactual.Nivel == 2) 
            {
                btnBorrarDeudores.Visibility = Visibility.Collapsed;
            }
            lbDeudores.ItemsSource = DeudorABM.ListaDeudores();
        
        }

        private void btnAltaDeudores_Click(object sender, RoutedEventArgs e)
        {
            VentanaDeudorAM AltaDeudor = new VentanaDeudorAM();
            AltaDeudor.ShowDialog();
        }

        private void btnModDeudores_Click(object sender, RoutedEventArgs e)
        {
            if (lbDeudores.SelectedItem != null)
            {
                Deudor deudorX =(Deudor) lbDeudores.SelectedItem; 
                VentanaDeudorAM ModDeudor = new VentanaDeudorAM(deudorX);
                ModDeudor.ShowDialog();
            }
            else MessageBox.Show("Debe seleccionar un deudor.");
        }
        private void btnBorrarDeudores_Click(object sender, RoutedEventArgs e)
        {
            if (lbDeudores.SelectedItem != null)
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("Esta seguro que desea eliminar el deudor?", "Confirmacion Borrar", System.Windows.MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    DeudorABM.BorrarDeudor((Deudor)lbDeudores.SelectedItem);
                }
            }
            else MessageBox.Show("Debe seleccionar un deudor.");
        }

        private void btnBuscarDeudores_Click(object sender, RoutedEventArgs e)
        {
            if (rdbDni.IsChecked.Value && txbBuscarDeudores.Text != null)
            {
                lbDeudores.ItemsSource = DeudorABM.DeudorPorAtributo("dni",txbBuscarDeudores.Text);
            }
            else lbDeudores.ItemsSource = DeudorABM.DeudorPorAtributo("nombre",txbBuscarDeudores.Text);
            if (lbDeudores.Items.Count == 0) lblNoticia.Content = "No Match";
            else lblNoticia.Content = "Se han encontrado " + lbDeudores.Items.Count + " Empresas.";
        }
    }
}
