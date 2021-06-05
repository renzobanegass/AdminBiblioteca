using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminBiblioteca.Models
{
    public class SocioVIP : Socio
    {
        private decimal cuotaMensual;

        public SocioVIP(string _nombre, string _apellido, int _id, decimal _cuota) 
            : base(_cuota)
        { 
            cuotaMensual = _cuota;
            maxPrestamo = 3;
        }
        public decimal consultarCuota()
        {
            return cuotaMensual;
        }
               
    }
}
