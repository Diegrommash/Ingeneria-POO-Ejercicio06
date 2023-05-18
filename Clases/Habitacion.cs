using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio06.Clases
{
    public class Habitacion
    {
        private string _numero;

        public string Numero
        {
            get { return _numero; }
            set { _numero = value; }
        }



        private string _tipo;

        public string Tipo
        {
            get { return _tipo; }
        }



        protected decimal _precio;

        public decimal Precio
        {
            get{ return _precio; }


        }

        private List<Artefacto> _artefactos;

        public List<Artefacto> Artefactos
        {
            get { return _artefactos; }
            set { _artefactos = value; }
        }

        private List<string> _camas;

        public List<string> Camas
        {
            get { return _camas; }
        }

        public Habitacion(string numero, string tipo, decimal precio, List<Artefacto> artefactos, List<string> camas)
        {
            _numero = numero;
            _tipo = tipo;
            _precio = precio;
            _artefactos = artefactos;
            _camas = camas;
            _reservada = false;
        }

        private bool _reservada;

        public bool Reservada
        {
            get 
            { return _reservada; }
            set { _reservada = value; }
        }

        private int _cntidadSolicitud;

        public int CantidadSolicitud
        {
            get { return _cntidadSolicitud; }
            set { _cntidadSolicitud = value; }
        }



    }
}
