using Trabis.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Trabis.Test
{
    public class UpdateNameServersRequest : BaseRequest
    {
        public string DomainName { get; set; }

        public IList<NameServer> CurrentNameServers { get; set; }
        public IList<NameServer> NewNameServers { get; set; }
    }
}