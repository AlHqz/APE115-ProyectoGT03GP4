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
//Vista para eliminar una categoría
namespace APE115_ProyectoGT03GP4.Vistas.Admin
{
    public partial class EliminarCategoria : Page
    {
        public EliminarCategoria()
        {
            InitializeComponent();

            List<Categoria> categorias = VistasProducto.Categorias();
            DtgCategorias.ItemsSource = categorias;
        }

        private void BtnBorrar_Click(object sender, RoutedEventArgs e)
        {
            if(DtgCategorias.SelectedItem != null)
            {
                Categoria cat = (Categoria)DtgCategorias.SelectedItem;
                GestionAdmin.EliminarCategoria(cat.IdCategoria);

                List<Categoria> categorias = VistasProducto.Categorias();
                DtgCategorias.ItemsSource = categorias;
            }
        }
    }
}
