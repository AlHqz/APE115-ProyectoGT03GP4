using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace APE115_ProyectoGT03GP4
{
    internal class VistasProducto
    {
        //Obtiene todas las categorías registradas en la BDD
        public static List<Categoria> Categorias()
        {
            string query = "SELECT * FROM Categoria";
            List<Categoria> categorias = new List<Categoria>();

            try
            {
                using (MySqlConnection conn = ConexionBD.Conectar())
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        categorias.Add(new Categoria(reader.GetInt32("idCategoria"), reader.GetString("nombreCategoria")));
                    }
                }
            }
            catch (MySqlException e)
            {
                throw new Exception("Error obteniendo categorías: " + e.Message);
            }
            return categorias;
        }
        //Obtiene la lista de todos los productos registrados
        public static List<Producto> ObtenerProductos()
        {
            List<Producto> lista = new List<Producto>();
            string query = "SELECT * FROM Producto";

            try
            {
                using (MySqlConnection conn = ConexionBD.Conectar())
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new Producto(
                            reader.GetInt32("codProducto"),
                            reader.GetString("descripcion"),
                            reader.GetDecimal("costo"),
                            reader.GetDecimal("precioVenta"),
                            reader.GetChar("estado"),
                            reader.GetInt32("stock"),
                            reader.GetInt32("stockMinimo"),
                            reader.GetInt32("idCategoria")
                        ));
                    }
                }
            }
            catch (MySqlException e)
            {
                throw new Exception("Error obteniendo categorías: " + e.Message);
            }
            return lista;
        }
        //Busca todos los productos registrados bajo la categoría indicada
        public static List<Producto> ObtenerPorCategoria(int idCategoria)
        {
            List<Producto> lista = new List<Producto>();
            string query = "SELECT * FROM Producto WHERE idCategoria = @idCat AND estado = 'A'";

            try
            {
                using (MySqlConnection conn = ConexionBD.Conectar())
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@idCat", idCategoria);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Producto(
                                reader.GetInt32("codProducto"),
                                reader.GetString("descripcion"),
                                reader.GetDecimal("costo"),
                                reader.GetDecimal("precioVenta"),
                                reader.GetChar("estado"),
                                reader.GetInt32("stock"),
                                reader.GetInt32("stockMinimo"),
                                reader.GetInt32("idCategoria")
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

        //Muestra todos los productos con un stock muy bajo que requieren reabastecimiento
        public static List<Producto> ObtenerInformeBajoStock()
        {
            List<Producto> lista = new List<Producto>();
            // Compara directamente el stock actual con el stockMinimo definido en la BD
            string query = "SELECT * FROM Producto WHERE stock <= stockMinimo AND estado = 'A'";

            try
            {
                using (MySqlConnection conn = ConexionBD.Conectar())
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new Producto(
                            reader.GetInt32("codProducto"),
                            reader.GetString("descripcion"),
                            reader.GetDecimal("costo"),
                            reader.GetDecimal("precioVenta"),
                            reader.GetChar("estado"),
                            reader.GetInt32("stock"),
                            reader.GetInt32("stockMinimo"),
                            reader.GetInt32("idCategoria")
                        ));
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al generar el informe de bajo stock: " + ex.Message);
            }
            return lista;
        }
    }
}
