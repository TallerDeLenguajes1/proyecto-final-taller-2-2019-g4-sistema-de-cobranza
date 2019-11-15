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
        public VentanaRegistroAM()
        {
            InitializeComponent();
        }
        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            if (!Helpers.VerificarCampos.Verificarnum(txbBuscar.Text))//Verificar si son numeros
            {
                var lista;
                if (rdbCuit.IsChecked.Value) AccesoADatos.DeudaABM.DeudaPorCuit(txbBuscar.Text);
                else AccesoADatos.DeudaABM.DeudaPorDni(txbBuscar.Text);
            }
            else MessageBox.Show("Debe ser solo numeros");
        }

    }
}
