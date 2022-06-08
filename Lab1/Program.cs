using System;
using Lab1.Contexts;
using Lab1.MenuProcessors;
using Lab1.Seeders;

namespace Lab1
{
    public class Program
    {
        public static void Main()
        {
            var xml = new XmlSeed();
            var context = new Context(xml);
            
            var menu = new MenuDisplay(context);
            while (true)
            {
                menu.MainMenu();
                Console.Clear();
            }
        }
    }
}