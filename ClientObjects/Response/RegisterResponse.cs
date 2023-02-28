using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Trabis.Test
{
    public class RegisterResponse : BaseResponse
    {
        /// <summary>
        /// waitingfordocument,active,waitingforregistration
        /// Olipso.Api.Trabis.Api.DomainStatusCodes
        /// </summary>
        public string Status { get; set; }

        public DomainInfo DomainInfo { get; set; }
    }
}