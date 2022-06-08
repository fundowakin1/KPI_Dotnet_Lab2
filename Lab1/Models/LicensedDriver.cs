using System;

namespace Lab1.Models
{
    public class LicensedDriver
    {
        public int LicenseId { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string RegistrationAddress { get; set; }

        public override bool Equals(object obj)
        {
            return obj is LicensedDriver driver && driver.Surname==Surname && driver.Name==Name;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = LicenseId;
                hashCode = (hashCode * 397) ^ (Surname != null ? Surname.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Patronymic != null ? Patronymic.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ DateOfBirth.GetHashCode();
                hashCode = (hashCode * 397) ^ (RegistrationAddress != null ? RegistrationAddress.GetHashCode() : 0);
                return hashCode;
            }
        }

        public override string ToString()
        {
            return $"{LicenseId}\t{Surname}\t{Name}\t{Patronymic}\t{DateOfBirth:dd.MM.yyyy}\t{RegistrationAddress}";
        }
    }
}
