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

namespace APE115_ProyectoGT03GP4.Vistas
{
    /// <summary>
    /// Interaction logic for AgregarUsuario.xaml
    /// </summary>
    /// 
    public class DatoComboTipoUsuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
    }
    public partial class AgregarUsuario : Window
    {
        public AgregarUsuario()
        {
            InitializeComponent();
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            List<TipoUsuario> datosTiposUsuario;
            List<DatoComboTipoUsuario> comboItems = new List<DatoComboTipoUsuario>();

            datosTiposUsuario = TipoUsuario.getTipoUsuario();

            if (datosTiposUsuario.Count > 0)
            {
                for (int i = 0; i < datosTiposUsuario.Count; i++)
                {
                    comboItems.Add(new DatoComboTipoUsuario
                    {
                        Id = datosTiposUsuario[i].idTipoUsuario,
                        Nombre = datosTiposUsuario[i].nombreTipoUsuario.ToString()
                    });
                }
                
            }

            cbTipoUsuario.ItemsSource = comboItems;
        }
    }
}
