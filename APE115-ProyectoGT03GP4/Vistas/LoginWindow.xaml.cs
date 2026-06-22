using APE115_ProyectoGT03GP4.Vistas;
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
    /// <summary>
    /// Lógica de interacción para LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            List<Usuario> datosUsuario;

            

            datosUsuario = Usuario.getUser(txtUsuario.Text);

            if (datosUsuario.Count == 1)
            {
                
                if (datosUsuario[0].getNombreUsuario() == txtUsuario.Text && Crypto.Decrypt(datosUsuario[0].getContraseña()) == txtContra.Password)
                {
                    switch (datosUsuario[0].getIdTipoUsuario())
                    {
                        case 1:
                            MessageBox.Show("¡Bienvenido " + datosUsuario[0].getNombreUsuario() + "!", "Login exitoso", MessageBoxButton.OK, MessageBoxImage.Information);
                            VistaAdmin adminWindow = new VistaAdmin();
                            adminWindow.Show();
                            this.Close();
                            break;
                        case 2:
                            MessageBox.Show("¡Bienvenido " + datosUsuario[0].getNombreUsuario() + "!", "Login exitoso", MessageBoxButton.OK, MessageBoxImage.Information);
                            VistaEmpleado EmpleadoWindow = new VistaEmpleado();
                            EmpleadoWindow.Show();
                            this.Close();
                            break;
                        
                    }
                    
                }
                else
                {
                    //txtUsuario.Text = Crypto.Encrypt(txtContra.Password);
                    MessageBox.Show("Usuario o contraseña incorrectos. ", "Error de login", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnRegistrar_Click(object sender, RoutedEventArgs e)
        {
            

        }

        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            AgregarUsuario agregarUsuarioWindow = new AgregarUsuario();
            agregarUsuarioWindow.Show();
            this.Close();
        }
    }
}
