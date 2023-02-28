using Trabis.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Trabis.Test
{
    public class CheckAvailabilityResponse : BaseResponse
    {
        public IList<DomainCheckResult> DomainCheckResults { get; set; }
    }
}