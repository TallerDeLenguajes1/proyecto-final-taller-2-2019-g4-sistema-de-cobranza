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
            deudaX = deuda;
            lbDeudores.ItemsSource = DeudorABM.ListaDeudores();
            lbEmpresas.ItemsSource = EmpresaABM.listaEmpresas();
            lbDeudores.SelectedItem = deuda.Deudor;
            lbEmpresas.SelectedItem = deuda.Empresa;
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
            lbDeudores.ItemsSource = DeudorABM.DeudorPorDniParecidos(txbBuscarDeudores.Text);
        }

        private void btnBuscarEmpresas_Click(object sender, RoutedEventArgs e)
        {
            lbEmpresas.ItemsSource = EmpresaABM.EmpresaPorNombre(txbBuscarEmpresas.Text);

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
    }
}
