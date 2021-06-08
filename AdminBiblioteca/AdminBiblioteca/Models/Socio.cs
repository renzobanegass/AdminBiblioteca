using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminBiblioteca.Models
{
    public class Socio
    {        
        protected string nombre;

        protected string apellido;

        protected int numId;

        protected List<Ejemplar> ejemplaresRetirados = new List<Ejemplar>();

        protected int maxPrestamo;

        public Socio(string _nombre, string _apellido, int _id)
        {
            nombre = _nombre;
            apellido = _apellido;
            numId = _id;
            maxPrestamo = 1;
        }

        public string consultarNombre()
        {
            return nombre;
        }

        public string consultarApellido()
        {
            return apellido;
        }
        public int consultarId()
        {
            return numId;
        }
        public bool consultarCupo()
        {
            if (ejemplaresRetirados.Count < maxPrestamo)
                return true;
            else
                return false;            
        }

        public void pedirEjemplar(Ejemplar ejemplar)
        {
            ejemplaresRetirados.Add(ejemplar);
        }

        public Ejemplar devolverEjemplar(Libro libro)
        {

            Ejemplar ejemplar = ejemplaresRetirados.FirstOrDefault(x => x.consultarLibro() == libro.getNombre());

            if(ejemplar != null)
            ejemplaresRetirados.Remove(ejemplar);

            return ejemplar;            
        }
    }
}
