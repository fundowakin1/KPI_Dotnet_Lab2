using System;
using System.IO;
using System.Xml.Linq;

namespace Lab1.XmlProcessors
{
    public class XmlEntityWriter 
    {
        public void AddElement<T>(string filename, T item)
        {
            var type = item.GetType();
            var properties = type.GetProperties();
            var elements = new XElement[properties.Length];
            for (var i = 0; i < properties.Length; i++)
            {
                if (properties[i].GetValue(item) is DateTime)
                    elements[i] = new XElement(properties[i].Name, $"{properties[i].GetValue(item):dd.MM.yyyy}");
                else
                    elements[i] = new XElement(properties[i].Name, properties[i].GetValue(item));
            }

            var newElement = new XElement(type.Name, elements);

            XDocument xDoc;

            if (File.Exists(filename))
            {
                xDoc = XDocument.Load(filename);

                if (xDoc.Root is null)
                    throw new NullReferenceException();

                if (!xDoc.Root.Name.LocalName.Equals($"{type.Name}s"))
                    throw new InvalidOperationException();
                xDoc.Root.Add(newElement);

            }
            else
            {
                xDoc = new XDocument(
                    new XElement($"{type.Name}s", newElement)
                );
            }

            xDoc.Save(filename);
        }

        public void Dispose()
        {
            
        }
    }
}
