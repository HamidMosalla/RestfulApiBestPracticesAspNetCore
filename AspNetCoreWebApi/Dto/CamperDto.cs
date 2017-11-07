using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreWebApi.Models;

namespace AspNetCoreWebApi.Dto
{
    public class CamperDto
    {
        public string CamperCode { get; set; }

        public string Name { get; set; }
        public int Age { get; set; }

        public IEnumerable<Address> Addresses { get; set; }
    }
}
