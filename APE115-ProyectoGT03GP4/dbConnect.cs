using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System.Text;
using MySqlConnector;

namespace APE115_ProyectoGT03GP4
{
    internal class dbConnect : IDisposable
    {
        private readonly string conString;
        private MySqlConnection conex;
        private bool disposed = false;

        public dbConnect()
        {
            var builder = new MySqlConnectionStringBuilder
            {
                Server = "localhost",
                Database = "inventariodb",
                UserID = "root",
                Password = ""
            };
            conString = builder.ConnectionString;
            conex = new MySqlConnection(conString);
        }

        /// <summary>
        /// Abre la conexión de forma asincrona a la base de datos.
        /// </summary>
        /// 

        public async Task<MySqlConnection> GetConnectionAsync()
        {
            if (conex.State == ConnectionState.Closed)
            {
                await conex.OpenAsync();
            }
            return conex;
        }

        public async Task<DataTable> ExecuteQueryAsync(string query, MySql.Data.MySqlClient.MySqlParameter mySqlParameter, MySqlParameter[] parameters = null)
        {
            var dataTable = new DataTable();
            var conn = await GetConnectionAsync();

            using (var cmd = new MySqlCommand(query, conn))
            {
                if (parameters != null)
                {
                    cmd.Parameters.AddRange(parameters);
                }
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    dataTable.Load(reader);
                }
            }
            return dataTable;
        }

        /// <summary>
        /// Ejecuta comandos que modifican datos (INSERT, UPDATE, DELETE)
        /// </summary>
        public async Task<int> ExecuteNonQueryAsync(string query, MySqlParameter[] parameters = null)
        {
            var conn = await GetConnectionAsync();

            using (var command = new MySqlCommand(query, conn))
            {
                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }

                return await command.ExecuteNonQueryAsync();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing && conex != null)
                {
                    if (conex != null)
                    {
                        conex.Dispose();
                        conex = null;
                    }
                }
                disposed = true;
            }
        }
    }
}
