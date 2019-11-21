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
            if (usuario.nivel == 0) MessageBox.Show("Error al iniciar sesión.");
            else
            {
                VentanaRegistros ventanaPrincipal = new VentanaRegistros(usuario);
                this.Close();
                ventanaPrincipal.ShowDialog();
            }
        }
    }
}
