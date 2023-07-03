using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCO.DI.WMSEntities
{
    public class WMSAPIResponse
    {
        public object type { get; set; }
        public int status { get; set; }
        public string title { get; set; }
        public object url { get; set; }

        public string json { get; set; }
    }

}
