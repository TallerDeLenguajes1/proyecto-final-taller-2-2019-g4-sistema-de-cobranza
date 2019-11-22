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
        Deuda deudaX;

        public VentanaDeudas()
        {
            InitializeComponent();

            deudas = DeudaABM.ListadeDeudas();
            lbDeudas.ItemsSource =  deudas;

        }

        private void btnBuscarDeudas_Click(object sender, RoutedEventArgs e)
        {
            if (rdbDni.IsChecked.Value)
            {
                deudas = DeudaABM.deudasPorAtributo("dni",txbBuscarDeudas.Text);
                foreach (Deuda x in deudas)
                {
                    string str = x.Empresa.Nombre + "/" + x.Empresa.Cuit + " / " + x.Deudor.ApellidoNombre + " / " + x.Deudor.Dni + " / " + x.Deudor.Telefono;
                    lbDeudas.Items.Add(str);
                    lbDeudas.Items.Refresh();
                }
                
            }
            else
            {
                deudas = DeudaABM.deudasPorAtributo("cuit", txbBuscarDeudas.Text);
                foreach (Deuda x in deudas)
                {
                    string str = x.Empresa.Nombre + "/" + x.Empresa.Cuit + " / " + x.Deudor.ApellidoNombre + " / " + x.Deudor.Dni + " / " + x.Deudor.Telefono;
                    lbDeudas.Items.Add(str);
                    lbDeudas.Items.Refresh();
                }
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
                VentanaDeudaAM deudaMod = new VentanaDeudaAM((Deuda) lbDeudas.SelectedItem);
                deudaMod.ShowDialog();
            }
            else MessageBox.Show("Debes escoger una deuda de la lista para modificarla.");
        }
        private void btnBorrarDeuda_Click(object sender, RoutedEventArgs e)
        {
            //acceso a datos para borrar una deuda
        }
    }
}
