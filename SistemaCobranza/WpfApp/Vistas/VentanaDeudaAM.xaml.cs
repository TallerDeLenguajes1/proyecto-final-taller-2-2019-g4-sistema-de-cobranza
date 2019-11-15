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
        Deudor deudorX;
        Empresa empresaX;
        List<Deudor> deudores;
        List<Empresa> empresas;

        public VentanaDeudaAM()
        {
            InitializeComponent();

            deudores = DeudorABM.ListaDeudores();
            empresas = EmpresaABM.listaEmpresas();

            foreach (var item in deudores)
            {
                string str = item.ApellidoNombre + "/" + item.Dni + " / " + item.Telefono;
                lbDeudores.Items.Add(str);
                lbDeudores.Items.Refresh();
            }
            foreach (var item in empresas)
            {
                string str = item.Nombre + "/" + item.Cuit;
                lbEmpresas.Items.Add(str);
                lbEmpresas.Items.Refresh();
            }

        }

        public VentanaDeudaAM(Deuda deuda)
        {
            InitializeComponent();
            deudaX = deuda;
            deudores = DeudorABM.ListaDeudores();
            empresas = EmpresaABM.listaEmpresas();

            foreach (var item in deudores)
            {
                string str = item.ApellidoNombre + "/" + item.Dni + " / " + item.Telefono;
                lbDeudores.Items.Add(str);
                lbDeudores.Items.Refresh();
            }
            foreach (var item in empresas)
            {
                string str = item.Nombre + "/" + item.Cuit;
                lbEmpresas.Items.Add(str);
                lbEmpresas.Items.Refresh();
            }

        }

        private void btnGuardarDeuda_Click(object sender, RoutedEventArgs e)
        {

            if (deudorX != null && empresaX != null && txbMonto.Text != "")
            {
                deudaX.Deudor = deudorX;
                deudaX.Empresa = empresaX;
                deudaX.Monto = Convert.ToDouble(txbMonto.Text);
             //   DeudaABM.InsertarDeuda(deudaX);
            }

        }

        private void BtnBuscarDeudores_Click(object sender, RoutedEventArgs e)
        {
            deudorX = DeudorABM.DeudorPorDni(txbBuscarDeudores.Text);
            string str = deudorX.ApellidoNombre + "/" + deudorX.Dni + "/" + deudorX.Telefono;
            lbDeudores.Items.Clear();
            lbDeudores.Items.Add(str);
            lbDeudores.Items.Refresh();
        }

        private void btnBuscarEmpresas_Click(object sender, RoutedEventArgs e)
        {
            empresaX = EmpresaABM.EmpresaPorCuit(txbBuscarEmpresas.Text);
            string str = empresaX.Nombre + "/" + empresaX.Cuit;
            lbDeudores.Items.Clear();
            lbDeudores.Items.Add(str);
            lbDeudores.Items.Refresh();
        }

        private void LbDeudores_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            deudorX = (Deudor)(lbDeudores.SelectedItem);
        }

        private void LbEmpresas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            empresaX = (Empresa)(lbEmpresas.SelectedItem);
        }
    }
}
