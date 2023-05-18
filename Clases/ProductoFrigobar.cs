using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio06.Clases
{
    public class ProductoFrigobar : Adicional
    {

        private int _cantidadMaxima;

        public int CantidadMaxima
        {
            get { return _cantidadMaxima; }
            set { _cantidadMaxima = value; }
        }

        private int _cantidadRestante;

        public ProductoFrigobar(string nombre, decimal precio, int cantidadMaxima) : base(nombre, precio)
        {
            this.Tipo = "frigobar";

        }

        public int CantidadRestande
        {
            get { return _cantidadRestante; }
            set { _cantidadRestante = value; }
        }

    }
}
