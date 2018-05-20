using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tryEFonce.Models
{
    class Contact
    {
        public int ContactId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        [ForeignKey("Organizer")]
        public int OrganizerId { get; set; }
        public Organizer Organizer { get; set; }
    }
}
