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
using System.Windows.Navigation;
using System.Windows.Shapes;
using AccesoADatos;
using Entidades;
using WpfApp.Vistas;

namespace WpfApp
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class VentanaLogueo : Window
    {
        Usuario usuario;

        public VentanaLogueo()
        {
            InitializeComponent();
            txbuser.Focus();
        }

        private void btnIngresar_Click(object sender, RoutedEventArgs e)
        {
            usuario = new Usuario();
            usuario.Nombre = txbuser.Text;
            usuario.Contrasena = pwbContra.Password;
            UsuarioABM.Loguear(usuario);
            if (usuario.Nivel == 0) MessageBox.Show("Nombre de usuario o contraseña incorrecta.");
            else
            {
                VentanaRegistros ventanaPrincipal = new VentanaRegistros(usuario);
                this.Close();
                ventanaPrincipal.ShowDialog();
            }
        }
        private void ckbShow_Checked(object sender, RoutedEventArgs e)
        {
            pwbContra.Visibility = Visibility.Collapsed;
            txbPass.Visibility = Visibility.Visible;
            txbPass.Focus();
        }

        private void ckbShow_Unchecked(object sender, RoutedEventArgs e)
        {
            pwbContra.Visibility = Visibility.Visible;
            txbPass.Visibility = Visibility.Collapsed;

            pwbContra.Focus();
        }

        private void pwbContra_PasswordChanged(object sender, RoutedEventArgs e)
        {
            txbPass.Text = pwbContra.Password.ToString();
        }

        private void txbPass_KeyDown(object sender, KeyEventArgs e)
        {
            pwbContra.Password = txbPass.Text;
        }
    }
}
