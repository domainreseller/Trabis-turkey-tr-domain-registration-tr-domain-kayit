using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Trabis.Test
{
    public class ModifyPrivacyResponse : BaseResponse
    {
        public string DomainName { get; set; }

        public bool AddressHidden { get; set; }

        public bool EmailHidden { get; set; }

        public bool NameHidden { get; set; }

        public bool PhoneHidden { get; set; }

        public bool FaxHidden { get; set; }
    }
}