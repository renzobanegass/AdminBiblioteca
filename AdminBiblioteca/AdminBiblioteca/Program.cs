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

        static void Main(string[] args)
        {

            bool showMenu = true;
            while (showMenu)
            {
                showMenu = MainMenu();
            }
        }

        private static bool MainMenu()
        {

            List<Libro> listadoLibros = new List<Libro>();
            List<Socio> listadoSocios = new List<Socio>();
            List<SocioVIP> listadoSociosVIP = new List<SocioVIP>();
            List<Prestamo> listadoPrestamos = new List<Prestamo>();
            List<Prestamo> listadoPrestamosSocio = new List<Prestamo>();
            LibroPresenter objLibroPresenter = new LibroPresenter(new Program());
            SocioPresenter objSocioPresenter = new SocioPresenter(new Program());
            SocioVipPresenter objSocioVipPresenter = new SocioVipPresenter(new Program());

            //Cargar datos de prueba

            List<Ejemplar> ejemplaresLibroUno = new List<Ejemplar>();
            List<Ejemplar> ejemplaresLibroDos = new List<Ejemplar>();

            Libro libroUno = new Libro(1, "Dracula", "32343-43432-4344", "Bram Stoker");
            listadoLibros.Add(libroUno);

            Ejemplar ejemplarUno = new Ejemplar(libroUno, 2001, "P7E5");
            Ejemplar ejemplarDos = new Ejemplar(libroUno, 2005, "P7E5");
            Ejemplar ejemplarTres= new Ejemplar(libroUno, 2012, "P7E5");

            libroUno.setEjemplares(ejemplaresLibroUno);

            Libro libroDos = new Libro(2 , "Frankenstein", "23232-4343-43443", "Mary Shelley");
            listadoLibros.Add(libroDos);


            Ejemplar ejemplarCuatro = new Ejemplar(libroDos, 2001, "P7E5");
            Ejemplar ejemplarCinco = new Ejemplar(libroDos, 2005, "P7E5");
            Ejemplar ejemplarSeis = new Ejemplar(libroDos, 2012, "P7E5");

            libroDos.setEjemplares(ejemplaresLibroDos);

            //Socios
            Socio socioUno = new Socio("John", "Doe", 233);
            SocioVIP socioVipUno = new SocioVIP("Jane", "Doe", 235,2000);
            listadoSocios.Add(socioUno);
            listadoSociosVIP.Add(socioVipUno);

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
            Console.WriteLine("6) Salir");
            Console.Write("\r\nSeleccione una opción: ");

            switch (Console.ReadLine())
            {
                case "1":
                    
                    Console.Clear();
                    Console.WriteLine("Elija un libro: ");
                    
                    foreach (var item in listadoLibros)
                    {
                        objLibroPresenter.consultarDatosLibro(item);
                    }
                    linea = Console.ReadLine();

                    idLibro = int.Parse(linea);

                    libroSeleccionado = listadoLibros.FirstOrDefault(x => x.getId() == idLibro);

                    Console.Clear();
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
                    Console.Clear();
                    Console.WriteLine("Elija un libro: ");
               
                    foreach (var item in listadoLibros)
                    {
                        objLibroPresenter.consultarDatosLibro(item);
                    }
                    linea = Console.ReadLine();

                    idLibro = int.Parse(linea);

                    libroSeleccionado = listadoLibros.FirstOrDefault(x => x.getId() == idLibro);

                    objLibroPresenter.consultaEjemplaresLibro(libroSeleccionado);

                    Console.ReadKey();
                    
                    return true;
                case "3":
                    Console.Clear();
                    Console.WriteLine("Elija un libro: ");
            
                    foreach (var item in listadoLibros)
                    {
                        objLibroPresenter.consultarDatosLibro(item);
                    }
                    linea = Console.ReadLine();

                    idLibro = int.Parse(linea);

                    libroSeleccionado = listadoLibros.FirstOrDefault(x => x.getId() == idLibro);

                    Console.Clear();
                    Console.WriteLine("Elija el tipo de Socio al que se presto el libro: ");
                    Console.WriteLine("1) Socio normal");
                    Console.WriteLine("2) Socio VIP");
                    linea = Console.ReadLine();

                    if (int.Parse(linea) == 1)
                    {
                        Console.Clear();
                        Console.WriteLine("Elija un socio: ");
                        i = 0;
                        foreach (var item in listadoSocios)
                        {
                            objSocioPresenter.consultarDatosSocio(item);
                        }
                        linea = Console.ReadLine();

                        idSocio = int.Parse(linea);

                        socioSeleccionado = listadoSocios.FirstOrDefault(x => x.consultarId() == idSocio);                        

                        ejemplarPrestado = objLibroPresenter.prestarEjemplarLibro(libroSeleccionado);

                        objSocioPresenter.pedirEjemplarSocio(socioSeleccionado, ejemplarPrestado);
                     

                        prestamoCreado = new Prestamo(ejemplarPrestado, socioSeleccionado);
                        Console.ReadKey();
                    }
                    else if (int.Parse(linea) == 2)
                    {
                        Console.Clear();
                        Console.WriteLine("Elija un socio VIP: ");
                        i = 0;
                        foreach (var item in listadoSociosVIP)
                        {
                            objSocioVipPresenter.consultarDatosSocioVip(item);
                        }
                        linea = Console.ReadLine();

                        idSocioVip = int.Parse(linea);

                        socioVipSeleccionado = listadoSociosVIP.FirstOrDefault(x => x.consultarId() == idSocioVip);

                        ejemplarPrestado = objLibroPresenter.prestarEjemplarLibro(libroSeleccionado);

                        objSocioVipPresenter.devolverEjemplarSocioVip(socioVipSeleccionado, libroSeleccionado);

                        prestamoCreado = new Prestamo(ejemplarPrestado, socioVipSeleccionado);
                        Console.ReadKey();
                    }

                    
                    return true;
                case "4":
                    Console.Clear();
                    Console.WriteLine("Elija un libro: ");

                    foreach (var item in listadoLibros)
                    {
                        objLibroPresenter.consultarDatosLibro(item);
                    }
                    linea = Console.ReadLine();

                    idLibro = int.Parse(linea);

                    libroSeleccionado = listadoLibros.FirstOrDefault(x => x.getId() == idLibro);
                    

                    Console.Clear();
                    Console.WriteLine("Elija el tipo de Socio al que se presto el libro: ");
                    Console.WriteLine("1) Socio normal");
                    Console.WriteLine("2) Socio VIP");
                    if(Console.ReadLine() == "1")
                    {
                        Console.Clear();
                        Console.WriteLine("Elija un socio: ");
                        i = 0;
                        foreach (var item in listadoSocios)
                        {
                            objSocioPresenter.consultarDatosSocio(item);
                        }
                        linea = Console.ReadLine();

                        idSocio = int.Parse(linea);

                        socioSeleccionado = listadoSocios.FirstOrDefault(x => x.consultarId() == idSocio);

                        ejemplarDevuelto = objSocioPresenter.devolverEjemplarSocio(socioSeleccionado, libroSeleccionado);

                        objLibroPresenter.registrarReingresoLibro(ejemplarDevuelto, libroSeleccionado);
                    }
                    else if(Console.ReadLine() == "2")
                    {
                        Console.Clear();
                        Console.WriteLine("Elija un socio VIP: ");
                        i = 0;
                        foreach (var item in listadoSociosVIP)
                        {
                            objSocioVipPresenter.consultarDatosSocioVip(item);
                        }
                        linea = Console.ReadLine();

                        idSocioVip = int.Parse(linea);

                        socioVipSeleccionado = listadoSociosVIP.FirstOrDefault(x => x.consultarId() == idSocioVip);

                        ejemplarDevuelto = objSocioVipPresenter.devolverEjemplarSocioVip(socioVipSeleccionado, libroSeleccionado);

                        objLibroPresenter.registrarReingresoLibro(ejemplarDevuelto, libroSeleccionado);
                    }
                    Console.ReadKey();

                    return true;
                case "5":                    
                    Console.Clear();
                    Console.WriteLine("Elija el tipo de Socio: ");
                    Console.WriteLine("1) Socio normal");
                    Console.WriteLine("2) Socio VIP");
                    if (Console.ReadLine() == "1")
                    {
                        Console.Clear();
                        Console.WriteLine("Elija un socio: ");
                        i = 0;
                        foreach (var item in listadoSocios)
                        {
                            objSocioPresenter.consultarDatosSocio(item);
                        }
                        linea = Console.ReadLine();

                        idSocio = int.Parse(linea);

                        socioSeleccionado = listadoSocios.FirstOrDefault(x => x.consultarId() == idSocio);

                        Console.Clear();
                        Console.WriteLine("Elija una opción: ");
                        Console.WriteLine("1) Ver préstamos");
                        listadoPrestamosSocio = listadoPrestamos.Where(x => x.consultarSocio() == idSocio).ToList();

                        Console.Clear();
                        foreach (var item in listadoPrestamosSocio)
                        {
                            Console.WriteLine($"Libro: {item.consultarLibro()}, Número de Edicion del ejemplar: {item.consultarEjemplar()}, Fecha de Préstamo: {item.consultarFecha()}");
                        }
                    }
                    else if (Console.ReadLine() == "2")
                    {
                        Console.Clear();
                        Console.WriteLine("Elija un socio VIP: ");
                        i = 0;
                        foreach (var item in listadoSociosVIP)
                        {
                            objSocioVipPresenter.consultarDatosSocioVip(item);
                        }
                        linea = Console.ReadLine();

                        idSocioVip = int.Parse(linea);

                        socioVipSeleccionado = listadoSociosVIP.FirstOrDefault(x => x.consultarId() == idSocioVip);

                        Console.Clear();
                        Console.WriteLine("Elija una opción: ");
                        Console.WriteLine("1) Ver préstamos");
                        listadoPrestamosSocio = listadoPrestamos.Where(x => x.consultarSocio() == idSocioVip).ToList();

                        Console.Clear();
                        foreach (var item in listadoPrestamosSocio)
                        {
                            Console.WriteLine($"Libro: {item.consultarLibro()}, Número de Edicion del ejemplar: {item.consultarEjemplar()}, Fecha de Préstamo: {item.consultarFecha()}");
                        }                     
                       
                    }

                    Console.ReadKey();

                    return true;
                case "6":
                    return false;
                default:
                    return true;
            }
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
