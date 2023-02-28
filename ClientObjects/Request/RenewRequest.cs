using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Trabis.Test
{
    public class RenewRequest : BaseRequest
    {
        public string DomainName { get; set; }

        public int Period { get; set; }
    }
}