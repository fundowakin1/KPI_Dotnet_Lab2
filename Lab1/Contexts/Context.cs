using System;
using System.Xml.Linq;
using Lab1.Seeders;

namespace Lab1.Contexts
{
    public class Context
    {
        public XmlSeed Seed { get; }
        public XDocument DriversXml { get; set; }
        public XDocument VehiclesXml { get; set; }
        public XDocument ModelsXml { get; set; }
        public XDocument ManufacturersXml { get; set; }
        public XDocument VehicleDriversXml { get; set; }

        public Context(XmlSeed xml)
        {
            Seed = xml ?? throw new NullReferenceException("Seed was null");
            Update();
        }

        public void Update()
        {
            DriversXml = XDocument.Load(Seed.DriversXml);
            VehiclesXml = XDocument.Load(Seed.VehiclesXml);
            ModelsXml = XDocument.Load(Seed.ModelsXml);
            ManufacturersXml = XDocument.Load(Seed.ManufacturersXml);
            VehicleDriversXml = XDocument.Load(Seed.VehicleDriversXml);
        }
    }
}
