using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Add using:
using System.ComponentModel.DataAnnotations;

namespace MinimalOwinWebApiSelfHost.Models
{
    public class Company
    {
        // Add Key Attribute:
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
