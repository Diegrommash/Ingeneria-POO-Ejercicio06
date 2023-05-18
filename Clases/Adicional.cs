using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio06.Clases
{
    public class Adicional
    {

        private string _tipo;

        public string Tipo
        {
            get { return _tipo; }
            set { _tipo = value; }
        }

        private string _nombre;

        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }

        private decimal _precio;

        public decimal Precio
        {
            get { return _precio; }
            set { _precio = value; }
        }

        public Adicional(string nombre, decimal precio)
        {
            _nombre = nombre;
            _precio = precio;
        }

        public Adicional(string tipo, string nombre, decimal precio)
        {
            _tipo = tipo;
            _nombre = nombre;
            _precio = precio;
        }
    }
}
