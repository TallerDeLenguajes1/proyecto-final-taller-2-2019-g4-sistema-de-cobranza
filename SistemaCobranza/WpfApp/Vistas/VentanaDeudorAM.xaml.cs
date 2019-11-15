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
using AccesoADatos;
using Helpers;

namespace WpfApp.Vistas
{

    /// <summary>
    /// Lógica de interacción para Add_Mod_Deudor.xaml
    /// </summary>
    public partial class Add_Mod_Deudor : Window
    {

        Deudor deudorX;

        public Add_Mod_Deudor()
        {
            InitializeComponent();
            btnModificar.Visibility = Visibility.Collapsed;
        }
        public Add_Mod_Deudor(Deudor deudorRecibido)
        {
            InitializeComponent();
            deudorX = deudorRecibido;
            btnAgregar.Visibility = Visibility.Collapsed;
            txbDNI.Text = deudorRecibido.Dni;
            txbNomYApe.Text = deudorRecibido.ApellidoNombre;
            txbTelefono.Text = deudorRecibido.Telefono;
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            deudorX = new Deudor();
            //bool valido = true;
            string resultado = Helpers.VerificarCampos.VerificarDeudor(txbDNI.Text, txbNomYApe.Text, txbTelefono.Text);
            if (resultado == "true")
            {
                deudorX.ApellidoNombre = txbNomYApe.Text;
                deudorX.Telefono = txbTelefono.Text;
                deudorX.Dni = txbDNI.Text;
                DeudorABM.InsertarDeudor(deudorX);
                this.Close();
            }
            else MessageBox.Show(resultado);
        }

        private void bModificar_Click(object sender, RoutedEventArgs e)
        {
            string resultado = Helpers.VerificarCampos.VerificarDeudor(txbDNI.Text, txbNomYApe.Text, txbTelefono.Text);
            if (resultado == "true")
            {
                deudorX.ApellidoNombre = txbNomYApe.Text;
                deudorX.Telefono = txbTelefono.Text;
                deudorX.Dni = txbDNI.Text;
                //DeudorABM.ModificarDeudor(deudorX);
                this.Close();
            }
            else MessageBox.Show(resultado);
        }
    }
}
