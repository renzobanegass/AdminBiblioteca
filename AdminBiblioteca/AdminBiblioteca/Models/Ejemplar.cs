using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminBiblioteca.Models
{
    public class Ejemplar
    {
        private Libro libro;

        private int numEdicion;

        private string ubicacion;               
        public int consultarEdicion()
        {
            return numEdicion;
        }

        public string consultarLibro()
        {
            return libro.getNombre();
        }
        public string consultarUbicacion()
        {
            return ubicacion;
        }
        public Ejemplar(Libro _libro, int _numEdicion, string _ubicacion)
        {
            libro = _libro;
            numEdicion = _numEdicion;
            ubicacion = _ubicacion;
        }
    }
}
