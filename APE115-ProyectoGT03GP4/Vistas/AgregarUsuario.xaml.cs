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
            string usuario = txtusuario.Text;
            string contra = txtContra.Password;
            string contraR = txtContraR.Password;

            if (contra == contraR)
            {

                if (cbTipoUsuario.SelectedItem is TipoUsuario tipoSeleccionado)
                {
                    Usuario nuevoUsuario = new Usuario(usuario, contra, tipoSeleccionado.idTipoUsuario, false);
                    if (nuevoUsuario.addUser(nuevoUsuario.getNombreUsuario(), nuevoUsuario.getContraseña(), nuevoUsuario.getIdTipoUsuario()))
                    {
                        MessageBox.Show("Usuario agregado exitosamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                        LoginWindow lw = new LoginWindow();
                        lw.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Error al agregar el usuario.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Por favor, seleccione un tipo de usuario.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                }

            }
            else
            {
                MessageBox.Show("Las contraseñas no coinciden. Por favor, inténtelo de nuevo.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            List<TipoUsuario> datosTiposUsuario;
            List<DatoComboTipoUsuario> comboItems = new List<DatoComboTipoUsuario>();

            datosTiposUsuario = TipoUsuario.getTipoUsuario();

            if (datosTiposUsuario.Count > 0)
            {
                cbTipoUsuario.ItemsSource = datosTiposUsuario;
            }

            
        }
    }
}
