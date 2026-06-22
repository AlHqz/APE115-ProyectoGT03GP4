using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace APE115_ProyectoGT03GP4.Vistas
{
    public partial class Stock : Page
    {
        public static RoutedCommand EnfocarBuscadorCmd = new RoutedCommand();
        public Stock()
        {
            InitializeComponent();

            CommandBindings.Add(new CommandBinding(EnfocarBuscadorCmd, ExecutedEnfocarBuscador));
            InputBindings.Add(new KeyBinding(EnfocarBuscadorCmd, Key.F, ModifierKeys.Control));

            RefrescarProductos();

            List<Categoria> categorias = VistasProducto.Categorias();
            categorias.Add(new Categoria(0, "Todas"));
            CmbCategoria.ItemsSource = categorias;
        }
        //Método que refresca el DataGridView
        private void RefrescarProductos()
        {
            List<Producto> productos = VistasProducto.ObtenerProductos();
            DtgProductos.ItemsSource = productos;
        }
        //Activa el buscador con la combinación de teclas
        private void ExecutedEnfocarBuscador(object sender, ExecutedRoutedEventArgs e)
        {
            TxtProducto.Focus();
            TxtProducto.SelectAll();
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
                        RefrescarProductos();
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
        //Busca un producto específico
        private void BuscarProducto(object sender, RoutedEventArgs e)
        {
            if(!String.IsNullOrEmpty(TxtProducto.Text))
            {
                List<Producto> productos = VistasProducto.BuscarProducto(TxtProducto.Text);
                DtgProductos.ItemsSource = productos;
            }
        }
        //Agrega stock según el artículo seleccionado y la cantidad ingresada
        private void AgregarStock(object sender, RoutedEventArgs e)
        {
            if(!String.IsNullOrEmpty(TxtCantidad.Text) && DtgProductos.SelectedItem != null)
            {
                try
                {
                    Producto producto = (Producto)DtgProductos.SelectedItem;
                    int idProducto = producto.IdProducto;
                    int cantidad = int.Parse(TxtCantidad.Text);
                    if(cantidad >= 0)
                    {
                        VistasProducto.AgregarStock(idProducto, cantidad);
                        MessageBox.Show($"{cantidad} existencias ingresadas correctamente del artículo '{producto.Descripcion}'", "Stock Agregado", MessageBoxButton.OK, MessageBoxImage.Information);
                        RefrescarProductos();
                    }
                } catch (Exception ex)
                {
                    MessageBox.Show("Ingrese un dato válido para cantidad: " + ex.Message, "Error de formato", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Por favor ingrese una cantidad y seleccione un artículo.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        //Retira stock según el artículo seleccionado y la cantidad ingresada
        private void RetirarStock(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(TxtCantidad.Text) && DtgProductos.SelectedItem != null)
            {
                try
                {
                    Producto producto = (Producto)DtgProductos.SelectedItem;
                    int idProducto = producto.IdProducto;
                    int cantidad = int.Parse(TxtCantidad.Text);
                    if (cantidad >= 0 && (producto.Stock - cantidad) > 0)
                    {
                        VistasProducto.RetirarStock(idProducto, cantidad);
                        MessageBox.Show($"{cantidad} existencias retiradas correctamente del artículo '{producto.Descripcion}'", "Stock Retirado", MessageBoxButton.OK, MessageBoxImage.Information);
                        if ((producto.Stock - cantidad) < producto.StockMinimo)
                            MessageBox.Show($"El stock del artículo {producto.Descripcion} bajó por debajo del mínimo de {producto.StockMinimo}, con {producto.Stock - cantidad} unidades.", "Alerta", MessageBoxButton.OK, MessageBoxImage.Warning);
                        RefrescarProductos();
                    }
                    else
                        MessageBox.Show("Error, el stock no puede ser menor a 0.", "Error", MessageBoxButton.OK , MessageBoxImage.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ingrese un dato válido para cantidad: " + ex.Message, "Error de formato", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Por favor ingrese una cantidad y seleccione un artículo.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
