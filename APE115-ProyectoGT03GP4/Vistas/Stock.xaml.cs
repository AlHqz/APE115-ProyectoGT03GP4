using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace APE115_ProyectoGT03GP4.Vistas
{
    public partial class Stock : Page
    {
        public Stock()
        {
            InitializeComponent();

            List<Producto> productos = VistasProducto.ObtenerProductos();
            DtgProductos.ItemsSource = productos;

            List<Categoria> categorias = VistasProducto.Categorias();
            categorias.Add(new Categoria(0, "Todas"));
            CmbCategoria.ItemsSource = categorias;
        }

        private void CmbCategoria_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Verificamos que realmente haya algo seleccionado para evitar errores null
            if (CmbCategoria.SelectedValue != null)
            {
                try
                {
                    int idCat = (int)CmbCategoria.SelectedValue;
                    if (idCat == 0)
                    {
                        List<Producto> noFiltrados = VistasProducto.ObtenerProductos();
                        DtgProductos.ItemsSource = noFiltrados;
                    }
                    else
                    {
                        List<Producto> filtrados = VistasProducto.ObtenerPorCategoria(idCat);
                        DtgProductos.ItemsSource = filtrados;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al filtrar: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        //Agrega stock según el artículo seleccionado y la cantidad ingresada
        private void AgregarStock(object sender, RoutedEventArgs e)
        {
            if(!String.IsNullOrEmpty(TxtCantidad.Text))
            {
                try
                {
                } catch (Exception ex)
                {
                    MessageBox.Show("Hubo un error modificando el stock: " + ex.Message);
                }
            }
        }
        //Retira stock según el artículo seleccionado y la cantidad ingresada
        private void RetirarStock(object sender, RoutedEventArgs e)
        {

        }
    }
}
