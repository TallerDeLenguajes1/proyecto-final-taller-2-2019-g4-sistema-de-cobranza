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
    /// Lógica de interacción para Registros.xaml
    /// </summary>
    public partial class VentanaRegistros : Window
    {
        Usuario UsuarioActual;
        public VentanaRegistros(Usuario usuarioRecibido)
        {
            UsuarioActual = usuarioRecibido;
            InitializeComponent();
            lbRegistros.ItemsSource = RegistroABM.ListaRegistros();
            txbBuscar.Focus();
        }

        private void btnDeudores_Click(object sender, RoutedEventArgs e)
        {
            Deudores ventanaDeudores = new Deudores(UsuarioActual);
            ventanaDeudores.ShowDialog();
        }

        private void btnBuscarRegistro_Click(object sender, RoutedEventArgs e)
        {
            if (rdbDni.IsChecked.Value) lbRegistros.ItemsSource = RegistroABM.RegistrosPorAtributo("dni", txbBuscar.Text);
            else if (rdbCuit.IsChecked.Value) lbRegistros.ItemsSource = RegistroABM.RegistrosPorAtributo("cuit", txbBuscar.Text);
            else lbRegistros.ItemsSource = RegistroABM.RegistrosPorAtributo("", txbBuscar.Text);
            if (lbRegistros.Items.Count == 0) lblNoticia.Content = "No Match";
            else lblNoticia.Content = "Se encontraron " + lbRegistros.Items.Count.ToString() + " Coincidencias.";
        }

        private void btnDeuda_Click(object sender, RoutedEventArgs e)
        {
            VentanaDeudas ventanaDeudas = new VentanaDeudas();
            ventanaDeudas.ShowDialog();
        }

        private void btnEmpresas_Click(object sender, RoutedEventArgs e)
        {
            VentanaEmpresas ventanaEmpresas = new VentanaEmpresas(UsuarioActual);
            ventanaEmpresas.ShowDialog();
        }

        private void btnAltaRegsitro_Click(object sender, RoutedEventArgs e)
        {
            VentanaRegistroAM AltaRegistro = new VentanaRegistroAM(UsuarioActual);
            AltaRegistro.ShowDialog();
        }

        private void btnModRegistro_Click(object sender, RoutedEventArgs e)
        {
            VentanaRegistroAM ModRegistro = new VentanaRegistroAM(UsuarioActual);
            ModRegistro.ShowDialog();
        }

        private void btnBorrar_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
