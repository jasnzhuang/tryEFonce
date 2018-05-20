using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tryEFonce.Models
{
    class Event
    {
        public int EventId { get; set; }
        public string Name { get; set; }
        public DateTime EvenTime { get; set; }
        public ICollection<Customer> Customers { get; set; }
        [ForeignKey("Organizer")]
        public int OrganizerId { get; set; }
        public Organizer Organizer { get; set; }
    }
}
