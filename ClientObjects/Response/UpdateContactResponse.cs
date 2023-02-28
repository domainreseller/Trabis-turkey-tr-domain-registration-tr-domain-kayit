using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Trabis.Test
{
    public class UpdateContactResponse : BaseResponse
    {
        public DomainInfo DomainInfo { get; set; }

        public string OwnerChangeResult { get; set; }
    }
}