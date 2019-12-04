using AccesoADatos;
using Entidades;
using Helpers;
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
            btnModificarDeuda.Visibility = Visibility.Collapsed;
        }

        public VentanaDeudaAM(Deuda deudaRecibida)
        {
            InitializeComponent();
            
            var deudores = DeudorABM.ListaDeudores();
            var empresas = EmpresaABM.listaEmpresas();
            deudaX = deudaRecibida;
            
            lbDeudores.ItemsSource = deudores;
            lbEmpresas.ItemsSource = empresas;
            
            lbDeudores.SelectedItem = deudores.Find(ob => ob.Dni == deudaRecibida.Deudor.Dni);
            lbEmpresas.SelectedItem = empresas.Find(ob => ob.Cuit == deudaRecibida.Empresa.Cuit);
            lbDeudores.IsEnabled = false;
            btnBuscarDeudores.IsEnabled = false;
            txbBuscarDeudores.IsEnabled = false;

            txbMonto.Text = deudaRecibida.Monto.ToString();
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
            if (Helpers.VerificarCampos.Verificarcaracteres(txbBuscarDeudores.Text) == true || Helpers.VerificarCampos.Verificarnum(txbBuscarDeudores.Text) == true || txbBuscarDeudores.Text == "")
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
            if (Helpers.VerificarCampos.Verificarcaracteres(txbBuscarEmpresas.Text) == true || Helpers.VerificarCampos.Verificarnum(txbBuscarEmpresas.Text) == true || txbBuscarEmpresas.Text == "")
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
                var cuitAnterior = deudaX.Empresa.Cuit;
                deudaX = new Deuda();
                deudaX.Deudor = ((Deudor) lbDeudores.SelectedItem);
                deudaX.Empresa = ((Empresa) lbEmpresas.SelectedItem);
                if (Helpers.VerificarCampos.Verificarnum(txbMonto.Text))
                {
                    deudaX.Monto = Convert.ToDouble(txbMonto.Text);
                    DeudaABM.ModificarDeuda(deudaX, cuitAnterior);
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
