using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Trabis.Test
{
    public abstract class BaseRequest
    {
        public bool? IsTestActive { get; set; }
    }
}