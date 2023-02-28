using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Trabis.Test
{
    public class DomainCheckResult
    {
        public string Domain { get; set; }

        /// <summary>
        /// available,notavailable,unsupportedtld
        /// </summary>
        public string Status { get; set; }
    }
}