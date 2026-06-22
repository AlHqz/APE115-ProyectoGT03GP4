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
using System.Windows.Shapes;

namespace APE115_ProyectoGT03GP4
{
    //Clase que controla la vista del administrador
    public partial class VistaAdmin : Window
    {
        //Definimos los atajos de teclado
        public static RoutedCommand NavegarStockCmd = new RoutedCommand();
        public static RoutedCommand NavegarInformeCmd = new RoutedCommand();
        public VistaAdmin()
        {
            InitializeComponent();

            //Enlazamos los comandos a sus respectivas acciones
            CommandBindings.Add(new CommandBinding(NavegarStockCmd, ExecutedNavegarStock));
            CommandBindings.Add(new CommandBinding(NavegarInformeCmd, ExecutedNavegarInforme));

            //Configurar la combinación de teclas
            InputBindings.Add(new KeyBinding(NavegarStockCmd, Key.Q, ModifierKeys.Control));
            InputBindings.Add(new KeyBinding(NavegarInformeCmd, Key.I, ModifierKeys.Control));
        }

        private void ExecutedNavegarStock(object sender, ExecutedRoutedEventArgs e)
        {
            Navegar("/Vistas/Stock.xaml");
        }

        private void ExecutedNavegarInforme(object sender, ExecutedRoutedEventArgs e)
        {
            Navegar("/Vistas/Informe.xaml");
        }

        private void NavegarAgregarCategoria(object sender, RoutedEventArgs e)
        {
            Navegar("/Vistas/Admin/AgregarCategoria.xaml");
        }
        private void NavegarEliminarCategoria(object sender, RoutedEventArgs e)
        {
            Navegar("/Vistas/Admin/EliminarCategoria.xaml");
        }
        private void NavegarAgregarProducto(object sender, RoutedEventArgs e)
        {
            Navegar("/Vistas/Admin/AgregarProducto.xaml");
        }
        private void NavegarEliminarProducto(object sender, RoutedEventArgs e)
        {
            Navegar("/Vistas/Admin/EliminarProducto.xaml");
        }
        //Método reutilizable para navegar a cualquier vista
        private void Navegar(string uri)
        {
            AreaContenido.Navigate(new Uri(uri, UriKind.Relative));
        }
    }
}
