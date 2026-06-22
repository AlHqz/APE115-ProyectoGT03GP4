using MySql.Data.MySqlClient;

namespace APE115_ProyectoGT03GP4
{
    internal class TipoUsuario
    {
        public int idTipoUsuario { get; set; }
        public string nombreTipoUsuario { get; set; }

        public TipoUsuario(int idTipoUsuario, string nombreTipoUsuario)
        {
            this.idTipoUsuario = idTipoUsuario;
            this.nombreTipoUsuario = nombreTipoUsuario;
        }

        public static List<TipoUsuario> getTipoUsuario()
        {
            List<TipoUsuario> lista = new List<TipoUsuario>();
            string query = "SELECT * FROM TipoUsuario";
            try
            {
                using (MySqlConnection conn = ConexionBD.Conectar())
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new TipoUsuario(
                            reader.GetInt32("idTipoUsuario"),
                            reader.GetString("nombreTipoUsuario")
                        ));
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los usuarios: " + ex.Message);
            }
            return lista;
        }
    }
}