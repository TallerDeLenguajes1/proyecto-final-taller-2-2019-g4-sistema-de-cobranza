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
        List<Deudor> deudores;
        public Deudores(Usuario usuarioactual)
        {
            InitializeComponent();
            if(usuarioactual.nivel == 3)
            {
                btnBorrarDeudores.Visibility = Visibility.Collapsed;
                btnModDeudores.Visibility = Visibility.Collapsed;
            }
            else if(usuarioactual.nivel == 2) 
            {
                btnBorrarDeudores.Visibility = Visibility.Collapsed;
            }
            deudores = DeudorABM.ListaDeudores();

            foreach (var item in deudores)
            {
                string str = item.Dni + "/" + item.ApellidoNombre + " / " + item.Telefono;
                lbDeudores.Items.Add(str);
                lbDeudores.Items.Refresh();
            }
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
                VentanaDeudorAM ModDeudor = new VentanaDeudorAM((Deudor)lbDeudores.SelectedItem);
                ModDeudor.ShowDialog();
            }
            else MessageBox.Show("Debe seleccionar un deudor.");
        }

        private void btnBorrarDeudores_Click(object sender, RoutedEventArgs e)
        {
            //borrar deudor
        }
    }
}
