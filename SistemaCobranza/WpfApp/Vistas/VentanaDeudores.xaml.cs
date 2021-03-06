﻿using System;
using AccesoADatos;
using Entidades;
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
    /// Lógica de interacción para Deudores.xaml
    /// </summary>
        public partial class Deudores : Window
    {
        public Deudores(Usuario usuarioactual)
        {
            InitializeComponent();
            if(usuarioactual.Nivel == 3)
            {
                btnBorrarDeudores.Visibility = Visibility.Collapsed;
                btnModDeudores.Visibility = Visibility.Collapsed;
                btnAltaDeudores.Visibility = Visibility.Collapsed;
            }
            else if(usuarioactual.Nivel == 2) 
            {
                btnBorrarDeudores.Visibility = Visibility.Collapsed;
            }
            Refresh();
        }

        private void btnAltaDeudores_Click(object sender, RoutedEventArgs e)
        {
            VentanaDeudorAM AltaDeudor = new VentanaDeudorAM();
            AltaDeudor.ShowDialog();
            Refresh();
        }

        private void btnModDeudores_Click(object sender, RoutedEventArgs e)
        {
            if (lbDeudores.SelectedItem != null)
            {
                Deudor deudorX =(Deudor) lbDeudores.SelectedItem; 
                VentanaDeudorAM ModDeudor = new VentanaDeudorAM(deudorX);
                ModDeudor.ShowDialog();
                Refresh();
            }
            else MessageBox.Show("Debe seleccionar un deudor.");
        }
        private void btnBorrarDeudores_Click(object sender, RoutedEventArgs e)
        {
            if (lbDeudores.SelectedItem != null)
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("Esta seguro que desea eliminar el deudor?", "Confirmacion Borrar", System.Windows.MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    DeudorABM.BorrarDeudor((Deudor)lbDeudores.SelectedItem);
                    Refresh();
                }
            }
            else MessageBox.Show("Debe seleccionar un deudor.");
        }
        private void Refresh()
        {
            lbDeudores.ItemsSource = DeudorABM.ListaDeudores();
        }
        private void btnBuscarDeudores_Click(object sender, RoutedEventArgs e)
        {

            if (rdbDni.IsChecked.Value == true)
            {
                if (Helpers.VerificarCampos.Verificarnum(txbBuscarDeudores.Text) == true) lbDeudores.ItemsSource = DeudorABM.DeudorPorAtributo("dni", txbBuscarDeudores.Text);
                else lblNoticia.Content = "Dni debe contener sólo numeros.";
            }
            else if(rdbNombre.IsChecked.Value == true)
            {
                if (Helpers.VerificarCampos.Verificarcaracteres(txbBuscarDeudores.Text) == true) lbDeudores.ItemsSource = DeudorABM.DeudorPorAtributo("nombre", txbBuscarDeudores.Text);
                else lblNoticia.Content = "Cuit debe contener sólo numeros.";
            }
            if (txbBuscarDeudores.Text == "")
            {
                Refresh();
            }

            lblNoticia.Content = "Se han encontrado " + lbDeudores.Items.Count + " coincidencias.";
        }
    }
}
