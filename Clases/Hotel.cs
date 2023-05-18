using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio06.Clases
{
    public class Hotel
    {
        #region enums
        enum TipoHabitacion
        {
            Simple,
            Doble_matrimonial,
            Habitacion_triple,
            Habitacion_cuadruple
        };

        enum TipoCama
        {
            Cama_de_una_plaza,
            Cama_de_dos_plazas,
            Cama_matrimonial,
            Cama_matrimonial_plus,
            Cama_cucheta,
        }
        #endregion

        #region objetos para isntanciar
        List<Artefacto> artefactos_f01;
        List<Artefacto> artefactos_f02;
        List<Artefacto> artefactos_f03;
        List<Artefacto> artefactos_f04;
        List<Artefacto> artefactos_f05;
        List<Artefacto> artefactos_f06;
        List<Artefacto> artefactos_f07;
        List<Artefacto> artefactos_f08;

        List<string> Camas_h01;
        List<string> Camas_h02;
        List<string> Camas_h03;
        List<string> Camas_h04;
        List<string> Camas_h05;
        List<string> Camas_h06;
        List<string> Camas_h07;
        List<string> Camas_h08;

        Habitacion habitacion01;
        Habitacion habitacion02;
        Habitacion habitacion03;
        Habitacion habitacion04;
        Habitacion habitacion05;
        Habitacion habitacion06;
        Habitacion habitacion07;
        Habitacion habitacion08;
        #endregion

        private List<Habitacion> _habitaciones;

        public List<Habitacion> Habitaciones
        {
            get { return _habitaciones; }
        }

        private List<Reserva> _reservas;

        public List<Reserva> Reservas
        {
            get { return _reservas; }
        }

        private List<Reserva> _reservasConcretadas;

        public List<Reserva> RerservasConcretadas
        {
            get { return _reservasConcretadas; }
        }


        private List<Adicional> _adicionales;

        public List<Adicional> Adicionales
        {
            get { return _adicionales; }
            set { _adicionales = value; }
        }


        public Hotel()
        {
            SetearArtefactos();
            SetearCamas();
            SetearHabitaciones();
            AgregarHabitaciones();

            Adicional cuna = new Adicional("prestacion", "cuna", 50);
            _adicionales.Add(cuna);
        }

        private decimal _totalRecaudado;

        public decimal TotalRecaudado
        {
            get { return _totalRecaudado; }
            set { _totalRecaudado = value; }
        }


        public string Reservar(Habitacion habitacion, List<Huesped> huespedes, DateTime checkIn, DateTime checkOut, List<ItemAdicional> adicionales, decimal deposito)
        {
            if (habitacion.Reservada == false)
            {

                int numeroDeReseva = _reservas.Count() + 1;
                Reserva reserva = new Reserva(numeroDeReseva, habitacion, huespedes, checkIn, checkOut);
                reserva.Adicionales = adicionales;

                if (deposito >= reserva.DepositoMinimo)
                {
                    _reservas.Add(reserva);
                }
                else
                {
                    return $"El deposito minimo es: ${reserva.DepositoMinimo} \n" +
                           $"Estas depositando:     ${deposito} \n" +
                           $"Te faltarian           ${reserva.DepositoMinimo - deposito}";
                }

                return "Reserva registrada";
            }
            else
            {
                Reserva reserva = _reservas.FirstOrDefault(r => r.Habitacion == habitacion);
                return $"no se pudo realizar la reserva, la habitacion esta ocupada \n " +
                    $"desde el {reserva.CheckIn} al {reserva.CheckOut} inclusive";
            }


        }

        public string ConcretarReserva(int numeroDeReseva)
        {
            Reserva reserva = _reservas.FirstOrDefault(r => r.NumeroDeReserva == numeroDeReseva);
            if (reserva != null)
            {
                _reservasConcretadas.Add(reserva);
                _reservas.Remove(reserva);
                return "La reserva fue concretada";
            }
            else
            {
                return "la reserva ingresada no existe";
            }
        }

        public string RealizarCheckOut(Reserva reserva)
        {
            reserva.Habitacion.Reservada = false;
            _totalRecaudado += reserva.MontoTotal;
            return "Checkout realizado con exito.";
        }

        public string CancelarReserva(Reserva reserva)
        {
            DateTime ahora = DateTime.Now;

            if (ahora < reserva.CheckIn)
            {
                TimeSpan resultado = reserva.CheckOut - ahora;
                decimal dineroDevuelto = decimal.Zero;
                decimal reservaSinModificacion = decimal.Zero;

                if (resultado.Days > 2)
                {

                    dineroDevuelto = reserva.DepositoReserva - reserva.DepositoMinimo;
                    reservaSinModificacion = reserva.DepositoReserva;
                    reserva.DepositoReserva = reserva.DepositoReserva - dineroDevuelto;
                    reserva.ReservaCancelada = true;

                    return $"Reserva cancelada con menos de 2 dias \n" +
                           $"Monto total: ${reserva.MontoTotal} \n" +
                           $"Deposito total: ${reservaSinModificacion} \n" +
                           $"se ha devuelto un total de $ {dineroDevuelto}" +
                           $"Reserva restante: ${reserva.DepositoReserva}";
                }

                else if (resultado.Days >= 2 && resultado.Days <= 7)
                {
                    decimal cincuentaPorcientoDelMinimo = reserva.DepositoMinimo / 2;
                    decimal exedenteDeposito = reserva.DepositoReserva - reserva.DepositoMinimo;
                    dineroDevuelto = cincuentaPorcientoDelMinimo + exedenteDeposito;
                    reserva.DepositoReserva = cincuentaPorcientoDelMinimo;
                    reserva.ReservaCancelada = true;

                    return $"Reserva cancelada dentro de la semana \n" +
                           $"Monto total: ${reserva.MontoTotal} \n" +
                           $"Deposito total: ${reservaSinModificacion} \n" +
                           $"se ha devuelto un total de $ {dineroDevuelto}" +
                           $"Reserva restante: ${reserva.DepositoReserva}";
                }

                else //if (resultado.Days > 7)
                {
                    dineroDevuelto = reserva.DepositoReserva;
                    reserva.DepositoReserva = 0;
                    reserva.ReservaCancelada = true;

                    return $"Reserva cancelada  mayor a la semana \n" +
                           $"Monto total: ${reserva.MontoTotal} \n" +
                           $"Deposito total: ${reservaSinModificacion} \n" +
                           $"se ha devuelto un total de $ {dineroDevuelto}" +
                           $"Reserva restante: ${reserva.DepositoReserva}";
                }

                              
            }
            else
            {
                return $"Imposible cancelar reserva \n" +
                       $"el checkin tiene fecha: {reserva.CheckIn}" +
                       $"y hoy es: {ahora}";
            }

        }

        #region seteo habitaciones
        private void SetearHabitaciones()
        {
            habitacion01 = new Habitacion("1", TipoHabitacion.Simple.ToString(), 200, artefactos_f01, Camas_h01);
            habitacion02 = new HabitacionVistaAlMar("2", TipoHabitacion.Simple.ToString(), 200, artefactos_f02, Camas_h02);
            habitacion03 = new Habitacion("3", TipoHabitacion.Doble_matrimonial.ToString(), 350, artefactos_f03, Camas_h03);
            habitacion04 = new HabitacionVistaAlMar("4", TipoHabitacion.Doble_matrimonial.ToString(), 350, artefactos_f04, Camas_h04);
            habitacion05 = new Habitacion("5", TipoHabitacion.Habitacion_triple.ToString(), 550, artefactos_f05, Camas_h05);
            habitacion06 = new HabitacionVistaAlMar("6", TipoHabitacion.Habitacion_triple.ToString(), 550, artefactos_f06, Camas_h06);
            habitacion05 = new Habitacion("7", TipoHabitacion.Habitacion_cuadruple.ToString(), 700, artefactos_f07, Camas_h07);
            habitacion06 = new HabitacionVistaAlMar("8", TipoHabitacion.Habitacion_cuadruple.ToString(), 700, artefactos_f08, Camas_h08);
        }
        #endregion

        #region seteo camas
        private void SetearCamas()
        {
            Camas_h01 = new List<string> { TipoCama.Cama_de_una_plaza.ToString() };
            Camas_h02 = new List<string> { TipoCama.Cama_de_una_plaza.ToString() };
            Camas_h03 = new List<string> { TipoCama.Cama_de_dos_plazas.ToString() };
            Camas_h04 = new List<string> { TipoCama.Cama_de_dos_plazas.ToString() };
            Camas_h05 = new List<string> { TipoCama.Cama_matrimonial_plus.ToString(), TipoCama.Cama_de_una_plaza.ToString() };
            Camas_h06 = new List<string> { TipoCama.Cama_matrimonial_plus.ToString(), TipoCama.Cama_de_una_plaza.ToString() };
            Camas_h07 = new List<string> { TipoCama.Cama_matrimonial.ToString(), TipoCama.Cama_cucheta.ToString() };
            Camas_h08 = new List<string> { TipoCama.Cama_matrimonial.ToString(), TipoCama.Cama_cucheta.ToString() };
        }
        #endregion

        #region seteo artefactos
        private void SetearArtefactos()
        {
            ProductoFrigobar producto01_f01 = new ProductoFrigobar("coca", 100, 10);
            ProductoFrigobar producto01_f02 = new ProductoFrigobar("coca", 100, 10);
            ProductoFrigobar producto01_f03 = new ProductoFrigobar("coca", 100, 10);
            ProductoFrigobar producto01_f04 = new ProductoFrigobar("coca", 100, 10);
            ProductoFrigobar producto01_f05 = new ProductoFrigobar("coca", 100, 10);
            ProductoFrigobar producto01_f06 = new ProductoFrigobar("coca", 100, 10);
            ProductoFrigobar producto01_f07 = new ProductoFrigobar("coca", 100, 10);
            ProductoFrigobar producto01_f08 = new ProductoFrigobar("coca", 100, 10);
            ProductoFrigobar producto02_f01 = new ProductoFrigobar("cerveza", 150, 10);
            ProductoFrigobar producto02_f02 = new ProductoFrigobar("cerveza", 150, 10);
            ProductoFrigobar producto02_f03 = new ProductoFrigobar("cerveza", 150, 10);
            ProductoFrigobar producto02_f04 = new ProductoFrigobar("cerveza", 150, 10);
            ProductoFrigobar producto02_f05 = new ProductoFrigobar("cerveza", 150, 10);
            ProductoFrigobar producto02_f06 = new ProductoFrigobar("cerveza", 150, 10);
            ProductoFrigobar producto02_f07 = new ProductoFrigobar("cerveza", 150, 10);
            ProductoFrigobar producto02_f08 = new ProductoFrigobar("cerveza", 150, 10);
            ProductoFrigobar producto03_f01 = new ProductoFrigobar("agua mineral", 50, 10);
            ProductoFrigobar producto03_f02 = new ProductoFrigobar("agua mineral", 50, 10);
            ProductoFrigobar producto03_f03 = new ProductoFrigobar("agua mineral", 50, 10);
            ProductoFrigobar producto03_f04 = new ProductoFrigobar("agua mineral", 50, 10);
            ProductoFrigobar producto03_f05 = new ProductoFrigobar("agua mineral", 50, 10);
            ProductoFrigobar producto03_f06 = new ProductoFrigobar("agua mineral", 50, 10);
            ProductoFrigobar producto03_f07 = new ProductoFrigobar("agua mineral", 50, 10);
            ProductoFrigobar producto03_f08 = new ProductoFrigobar("agua mineral", 50, 10);

            List<ProductoFrigobar> productosFrigobar01 = new List<ProductoFrigobar> { producto01_f01, producto02_f01, producto03_f01 };
            List<ProductoFrigobar> productosFrigobar02 = new List<ProductoFrigobar> { producto01_f02, producto02_f02, producto03_f02 };
            List<ProductoFrigobar> productosFrigobar03 = new List<ProductoFrigobar> { producto01_f03, producto02_f03, producto03_f03 };
            List<ProductoFrigobar> productosFrigobar04 = new List<ProductoFrigobar> { producto01_f04, producto02_f04, producto03_f04 };
            List<ProductoFrigobar> productosFrigobar05 = new List<ProductoFrigobar> { producto01_f05, producto02_f05, producto03_f05 };
            List<ProductoFrigobar> productosFrigobar06 = new List<ProductoFrigobar> { producto01_f06, producto02_f06, producto03_f06 };
            List<ProductoFrigobar> productosFrigobar07 = new List<ProductoFrigobar> { producto01_f07, producto02_f07, producto03_f07 };
            List<ProductoFrigobar> productosFrigobar08 = new List<ProductoFrigobar> { producto01_f08, producto02_f08, producto03_f08 };

            Frigobar frigobar01 = new Frigobar("Frigobar01", productosFrigobar01);
            Frigobar frigobar02 = new Frigobar("Frigobar01", productosFrigobar02);
            Frigobar frigobar03 = new Frigobar("Frigobar01", productosFrigobar03);
            Frigobar frigobar04 = new Frigobar("Frigobar01", productosFrigobar04);
            Frigobar frigobar05 = new Frigobar("Frigobar01", productosFrigobar05);
            Frigobar frigobar06 = new Frigobar("Frigobar01", productosFrigobar06);
            Frigobar frigobar07 = new Frigobar("Frigobar01", productosFrigobar07);
            Frigobar frigobar08 = new Frigobar("Frigobar01", productosFrigobar08);

            artefactos_f01 = new List<Artefacto> { frigobar01 };
            artefactos_f02 = new List<Artefacto> { frigobar01 };
            artefactos_f03 = new List<Artefacto> { frigobar01 };
            artefactos_f04 = new List<Artefacto> { frigobar01 };
            artefactos_f05 = new List<Artefacto> { frigobar01 };
            artefactos_f06 = new List<Artefacto> { frigobar01 };
            artefactos_f07 = new List<Artefacto> { frigobar01 };
            artefactos_f08 = new List<Artefacto> { frigobar01 };
        }
        #endregion

        #region agregar habitaciones
        private void AgregarHabitaciones()
        {
            _habitaciones = new List<Habitacion>();
            _habitaciones.Add(habitacion01);
            _habitaciones.Add(habitacion02);
            _habitaciones.Add(habitacion03);
            _habitaciones.Add(habitacion04);
            _habitaciones.Add(habitacion05);
            _habitaciones.Add(habitacion06);
            _habitaciones.Add(habitacion07);
            _habitaciones.Add(habitacion08);
        }
        #endregion
    }
}
