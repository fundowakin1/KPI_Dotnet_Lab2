using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Lab1.Extensions;
using Lab1.Models;

namespace Lab1.XmlProcessors
{
    public class XmlEntityReader
    {
        public IEnumerable<LicensedDriver> GetDrivers(string filename)
        {
            if (!File.Exists(filename))
                throw new FileNotFoundException($"There is no existing file '{filename}'");

            var list = new List<LicensedDriver>();
            var xml = XDocument.Load(filename);

            if (xml.Root == null)
                throw new FileLoadException($"The file {filename} is empty");

            list.AddRange(xml.Root.Elements()
                .Select(driver => driver.ToLicensedDriver()));

            return list;
        }

        public IEnumerable<Vehicle> GetVehicles(string filename)
        {
            if (!File.Exists(filename))
                throw new FileNotFoundException($"There is no existing file '{filename}'");

            var list = new List<Vehicle>();
            var xml = XDocument.Load(filename);

            if (xml.Root == null)
                throw new FileLoadException($"The file {filename} is empty");

            list.AddRange(xml.Root.Elements()
                .Select(vehicle => vehicle.ToVehicle()));

            return list;
        }

        public IEnumerable<Model> GetModels(string filename)
        {
            if (!File.Exists(filename))
                throw new FileNotFoundException($"There is no existing file '{filename}'");

            var list = new List<Model>();
            var xml = XDocument.Load(filename);

            if (xml.Root == null)
                throw new FileLoadException($"The file {filename} is empty");

            list.AddRange(xml.Root.Elements()
                .Select(model => model.ToModel()));

            return list;
        }
        public IEnumerable<Manufacturer> GetManufacturers(string filename)
        {
            if (!File.Exists(filename))
                throw new FileNotFoundException($"There is no existing file '{filename}'");

            var list = new List<Manufacturer>();
            var xml = XDocument.Load(filename);

            if (xml.Root == null)
                throw new FileLoadException($"The file {filename} is empty");

            list.AddRange(xml.Root.Elements()
                .Select(manufacturer => manufacturer.ToManufacturer()));

            return list;
        }
        public IEnumerable<VehicleDriver> GetVehicleDrivers(string filename)
        {
            if (!File.Exists(filename))
                throw new FileNotFoundException($"There is no existing file '{filename}'");

            var list = new List<VehicleDriver>();
            var xml = XDocument.Load(filename);

            if (xml.Root == null)
                throw new FileLoadException($"The file {filename} is empty");

            list.AddRange(xml.Root.Elements()
                .Select(vd => vd.ToVehicleDriver()));

            return list;
        }
    }
}