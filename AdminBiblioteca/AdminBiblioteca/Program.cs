using AdminBiblioteca.Models;
using AdminBiblioteca.Presentators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminBiblioteca
{
    class Program : ILibroView, ISocioView, ISocioVipView
    {
        private static LibroPresenter _presenter;

        
        static LibroPresenter objLibroPresenter = new LibroPresenter(new Program());
        static SocioPresenter objSocioPresenter = new SocioPresenter(new Program());
        static SocioVipPresenter objSocioVipPresenter = new SocioVipPresenter(new Program());

        #region Carga de dummy data
        static List<Libro> listadoLibros = new List<Libro>();
        static List<Socio> listadoSocios = new List<Socio>();
        static List<SocioVIP> listadoSociosVIP = new List<SocioVIP>();
        static List<Prestamo> listadoPrestamos = new List<Prestamo>();
        static List<Prestamo> listadoPrestamosSocio = new List<Prestamo>();
        static List<Ejemplar> ejemplaresLibroUno = new List<Ejemplar>();
        static List<Ejemplar> ejemplaresLibroDos = new List<Ejemplar>();

        static Libro libroUno = new Libro(1, "Dracula", "32343-43432-4344", "Bram Stoker");
        

        static Ejemplar ejemplarUno = new Ejemplar(libroUno, 2001, "P7E5");
        static Ejemplar ejemplarDos = new Ejemplar(libroUno, 2005, "P7E5");
        static Ejemplar ejemplarTres = new Ejemplar(libroUno, 2012, "P7E5");

        
         static Libro libroDos = new Libro(2, "Frankenstein", "23232-4343-43443", "Mary Shelley");
       


        static Ejemplar ejemplarCuatro = new Ejemplar(libroDos, 2001, "P7E5");
        static Ejemplar ejemplarCinco = new Ejemplar(libroDos, 2005, "P7E5");
        static Ejemplar ejemplarSeis = new Ejemplar(libroDos, 2012, "P7E5");
        
        //Socios
        static Socio socioUno = new Socio("John", "Doe", 233);
        static SocioVIP socioVipUno = new SocioVIP("Jane", "Doe", 235, 2000);

        #endregion
        static void Main(string[] args)
        {
            listadoLibros.Add(libroUno);
            ejemplaresLibroUno.Add(ejemplarUno);
            ejemplaresLibroUno.Add(ejemplarDos);
            ejemplaresLibroUno.Add(ejemplarTres);

            libroUno.setEjemplares(ejemplaresLibroUno);

            listadoLibros.Add(libroDos);
            ejemplaresLibroDos.Add(ejemplarCuatro);
            ejemplaresLibroDos.Add(ejemplarCinco);
            ejemplaresLibroDos.Add(ejemplarSeis);

            libroDos.setEjemplares(ejemplaresLibroDos);
            listadoSocios.Add(socioUno);
            listadoSociosVIP.Add(socioVipUno);

            bool showMenu = true;
            while (showMenu)
            {
                showMenu = MainMenu();
            }
        }

        private static bool MainMenu()
        {

            var linea = string.Empty;
            int i;
            int idLibro;
            int idSocio;
            int idSocioVip;

            Libro libroSeleccionado;
            Socio socioSeleccionado;
            SocioVIP socioVipSeleccionado;
            Ejemplar ejemplarDevuelto;
            Ejemplar ejemplarPrestado;
            Prestamo prestamoCreado;


            Console.Clear();
            Console.WriteLine("Elije una opción:");
            Console.WriteLine("1) Agregar ejemplar de un libro");
            Console.WriteLine("2) Consultar si hay ejemplares disponibles de un libro");
            Console.WriteLine("3) Prestar ejemplar de un libro");
            Console.WriteLine("4) Reingresar un ejemplar prestado");
            Console.WriteLine("5) Ver préstamos de un socio");
            Console.WriteLine("6) Agregar un libro");
            Console.WriteLine("7) Añadir un socio");
            Console.WriteLine("8) Consultar todos los prestamos");
            Console.WriteLine("9) Salir");
            Console.Write("\r\nSeleccione una opción: ");

            try
            {

                switch (Console.ReadLine())
                {
                    case "1":
           
                        libroSeleccionado = GetLibro();

                        Console.Clear();
                        if (libroSeleccionado == null)
                        {
                            Console.WriteLine("No hay un libro que corresponda al ID ingresado. Intente con un ID del listado.");
                            Console.WriteLine("Presione una tecla para volver al menu...");
                            Console.ReadKey();
                            return true;
                        }
                        Console.WriteLine("Libro seleccionado: ");
                        objLibroPresenter.consultarDatosLibro(libroSeleccionado);

                        Console.WriteLine("Ingrese número de edición del ejemplar a agregar: ");
                        linea = Console.ReadLine();

                        int numEdicion = int.Parse(linea);

                        Console.WriteLine("Ingrese ubicación del ejemplar a agregar: ");
                        linea = Console.ReadLine();

                        string ubicacion = linea;

                        objLibroPresenter.agregarEjemplarLibro(numEdicion, ubicacion, libroSeleccionado);

                        Console.ReadKey();
                                       
                        return true;
                    case "2":

                        libroSeleccionado = GetLibro();

                        if (libroSeleccionado == null)
                        {
                            Console.WriteLine("No hay un libro que corresponda al ID ingresado. Intente con un ID del listado.");
                            Console.WriteLine("Presione una tecla para volver al menu...");
                            Console.ReadKey();
                            return true;
                        }

                        objLibroPresenter.consultaEjemplaresLibro(libroSeleccionado);

                        Console.ReadKey();
                    
                        return true;
                    case "3":

                        libroSeleccionado = GetLibro();

                        if (libroSeleccionado == null)
                        {
                            Console.WriteLine("No hay un libro que corresponda al ID ingresado. Intente con un ID del listado.");
                            Console.WriteLine("Presione una tecla para volver al menu...");
                            Console.ReadKey();
                            return true;
                        }

                        if(!libroSeleccionado.consultaEjemplares())
                        {
                            Console.WriteLine("El libro ya no tiene ejemplares disponibles.");
                            Console.WriteLine("Presione una tecla para volver al menu...");
                            Console.ReadKey();
                            return true;
                        }


                        Console.Clear();
                        Console.WriteLine("Elija el tipo de Socio al que se presta el libro: ");
                        Console.WriteLine("1) Socio normal");
                        Console.WriteLine("2) Socio VIP");
                        linea = Console.ReadLine();

                        if (int.Parse(linea) == 1)
                        {
                            socioSeleccionado = GetSocio();

                            if (socioSeleccionado == null)
                            {
                                Console.WriteLine("No hay un socio que corresponda al ID ingresado. Intente con un ID del listado.");
                                Console.WriteLine("Presione una tecla para volver al menu...");
                                Console.ReadKey();
                                return true;
                            }

                            if (socioSeleccionado.consultarCupo())
                            {
                                ejemplarPrestado = objLibroPresenter.prestarEjemplarLibro(libroSeleccionado);

                                objSocioPresenter.pedirEjemplarSocio(socioSeleccionado, ejemplarPrestado);

                                prestamoCreado = new Prestamo(ejemplarPrestado, socioSeleccionado);

                                listadoPrestamos.Add(prestamoCreado);

                            }
                            else
                            {
                                Console.WriteLine("El socio ya llego al limite de prestamos disponibles para socios comunes.");
                            }

                            Console.ReadKey();
                        }
                        else if (int.Parse(linea) == 2)
                        {
                            socioVipSeleccionado = GetSocioVip();

                            if (socioVipSeleccionado == null)
                            {
                                Console.WriteLine("No hay un socio que corresponda al ID ingresado. Intente con un ID del listado.");
                                Console.WriteLine("Presione una tecla para volver al menu...");
                                Console.ReadKey();
                                return true;
                            }

                            if (socioVipSeleccionado.consultarCupo())
                            {
                                ejemplarPrestado = objLibroPresenter.prestarEjemplarLibro(libroSeleccionado);

                                objSocioVipPresenter.pedirEjemplarSocioVip(socioVipSeleccionado, ejemplarPrestado);

                                prestamoCreado = new Prestamo(ejemplarPrestado, socioVipSeleccionado);

                                listadoPrestamos.Add(prestamoCreado);
                            }
                            else
                            {
                                Console.WriteLine("El socio ya llego al limite de prestamos disponibles para socios VIP.");
                            }
                      

                            Console.ReadKey();
                        }
                        else
                        {

                            Console.WriteLine("Opción invalida, elija una de las opciones del listado.");
  

                        }

                        Console.WriteLine("Presione una tecla para volver al menu...");
                        return true;
                    case "4":

                        libroSeleccionado = GetLibro();

                        if (libroSeleccionado == null)
                        {
                            Console.WriteLine("No hay un libro que corresponda al ID ingresado. Intente con un ID del listado.");
                            Console.WriteLine("Presione una tecla para volver al menu");
                            Console.ReadKey();
                            return true;
                        }

                        Console.Clear();
                        Console.WriteLine("Elija el tipo de Socio al que se presto el libro: ");
                        Console.WriteLine("1) Socio normal");
                        Console.WriteLine("2) Socio VIP");
                        linea = Console.ReadLine();

                        if (int.Parse(linea) == 1)
                        {

                            socioSeleccionado = GetSocio();

                            if (socioSeleccionado == null)
                            {
                                Console.WriteLine("No hay un socio que corresponda al ID ingresado. Intente con un ID del listado.");
                                Console.WriteLine("Presione una tecla para volver al menu");
                                Console.ReadKey();
                                return true;
                            }

                            ejemplarDevuelto = objSocioPresenter.devolverEjemplarSocio(socioSeleccionado, libroSeleccionado);

                            objLibroPresenter.registrarReingresoLibro(ejemplarDevuelto, libroSeleccionado);
                        }
                        else if (int.Parse(linea) == 2)
                        {
                            socioVipSeleccionado = GetSocioVip();

                            if (socioVipSeleccionado == null)
                            {
                                Console.WriteLine("No hay un socio que corresponda al ID ingresado. Intente con un ID del listado.");
                                Console.WriteLine("Presione una tecla para volver al menu");
                                Console.ReadKey();
                                return true;
                            }

                            ejemplarDevuelto = objSocioVipPresenter.devolverEjemplarSocioVip(socioVipSeleccionado, libroSeleccionado);

                            objLibroPresenter.registrarReingresoLibro(ejemplarDevuelto, libroSeleccionado);
                        }
                        else
                        {                                    
    
                            Console.WriteLine("Opción invalida, elija una de las opciones del listado.");
                            Console.WriteLine("Presione una tecla para volver al menu");

                        }

                        Console.WriteLine("Presione una tecla para volver al menu...");
                        Console.ReadKey();

                        return true;
                    case "5":                    
                        Console.Clear();
                        Console.WriteLine("Elija el tipo de Socio: ");
                        Console.WriteLine("1) Socio normal");
                        Console.WriteLine("2) Socio VIP");
                        linea = Console.ReadLine();

                        if (int.Parse(linea) == 1)
                        {

                            socioSeleccionado = GetSocio();

                            if (socioSeleccionado == null)
                            {
                                Console.WriteLine("No hay un socio que corresponda al ID ingresado. Intente con un ID del listado.");
                                Console.WriteLine("Presione una tecla para volver al menu...");
                                Console.ReadKey();
                                return true;
                            }

                            Console.Clear();
                            Console.WriteLine("Elija una opción: ");
                            Console.WriteLine("1) Ver préstamos");
                            listadoPrestamosSocio = listadoPrestamos.Where(x => x.consultarSocio() == socioSeleccionado.consultarId()).ToList();

                            Console.Clear();
                        
                            if(listadoPrestamosSocio.Count() > 0)
                            {
                                Console.WriteLine($"Historial de prestamos del socio {socioSeleccionado.consultarNombre()} {socioSeleccionado.consultarApellido()} Id: {socioSeleccionado.consultarId()}");

                                foreach (var item in listadoPrestamosSocio)
                                {
                                
                                    Console.WriteLine($"Fecha de Préstamo: {item.consultarFecha()}, Libro: {item.consultarLibro()}, Número de Edicion del ejemplar: {item.consultarEjemplar()}");
                                }
                            }
                            else
                            {
                                Console.WriteLine($"Este socio no tiene préstamos.");
                            }

                       
                        }
                        else if (int.Parse(linea) == 2)
                        {
                            socioVipSeleccionado = GetSocioVip();

                            if (socioVipSeleccionado == null)
                            {
                                Console.WriteLine("No hay un socio que corresponda al ID ingresado. Intente con un ID del listado.");
                                Console.WriteLine("Presione una tecla para volver al menu...");
                                Console.ReadKey();
                                return true;
                            }
                            
                            Console.Clear();
                            Console.WriteLine("Elija una opción: ");
                            Console.WriteLine("1) Ver préstamos");
                            listadoPrestamosSocio = listadoPrestamos.Where(x => x.consultarSocio() == socioVipSeleccionado.consultarId()).ToList();

                            Console.Clear();

                            if (listadoPrestamosSocio.Count() > 0)
                            {
                                Console.WriteLine($"Historial de prestamos del socio {socioVipSeleccionado.consultarNombre()} {socioVipSeleccionado.consultarApellido()} Id: {socioVipSeleccionado.consultarId()}");

                                foreach (var item in listadoPrestamosSocio)
                                {

                                    Console.WriteLine($"Fecha de Préstamo: {item.consultarFecha()}, Libro: {item.consultarLibro()}, Número de Edicion del ejemplar: {item.consultarEjemplar()}");
                                }
                            }
                            else
                            {
                                Console.WriteLine($"Este socio no tiene préstamos.");
                            }

                        }

                        else
                        {                                    
    
                            Console.WriteLine("Opción invalida, elija una de las opciones del listado.");
                         

                        }

                        Console.WriteLine("Presione una tecla para volver al menu...");
                        Console.ReadKey();

                        return true;
                    case "6":

                        Console.Clear();
                        Console.WriteLine("Ingrese el titulo del libro: ");
                        linea = Console.ReadLine();

                        string tituloNuevoLibro = linea;

                        Console.WriteLine("Ingrese el autor del libro: ");
                        linea = Console.ReadLine();

                        string autorNuevoLibro = linea;

                        Console.WriteLine("Ingrese el ISBN del libro: ");
                        linea = Console.ReadLine();

                        string isbnNuevoLibro = linea;

                        int idNuevoLibro = listadoLibros.Count + 1;

                        
                        Libro nuevoLibro = new Libro(idNuevoLibro, tituloNuevoLibro, isbnNuevoLibro, autorNuevoLibro);

                        listadoLibros.Add(nuevoLibro);

                        Console.WriteLine("Nuevo libro agregado correctamente");
                        Console.WriteLine("Presione una tecla para volver al menu...");                        
                        Console.ReadKey();

                        return true;
                    case "7":

                        Console.Clear();
                        Console.WriteLine("Elija el tipo de Socio: ");
                        Console.WriteLine("1) Socio normal");
                        Console.WriteLine("2) Socio VIP");
                        linea = Console.ReadLine();

                        string nombreNuevoSocio;
                        string apellidoNuevoSocio;
                        int idNuevoSocio;
                        decimal cuotaNuevoSocio;

                        if (int.Parse(linea) == 1)
                        {
                            Console.Clear();
                            Console.WriteLine("Ingrese el nombre del nuevo socio: ");
                            linea = Console.ReadLine();

                            nombreNuevoSocio = linea;

                            Console.WriteLine("Ingrese el apellido del nuevo socio: ");
                            linea = Console.ReadLine();

                            apellidoNuevoSocio = linea;

                            idNuevoSocio = listadoSocios.Count + 1;

                            Socio nuevoSocio = new Socio(nombreNuevoSocio, apellidoNuevoSocio, idNuevoSocio);

                            listadoSocios.Add(nuevoSocio);

                            Console.WriteLine("Nuevo socio agregado correctamente");
                            Console.WriteLine("Presione una tecla para volver al menu");
                        }
                        else if (int.Parse(linea) == 2)
                        {
                            Console.Clear();
                            Console.WriteLine("Ingrese el nombre del nuevo socio VIP: ");
                            linea = Console.ReadLine();

                            nombreNuevoSocio = linea;

                            Console.WriteLine("Ingrese el apellido del nuevo socio VIP: ");
                            linea = Console.ReadLine();

                            apellidoNuevoSocio = linea;

                            Console.WriteLine("Ingrese la cuota mensual del nuevo socio VIP: ");
                            linea = Console.ReadLine();

                            cuotaNuevoSocio = decimal.Parse(linea);

                            idNuevoSocio = listadoSociosVIP.Count + 1;

                            SocioVIP nuevoSocio = new SocioVIP(nombreNuevoSocio, apellidoNuevoSocio, idNuevoSocio, cuotaNuevoSocio);

                            listadoSociosVIP.Add(nuevoSocio);

                            Console.WriteLine("Nuevo socio Vip agregado correctamente");
                            Console.WriteLine("Presione una tecla para volver al menu...");
                        }
                        else
                        {

                            Console.WriteLine("Opción invalida, elija una de las opciones del listado.");
                            Console.WriteLine("Presione una tecla para volver al menu...");

                        }

                        Console.ReadKey();
                        return true;
                    case "8":

                        Console.Clear();
                        Console.WriteLine("Historial de préstamos: ");

                        if (listadoPrestamos.Count() > 0)
                        {
                            
                            foreach (var item in listadoPrestamosSocio)
                            {

                                Console.WriteLine($"Fecha de Préstamo: {item.consultarFecha()}, Libro: {item.consultarLibro()}, Número de Edicion del ejemplar: {item.consultarEjemplar()}, Id Socio: {item.consultarSocio()}");
                            }
                        }
                        else
                        {
                            Console.WriteLine($"No hay préstamos.");
                        }
                        
                        Console.WriteLine("Presione una tecla para volver al menu...");
                        Console.ReadKey();

                        return true;
                    case "9":
                        return false;
                    default:
                        return true;
            }

            }
            catch (FormatException ex)
            {
                Console.WriteLine("Formato incorrecto, ingrese un numero.");
                Console.WriteLine("Presione una tecla para volver al menu");
                Console.ReadKey();
                return true;
            }

        }


        private static Libro GetLibro()
        {
            string linea;
            int idLibro;
            Libro libroSeleccionado;

            Console.Clear();
            Console.WriteLine("Elija un libro ingresando la id que corresponda: ");

            foreach (var item in listadoLibros)
            {
                objLibroPresenter.consultarDatosLibro(item);
            }
            linea = Console.ReadLine();

            idLibro = int.Parse(linea);

            libroSeleccionado = listadoLibros.FirstOrDefault(x => x.getId() == idLibro);

            return libroSeleccionado;
        }

        private static Socio GetSocio()
        {
            string linea;
            int idSocio;
            Socio socioSeleccionado;


            Console.Clear();
            Console.WriteLine("Elija un socio ingresando la id que le corresponda: ");

            foreach (var item in listadoSocios)
            {
                objSocioPresenter.consultarDatosSocio(item);
            }
            linea = Console.ReadLine();

            idSocio = int.Parse(linea);

            socioSeleccionado = listadoSocios.FirstOrDefault(x => x.consultarId() == idSocio);

            return socioSeleccionado;            
        }

        private static SocioVIP GetSocioVip()
        {
            string linea;
            int idSocioVip;
            SocioVIP socioVipSeleccionado;

            Console.Clear();
            Console.WriteLine("Elija un socio VIP ingresando la id que le corresponda: ");

            foreach (var item in listadoSociosVIP)
            {
                objSocioVipPresenter.consultarDatosSocioVip(item);
            }
            linea = Console.ReadLine();

            idSocioVip = int.Parse(linea);

            socioVipSeleccionado = listadoSociosVIP.FirstOrDefault(x => x.consultarId() == idSocioVip);

            return socioVipSeleccionado;
        }




        #region Interface Members

        public Libro Libro
        {
            get
            {
                return this.Libro;
            }
        }

        public Socio Socio 
        {
            get
            {
                return this.Socio;
            }
        }

        public SocioVIP SocioVip
        {
            get
            {
                return this.SocioVip;
            }
        }

        public void SendInfo(string strMessage)
        {
            //Console.Clear();
            Console.WriteLine(strMessage);            
        }

        #endregion
    }
}
