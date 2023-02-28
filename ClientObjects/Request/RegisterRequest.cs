using Trabis.Test;
using System.Collections.Generic;

namespace Trabis.Test
{
    public class RegisterRequest : BaseRequest
    {
        public string DomainName { get; set; }

        public int Period { get; set; }

        public OwnerContact OwnerContact { get; set; }

        public DomainCategory DomainCategory { get; set; }

        public List<NameServer> NameServers { get; set; }

        public RegContacts RegContacts { get; set; }
    }
}