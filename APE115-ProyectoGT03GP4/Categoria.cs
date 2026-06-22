using System;
using System.Collections.Generic;
using System.Text;

namespace APE115_ProyectoGT03GP4
{
    internal class Categoria
    {
        public int IdCategoria { get; set; }
        public string Nombre { get; set; }
        public Categoria(int id, string nom)
        {
            this.IdCategoria = id;
            this.Nombre = nom;
        }
    }
}
