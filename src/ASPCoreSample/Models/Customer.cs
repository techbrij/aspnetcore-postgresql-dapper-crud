using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace ASPCoreSample.Models
{
    public class Customer : BaseEntity
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Phone { get; set; }


        public string Address { get; set; }
    }
}
