using Trabis.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Trabis.Test
{
    public class TransferRequest : BaseRequest
    {
        public string DomainName { get; set; }

        public string AuthCode { get; set; }

        public RegContacts RegContacts { get; set; }
    }
}