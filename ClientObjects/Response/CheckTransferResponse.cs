using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Trabis.Test
{
    public class CheckTransferResponse : BaseResponse
    {
        public bool IsEligible { get; set; }

        public bool IsForced { get; set; }

        public bool WillRenew { get; set; }

        public string Reason { get; set; }
    }
}