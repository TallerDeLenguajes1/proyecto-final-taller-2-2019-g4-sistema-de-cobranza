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
        Registro registroX;
        readonly Usuario usuarioActual;

        public VentanaRegistroAM(Usuario usuarioRecibido)
        {
            InitializeComponent();
            usuarioActual = usuarioRecibido;
            lbDeudas.ItemsSource = DeudaABM.ListadeDeudas();
            btnModificarRegistro.Visibility = Visibility.Collapsed;

        }
        public VentanaRegistroAM(Usuario usuarioRecibido,Registro registroRecibido)
        {
            InitializeComponent();
            usuarioActual = usuarioRecibido;
            registroX = registroRecibido;

            txbTitulo.Text = "Modificar Regsitro";
            btnGuardarRegistro.Visibility = Visibility.Collapsed;

            var deudas = DeudaABM.ListadeDeudas();
            lbDeudas.ItemsSource = deudas;

            lbDeudas.SelectedItem = deudas.Find(ob => (ob.Deudor.Dni == registroRecibido.Deuda.Deudor.Dni && ob.Empresa.Cuit == registroRecibido.Deuda.Empresa.Cuit));
            lbDeudas.IsEnabled = false;
            btnBuscarDeudas.IsEnabled = false;
            txbBuscarDeudas.IsEnabled = false;
            txbObservacion.Text = registroRecibido.Observacion;

        }

        private void btnGuardarRegistro_Click(object sender, RoutedEventArgs e)
        {
            registroX = new Registro();
            registroX.Usuario = usuarioActual;
            registroX.Observacion = txbObservacion.Text;
            registroX.FechaHora = DateTime.Now.ToShortDateString();
            
            if (rdbContesto.IsChecked.Value) registroX.Resultado = "Contesto";
            else if (rdbNoContesto.IsChecked.Value) registroX.Resultado = "No contesto";
            else registroX.Resultado = "Corto";
            
            if (lbDeudas.SelectedItem != null)
            {
                registroX.Deuda = (Deuda)lbDeudas.SelectedItem;
                RegistroABM.InsertarRegistro(registroX);
                this.Close();
            }
            else
            {
                MessageBox.Show("No se seleccionó una deuda");
            }          
        }

        private void btnModificarRegistro_Click(object sender, RoutedEventArgs e)
        {
            registroX.Usuario = usuarioActual;
            registroX.Observacion = txbObservacion.Text;
            registroX.FechaHora = DateTime.Now.ToShortDateString();

            if (rdbContesto.IsChecked.Value) registroX.Resultado = "Contesto";
            else if (rdbNoContesto.IsChecked.Value) registroX.Resultado = "No contesto";
            else registroX.Resultado = "Corto";

            registroX.Deuda = (Deuda)lbDeudas.SelectedItem;
            RegistroABM.ModificarRegistro(registroX);
            this.Close();
        }


        private void btnBuscarDeudas_Click(object sender, RoutedEventArgs e)
        {
            if (!Helpers.VerificarCampos.Verificarnum(txbBuscarDeudas.Text))
            {
                if (rdbCuit.IsChecked.Value) lbDeudas.ItemsSource = DeudaABM.deudasPorAtributo("cuit",txbBuscarDeudas.Text);
                else lbDeudas.ItemsSource = DeudaABM.deudasPorAtributo("dni",txbBuscarDeudas.Text);

            }
            else MessageBox.Show("Debe ser solo numeros");
        }

    }
}
