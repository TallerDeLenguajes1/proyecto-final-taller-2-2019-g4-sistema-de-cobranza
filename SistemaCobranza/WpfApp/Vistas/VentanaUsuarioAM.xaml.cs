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
using Entidades;
using AccesoADatos;

namespace WpfApp.Vistas
{
    /// <summary>
    /// Lógica de interacción para VentanaUsuarioAM.xaml
    /// </summary>
    public partial class VentanaUsuarioAM : Window
    {
        Usuario Recibido;
        public VentanaUsuarioAM()
        {
            InitializeComponent();
            btnModificarUsuario.Visibility = Visibility.Collapsed;
        }
        public VentanaUsuarioAM(Usuario UsuarioRecibido)
        {
            Recibido = UsuarioRecibido;
            InitializeComponent();
            btnAgregarUsuario.Visibility = Visibility.Collapsed;
            txbUsuario.Text = Recibido.Nombre;
            txbNivel.Text = Recibido.Nivel.ToString();
        }

        private void btnModificarUsuario_Click(object sender, RoutedEventArgs e)
        {
            Recibido.Nombre = txbUsuario.Text;
            if (txbContrasena.Text != "") Recibido.Contrasena = txbContrasena.Text;
            Recibido.Nivel = Convert.ToInt32(txbNivel.Text);
            UsuarioABM.ModificarUsuario(Recibido);
            this.Close();
        }

        private void btnAgregarUsuario_Click(object sender, RoutedEventArgs e)
        {
            Usuario UsuarioNovo = new Usuario();
            UsuarioNovo.Nombre = txbUsuario.Text;
            UsuarioNovo.Contrasena = txbContrasena.Text;
            UsuarioNovo.Nivel = Convert.ToInt32(txbNivel.Text);
            UsuarioABM.Crearuser(UsuarioNovo);
            this.Close();
        }
    }
}
