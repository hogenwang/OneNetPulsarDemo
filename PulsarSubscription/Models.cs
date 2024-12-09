using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneNetPulsarDemo.PulsarSubscription
{
    public class PulsarMessageBody
    {
        public string data { get; set; }
        public string pv { get; set; }
        public string sign { get; set; }
        public int superMsg { get; set; }
        public long t { get; set; }
    }

}
