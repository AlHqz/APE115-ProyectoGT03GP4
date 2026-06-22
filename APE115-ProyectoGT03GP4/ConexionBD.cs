using System;
using MySql.Data.MySqlClient;

namespace APE115_ProyectoGT03GP4
{
    //Clase que gestiona la conexión a la BDD, utilizando variables de entorno para la seguridad de las credenciales
    class ConexionBD
    {
        //Conecta a la base de datos y regresa la conexión
        public static MySqlConnection Conectar()
        {
            //Recupera los campos user y password del .env para formar la cadena de conexión
            string user = DotNetEnv.Env.GetString("MYSQL_USER");
            string password = DotNetEnv.Env.GetString("MYSQL_PASSWORD");
            string stringConexion = $"Server=localhost;Database=InventarioDB;Uid={user};Pwd={password}";
            //Intenta abrir la conexión con la cadena, regresando la conexión si es exitosa
            MySqlConnection conexion = new MySqlConnection(stringConexion);
            try
            {
                conexion.Open();
                return conexion;
            } catch (MySqlException ex)
            {
                throw new Exception("Error conectando a la base de datos: " + ex.Message);
            }
        }
    }
}
