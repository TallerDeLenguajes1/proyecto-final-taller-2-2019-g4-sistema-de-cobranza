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
using MySql.Data.MySqlClient;
using Entidades;
using AccesoADatos;

namespace WpfApp
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        UsuarioConect nuevologin;
        Usuario nuevo;
        //CrearUsuario nuevouser = new CrearUsuario();
        int nivel;
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            nuevo = new Usuario();
            nuevo.Nombre = txbuser.Text;
            nuevo.Contrasena = txbcontra.Text;
            nuevologin = new UsuarioConect();
            //nuevouser.Crearuser(nuevo);
            nivel = nuevologin.Loguear(nuevo);
            if (nivel == 0) MessageBox.Show("Error al iniciar sesión.");
            else MessageBox.Show("Sesión iniciada con éxito.");
        }
    }
}
