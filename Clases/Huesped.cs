using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio06.Clases
{
    public class Huesped
    {
        private string _nombre;

        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }

        private string _apellido;

        public string Apellido
        {
            get { return _apellido; }
            set { _apellido = value; }
        }

        private DateTime _fechaNacimiento;

        public DateTime FechaNacimiento
        {
            get { return _fechaNacimiento; }
            set { _fechaNacimiento = value; }
        }

        private int _dni;

        public int DNI
        {
            get { return _dni; }
            set { _dni = value; }
        }

        private int _edad;

        public int Edad
        {
            get { return _edad; }
            set { _edad = value; }
        }

        private int _hospedajes;

        public int Hospedajes
        {
            get { return _hospedajes;; }
            set { _hospedajes = value; }
        }




    }
}
