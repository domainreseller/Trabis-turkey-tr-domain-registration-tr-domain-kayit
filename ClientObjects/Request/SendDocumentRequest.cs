using Trabis.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Trabis.Test
{
    public class SendDocumentRequest : BaseRequest
    {
        public string DomainName { get; set; }

        public int Category { get; set; }

        public DocumentInfo DocumentInfo { get; set; }
    }
}