using Trabis.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Trabis.Test
{
    public class SendEmailRequest : BaseRequest
    {
        public string DomainName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Category { get; set; }

    }
}