using System;
using System.Linq;
using System.Text.RegularExpressions;
using Lab1.Contexts;
using Lab1.Enums;
using Lab1.Models;
using Lab1.XmlProcessors;

namespace Lab1.IODataProcessors
{
    public class ConsoleReader
    {
        private readonly Context _context;
        private readonly XmlEntityReader _reader;

        public ConsoleReader(Context context, XmlProcessors.XmlEntityReader reader)
        {
            _context = context;
            _reader = reader;
        }

        public LicensedDriver ReadDriver()
        {
            Console.WriteLine("Creating a new licensed driver: ");
            var driver = new LicensedDriver();
            Console.Write("\tEnter license id: ");
            if (!int.TryParse(Console.ReadLine(), out var id))
                throw new InvalidCastException("The license id should be integer");

            driver.LicenseId = id;
            Console.Write("\tEnter name: ");
            driver.Name = Console.ReadLine();
            Console.Write("\tEnter surname: ");
            driver.Surname = Console.ReadLine();
            Console.Write("\tEnter patronymic: ");
            driver.Patronymic = Console.ReadLine();
            Console.Write("\tEnter date of birth: (dd.mm.yyyy) ");
            if (!DateTime.TryParse(Console.ReadLine(), out var date))
                throw new InvalidCastException("Invalid format of the date");

            driver.DateOfBirth = date;
            Console.Write("\tEnter registration city: ");
            driver.RegistrationAddress = Console.ReadLine();

            return driver;
        }
        public Vehicle ReadVehicle()
        {
            Console.WriteLine("Creating a new vehicle: ");
            var vehicle = new Vehicle();
            Console.Write("\tEnter Id: ");
            if (!int.TryParse(Console.ReadLine(), out var id))
                throw new InvalidCastException("The id should be integer");

            vehicle.Id = id;
            Console.WriteLine("Colors: ");
            var colors = Enum.GetValues(typeof(Color));
            for (var i = 0; i < colors.Length; i++)
            {
                Console.WriteLine($"{i} {colors.GetValue(i)}");
            }
            Console.Write("\n\tEnter color Id: ");
            if (!int.TryParse(Console.ReadLine(), out var colorId))
                throw new InvalidCastException("The id should be integer");
            if (colorId < 0 || colorId >= colors.Length)
                throw new IndexOutOfRangeException("Invalid id for color (so low or so high)");

            vehicle.Color = (Color)colorId;

            Console.WriteLine("Body Types: ");
            var bodyTypes = Enum.GetValues(typeof(BodyType));
            for (var i = 0; i < bodyTypes.Length; i++)
            {
                Console.WriteLine($"{i} {bodyTypes.GetValue(i)}");
            }
            Console.Write("\n\tEnter body type Id: ");
            if (!int.TryParse(Console.ReadLine(), out var bodyTypeId))
                throw new InvalidCastException("The id should be integer");
            if (bodyTypeId < 0 || bodyTypeId >= bodyTypes.Length)
                throw new IndexOutOfRangeException("Invalid id for body type (so low or so high)");

            vehicle.BodyType = (BodyType) bodyTypeId;
            Console.Write("\tEnter license plate: ");
            vehicle.LicensePlate = Console.ReadLine();
            Console.Write("\tEnter year of issue: (yyyy) ");
            if (!int.TryParse(Console.ReadLine(), out var year))
                throw new InvalidCastException("The year should be integer");
            if (!Regex.IsMatch(year.ToString(), "[0-9]{4}"))
                throw new InvalidCastException("Year should be 4-digit length");

            vehicle.YearOfIssue = year;
            Console.Write("\tEnter VIN code: ");
            vehicle.VinCode = Console.ReadLine();
            var models = _reader.GetModels(_context.Seed.ModelsXml);
            Console.WriteLine("Models:");
            foreach (var model in models)
            {
                Console.WriteLine(model);
            }
            Console.Write("\n\tEnter model Id: ");

            if (!int.TryParse(Console.ReadLine(), out var modelId))
                throw new InvalidCastException("The model id should be integer");
            if (modelId <= 0 || modelId > models.Count())
                throw new IndexOutOfRangeException("Invalid id for model (so low or so high)");

            vehicle.ModelId = modelId;
            Console.Write("\tEnter condition: ");
            vehicle.Condition = Console.ReadLine();

            return vehicle;
        }
        public Model ReadModel()
        {
            Console.WriteLine("Creating a new model: ");
            var model = new Model();
            Console.Write("\tEnter Id: ");
            if (!int.TryParse(Console.ReadLine(), out var id))
                throw new InvalidCastException("The id should be integer");

            model.Id = id;
            Console.Write("\tEnter name: ");
            model.Name = Console.ReadLine();
            Console.Write("\tEnter brand name: ");
            model.BrandName = Console.ReadLine();
            var manufacturers = _reader.GetManufacturers(_context.Seed.ManufacturersXml);
            Console.WriteLine("Manufacturers:");
            foreach (var manufacturer in manufacturers)
            {
                Console.WriteLine(manufacturer);
            }
            Console.Write("\n\tEnter manufacturer Id: ");
            if (!int.TryParse(Console.ReadLine(), out var manufacturerId))
                throw new InvalidCastException("The manufacturer id should be integer");
            if (manufacturerId <= 0 || manufacturerId > manufacturers.Count())
                throw new IndexOutOfRangeException("Invalid id for manufacturer (so low or so high)");
            model.ManufacturerId = manufacturerId;

            return model;
        }

        public Manufacturer ReadManufacturer()
        {
            Console.WriteLine("Creating a new manufacturer: ");
            var manufacturer = new Manufacturer();
            Console.Write("\tEnter Id: ");
            if (!int.TryParse(Console.ReadLine(), out var id))
                throw new InvalidCastException("The id should be integer");

            manufacturer.Id = id;
            Console.Write("\tEnter name: ");
            manufacturer.Name = Console.ReadLine();

            return manufacturer;
        }

        public VehicleDriver ReadVehicleDriver()
        {
            Console.WriteLine("Creating a new Vehicles To Drivers connection: ");
            var vehicleDriver = new VehicleDriver();
            var drivers = _reader.GetDrivers(_context.Seed.DriversXml); 
            Console.WriteLine("Drivers:");
            foreach (var driver in drivers)
            {
                Console.WriteLine(driver);
            }
            Console.Write("\n\tEnter driver Id: ");
            if (!int.TryParse(Console.ReadLine(), out var driverId))
                throw new InvalidCastException("The driver id should be integer");
            if (driverId <= 0 || driverId > drivers.Count())
                throw new IndexOutOfRangeException("Invalid id for driver (so low or so high)");

            vehicleDriver.DriverId = driverId;

            var vehicles = _reader.GetVehicles(_context.Seed.VehiclesXml); 
            Console.WriteLine("Vehicles:");
            foreach (var vehicle in vehicles)
            {
                Console.WriteLine(vehicle);
            }
            Console.Write("\n\tEnter vehicle Id: ");
            if (!int.TryParse(Console.ReadLine(), out var vehicleId))
                throw new InvalidCastException("The vehicle id should be integer");
            if (vehicleId <= 0 || vehicleId > vehicles.Count())
                throw new IndexOutOfRangeException("Invalid id for vehicle (so low or so high)");

            vehicleDriver.VehicleId = vehicleId;
            Console.Write("\tEnter if chosen driver is owner: (true/false)");
            if (!bool.TryParse(Console.ReadLine(), out var isOwner))
                throw new InvalidCastException("It should be either true or false only");
            vehicleDriver.IsOwner = isOwner;

            return vehicleDriver;
        }
    }
}
