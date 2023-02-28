using Trabis.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Trabis.Test
{
    public class GetCityResponse : BaseResponse
    {
        public List<CityInfo> CitiyInfoList { get; set; }

    }
}