using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Task45.Models
{
	public class User : IdentityUser
	{
        public string Name { get; set; }

        public string Surname { get; set; }
        
        public string Status { get; set; }

        [DataType(DataType.Date)]
        public DateTime RegisterDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime LastLoginDate { get; set; }
    }
}

