using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Trabis.Test
{
    public class RegistrarContact
    {
        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public string CitizenId { get; set; }

        public int CityId { get; set; }

        public int CountryId { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }

        public string NicHandle { get; set; }

        public string Organization { get; set; }

        public PhoneInfo Phone { get; set; }

        public PhoneInfo Fax { get; set; }

        public string TaxNumber { get; set; }

        public string TaxOffice { get; set; }

        public string WWW { get; set; }

        public string ZipCode { get; set; }

    }
}