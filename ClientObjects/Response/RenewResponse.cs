using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Trabis.Test
{
    public class RenewResponse : BaseResponse
    {
        public string ExpirationDate { get; set; }

        public DomainInfo DomainInfo { get; set; }
    }
}