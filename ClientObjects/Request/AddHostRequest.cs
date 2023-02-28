using Trabis.Test;
using System.Collections.Generic;

namespace Trabis.Test
{
    public class AddHostRequest : BaseRequest
    {
        public string DomainName { get; set; }

        public Host Host { get; set; }
    }
}