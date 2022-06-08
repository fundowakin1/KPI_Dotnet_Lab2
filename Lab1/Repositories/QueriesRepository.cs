using System;
using System.Collections.Generic;
using System.Linq;
using Lab1.Auxiliary;
using Lab1.Contexts;
using Lab1.Enums;
using Lab1.Extensions;
using Lab1.Models;
using Lab1.ValueConstants;
using Lab1.ViewModels;

namespace Lab1.Repositories
{
    public class QueriesRepository
    {
        private readonly Context _context;

        public QueriesRepository(Context context)
        {
            _context = context;
        }

        public IEnumerable<Vehicle> GetAllVehicles()
        {
            return from vehicle in _context.VehiclesXml.Root.Elements(EntityNameConstants.VehicleString)
                select vehicle.ToVehicle();
        }

        public IEnumerable<FullVehicleInfoViewModel> GetFullInfoAboutVehicles()
        {
            return from vehicle in _context.VehiclesXml.Root.Elements(EntityNameConstants.VehicleString)
                   join model in _context.ModelsXml.Root.Elements(EntityNameConstants.ModelString) 
                       on (int)vehicle.Element("ModelId") 
                       equals (int)model.Element("Id")
                join manufacturer in _context.ManufacturersXml.Root.Elements(EntityNameConstants.ManufacturerString) 
                    on (int)model.Element("ManufacturerId") 
                    equals (int)manufacturer.Element("Id")
                select new FullVehicleInfoViewModel()
                {
                    Vehicle = vehicle.ToVehicle(),
                    Model = model.ToModel(),
                    Manufacturer = manufacturer.ToManufacturer()
                };
        }

        public IEnumerable<LicensedDriver> GetDriversWithNameStartingWithA()
        {
            return from driver in _context.DriversXml.Root.Elements(EntityNameConstants.DriverString)
                   where driver.Element("Name").Value.StartsWith("A")
                select driver.ToLicensedDriver();
        }

        public IEnumerable<OwnerWithCarNameViewModel> GetVehicleOwnersWithModelNames()
        {
            return from driver in _context.DriversXml.Root.Elements(EntityNameConstants.DriverString)
                   join vehicleDriver in _context.VehicleDriversXml.Root.Elements(EntityNameConstants.VehicleDriverString) 
                       on (int)driver.Element("LicenseId") 
                       equals (int)vehicleDriver.Element("DriverId")
                join vehicle in _context.VehiclesXml.Root.Elements(EntityNameConstants.VehicleString) 
                    on (int)vehicleDriver.Element("VehicleId") 
                    equals (int)vehicle.Element("Id")
                join model in _context.ModelsXml.Root.Elements(EntityNameConstants.ModelString) 
                    on (int)vehicle.Element("ModelId") 
                    equals (int)model.Element("Id")
                where (bool)vehicleDriver.Element("IsOwner")
                select new OwnerWithCarNameViewModel()
                {
                    LicensedDriver = driver.ToLicensedDriver(), 
                    Model = model.ToModel()
                };
        }

        public IEnumerable<IGrouping<string, Vehicle>> GetVehiclesGroupedByCondition()
        {
            return from vehicle in _context.VehiclesXml.Root.Elements(EntityNameConstants.VehicleString)
                group vehicle by vehicle.Element("Condition").Value
                into newVehicle
                select new Grouping<string, Vehicle>()
                {
                    Key = newVehicle.Key,
                    Values = from g in newVehicle
                        select g.ToVehicle()
                };
        }

        public IEnumerable<LicensedDriver> GetNotOwnersOrderedByAge()
        {
           return from driver in _context.DriversXml.Root.Elements(EntityNameConstants.DriverString)
                  join vehicleDriver in _context.VehicleDriversXml.Root.Elements(EntityNameConstants.VehicleDriverString) 
                      on (int)driver.Element("LicenseId") 
                      equals (int)vehicleDriver.Element("DriverId")
                where !(bool)vehicleDriver.Element("IsOwner")
                orderby Convert.ToDateTime(driver.Element("DateOfBirth").Value)
                select driver.ToLicensedDriver();
        }

        public IEnumerable<VehicleDriverViewModel> GetRedAndBlackVehiclesWithDrivers()
        {
            return from vehicle in _context.VehiclesXml.Root.Elements(EntityNameConstants.VehicleString)
                   join vehicleDriver in _context.VehicleDriversXml.Root.Elements(EntityNameConstants.VehicleDriverString)
                       on (int)vehicle.Element("Id") 
                       equals (int)vehicleDriver.Element("VehicleId")
                join driver in _context.DriversXml.Root.Elements(EntityNameConstants.DriverString)
                    on (int)vehicleDriver.Element("DriverId") 
                    equals (int)driver.Element("LicenseId")
                where vehicle.Element("Color").Value.ParseToColor() is Color.Black or Color.Red
                select new VehicleDriverViewModel()
                {
                    Vehicle = vehicle.ToVehicle(), 
                    Driver = driver.ToLicensedDriver()
                };
            
        }

        public int GetVehiclesOlderThan3Years()
        {
            return (from vehicle in _context.VehiclesXml.Root.Elements(EntityNameConstants.VehicleString)
                    where (DateTime.Now.Year - (int)vehicle.Element("YearOfIssue")) > 3
                                     select vehicle).Count();
        }

