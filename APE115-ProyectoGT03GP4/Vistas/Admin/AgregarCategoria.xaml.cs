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
    /// <summary>
    /// Lógica de interacción para AgregarCategoria.xaml
    /// </summary>
    public partial class AgregarCategoria : Page
    {
        public AgregarCategoria()
        {
            InitializeComponent();
        }

        private void BtnGuardarCategoria_Click(object sender, RoutedEventArgs e)
        {
            if(!String.IsNullOrEmpty(TxtNombreCategoria.Text))
            {
                GestionAdmin.AgregarCategoria(TxtNombreCategoria.Text);
            }
        }
    }
}
