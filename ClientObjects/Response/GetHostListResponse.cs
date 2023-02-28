using Trabis.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Trabis.Test
{
    public class GetHostListResponse : BaseResponse
    {
        public IList<Host> Hosts { get; set; }
    }
}