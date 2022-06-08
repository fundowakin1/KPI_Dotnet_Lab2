using System;
using System.IO;
using Lab1.Contexts;
using Lab1.IODataProcessors;
using Lab1.Repositories;
using Lab1.XmlProcessors;

namespace Lab1.MenuProcessors
{
    public class MenuDisplay
    {
        private readonly Context _context;
        private readonly Output _output;
        private readonly QueriesRepository _queriesRepository;
        private readonly XmlEntityReader _xmlReader;
        private readonly XmlEntityWriter _xmlWriter;
        private readonly ConsoleReader _consoleReader;

        public MenuDisplay(Context context)
        {
            _context = context;
            _output = new Output();
            _queriesRepository = new QueriesRepository(context);
            _xmlReader = new XmlEntityReader();
            _xmlWriter = new XmlEntityWriter();
            _consoleReader = new ConsoleReader(context, _xmlReader);
        }

        public void MainMenu()
        {
            Console.Clear();
            Console.WriteLine("Kalchenko Yehor IS-02, Variant 7 - Vehicle registration\n");
            Console.WriteLine("1. Get elements");
            Console.WriteLine("2. Add elements");
            Console.WriteLine("3. Run queries");
            Console.WriteLine("4. Exit");
            var key = Console.ReadKey().KeyChar;
            Console.Clear();
            switch (key)
            {
                case '1':
                    GetElementsMenu();
                    break;
                case '2':
                    AddElementsMenu();
                    break;
                case '3':
                    _output.RunQueries(_queriesRepository);
                    Console.ReadKey();
                    break;
                case '4':
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid input");
                    Console.ReadKey();
                    break;
            }
        }

        public void GetElementsMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("1. Get Drivers");
                Console.WriteLine("2. Get Vehicles");
                Console.WriteLine("3. Get Models");
                Console.WriteLine("4. Get Manufacturers");
                Console.WriteLine("5. Get Vehicles To Drivers Connections");
                Console.WriteLine("6. Back to Main Menu");
                var key = Console.ReadKey().KeyChar;
                Console.Clear();
                try
                {
                    switch (key)
                    {
                        case '1':
                            var drivers = _xmlReader.GetDrivers(_context.Seed.DriversXml);
                            foreach (var item in drivers)
                                Console.WriteLine(item);

                            Console.ReadKey();
                            break;
                        case '2':
                            var vehicles = _xmlReader.GetVehicles(_context.Seed.VehiclesXml);
                            foreach (var item in vehicles)
                                Console.WriteLine(item);

                            Console.ReadKey();
                            break;
                        case '3':
                            var models = _xmlReader.GetModels(_context.Seed.ModelsXml);
                            foreach (var item in models)
                                Console.WriteLine(item);

                            Console.ReadKey();
                            break;
                        case '4':
                            var manufacturers = _xmlReader.GetManufacturers(_context.Seed.ManufacturersXml);
                            foreach (var item in manufacturers)
                                Console.WriteLine(item);

                            Console.ReadKey();
                            break;
                        case '5':
                            var vds = _xmlReader.GetVehicleDrivers(_context.Seed.VehicleDriversXml);
                            foreach (var item in vds)
                                Console.WriteLine(item);

                            Console.ReadKey();
                            break;
                        case '6':
                            return;
                        default:
                            Console.WriteLine("Invalid input");
                            Console.ReadKey();
                            Console.Clear();
                            break;
                    }
                }
                catch (Exception ex)
                {
                    if (ex is FileLoadException or FileNotFoundException)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
            
        }

        public void AddElementsMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("1. Add Driver");
                Console.WriteLine("2. Add Vehicle");
                Console.WriteLine("3. Add Model");
                Console.WriteLine("4. Add Manufacturer");
                Console.WriteLine("5. Add Vehicles To Drivers Connection");
                Console.WriteLine("6. Back to Main Menu");
                var key = Console.ReadKey().KeyChar;
                Console.Clear();
                try
                {
                    switch (key)
                    {
                        case '1':
                            var driver = _consoleReader.ReadDriver();

                            _xmlWriter.AddElement(_context.Seed.DriversXml, driver);
                            Console.WriteLine($"\n{driver}\n\tSuccessfully added");

                            Console.ReadKey();
                            break;
                        case '2':
                            var vehicle = _consoleReader.ReadVehicle();

                            _xmlWriter.AddElement(_context.Seed.VehiclesXml, vehicle);
                            Console.WriteLine($"\n{vehicle}\n\tSuccessfully added");

                            Console.ReadKey();
                            break;
                        case '3':
                            var model = _consoleReader.ReadModel();

                            _xmlWriter.AddElement(_context.Seed.ModelsXml, model);
                            Console.WriteLine($"\n{model}\n\tSuccessfully added");

                            Console.ReadKey();
                            break;
                        case '4':
                            var manufacturer = _consoleReader.ReadManufacturer();

                            _xmlWriter.AddElement(_context.Seed.ManufacturersXml, manufacturer);
                            Console.WriteLine($"\n{manufacturer}\n\tSuccessfully added");

                            Console.ReadKey();
                            break;
                        case '5':
                            var vd = _consoleReader.ReadVehicleDriver();

                            _xmlWriter.AddElement(_context.Seed.VehicleDriversXml, vd);
                            Console.WriteLine($"\n{vd}\n\tSuccessfully added");

                            Console.ReadKey();
                            break;
                        case '6':
                            return;
                        default:
                            Console.WriteLine("Invalid input");
                            Console.ReadKey();
                            Console.Clear();
                            break;
                    }
                }
                catch (Exception ex)
                {
                    if (ex is InvalidCastException or IndexOutOfRangeException 
                        or InvalidOperationException or NullReferenceException)
                    {
                        Console.WriteLine(ex.Message);
                        Console.ReadKey();
                    }
                }

            }

        }
    }
}
