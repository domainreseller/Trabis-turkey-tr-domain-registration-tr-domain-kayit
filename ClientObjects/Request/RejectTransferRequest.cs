using Trabis.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Trabis.Test
{
    public class RejectTransferRequest : BaseRequest
    {
        public string DomainName { get; set; }

        public string Reason { get; set; }
    }
}