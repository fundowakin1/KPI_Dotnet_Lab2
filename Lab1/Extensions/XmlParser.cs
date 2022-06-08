using System;
using System.Xml.Linq;
using Lab1.Models;

namespace Lab1.Extensions
{
    public static class XmlParser
    {
        public static LicensedDriver ToLicensedDriver(this XElement element)
        {
            return new LicensedDriver()
            {
                LicenseId = Convert.ToInt32(element.Element("LicenseId")?.Value),
                Name = element.Element("Name")?.Value,
                Surname = element.Element("Surname")?.Value,
                Patronymic = element.Element("Patronymic")?.Value,
                DateOfBirth = Convert.ToDateTime(element.Element("DateOfBirth")?.Value),
                RegistrationAddress = element.Element("RegistrationAddress")?.Value
            };
        }

        public static Vehicle ToVehicle(this XElement element)
        {
            return new Vehicle()
            {
                Id = Convert.ToInt32(element.Element("Id")?.Value),
                BodyType = element.Element("BodyType")!.Value.ParseToBodyType(),
                Color = element.Element("Color")!.Value.ParseToColor(),
                Condition = element.Element("Condition")?.Value,
                LicensePlate = element.Element("LicensePlate")?.Value,
                ModelId = Convert.ToInt32(element.Element("ModelId")?.Value),
                VinCode = element.Element("VinCode")?.Value,
                YearOfIssue = Convert.ToInt32(element.Element("YearOfIssue")?.Value),
            };
        }

        public static Model ToModel(this XElement element)
        {
            return new Model()
            {
                Id = Convert.ToInt32(element.Element("Id")?.Value),
                BrandName = element.Element("BrandName")?.Value,
                Name = element.Element("Name")?.Value,
                ManufacturerId = Convert.ToInt32(element.Element("ManufacturerId")?.Value),
            };
        }

        public static Manufacturer ToManufacturer(this XElement element)
        {
            return new Manufacturer()
            {
                Id = Convert.ToInt32(element.Element("Id")?.Value),
                Name = element.Element("Name")?.Value,
            };
        }

        public static VehicleDriver ToVehicleDriver(this XElement element)
        {
            return new VehicleDriver()
            {
                VehicleId = Convert.ToInt32(element.Element("VehicleId")?.Value),
                DriverId = Convert.ToInt32(element.Element("DriverId")?.Value),
                IsOwner = Convert.ToBoolean(element.Element("IsOwner")?.Value)
            };
        }
    }
}
