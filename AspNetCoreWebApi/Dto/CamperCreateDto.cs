using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AspNetCoreWebApi.Models;

namespace AspNetCoreWebApi.Dto
{
    public class CamperCreateDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }

        public ICollection<Address> Addresses { get; set; }
    }
}