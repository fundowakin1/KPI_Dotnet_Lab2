namespace Lab1.Models
{
    public class Model
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string BrandName { get; set; }
        public int ManufacturerId { get; set; }

        public override string ToString()
        {
            return $"{Id}.\t{Name}\t{BrandName}\t{ManufacturerId}";
        }
    }
}
