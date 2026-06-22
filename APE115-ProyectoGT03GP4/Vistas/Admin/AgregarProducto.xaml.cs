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

namespace APE115_ProyectoGT03GP4.Vistas.Admin
{
    //Clase para controlar la vista de agregar un producto
    public partial class AgregarProducto : Page
    {
        public AgregarProducto()
        {
            InitializeComponent();

            List<Categoria> categorias = VistasProducto.Categorias();
            CmbCategoria.ItemsSource = categorias;
        }
        //Al momento de hacer click en el botón, se toma toda la información ingresada y se ingresa el producto
        private void BtnGuardar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(TxtDescripcion.Text) || CmbCategoria.SelectedValue == null)
                {
                    MessageBox.Show("Por favor complete los campos obligatorios.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                string descripcion = TxtDescripcion.Text;
                int idCat = (int)CmbCategoria.SelectedValue;
                decimal costo = decimal.Parse(TxtCosto.Text);
                decimal precio = decimal.Parse(TxtPrecio.Text);
                int stock = int.Parse(TxtStock.Text);
                int stockMin = int.Parse(TxtStockMinimo.Text);

                GestionAdmin.AgregarProducto(descripcion, idCat, costo, precio, stock, stockMin);

                LimpiarCampos();
            }
            catch (FormatException)
            {
                MessageBox.Show("Asegúrese de que el costo, precio y stock sean números válidos.", "Error de Formato", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        //Limpia todos los campos
        private void LimpiarCampos()
        {
            TxtDescripcion.Clear();
            TxtCosto.Clear();
            TxtPrecio.Clear();
            TxtStock.Clear();
            TxtStockMinimo.Clear();
            CmbCategoria.SelectedIndex = -1;
            TxtDescripcion.Focus();
        }
    }
}
