using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio06.Clases
{
    internal class HabitacionVistaAlMar : Habitacion
    {
        public HabitacionVistaAlMar(string numero, string tipo, decimal precio, List<Artefacto> artefactos, List<string> camas) : base(numero, tipo, precio, artefactos, camas)
        {
            _precio += _precio / 15;
        }
    }
}
