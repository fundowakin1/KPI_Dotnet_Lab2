using Lab1.Enums;

namespace Lab1.Models
{
    public class Vehicle
    {
        public int Id { get; set; }
        public Color Color { get; set; }
        public BodyType BodyType { get; set; }
        public int YearOfIssue { get; set; }
        public string LicensePlate { get; set; }
        public string Condition { get; set; }
        public string VinCode { get; set; }
        public int ModelId { get; set; }
        public override string ToString()
        {
            return $"{Id}\t{Color}\t{BodyType}\t{YearOfIssue}\t{LicensePlate}\t{Condition}\t{VinCode}\t{ModelId}";
        }
    }

}
