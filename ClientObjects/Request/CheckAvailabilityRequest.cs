using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Trabis.Test
{
    public class CheckAvailabilityRequest : BaseRequest
    {
        public IList<string> DomainList { get; set; } 
    }
}