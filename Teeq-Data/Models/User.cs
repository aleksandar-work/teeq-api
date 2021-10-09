using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Teeq_Data.Models
{
    public class User
    {
        public Guid Id { get; set; }

        public IdentityUser IdentityUser { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }

        public string FirebaseId { get; set; }
    }
}
