using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio06.Clases
{
    public class Reserva
    {
        private int _numeroDeReserva;

        public int NumeroDeReserva
        {
            get { return _numeroDeReserva; }
        }


        private Habitacion _habitacion;

        public Habitacion Habitacion
        {
            get { return _habitacion; }
            set { _habitacion = value; }
        }

        private List<Huesped> _huespedes;

        public List<Huesped> Huespedes
        {
            get { return _huespedes; }
            set { _huespedes = value; }
        }

        private DateTime _checkIn;

        public DateTime CheckIn
        {
            get { return _checkIn; }
            set { _checkIn = value; }
        }

        private DateTime _checkOut;

        public DateTime CheckOut
        {
            get { return _checkOut; }
            set { _checkOut = value; }
        }

        private List<ItemAdicional> _adicionales;

        public List<ItemAdicional> Adicionales
        {
            get { return _adicionales; }
            set { _adicionales = value; }
        }

        public Reserva(int numeroDeReserva, Habitacion habitacion, List<Huesped> huespedes, DateTime checkIn, DateTime checkOut)
        {
            _numeroDeReserva = numeroDeReserva;
            _habitacion = habitacion;
            _huespedes = huespedes;
            _checkIn = checkIn;
            _checkOut = checkOut;
            _reservaCancelada = false;

            //_habitacion.Reservada = true;
            _habitacion.CantidadSolicitud ++;
            _huespedes.ForEach(h => h.Hospedajes ++);
        }

        private decimal _depositoReserva;

        public decimal DepositoReserva
        {
            get {return _depositoReserva;}
            set { _depositoReserva = value; }
        }


        private decimal _MontoTotal;

        public decimal MontoTotal
        {
            get 
            {
                _MontoTotal = CalcularMontoAlquiler() + CalcularTotalAdicionales();              
                return _MontoTotal; 
            }
        }


        private decimal _depositoMinimo;

        public decimal DepositoMinimo
        {
            get
            {
                _depositoMinimo = CalcularDepositoMinimoReserva();
                return _depositoMinimo; 
            }
        }

        private bool _reservaCancelada;

        public bool ReservaCancelada
        {
            get { return _reservaCancelada; }
            set 
            { 
                _reservaCancelada = value;
                if (value == true) 
                {
                    _MontoTotal = _depositoReserva;
                    _habitacion.Reservada = false;
                    _checkIn = DateTime.MinValue;
                    _checkOut = DateTime.MinValue;
                    _huespedes.Clear();
                }
            }
        }




        private decimal  CalcularDepositoMinimoReserva()
        {
            return CalcularMontoAlquiler() / 10;
        }

        private decimal CalcularMontoAlquiler()
        {
            if (_habitacion.Reservada == true)
            {
                decimal monto = CalcularCantidadDeDias() * _habitacion.Precio;

                if (_adicionales.Any(a => a.Adicional.Nombre.ToLower() == "cuna"))
                {
                    decimal diasCuna = CalcularCantidadDeDias() * _adicionales.Find(a => a.Adicional.Nombre.ToLower() == "cuna").Adicional.Precio;
                    return monto + diasCuna;
                }
                else
                {
                    return monto;
                }
            }
            else
            {
                return 0;
            }
            

        }

        private int CalcularCantidadDeDias()
        {
            TimeSpan duracion = _checkOut.Subtract(_checkIn);
            return  duracion.Days;
        }
        /*
        private decimal CalcularTotalFrigobar()
        {
            Frigobar frigobar = _habitacion.Artefactos.FirstOrDefault(f => f.Nombre.ToLower() == "frigobar") as Frigobar;
            if (frigobar != null)
            {
                return frigobar.MontoTotalProductosRetirados;
            }
            return 0;
        }
        */
        private decimal CalcularTotalFrigobar()
        {
            if (_habitacion.Artefactos.FirstOrDefault(f => f.Nombre.ToLower() == "frigobar") is Frigobar frigobar)
            {
                return frigobar.MontoTotalProductosRetirados;
            }
            return 0;
        }

        private decimal CalcularTotalPrestaciones()
        {
            return _adicionales.Where(a => a.Adicional.Nombre.ToLower() != "cuna").Sum(a => a.Monto);
        }

        private decimal CalcularTotalAdicionales()
        {

            return CalcularTotalFrigobar() + CalcularTotalPrestaciones();
        }


    }
}
