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
            Seed = xml;
            DriversXml = XDocument.Load(xml.DriversXml);
            VehiclesXml = XDocument.Load(xml.VehiclesXml);
            ModelsXml = XDocument.Load(xml.ModelsXml);
            ManufacturersXml = XDocument.Load(xml.ManufacturersXml);
            VehicleDriversXml = XDocument.Load(xml.VehicleDriversXml);
        }
    }
}
