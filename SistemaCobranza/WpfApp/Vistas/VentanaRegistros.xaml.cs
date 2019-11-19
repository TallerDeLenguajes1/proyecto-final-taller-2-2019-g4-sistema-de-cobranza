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
        List<Registro> registros;
        public VentanaRegistros(Usuario usuarioRecibido)
        {
            UsuarioActual = usuarioRecibido;
            InitializeComponent();
            //VentanaLogueo logout = new VentanaLogueo();
            //logout.ShowDialog();
        }

        private void btnDeudores_Click(object sender, RoutedEventArgs e)
        {
            Deudores ventanaDeudores = new Deudores();
            ventanaDeudores.ShowDialog();
        }

        private void btnBuscarRegistro_Click(object sender, RoutedEventArgs e)
        {
            string bot = txbBuscar.Text;
            if (rdbDni.IsChecked.Value) registros = RegistroABM.RegistrosPorAtributo("dni",bot);
            else if (rdbCuit.IsChecked.Value) registros = RegistroABM.RegistrosPorAtributo("cuit", bot);
            else registros = RegistroABM.RegistrosPorAtributo(UsuarioActual.Id_usuario.ToString(), bot);
        }

        private void btnDeuda_Click(object sender, RoutedEventArgs e)
        {
            VentanaDeudas ventanaDeudas = new VentanaDeudas();
            ventanaDeudas.ShowDialog();
        }

        private void btnEmpresas_Click(object sender, RoutedEventArgs e)
        {
            VentanaEmpresas ventanaEmpresas = new VentanaEmpresas();
            ventanaEmpresas.ShowDialog();
        }

        private void btnAltaRegsitro_Click(object sender, RoutedEventArgs e)
        {
            VentanaRegistroAM AltaRegistro = new VentanaRegistroAM(UsuarioActual);
            AltaRegistro.ShowDialog();
        }

        private void btnModRegistro_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnBorrar_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
