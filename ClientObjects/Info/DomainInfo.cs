using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Trabis.Test
{
    public class DomainInfo
    {
        public string DomainName { get; set; }

        public string StartDate { get; set; }

        public string ExpirationDate { get; set; }

        public bool Frozen { get; set; }

        public bool TransferLock { get; set; }

        public string AuthCode { get; set; }

        public DomainCategory DomainCategory { get; set; }

        /// <summary>
        /// Status of the domain (Available, Reserved, etc.) DOMAIN_ACTIVE = 0 : Domain is in use, 
        /// so one CAN NOT apply for it DOMAIN_SUSPENDED = 1 : Domain is suspended,
        /// so one CAN NOT apply for this domain DOMAIN_DELETED = 2 : Domain is deleted, 
        /// so one CAN NOT apply for this domain (used for special cases) DOMAIN_OLD = 3 : Domain is NOT in use, 
        /// so one CAN apply for this domain
        /// </summary>
        public string Status { get; set; }

        public List<NameServer> NameServers { get; set; }

        public List<AttributeInfo> AttributeInfos { get; set; }

        public RegistrarContact RegistrarContact { get; set; }

        public OwnerContact OwnerContact { get; set; }

        public RegContacts RegContacts { get; set; }

        public string LastUpdateDatabase { get; set; }
    }
}