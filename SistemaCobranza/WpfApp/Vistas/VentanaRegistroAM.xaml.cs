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
    /// Lógica de interacción para VentanaResitroAM.xaml
    /// </summary>
    public partial class VentanaRegistroAM : Window
    {
        List<Deuda> listaDeudas;
        Registro registroX;
        Usuario usuarioActual;

        public VentanaRegistroAM(Usuario usuarioRecibido)
        {
            usuarioActual = usuarioRecibido;
            InitializeComponent();
        }
        //private void btnBuscar_Click(object sender, RoutedEventArgs e)
        private void btnGuardarRegistro_Click(object sender, RoutedEventArgs e)
        {
            registroX = new Registro();
            registroX.Usuario = usuarioActual;
            registroX.Observacion = txbObservacion.Text;

            if (rdbContesto.IsChecked.Value) registroX.Resultado = "Contesto";
            else if (rdbNoContesto.IsChecked.Value) registroX.Resultado = "No contesto";
            else registroX.Resultado = "Corto";

            if (lbDeudas.SelectedItem != null)
            {
                registroX.Deuda = (Deuda)lbDeudas.SelectedItem;
                RegistroABM.InsertarRegistro(registroX);
            }
            else
            {
                MessageBox.Show("No se seleccionó una deuda");
            }          
        }

        private void btnBuscarDeudas_Click(object sender, RoutedEventArgs e)
        {
            if (!Helpers.VerificarCampos.Verificarnum(txbBuscarDeudas.Text))//Verificar si son numeros
            {
                if (rdbCuit.IsChecked.Value) listaDeudas = DeudaABM.deudasPorAtributo("cuit",txbBuscarDeudas.Text);
                else listaDeudas = DeudaABM.deudasPorAtributo("dni",txbBuscarDeudas.Text);
            }
            else MessageBox.Show("Debe ser solo numeros");
        }

    }
}
