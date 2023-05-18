using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio06.Clases
{
    public class Frigobar : Artefacto
    {
        private List<ProductoFrigobar> _productos;

        public List<ProductoFrigobar> Productos
        {
            get { return _productos; }
        }

        public Frigobar(string nombre, List<ProductoFrigobar> productos) : base(nombre)
        {
            _productos = productos;
        }

        private List<ItemAdicional> _productosRetirados;

        public List<ItemAdicional> ProductosRetirados
        {
            get { return _productosRetirados; }
        }

        private decimal _montoTotalProductosRetirados;

        public decimal MontoTotalProductosRetirados
        {
            get 
            {
                _montoTotalProductosRetirados = CalcularTotalProductosRetirados();
                return _montoTotalProductosRetirados; 
            }
        }


        public string AgregarProductos(ProductoFrigobar producto)
        {
            ProductoFrigobar productoNuevo = _productos.FirstOrDefault(a => a.Nombre.ToLower() == producto.Nombre.ToLower());
            if (productoNuevo != null)
            {
                return "Producto ya existente";
            }
            else 
            {
                _productos.Add(productoNuevo);
                return "Producto ingresado";
            }
            
        }

        public string ReponerProductos(ProductoFrigobar producto, int cantidad)
        {
            ProductoFrigobar productoEncontrado = _productos.FirstOrDefault(a => a.Nombre.ToLower() == producto.Nombre.ToLower());
            if (productoEncontrado == null)
            {
                if (productoEncontrado.CantidadMaxima <= cantidad)
                {
                    productoEncontrado.CantidadRestande += cantidad;
                    return "Producto repuesto";
                }
                else 
                {
                    return $"La cantidad maxima del producto {producto.Nombre} es: {producto.CantidadMaxima} \n" +
                           $"Estas ingresando un total de: {cantidad} unidades \n" +
                           $"Te estas exediendo en {cantidad - producto.CantidadMaxima}";
                }
                
            }
            else
            {
                _productos.Add(productoEncontrado);
                return "Producto ingresado";
            }

        }

        public void ReponerTodosLosProductos()
        {
            _productos.ForEach(p => p.CantidadRestande = p.CantidadMaxima);
        }


        public string RetirarProductos(Adicional adicional, int cantidad)
        {
            ProductoFrigobar producto = _productos.FirstOrDefault(a => a.Nombre.ToLower() == adicional.Nombre.ToLower());
            if (producto != null)
            {
                if (producto.CantidadRestande >= cantidad)
                {
                    producto.CantidadRestande -= cantidad;
                    ItemAdicional itemAdicional = new ItemAdicional(producto, cantidad);
                    ProductosRetirados.Add(itemAdicional);
                    return "Producto retirado";
                }
                else 
                {
                    return $"quedan : {producto.CantidadMaxima} unidades \n" +
                           $"Estas retirando: {cantidad} unidades \n";
                }              
            }
            else
            {
                return "Producto no encontrado";
            }
        }

        private decimal CalcularTotalProductosRetirados()
        {
            return _productosRetirados.Sum(p => p.Monto);
        }



    }
}
