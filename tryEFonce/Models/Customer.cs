using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tryEFonce.Models
{
    class Customer
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        [ForeignKey("Events")]
        public int EventId { get; set; }
        public Event Events { get; set; }
    }
}
