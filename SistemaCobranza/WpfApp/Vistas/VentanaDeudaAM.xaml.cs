using AccesoADatos;
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

namespace WpfApp.Vistas
{
    /// <summary>
    /// Lógica de interacción para VentanaDeudaAM.xaml
    /// </summary>
    public partial class VentanaDeudaAM : Window
    {
        Deuda deudaX;
        public VentanaDeudaAM()
        {
            InitializeComponent();
            lbDeudores.ItemsSource = DeudorABM.ListaDeudores();
            lbEmpresas.ItemsSource = EmpresaABM.listaEmpresas();
        }

        public VentanaDeudaAM(Deuda deuda)
        {
            InitializeComponent();
            lbDeudores.ItemsSource = DeudorABM.ListaDeudores();
            lbEmpresas.ItemsSource = EmpresaABM.listaEmpresas();
            lbDeudores.SelectedItem = deuda.Deudor; //arreglar
            lbEmpresas.SelectedItem = deuda.Empresa; //arreglar
            txbMonto.Text = deuda.Monto.ToString();
            btnGuardarDeuda.Visibility = Visibility.Collapsed; 
        }
        private void btnGuardarDeuda_Click(object sender, RoutedEventArgs e)
        {
            if (lbDeudores.SelectedItem != null && lbEmpresas.SelectedItem != null)
            {
                deudaX = new Deuda();
                deudaX.Deudor = ((Deudor)lbDeudores.SelectedItem);
                deudaX.Empresa = ((Empresa)lbEmpresas.SelectedItem);
                if (Helpers.VerificarCampos.Verificarnum(txbMonto.Text))
                {
                    deudaX.Monto = Convert.ToDouble(txbMonto.Text);
                    DeudaABM.InsertarDeuda(deudaX);
                    this.Close();
                }
                else MessageBox.Show("Monto debe ser Numérico.");
            }
            else MessageBox.Show("Debe elegir al menos un Deudor y una Empresa.");
        }
        private void BtnBuscarDeudores_Click(object sender, RoutedEventArgs e)
        {
            if (Helpers.VerificarCampos.Verificarcaracteres(txbBuscarDeudores.Text) == true || Helpers.VerificarCampos.Verificarnum(txbBuscarDeudores.Text) == true)
            {
                if (rdbDni.IsChecked == true) lbDeudores.ItemsSource = DeudorABM.DeudorPorAtributo("dni", txbBuscarDeudores.Text);
                else lbDeudores.ItemsSource = DeudorABM.DeudorPorAtributo("nombre", txbBuscarDeudores.Text);
            }
            else lbNoticiaDeudor.Content = "Dni sólo numero y Nombre sólo letras.";
            if (lbDeudores.Items.Count == 0) lbNoticiaDeudor.Content = "No Match.";
            else lbNoticiaDeudor.Content = "Se han encontrado "+ lbDeudores.Items.Count +" Coincidencias.";
        }

        private void btnBuscarEmpresas_Click(object sender, RoutedEventArgs e)
        {
            if (Helpers.VerificarCampos.Verificarcaracteres(txbBuscarDeudores.Text) == true || Helpers.VerificarCampos.Verificarnum(txbBuscarDeudores.Text) == true)
            {
                if (rdbCuit.IsChecked == true) lbEmpresas.ItemsSource = EmpresaABM.EmpresasPorAtributo("cuit", txbBuscarEmpresas.Text);
                else lbEmpresas.ItemsSource = EmpresaABM.EmpresasPorAtributo("nombre", txbBuscarEmpresas.Text);
            }
            else lbNoticiaEmpresa.Content = "Dni sólo numero y Nombre sólo letras.";
            if (lbEmpresas.Items.Count == 0) lbNoticiaEmpresa.Content = "No Match.";
            else lbNoticiaEmpresa.Content = "Se han encontrado " + lbEmpresas.Items.Count + " Coincidencias.";
            
        }

        private void btnModificarDeuda_Click(object sender, RoutedEventArgs e)
        {
            if (lbDeudores.SelectedItem != null && lbEmpresas.SelectedItem != null)
            {
                deudaX = new Deuda();
                deudaX.Deudor = ((Deudor) lbDeudores.SelectedItem);
                deudaX.Empresa = ((Empresa) lbEmpresas.SelectedItem);
                if (Helpers.VerificarCampos.Verificarnum(txbMonto.Text))
                {
                    deudaX.Monto = Convert.ToDouble(txbMonto.Text);
                    DeudaABM.ModificarDeuda(deudaX);
                    this.Close();
                }
                else MessageBox.Show("Monto debe ser Numérico.");
            }
            else MessageBox.Show("Debe elegir al menos un Deudor y una Empresa.");
         
        }

        private void txbBuscarEmpresas_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) btnBuscarEmpresas_Click(null,null);
        }
    }
}
