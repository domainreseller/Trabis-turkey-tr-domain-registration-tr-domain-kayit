using Trabis.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Trabis.Test
{
    public class GetCountryResponse : BaseResponse
    {
        public List<CountryInfo> CountryInfoList { get; set; }
    }
}