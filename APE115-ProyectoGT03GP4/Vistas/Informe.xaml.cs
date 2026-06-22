using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace APE115_ProyectoGT03GP4.Vistas
{
    /// <summary>
    /// Lógica de interacción para Informe.xaml
    /// </summary>
    public partial class Informe : Page
    {
        public Informe()
        {
            InitializeComponent();

            CargarInforme();

            List<Categoria> categorias = VistasProducto.Categorias();
            CmbCategoriaInforme.ItemsSource = categorias;
        }
        //Obtiene todos los productos con un stock menor al mínimo y los muestra en pantalla
        private void CargarInforme()
        {
            List<Producto> listaCritica = VistasProducto.ObtenerInformeBajoStock();

            GridInformeBajoStock.ItemsSource = listaCritica;
            TxtTotalAlertas.Text = listaCritica.Count.ToString();
        }
        //Método que permite filtrar categorías
        private void CmbCategoriaInforme_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CmbCategoriaInforme.SelectedValue != null)
            {
                try
                {
                    int idCat = (int)CmbCategoriaInforme.SelectedValue;

                    //Recupera todos los productos con bajo stock
                    List<Producto> listaCompletaBajoStock = VistasProducto.ObtenerInformeBajoStock();
                    //Filtra los productos por categoría
                    List<Producto> listaFiltrada = listaCompletaBajoStock.Where(p => p.IdCategoria == idCat).ToList();

                    GridInformeBajoStock.ItemsSource = listaFiltrada;
                    TxtTotalAlertas.Text = listaFiltrada.Count.ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al aplicar el filtro: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        //Limpia el filtro de categoría
        private void BtnLimpiarFiltro_Click(object sender, RoutedEventArgs e)
        {
            CmbCategoriaInforme.SelectedIndex = -1;
            CargarInforme();
        }
        //Genera el imprimible del reporte
        private void BtnImprimir_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                PrintDialog pf = new PrintDialog();

                if (pf.ShowDialog() == true)
                {
                    //Oculta los botones para evitar interferencia
                    BtnImprimir.Visibility = Visibility.Collapsed;
                    BtnLimpiarFiltro.Visibility = Visibility.Collapsed;
                    CmbCategoriaInforme.Visibility = Visibility.Collapsed;

                    //Manda a imprimir todo lo visible
                    pf.PrintVisual(this, "Informe de Bajo Stock - Ferretería");

                    //Se vuelven a mostrar los botones
                    BtnImprimir.Visibility = Visibility.Visible;
                    BtnLimpiarFiltro.Visibility = Visibility.Visible;
                    CmbCategoriaInforme.Visibility = Visibility.Visible;

                    MessageBox.Show("El informe se ha enviado a la cola de impresión con éxito.",
                                    "Informe Generado", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al intentar generar el archivo físico: " + ex.Message,
                                "Error de Impresión", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
