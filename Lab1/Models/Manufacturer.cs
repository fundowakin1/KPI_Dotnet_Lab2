namespace Lab1.Models
{
    public class Manufacturer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public override string ToString()
        {
            return $"{Id}\t{Name}";
        }

        public override bool Equals(object obj)
        {
            var manufacturer = obj as Manufacturer;
            return manufacturer.Id == Id && manufacturer.Name.Equals(Name);
        }
        public override int GetHashCode()
        {
            unchecked
            {
                return (Id * 397) ^ (Name != null ? Name.GetHashCode() : 0);
            }
        }
    }
}
