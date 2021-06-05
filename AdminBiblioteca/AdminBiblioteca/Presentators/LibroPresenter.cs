using AdminBiblioteca.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminBiblioteca.Presentators
{
    public class LibroPresenter
    {
        private ILibroView objLibroView = null;

        public LibroPresenter(ILibroView _objLibroView)
        {
            objLibroView = _objLibroView;
        }

        public void consultarDatosLibro(Libro libro)
        {            
         
            objLibroView.SendInfo($"Id: {libro.getId()}, Nombre: {libro.getNombre()}, ISBN: {libro.getISBN()}, Autor: {libro.getAutor()}");

        }

        public void agregarEjemplarLibro(int numEdicion, string ubicacion, Libro libro)
        {
            libro.agregarEjemplar(numEdicion, ubicacion);

            objLibroView.SendInfo("Ejemplar agregado correctamente");
        }

        public void consultaEjemplaresLibro(Libro libro)
        {
            bool tieneEjemplares = libro.consultaEjemplares();

            if(tieneEjemplares)
            {
                objLibroView.SendInfo("El libro tiene ejemplares disponibles");
            }
            else
            {
                objLibroView.SendInfo("El libro no tiene ejemplares disponibles");
            }
            
        }

        public Ejemplar prestarEjemplarLibro(Libro libro)
        {
            Ejemplar ejemplar = libro.prestarEjemplar();

            objLibroView.SendInfo($"Número de edición del ejemplar prestado: {ejemplar.consultarEdicion()} y su ubicación es: {ejemplar.consultarUbicacion()}");

            return ejemplar;
        }

        public void registrarReingresoLibro(Ejemplar ejemplar, Libro libro)
        {
            libro.registrarReingreso(ejemplar);

            objLibroView.SendInfo("Ejemplar reingresado correctamente");
        }

    }
}
