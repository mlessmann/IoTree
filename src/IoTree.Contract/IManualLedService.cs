using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace IoTree.Contract
{
    [ServiceContract]
    public interface IManualLedService
    {
        [OperationContract]
        [WebGet(
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "getleds")]
        ResponseToken<double[]> GetLeds();

        [OperationContract]
        [WebGet(
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "setled/{led}/{value}")]
        ResponseToken SetLed(string led, string value);
    }
}
