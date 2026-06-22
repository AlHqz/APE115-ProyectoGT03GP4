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

        private dbConnect con = new dbConnect();

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

        public void setContraseña(string contraseña)
        {
            this.contraseña = Crypto.Encrypt(contraseña);
        }

        public string getContraseña()
        {

            return this.contraseña;
        }

        public async Task<List<string>> getUsuario(string username)
        {
            var datosUsuario = new List<string>();

            if (string.IsNullOrWhiteSpace(username)) return datosUsuario; // Retorna lista vacía

            string query = "SELECT nombreUsuario, contra, idTipoUsuario FROM usuario WHERE nombreUsuario = @username LIMIT 1;";

            var conn = await con.GetConnectionAsync();

            using (var command = new MySqlCommand(query))
            {
                command.Parameters.AddWithValue("@username", username.Trim());

                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        // Agregamos cada columna como un string a la lista
                        datosUsuario.Add(reader.GetString("nombreUsuario"));
                        datosUsuario.Add(reader.GetString("contra"));
                        datosUsuario.Add(reader.GetInt32("idTipoUsuario").ToString());
                    }
                }
            }

            return datosUsuario; // Si no lo encuentra, la lista tendrá 0 elementos (Count == 0)
        }


        /*public List[string,string] getUser(string usuario) { 

            List<string> users = new List<string>();


            dbConnect db = new dbConnect();
            db.GetConnectionAsync().Wait();

            DataTable dt = db.ExecuteQueryAsync("SELECT * FROM usuarios WHERE nombre_usuario = @usuario", new MySqlParameter("@usuario", usuario)).Result;

            while (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    users.Add(row["nombre_usuario"].ToString());
                    users.Add(row["contraseña"].ToString());

                }
            }

        }*/


    }
}
