using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio06.Clases
{
    public class ItemAdicional
    {

        private Adicional _adicional;

        public Adicional Adicional
        {
            get { return _adicional; }
        }


        private int _cantidad;

        public int Cantidad
        {
            get { return _cantidad; }
            set { _cantidad = value; }
        }

        public ItemAdicional(Adicional adicional, int cantidad)
        {
            _adicional = adicional;
            _cantidad = cantidad;
        }


        private decimal _monto;

        public decimal Monto
        {
            get
            {
                _monto = _adicional.Precio * _cantidad;
                return _monto;
            }
        }
    }
}
