using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Trabis.Test
{
    public class TransferResponse : BaseResponse
    {
        /// <summary>
        /// waitingfordocument
        /// Olipso.Api.Trabis.Api.DomainStatusCodes
        /// </summary>
        public string Status { get; set; }
    }
}