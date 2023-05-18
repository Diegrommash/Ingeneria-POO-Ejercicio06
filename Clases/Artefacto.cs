using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio06.Clases
{
    public class Artefacto
    {
        private string _nombre;

        public string Nombre
        {
            get { return _nombre; }
        }

        public Artefacto(string nombre)
        {
            _nombre = nombre;
        }
    }
}
