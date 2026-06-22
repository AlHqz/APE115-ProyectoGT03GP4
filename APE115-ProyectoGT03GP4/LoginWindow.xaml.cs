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
            Task<List<string>> datosUsuario;

            Usuario user = new Usuario();

            datosUsuario = user.getUsuario(txtUsuario.Text);

            if (datosUsuario.Result.Count == 1)
            {
                user.setNombreUsuario(datosUsuario.Result[0]);
                user.setContraseña(datosUsuario.Result[1]);
                user.setIdTipoUsuario(int.Parse(datosUsuario.Result[2]));

                if (datosUsuario.Result[0] == txtUsuario.Text && datosUsuario.Result[1] == Crypto.Encrypt(txtContra.Password))
                {
                    switch (user.getIdTipoUsuario())
                    {
                        case 1:
                            MessageBox.Show("¡Bienvenido " + user.getNombreUsuario() + "!", "Login exitoso", MessageBoxButton.OK, MessageBoxImage.Information);
                            VistaAdmin adminWindow = new VistaAdmin();
                            adminWindow.Show();
                            this.Close();
                            break;
                        case 2:
                            MessageBox.Show("¡Bienvenido " + user.getNombreUsuario() + "!", "Login exitoso", MessageBoxButton.OK, MessageBoxImage.Information);
                            VistaEmpleado EmpleadoWindow = new VistaEmpleado();
                            EmpleadoWindow.Show();
                            this.Close();
                            break;
                        
                    }
                    
                }
                else
                {
                    MessageBox.Show("Usuario o contraseña incorrectos.", "Error de login", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
