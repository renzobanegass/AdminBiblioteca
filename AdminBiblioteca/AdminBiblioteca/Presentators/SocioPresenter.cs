using AdminBiblioteca.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminBiblioteca.Presentators
{
    public class SocioPresenter
    {
        private ISocioView objSocioView = null;

        public SocioPresenter(ISocioView _objSocioView)
        {
            objSocioView = _objSocioView;
        }

        public void consultarDatosSocio(Socio socio)
        {                        
           objSocioView.SendInfo($"Número Id: {socio.consultarId()}, Nombre: {socio.consultarNombre()}, Apellido: {socio.consultarApellido()}");
        }

        //public void agregarEjemplarLibro(int numEdicion, string ubicacion, Libro libro)
        //{
        //    libro.agregarEjemplar(numEdicion, ubicacion);

        //    objSocioView.SendInfo("Ejemplar agregado correctamente");
        //}

        public void consultarCupoSocio(Socio socio)
        {
            bool puedePedirEjemplar = socio.consultarCupo();

            if(puedePedirEjemplar)
            {
                objSocioView.SendInfo("El socio puede pedir prestado un ejemplar");
            }
            else
            {
                objSocioView.SendInfo("El socio ya llegó al máximo disponible de ejemplares");
            }
            
        }

        public void pedirEjemplarSocio(Socio socio, Ejemplar ejemplar)
        {
            socio.pedirEjemplar(ejemplar);

            //objSocioView.SendInfo($"Número de edición del ejemplar prestado: {ejemplar.consultarEdicion()} y su ubicación es: {ejemplar.consultarUbicacion()}, pertenece al libro: {ejemplar.consultarLibro()}");
        }

        public Ejemplar devolverEjemplarSocio(Socio socio, Libro libro)
        {
            Ejemplar ejemplar = socio.devolverEjemplar(libro);

            return ejemplar;
            //objSocioView.SendInfo("Ejemplar reingresado correctamente");
        }

    }
}
