using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Markup;

namespace APE115_ProyectoGT03GP4
{
    internal class Usuario
    {
        private string nombreUsuario;
        private string contraseña; // contraseña cifrada
        private int idTipoUsuario;

        public Usuario(string nombreUsuario, string contraseña, int idTipoUsuario,bool cyp)
        {
            this.nombreUsuario = nombreUsuario;
            setContraseña(contraseña,cyp); // Cifrar la contraseña al establecerla
            this.idTipoUsuario = idTipoUsuario;
        }

        public void setNombreUsuario(string nombre)
        {
            nombreUsuario = nombre;
        }

        public string getNombreUsuario()
        {
            return nombreUsuario;
        }

        public void setIdTipoUsuario(int idTipo)
        {
            idTipoUsuario = idTipo;
        }

        public int getIdTipoUsuario()
        {
            return idTipoUsuario;
        }

        public void setContraseña(string contraseña,bool cyphered)
        {
            if(cyphered)
            {
                this.contraseña = contraseña;
            }
            else
            {
                this.contraseña = Crypto.Encrypt(contraseña);
            }
            
        }

        public string getContraseña()
        {

            return this.contraseña;
        }

        public bool addUser(string nombreUsuario, string contraseña, int idTipoUsuario)
        {
            string query = "INSERT INTO usuario (nombreUsuario, contra, idTipoUsuario) VALUES (@nombreUsuario, @contra, @idTipoUsuario)";
            try
            {
                using (MySqlConnection conn = ConexionBD.Conectar())
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@nombreUsuario", nombreUsuario);
                    cmd.Parameters.AddWithValue("@contra", contraseña);
                    cmd.Parameters.AddWithValue("@idTipoUsuario", idTipoUsuario);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0; // Retorna true si se insertó al menos una fila
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar el usuario: " + ex.Message);
            }
        }

        
        public static List<Usuario> getUser(string user)
        {
            List<Usuario> lista = new List<Usuario>();
            string query = "SELECT * FROM usuario WHERE nombreUsuario = @user";

            try
            {
                using (MySqlConnection conn = ConexionBD.Conectar())
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@user", user);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Usuario(
                                reader.GetString("nombreUsuario"),
                                reader.GetString("contra"),
                                reader.GetInt32("idTipoUsuario"), 
                                true
                            ));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al filtrar por categoría: " + ex.Message);
            }
            return lista;
        }


    }
}
