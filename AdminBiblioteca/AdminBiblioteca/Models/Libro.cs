using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminBiblioteca.Models
{
    public class Libro
    {
        private int id;

        private string nombre;

        private string ISBN;

        private string autor;

        private List<Ejemplar> listaEjemplares = new List<Ejemplar>();

        public Libro(int _id, string _nombre, string _ISBN, string _autor)
        {
            id = _id;
            nombre = _nombre;
            ISBN = _ISBN;
            autor = _autor;     
        }

        public void setEjemplares(List<Ejemplar> _ejemplares)
        {
            listaEjemplares = _ejemplares;
        }
        public int getId()
        {
            return id;  
        }
        public string getNombre()
        {
            return nombre;
        }
        public string getISBN()
        {
            return ISBN;
        }

        public string getAutor()
        {
            return autor;
        }

        public void agregarEjemplar(int _numEdicion, string _ubicacion)
        {
            Ejemplar ejemplar = new Ejemplar(this, _numEdicion, _ubicacion);

            listaEjemplares.Add(ejemplar);
        }

        public bool consultaEjemplares()
        {
            if (listaEjemplares.Count > 0)
                return true;
            else
                return false;            
        }

        public Ejemplar prestarEjemplar()
        {
            Ejemplar ejemplar = listaEjemplares.FirstOrDefault();

            listaEjemplares.Remove(ejemplar);

            return ejemplar;
        }

        public void registrarReingreso(Ejemplar ejemplar)
        {
            listaEjemplares.Add(ejemplar);
        }



    }
}
