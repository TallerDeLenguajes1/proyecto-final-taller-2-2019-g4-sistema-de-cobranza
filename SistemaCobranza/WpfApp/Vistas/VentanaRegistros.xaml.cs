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

        List<Registro> registros;
        public VentanaRegistros(Usuario usuarioRecibido)
        {
            InitializeComponent();
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void RadioButton_Checked_1(object sender, RoutedEventArgs e)
        {

        }

        private void btnDeudores_Click(object sender, RoutedEventArgs e)
        {
            Deudores ventanaDeudores = new Deudores();
            ventanaDeudores.ShowDialog();
        }

        private void btnBuscarRegistro_Click(object sender, RoutedEventArgs e)
        {
       
        }


    }
}
