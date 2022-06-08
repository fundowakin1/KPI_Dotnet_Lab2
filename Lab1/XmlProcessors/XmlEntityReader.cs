using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Lab1.Extensions;
using Lab1.Models;
using Lab1.ValueConstants;

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

            list.AddRange(xml.Descendants(EntityNameConstants.DriverString)
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

            list.AddRange(xml.Descendants(EntityNameConstants.VehicleString)
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

            list.AddRange(xml.Descendants(EntityNameConstants.ModelString)
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

            list.AddRange(xml.Descendants(EntityNameConstants.ManufacturerString)
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

            list.AddRange(xml.Descendants(EntityNameConstants.VehicleDriverString)
                .Select(vd => vd.ToVehicleDriver()));

            return list;
        }
        public IEnumerable<T> GetElements<T>(string filename) where T : class, new()
        {
            if (!File.Exists(filename))
                throw new FileNotFoundException($"There is no existing file '{filename}'");

            var list = new List<T>();

            var xml = XDocument.Load(filename);

            if (xml.Root == null)
                throw new FileLoadException($"The file {filename} is empty");

            var type = typeof(T);

            switch (type.Name)
            {
                case nameof(LicensedDriver):
                    var result = list as IEnumerable<LicensedDriver>;
                    result = GetDrivers(filename);
                    break;
            }
            //var properties = type.GetProperties();
            //var descendants = xml.Descendants(type.Name);
            //foreach (var element in descendants)
            //{
            //    var item = new T();
            //    foreach (var property in properties)
            //    {
            //        if (property.Name.Contains("Date"))
            //            property.SetValue(item, Convert.ToDateTime(element.Element(property.Name)?.Value));
            //        else if (property.Name.Contains("Id") || property.Name.Contains("Year"))
            //            property.SetValue(item, Convert.ToInt32(element.Element(property.Name)?.Value));
            //        else if (property.Name.Contains("Is"))
            //            property.SetValue(item, Convert.ToBoolean(element.Element(property.Name)?.Value));
            //        else if (property.Name.Contains("Color"))
            //            property.SetValue(item, element.Element(property.Name)?.Value.ParseToColor());
            //        else if (property.Name.Contains("BodyType"))
            //            property.SetValue(item, element.Element(property.Name)?.Value.ParseToBodyType());
            //        else
            //            property.SetValue(item, element.Element(property.Name)?.Value);
            //    }
            //    list.Add(item);
            //}


            return list;
        }
    }
}