using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Trabis.Test
{
    public class RegContacts
    {
        public RegContacts()
        {
           
        }
        public ContactInfo Admin { get; set; }
        public ContactInfo Billing { get; set; }
        public ContactInfo Tech { get; set; }
    }
}