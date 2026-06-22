using System;

namespace APE115_ProyectoGT03GP4
{
    //Clase plantilla que representa un producto
    class Producto
    {
        //Campos que componen el producto, según su estructura en la BDD
        public int IdProducto { get; set; }
        public string Descripcion { get; set; }
        public decimal Costo { get; set; }
        public decimal Precio { get; set; }
        public char Estado { get; set; }
        public int Stock { get; set; }
        public int StockMinimo { get; set; }
        public int IdCategoria { get; set; }
        public decimal Total { get; set; }
        //Constructor para la clase Producto con validaciones
        public Producto (int idProducto, string descripcion, decimal costo, decimal precio, char estado, int stock, int stockMinimo, int idCategoria)
        {
            //Valida que los campos no sean inválidos
            if (stockMinimo < 0 || stock < 0)
                throw new ArgumentException("El stock y el stock mínimo no pueden ser negativos.");

            if (precio <= 0 || costo < 0)
                throw new ArgumentException("El precio/costo debe ser mayor a cero.");
            //Asigna los valores a los campos de la clase
            this.IdProducto = idProducto;
            this.Descripcion = descripcion;
            this.Costo = costo;
            this.Precio = precio;
            this.Estado = estado;
            this.Stock = stock;
            this.StockMinimo = stockMinimo;
            this.IdCategoria = idCategoria;
            this.Total = precio * stock;
        }
        //Método para generar reportes de reabastecimiento
        public bool Reabestecer()
        {
            return Stock < StockMinimo;
        }
    }
}
