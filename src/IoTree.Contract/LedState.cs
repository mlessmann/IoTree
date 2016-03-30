using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Net;

namespace IoTree.Contract
{
    public class LedState
    {
        public int Led { get; set; }
        public double Value { get; set; }
    }
}