        public IEnumerable<LicensedDriver> GetDriversWithoutLicense()
        {
            return _context.DriversXml.Root.Elements(EntityNameConstants.DriverString)
                .Except(
                    _context.DriversXml.Root.Elements(EntityNameConstants.DriverString)
                        .Join(_context.VehicleDriversXml.Root.Elements(EntityNameConstants.VehicleDriverString),
                            driver => (int)driver.Element("LicenseId"),
                            vehicleDriver => (int)vehicleDriver.Element("DriverId"),
                            (driver, vehicleDriver) => driver))
                .Select(driver => driver.ToLicensedDriver());
        }

        public IEnumerable<VehicleModelViewModel> GetAllAudiExceptBlack()
        {
            return _context.VehiclesXml.Root.Elements(EntityNameConstants.VehicleString)
                .Join(_context.ModelsXml.Root.Elements(EntityNameConstants.ModelString), 
                    vehicle => (int)vehicle.Element("ModelId"), 
                    model => (int)model.Element("Id"),
                    (vehicle, model) => new VehicleModelViewModel()
                    {
                        Vehicle = vehicle.ToVehicle(), 
                        Model = model.ToModel()
                    })
                .Where(car => car.Model.BrandName.Normalize().Equals("Audi".Normalize()) 
                              && car.Vehicle.Color != Color.Black);
        }

        public IEnumerable<EntityAndQuantityViewModel<Manufacturer>> GetManufacturerAndModelsQuantity()
        {
            return _context.ManufacturersXml.Root.Elements(EntityNameConstants.ManufacturerString)
                .Join(_context.ModelsXml.Root.Elements(EntityNameConstants.ModelString), 
                    manufacturer => (int)manufacturer.Element("Id"), 
                    model => (int)model.Element("ManufacturerId"), 
                    (manufacturer, model) => new {
                        manufacturer = manufacturer.ToManufacturer(), 
                        model = model.ToModel()
                    })
                .GroupBy(manufacturer => manufacturer.manufacturer)
                .Select(x => new EntityAndQuantityViewModel<Manufacturer>()
                {
                    Entity = x.Key, 
                    Quantity = x.Count()
                });
        }

        public AverageQuantityViewModel GetAverageVehiclesYearOfIssueAndQuantity()
        {
           return _context.VehiclesXml.Root.Elements(EntityNameConstants.VehicleString)
                .Select(x => new AverageQuantityViewModel()
                {
                    Count = _context.VehiclesXml.Root.Elements(EntityNameConstants.VehicleString).Count(), 
                    Average = _context.VehiclesXml.Root.Elements(EntityNameConstants.VehicleString)
                        .Average(y => (int)y.Element("YearOfIssue"))
                })
                .FirstOrDefault();
            }

        public IEnumerable<EntityAndQuantityViewModel<LicensedDriver>> GetDriversWith2AndMoreVehicles()
        {
           return _context.DriversXml.Root.Elements(EntityNameConstants.DriverString)
                .Join(_context.VehicleDriversXml.Root.Elements(EntityNameConstants.VehicleDriverString), 
                    driver => (int)driver.Element("LicenseId"), 
                    vehicleDriver => (int)vehicleDriver.Element("DriverId"),
                    (driver, vehicleDriver) => new {
                        driver = driver.ToLicensedDriver(), 
                        vehicleDriver = vehicleDriver.ToVehicleDriver()
                    })
                .GroupBy(x => x.driver)
                .Select(x => new EntityAndQuantityViewModel<LicensedDriver>()
                {
                    Entity = x.Key, 
                    Quantity = x.Count()
                })
                .Where(x => x.Quantity > 1);
        }

        public IEnumerable<Color> GetAllColorsOfVolkswagen()
        {
            return _context.ManufacturersXml.Root.Elements(EntityNameConstants.ManufacturerString)
                .Join(_context.ModelsXml.Root.Elements(EntityNameConstants.ModelString), 
                    manufacturer => (int)manufacturer.Element("Id"), 
                    model => (int)model.Element("ManufacturerId"),
                    (manufacturer, model) => (manufacturer, model))
                .Join(_context.VehiclesXml.Root.Elements(EntityNameConstants.VehicleString), 
                    tuple => (int)tuple.model.Element("Id"), 
                    vehicle => (int)vehicle.Element("ModelId"),
                    (tuple, vehicle) => new 
                    { 
                        tuple.manufacturer, 
                        vehicle = vehicle.ToVehicle()
                    })
                .Where(x => x.manufacturer.Element("Name").Value.Contains("Volkswagen"))
                .Select(x => x.vehicle.Color);
            
        }

        public IEnumerable<EntityAndQuantityViewModel<Model>> GetUniqueModelOfManufacturer()
        {
            return _context.ModelsXml.Root.Elements(EntityNameConstants.ModelString)
                .GroupBy(model => (int)model.Element("ManufacturerId"))
                .Select(x => new EntityAndQuantityViewModel<Model>()
                {
                    Entity = x.FirstOrDefault().ToModel(), 
                    Quantity = x.Count()
                })
                .Where(x => x.Quantity == 1);
           
        }
    }
}