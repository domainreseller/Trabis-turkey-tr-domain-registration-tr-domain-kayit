

using Trabis.Test;
using System.Collections.Generic;

namespace Trabis.Test
{
    public class DeleteNameServersRequest : BaseRequest
    {
        public string DomainName { get; set; }

        public IList<NameServer> NameServers { get; set; }
    }
}