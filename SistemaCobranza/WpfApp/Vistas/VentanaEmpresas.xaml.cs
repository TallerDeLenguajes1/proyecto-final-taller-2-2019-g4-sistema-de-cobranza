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
            if(usuarioactual.nivel != 1)
            {
                btnBorrarEmpresa.Visibility = Visibility.Collapsed;
            }
            else if(usuarioactual.nivel == 3)
            {
                btnModEmpresa.Visibility = Visibility.Collapsed;
            }
            empresas = EmpresaABM.listaEmpresas();
            foreach (Empresa x in empresas)
            {
                lbEmpresas.Items.Add(x.ToString());
            }
        }
        private void btnBuscarEmpresas_Click(object sender, RoutedEventArgs e)
        {
            if (rdbCuit.IsChecked.Value)
            {
                if (Helpers.VerificarCampos.Verificarnum(txbBuscarEmpresas.Text))
                {
                    empresaX = EmpresaABM.EmpresaPorCuit(txbBuscarEmpresas.Text);
                    lbEmpresas.ItemsSource = null;
                    lbEmpresas.Items.Clear();
                    lbEmpresas.Items.Add(empresaX);
                }
                else MessageBox.Show("Cuit debe ser sólo numeros tal vez quiso buscar por nombre?.");
               
            }
            else
            {
                empresas = EmpresaABM.EmpresaPorNombre(txbBuscarEmpresas.Text);
                lbEmpresas.ItemsSource = null;
                lbEmpresas.Items.Clear();
                lbEmpresas.Items.Add(empresaX);
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
                empresaX = CadenaAEntidad.StringToEmpresa(lbEmpresas.SelectedItem.ToString());
                VentanaEmpresaAM ModEmpresa = new VentanaEmpresaAM(empresaX);
                ModEmpresa.ShowDialog();
            }
            else MessageBox.Show("No se ha elegido una empresa.");
        }

        private void btnBorrarEmpresa_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
