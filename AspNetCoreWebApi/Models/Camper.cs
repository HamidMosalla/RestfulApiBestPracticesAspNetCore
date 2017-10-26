using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AspNetCoreWebApi.Models
{
    public class Camper
    {
        //I shouldn't be using entities directly, but for this trivial sample, it works
        [Key]
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }

        public virtual ICollection<Address> Addresses { get; set; }
    }
}