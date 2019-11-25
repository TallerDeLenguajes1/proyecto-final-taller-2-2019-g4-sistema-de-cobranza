using System;
using Entidades;
using AccesoADatos;
using Helpers;
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
    /// Lógica de interacción para Empresa.xaml
    /// </summary>
    public partial class VentanaEmpresas : Window
    {
        List<Empresa> empresas;
        Empresa empresaX;
        public VentanaEmpresas(Usuario usuarioactual)
        {
            InitializeComponent();
            if(usuarioactual.Nivel != 1)
            {
                btnBorrarEmpresa.Visibility = Visibility.Collapsed;
            }
            else if(usuarioactual.Nivel == 3)
            {
                btnModEmpresa.Visibility = Visibility.Collapsed;
            }
            lbEmpresas.ItemsSource = EmpresaABM.listaEmpresas();

        }
        private void btnBuscarEmpresas_Click(object sender, RoutedEventArgs e)
        {
            if (rdbCuit.IsChecked.Value)
            {
                if (VerificarCampos.Verificarnum(txbBuscarEmpresas.Text))
                {
                    lbEmpresas.ItemsSource = EmpresaABM.EmpresasPorAtributo("cuit",txbBuscarEmpresas.Text);
                }
                else MessageBox.Show("Cuit debe ser sólo numeros tal vez quiso buscar por nombre?.");
               
            }
            else
            {
                lbEmpresas.ItemsSource = EmpresaABM.EmpresasPorAtributo("nombre",txbBuscarEmpresas.Text);
            }
        }
        private void btnAltaEmpresa_Click(object sender, RoutedEventArgs e)
        {
            VentanaEmpresaAM AltaEmpresa = new VentanaEmpresaAM();
            AltaEmpresa.ShowDialog();
        }

        private void btnModEmpresa_Click(object sender, RoutedEventArgs e)
        {
            if (lbEmpresas.SelectedItem != null)
            { 
                VentanaEmpresaAM ModEmpresa = new VentanaEmpresaAM((Empresa)lbEmpresas.SelectedItem);
                ModEmpresa.ShowDialog();
            }
            else MessageBox.Show("No se ha elegido una empresa.");
        }
        private void btnBorrarEmpresa_Click(object sender, RoutedEventArgs e)
        {
            EmpresaABM.BorrarEmpresa((Empresa)lbEmpresas.SelectedItem);
        }
    }
}
