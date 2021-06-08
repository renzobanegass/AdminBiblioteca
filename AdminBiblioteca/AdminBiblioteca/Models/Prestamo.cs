using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminBiblioteca.Models
{
    public class Prestamo
    {
        private DateTime fechaPrestamo;

        private Ejemplar ejemplar;

        private Socio socio;

        public int consultarSocio()
        {
            return socio.consultarId();
        }

        public string consultarLibro()
        {
            return ejemplar.consultarLibro();
        }

        public int consultarEjemplar()
        {
            return ejemplar.consultarEdicion();
        }

        public string consultarFecha()
        {
            return fechaPrestamo.ToShortDateString();
        }

        public Prestamo(Ejemplar _ejemplar, Socio _socio)
        {
            ejemplar = _ejemplar;
            socio = _socio;
            fechaPrestamo = DateTime.Now.Date;
        }
    }
}
