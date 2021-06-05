using AdminBiblioteca.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminBiblioteca.Presentators
{
    public class SocioVipPresenter
    {
        private ISocioView objSocioVipView = null;

        public SocioVipPresenter(ISocioView _objSocioView)
        {
            objSocioVipView = _objSocioView;
        }

        public void consultarDatosSocioVip(SocioVIP socio)
        {
            objSocioVipView.SendInfo($"Número Id: {socio.consultarId()}, Nombre: {socio.consultarNombre()}, Apellido: {socio.consultarApellido()}, Cuota Mensual: ${socio.consultarCuota()}");
        }

        //public void agregarEjemplarLibro(int numEdicion, string ubicacion, Libro libro)
        //{
        //    libro.agregarEjemplar(numEdicion, ubicacion);

        //    objSocioVipView.SendInfo("Ejemplar agregado correctamente");
        //}

        public void consultarCupoSocioVip(SocioVIP socio)
        {
            bool puedePedirEjemplar = socio.consultarCupo();

            if (puedePedirEjemplar)
            {
                objSocioVipView.SendInfo("El socio puede pedir prestado un ejemplar");
            }
            else
            {
                objSocioVipView.SendInfo("El socio ya llegó al máximo disponible de ejemplares");
            }

        }

        public void pedirEjemplarSocioVip(SocioVIP socio, Ejemplar ejemplar)
        {
            socio.pedirEjemplar(ejemplar);

            //objSocioView.SendInfo($"Número de edición del ejemplar prestado: {ejemplar.consultarEdicion()} y su ubicación es: {ejemplar.consultarUbicacion()}, pertenece al libro: {ejemplar.consultarLibro()}");
        }

        public Ejemplar devolverEjemplarSocioVip(SocioVIP socio, Libro libro)
        {
            Ejemplar ejemplar = socio.devolverEjemplar(libro);

            return ejemplar;
            //objSocioView.SendInfo("Ejemplar reingresado correctamente");
        }

    }
}
