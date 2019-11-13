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
            bool valido = true;

            if ( ! validarNumero(txbDNI.Text))
            {
                MessageBox.Show("Dni no valido");
                valido = false;    
            }

            if ( ! validarNumero(txbTelefono.Text))
            {
                MessageBox.Show("Telefono no valido");
                valido = false;
            }

            if (validarNombyApe(txbNomYApe.Text))
            {
                MessageBox.Show("Nombre y apellido no valido");
                valido = false;
            }

            if (valido)
            {
                deudorX.ApellidoNombre = txbNomYApe.Text;
                deudorX.Telefono = txbTelefono.Text;
                deudorX.Dni = txbDNI.Text;

                DeudorABM.InsertarDeudor(deudorX);
                this.Close();
            }
            

        }

        private void bModificar_Click(object sender, RoutedEventArgs e)
        {
            bool valido = true;

            if (!validarNumero(txbDNI.Text))
            {
                MessageBox.Show("Dni no valido");
                valido = false;
            }

            if (!validarNumero(txbTelefono.Text))
            {
                MessageBox.Show("Telefono no valido");
                valido = false;
            }

            if (validarNombyApe(txbNomYApe.Text))
            {
                MessageBox.Show("Nombre y apellido no valido");
                valido = false;
            }

            if (valido)
            {
                deudorX.ApellidoNombre = txbNomYApe.Text;
                deudorX.Telefono = txbTelefono.Text;
                deudorX.Dni = txbDNI.Text;

                DeudorABM.ModificarDeudor(deudorX);
                this.Close();
            }

        }
    }
}
