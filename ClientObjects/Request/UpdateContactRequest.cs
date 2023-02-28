using Trabis.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Trabis.Test
{
    public class UpdateContactRequest : BaseRequest
    {
        public string DomainName { get; set; }

        public OwnerContact OwnerContact { get; set; }

        public DomainCategory DomainCategory { get; set; }

        public RegContacts RegContacts { get; set; }

        public bool isOwnerChange { get; set; }

    }
}