using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Agency04.Models
{
    public class EmailEvents
    {
        [Required]
        [Display(Name = "To (Email Address)")]
        public string ToEmail { get; set; }
    }
}
