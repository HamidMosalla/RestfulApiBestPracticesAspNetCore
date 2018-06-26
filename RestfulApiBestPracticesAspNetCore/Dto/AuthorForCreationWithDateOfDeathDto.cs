using System;

namespace RestfulApiBestPracticesAspNetCore.Dto
{
    public class AuthorForCreationWithDateOfDeathDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTimeOffset DateOfBirth { get; set; }
        public DateTimeOffset? DateOfDeath { get; set; }
        public string Genre { get; set; }
    }
}
