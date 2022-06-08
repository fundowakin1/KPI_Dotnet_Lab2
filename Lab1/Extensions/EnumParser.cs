using System;
using Lab1.Enums;

namespace Lab1.Extensions
{
    public static class Parser
    {
        public static Color ParseToColor(this string value)
        {
            return value.ToUpper() switch
            {
                "RED" => Color.Red,
                "BlUE" => Color.Blue,
                "BLACK" => Color.Black,
                "GOLD" => Color.Gold,
                _ => throw new InvalidCastException("There is no color found")
            };
        }
        public static BodyType ParseToBodyType(this string value)
        {
            return value.ToUpper() switch
            {
                "COUPE" => BodyType.Coupe,
                "HATCHBACK" => BodyType.Hatchback,
                "MINIVAN" => BodyType.Minivan,
                "PICKUP" => BodyType.Pickup,
                "SEDAN" => BodyType.Sedan,
                _ => throw new InvalidCastException("There is no body type found")
            };
        }

    }
}
