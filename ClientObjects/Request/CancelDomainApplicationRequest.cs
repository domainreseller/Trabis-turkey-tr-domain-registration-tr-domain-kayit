using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Trabis.Test
{
    public class CancelDomainApplicationRequest : BaseRequest
    {
        public string DomainName { get; set; }
    }
}