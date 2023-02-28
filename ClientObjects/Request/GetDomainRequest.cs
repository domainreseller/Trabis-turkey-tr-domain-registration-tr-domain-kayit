using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Trabis.Test
{
    public class GetDomainRequest : BaseRequest
    {
        public string DomainName { get; set; }

        public string AuthCode { get; set; }
    }
}