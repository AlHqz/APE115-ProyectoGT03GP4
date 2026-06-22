using System;

namespace APE115_ProyectoGT03GP4
{
    //Clase plantilla que representa un producto
    class Producto
    {
        //Campos que componen el producto, según su estructura en la BDD
        public int idProducto { get; set; }
        public string descripcion { get; set; }
        public decimal costo { get; set; }
        public decimal precio { get; set; }
        public char estado { get; set; }
        public int stock { get; set; }
        public int stockMinimo { get; set; }
        public int idCategoria { get; set; }
        //Constructor para la clase Producto con validaciones
        public Producto (int idProducto, string descripcion, decimal costo, decimal precio, char estado, int stock, int stockMinimo, int idCategoria)
        {
            //Valida que los campos no sean inválidos
            if (stockMinimo < 0 || stock < 0)
                throw new ArgumentException("El stock y el stock mínimo no pueden ser negativos.");

            if (precio <= 0 || costo < 0)
                throw new ArgumentException("El precio/costo debe ser mayor a cero.");
            //Asigna los valores a los campos de la clase
            this.idProducto = idProducto;
            this.descripcion = descripcion;
            this.costo = costo;
            this.precio = precio;
            this.estado = estado;
            this.stock = stock;
            this.stockMinimo = stockMinimo;
            this.idCategoria = idCategoria;
        }
        //Método para generar reportes de reabastecimiento
        public bool Reabestecer()
        {
            return stock < stockMinimo;
        }
    }
}
