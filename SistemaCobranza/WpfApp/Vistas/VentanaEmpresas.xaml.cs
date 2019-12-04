﻿using System;
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
        public VentanaEmpresas(Usuario usuarioactual)
        {
            InitializeComponent();
            if(usuarioactual.Nivel != 1)
            {
                btnBorrarEmpresa.Visibility = Visibility.Collapsed;
            }
            if(usuarioactual.Nivel == 3)
            {
                btnAltaEmpresa.Visibility = Visibility.Collapsed;
                btnModEmpresa.Visibility = Visibility.Collapsed;
            }
            Refresh();
        }
        private void Refresh()
        {
            lbEmpresas.ItemsSource = EmpresaABM.listaEmpresas();
        }
        private void btnBuscarEmpresas_Click(object sender, RoutedEventArgs e)
        {
            if (rdbCuit.IsChecked.Value)
            {
                if (VerificarCampos.Verificarnum(txbBuscarEmpresas.Text)) lbEmpresas.ItemsSource = EmpresaABM.EmpresasPorAtributo("cuit",txbBuscarEmpresas.Text);
                else lblNoticia.Content = "Dni debe contener sólo numeros.";
            }
            else
            {
                if (Helpers.VerificarCampos.Verificarcaracteres(txbBuscarEmpresas.Text) == true) lbEmpresas.ItemsSource = EmpresaABM.EmpresasPorAtributo("nombre", txbBuscarEmpresas.Text);
                else lblNoticia.Content = "Usuario debe contener sólo letras.";  
            }
            if (txbBuscarEmpresas.Text == "")
            {
                Refresh();
            }
            lblNoticia.Content = "Se han encontrado " + lbEmpresas.Items.Count + " Empresas.";
        }
        private void btnAltaEmpresa_Click(object sender, RoutedEventArgs e)
        {
            VentanaEmpresaAM AltaEmpresa = new VentanaEmpresaAM();
            AltaEmpresa.ShowDialog();
            Refresh();
        }

        private void btnModEmpresa_Click(object sender, RoutedEventArgs e)
        {
            if (lbEmpresas.SelectedItem != null)
            { 
                VentanaEmpresaAM ModEmpresa = new VentanaEmpresaAM((Empresa)lbEmpresas.SelectedItem);
                ModEmpresa.ShowDialog();
                Refresh();
            }
            else MessageBox.Show("No se ha elegido una empresa.");
        }
        private void btnBorrarEmpresa_Click(object sender, RoutedEventArgs e)
        {
            if (lbEmpresas.SelectedItem != null)
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("Esta seguro que desea eliminar el Empresa? Esta accion borrara todas las deudas asociadas a esta", "Confirmacion Borrar", System.Windows.MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    EmpresaABM.BorrarEmpresa((Empresa)lbEmpresas.SelectedItem);
            Refresh();
                }
            }
            else MessageBox.Show("Debe seleccionar una Empresa.");
        }
    }
}
