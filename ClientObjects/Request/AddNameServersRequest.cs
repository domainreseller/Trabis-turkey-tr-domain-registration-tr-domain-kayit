using Trabis.Test;
using System.Collections.Generic;

namespace Trabis.Test
{
    public class AddNameServersRequest : BaseRequest
    {
        public string DomainName { get; set; }

        public IList<NameServer> NameServers { get; set; }
    }
}