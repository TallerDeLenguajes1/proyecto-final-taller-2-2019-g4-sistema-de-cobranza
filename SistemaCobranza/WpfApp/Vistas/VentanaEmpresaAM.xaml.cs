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
    public partial class Add_Mod_Empresa : Window
    {
        Entidades.Empresa empresaX = new Entidades.Empresa();
        public Add_Mod_Empresa()
        {
            InitializeComponent();
        }
        public Add_Mod_Empresa(Entidades.Empresa EmpresaRecibida)
        {
            InitializeComponent();
            empresaX = EmpresaRecibida;
            btnAgregar.Visibility = Visibility.Collapsed;
            tbxCuit.Text = empresaX.Cuit;
            tbxNombre.Text = empresaX.Nombre;
        }
        private void bAgregar_Click(object sender, RoutedEventArgs e)
        {
            empresaX = new Entidades.Empresa();
            empresaX.Cuit = tbxCuit.Text;
            empresaX.Nombre = tbxNombre.Text;
            AccesoADatos.EmpresaABM.InsertarEmpresa(empresaX);
        }
        private void bModificar_Click(object sender, RoutedEventArgs e)
        {
            empresaX.Cuit = tbxCuit.Text;
            empresaX.Nombre = tbxNombre.Text;
            AccesoADatos.EmpresaABM.ModificarEmpresa(empresaX);
        }
    }
}
