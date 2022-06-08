namespace Lab1.Models
{
    public class VehicleDriver
    {
        public int DriverId { get; set; }
        public int VehicleId { get; set; }
        public bool IsOwner { get; set; }
        public override string ToString()
        {
            return $"{DriverId}\t{VehicleId}\t{IsOwner}";
        }
    }
}
