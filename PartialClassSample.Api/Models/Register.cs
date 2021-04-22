using System;
using System.Collections.Generic;

#nullable disable

namespace PartialClassSample.Api.Models
{
    public partial class Register
    {
        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string PassWord { get; set; }
    }
}
