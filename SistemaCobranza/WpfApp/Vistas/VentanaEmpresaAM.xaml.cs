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

namespace WpfApp.Vistas
{
    /// <summary>
    /// Lógica de interacción para Add_Mod_Empresa.xaml
    /// </summary>
    public partial class VentanaEmpresaAM : Window
    {
        Empresa empresaX;
        public VentanaEmpresaAM()
        {
            InitializeComponent();
        }
        public VentanaEmpresaAM(Empresa EmpresaRecibida)
        {
            InitializeComponent();
            empresaX = EmpresaRecibida;
            btnAgregarEmpresa.Visibility = Visibility.Collapsed;
            txbCuit.Text = empresaX.Cuit;
            txbNombre.Text = empresaX.Nombre;
        }
        private void bAgregar_Click(object sender, RoutedEventArgs e)
        {
            empresaX = new Empresa();
            empresaX.Cuit = txbCuit.Text;
            empresaX.Nombre = txbNombre.Text;
            AccesoADatos.EmpresaABM.InsertarEmpresa(empresaX);
        }
        private void bModificar_Click(object sender, RoutedEventArgs e)
        {
            empresaX.Cuit = txbCuit.Text;
            empresaX.Nombre = txbNombre.Text;
           // AccesoADatos.EmpresaABM.ModificarEmpresa(empresaX); // Todo
        }
    }
}
