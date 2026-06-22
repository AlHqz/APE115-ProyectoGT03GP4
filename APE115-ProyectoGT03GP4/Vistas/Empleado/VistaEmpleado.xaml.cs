using System;
using System.Windows;
using System.Windows.Input;

namespace APE115_ProyectoGT03GP4
{
    //Clase que controla la lógica del menú universal
    public partial class VistaEmpleado : Window
    {
        //Definimos los atajos de teclado
        public static RoutedCommand NavegarStockCmd = new RoutedCommand();
        public static RoutedCommand NavegarGestionCmd = new RoutedCommand();
        public static RoutedCommand NavegarInformeCmd = new RoutedCommand();
        public VistaEmpleado()
        {
            InitializeComponent();

            //Enlazamos los comandos a sus respectivas acciones
            CommandBindings.Add(new CommandBinding(NavegarStockCmd, ExecutedNavegarStock));
            CommandBindings.Add(new CommandBinding(NavegarGestionCmd, ExecutedNavegarGestion));
            CommandBindings.Add(new CommandBinding(NavegarInformeCmd, ExecutedNavegarInforme));

            //Configurar la combinación de teclas
            InputBindings.Add(new KeyBinding(NavegarStockCmd, Key.Q, ModifierKeys.Control));
            InputBindings.Add(new KeyBinding(NavegarGestionCmd, Key.G, ModifierKeys.Control));
            InputBindings.Add(new KeyBinding(NavegarInformeCmd, Key.I, ModifierKeys.Control));
        }
        //Navegación con los atajos
        private void ExecutedNavegarStock(object sender, ExecutedRoutedEventArgs e)
        {
            Navegar("/Vistas/Stock.xaml");
        }

        private void ExecutedNavegarInforme(object sender, ExecutedRoutedEventArgs e)
        {
            Navegar("/Vistas/Informe.xaml");
        }

        private void ExecutedNavegarGestion(object sender, ExecutedRoutedEventArgs e)
        {
            Navegar("/Vistas/Empleado/Gestion.xaml");
        }
        //Método reutilizable para navegar a cualquier vista
        private void Navegar(string uri)
        {
            AreaContenido.Navigate(new Uri(uri, UriKind.Relative));
        }
    }
}
