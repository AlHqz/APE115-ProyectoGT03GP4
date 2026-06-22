using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace APE115_ProyectoGT03GP4.Vistas.Admin
{
    internal class GestionAdmin
    {
        public static void AgregarProducto(string desc, int idCat, decimal costo, decimal precio, int stock, int stockMin)
        {
            string query = "INSERT INTO Producto (descripcion, idCategoria, costo, precioVenta, estado, stock, stockMinimo) " +
                           "VALUES (@desc, @cat, @cos, @pre, 'A', @stk, @min)";
            try
            {
                using (MySqlConnection conn = ConexionBD.Conectar())
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@desc", desc.Trim());
                    cmd.Parameters.AddWithValue("@cat", idCat);
                    cmd.Parameters.AddWithValue("@cos", costo);
                    cmd.Parameters.AddWithValue("@pre", precio);
                    cmd.Parameters.AddWithValue("@stk", stock);
                    cmd.Parameters.AddWithValue("@min", stockMin);
                    cmd.ExecuteNonQuery();
                }
                MessageBox.Show("Producto agregado exitosamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information); 
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error al insertar producto en la BD: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public static void AgregarCategoria(string nombre)
        {
            string query = "INSERT INTO Categoria (nombreCategoria) VALUES (@nombre)";

            try
            {
                using (MySqlConnection conn = ConexionBD.Conectar())
                {
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@nombre", nombre.Trim());
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Categoría ingresada con éxito.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
            catch (MySqlException ex)
            {
                if (ex.Number == 1062)
                {
                    MessageBox.Show("Esta categoría ya se encuentra registrada en el sistema.", "Error", MessageBoxButton.OK, MessageBoxImage.Stop);
                }
                MessageBox.Show("Error crítico en la base de datos al registrar: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public static void EliminarCategoria(int idCat)
        {
            string query = "DELETE FROM Categoria WHERE idCategoria = @id";

            try
            {
                using (MySqlConnection conn = ConexionBD.Conectar())
                {
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", idCat);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Categoría eliminada con éxito.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error crítico en la base de datos al eliminar: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public static void EliminarProducto(int idProducto)
        {
            string query = "UPDATE Producto SET estado = 'I' WHERE codProducto = @id";

            try
            {
                using (MySqlConnection conn = ConexionBD.Conectar())
                {
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", idProducto);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Producto eliminado con éxito.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error crítico en la base de datos al eliminar: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
