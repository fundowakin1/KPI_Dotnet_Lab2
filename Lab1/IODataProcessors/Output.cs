using System;
using System.Collections.Generic;
using System.Linq;
using Lab1.Enums;
using Lab1.Models;
using Lab1.Repositories;
using Lab1.ViewModels;

namespace Lab1.IODataProcessors
{
    public class Output
    {
        public void PrintVehicles(IEnumerable<Vehicle> vehicles)
        {
            Console.WriteLine("\nQuery 1");
            foreach (var info in vehicles)
            {
                Console.WriteLine($"{info.Id} {info.LicensePlate} {info.Condition}");
            }
        }

        public void PrintFullInfoAboutVehicles(IEnumerable<FullVehicleInfoViewModel> list)
        {
            Console.WriteLine("\nQuery 2");
            foreach (var info in list)
            {
                Console.WriteLine($"{info.Vehicle.LicensePlate} {info.Model.Name} " +
                                  $"{info.Model.BrandName} {info.Manufacturer.Name} " +
                                  $"{info.Vehicle.Color}");
            }
        }

        public void PrintDrivers(IEnumerable<LicensedDriver> drivers)
        {
            Console.WriteLine("\nQuery 3");
            foreach (var info in drivers)
            {
                Console.WriteLine($"{info.Name} {info.Surname} {info.Patronymic} ");
            }
        }

        public void PrintVehicleOwnersWithModelNames(IEnumerable<OwnerWithCarNameViewModel> list)
        {
            Console.WriteLine("\nQuery 4");
            foreach (var info in list)
            {
                Console.WriteLine($"{info.LicensedDriver.Name} {info.LicensedDriver.Surname} {info.Model.BrandName} {info.Model.Name}");
            }
        }

        public void PrintVehiclesGroupedByCondition(IEnumerable<IGrouping<string, Vehicle>> groups)
        {
            Console.WriteLine("\nQuery 5");
            foreach (var info in groups)
            {
                Console.Write("Key: ");
                Console.WriteLine(info.Key);
                foreach (var vehicle in info)
                {
                    Console.WriteLine($"\t{vehicle.Id} {vehicle.LicensePlate} ");
                }
            }
        }

        public void PrintDriversWithBirthdate(IEnumerable<LicensedDriver> drivers)
        {
            Console.WriteLine("\nQuery 6");
            foreach (var info in drivers)
            {
                Console.WriteLine($"{info.Name} {info.Surname} {info.DateOfBirth:dd/MM/yyyy}");
            }
        }

        public void PrintVehiclesAndDrivers(IEnumerable<VehicleDriverViewModel> list)
        {
            Console.WriteLine("\nQuery 7");
            foreach (var info in list)
            {
                Console.WriteLine($"{info.Vehicle.LicensePlate} {info.Vehicle.Color} {info.Driver.Name} {info.Driver.Surname}");
            }
        }

        public void PrintQuantity(int quantity)
        {
            Console.WriteLine("\nQuery 8");
            Console.WriteLine($"Count of cars: {quantity}");
        }

        public void PrintDriversWithoutLicense(IEnumerable<LicensedDriver> list)
        {
            Console.WriteLine("\nQuery 9");
            foreach (var info in list)
            {
                Console.WriteLine($"{info.Name} {info.Surname} {info.LicenseId}");
            }
        }

        public void PrintAudiExceptBlack(IEnumerable<VehicleModelViewModel> list)
        {
            Console.WriteLine("\nQuery 10");
            foreach (var info in list)
            {
                Console.WriteLine($"{info.Vehicle.Color} {info.Vehicle.LicensePlate} {info.Model.BrandName}");
            }
        }

        public void PrintModelsQuantityByManufacturer(IEnumerable<EntityAndQuantityViewModel<Manufacturer>> list)
        {
            Console.WriteLine("\nQuery 11");
            foreach (var info in list)
            {
                Console.WriteLine($"{info.Entity.Name} {info.Quantity}");
            }
        }

        public void PrintAverageAndQuantity(AverageQuantityViewModel model)
        {
            Console.WriteLine("\nQuery 12");
            Console.WriteLine($"Count: {model.Count} Average year of issue: {model.Average:0000.00}");
        }

        public void PrintVehiclesQuantityByDriver(IEnumerable<EntityAndQuantityViewModel<LicensedDriver>> list)
        {
            Console.WriteLine("\nQuery 13");
            foreach (var info in list)
            {
                Console.WriteLine($"{info.Entity.Name} {info.Entity.Surname} {info.Quantity}");
            }
        }

        public void PrintColors(IEnumerable<Color> colors)
        {
            Console.WriteLine("\nQuery 14\nVolkswagen colors:");
            foreach (var info in colors)
            {
                Console.WriteLine(info.ToString());
            }
        }

        public void PrintUniqueModelsOfManufacturers(IEnumerable<EntityAndQuantityViewModel<Model>> list)
        {
            Console.WriteLine("\nQuery 15:");
            foreach (var info in list)
            {
                Console.WriteLine(info.Entity.Name);
            }
        }

        public void RunQueries(QueriesRepository queries)
        {
            PrintVehicles(
                queries.GetAllVehicles());

            PrintFullInfoAboutVehicles(
                queries.GetFullInfoAboutVehicles());

            PrintDrivers(
                queries.GetDriversWithNameStartingWithA());

            PrintVehicleOwnersWithModelNames(
                queries.GetVehicleOwnersWithModelNames());

            PrintVehiclesGroupedByCondition(
                queries.GetVehiclesGroupedByCondition());

            PrintDriversWithBirthdate(
                queries.GetNotOwnersOrderedByAge());

            PrintVehiclesAndDrivers(
                queries.GetRedAndBlackVehiclesWithDrivers());

            PrintQuantity(
                queries.GetVehiclesOlderThan3Years());

            PrintDriversWithoutLicense(
                queries.GetDriversWithoutLicense());

            PrintAudiExceptBlack(
                queries.GetAllAudiExceptBlack());

            PrintModelsQuantityByManufacturer(
                queries.GetManufacturerAndModelsQuantity());

            PrintAverageAndQuantity(
                queries.GetAverageVehiclesYearOfIssueAndQuantity());

            PrintVehiclesQuantityByDriver(
                queries.GetDriversWith2AndMoreVehicles());

            PrintColors(
                queries.GetAllColorsOfVolkswagen());

            PrintUniqueModelsOfManufacturers(
                queries.GetUniqueModelOfManufacturer());
        }
    }
}