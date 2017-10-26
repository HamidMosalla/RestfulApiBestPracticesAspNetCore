using System.ComponentModel.DataAnnotations;

namespace AspNetCoreWebApi.Models
{
    public class Address
    {
        [Key]
        public int AddressId { get; set; }
        public string Country { get; set; }
        public string Province { get; set; }
        public string StreetName { get; set; }

        public int CamperId { get; set; }
        public Camper Camper { get; set; }
    }
}