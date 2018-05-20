using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tryEFonce.Models
{
    class Organizer
    {
        public int OrganizerId { get; set; }
        public string Name { get; set; }
        public ICollection<Event> Events { get; set; }
        public ICollection<Contact> Contacts { get; set; }
    }
}
