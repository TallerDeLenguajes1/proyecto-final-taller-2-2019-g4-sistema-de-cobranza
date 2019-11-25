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

        List<Deuda> deudas;
        public VentanaDeudas()
        {
            InitializeComponent();
            lbDeudas.ItemsSource = DeudaABM.ListadeDeudas();
        }

        private void btnBuscarDeudas_Click(object sender, RoutedEventArgs e)
        {
            if (rdbDni.IsChecked.Value)
            {
                lbDeudas.ItemsSource = DeudaABM.deudasPorAtributo("dni",txbBuscarDeudas.Text);
            }
            else
            {
                lbDeudas.ItemsSource = DeudaABM.deudasPorAtributo("cuit", txbBuscarDeudas.Text);
            }
        }
        private void btnAltaDeuda_Click(object sender, RoutedEventArgs e)
        {
            VentanaDeudaAM Altadeuda = new VentanaDeudaAM();
            Altadeuda.ShowDialog();
        }
        private void btnModDeuda_Click(object sender, RoutedEventArgs e)
        {
            if (lbDeudas.SelectedItem != null)
            {
                VentanaDeudaAM deudaMod = new VentanaDeudaAM((Deuda)lbDeudas.SelectedItem);
                deudaMod.ShowDialog();
            }
            else MessageBox.Show("Debes escoger una deuda de la lista para modificarla.");
        }
        private void btnBorrarDeuda_Click(object sender, RoutedEventArgs e)
        {
            DeudaABM.BorrarDeuda((Deuda)lbDeudas.SelectedItem);
        }
    }
}
