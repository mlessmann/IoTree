using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Net;

namespace IoTree.Contract
{
    public class ResponseToken
    {
        public HttpStatusCode StatusCode { get; set; }

        public string Method { get; set; }

        public string ErrorInfo { get; set; }

        public static ResponseToken Ok(string method, params string[] methodParameters)
        {
            return new ResponseToken
            {
                Method = FormatMethod(method, methodParameters),
                StatusCode = HttpStatusCode.OK
            };
        }

        public static ResponseToken<T> OkData<T>(T payload, string method, params string[] methodParameters)
        {
            return new ResponseToken<T>
            {
                Method = FormatMethod(method, methodParameters),
                StatusCode = HttpStatusCode.OK,
                Payload = payload
            };
        }

        public static ResponseToken Error(Exception e, string method, params string[] methodParameters)
        {
            return new ResponseToken
            {
                Method = FormatMethod(method, methodParameters),
                StatusCode = HttpStatusCode.InternalServerError,
                ErrorInfo = e.ToString()
            };
        }

        public static ResponseToken<T> ErrorData<T>(Exception e, T payload, string method, params string[] methodParameters)
        {
            return new ResponseToken<T>
            {
                Method = FormatMethod(method, methodParameters),
                StatusCode = HttpStatusCode.InternalServerError,
                ErrorInfo = e.ToString(),
                Payload = payload
            };
        }

        private static string FormatMethod(string method, params string[] parameters)
        {
            return method + "(" + String.Join(", ", parameters) + ")";
        }
    }

    public class ResponseToken<TPayload> : ResponseToken
    {
        public TPayload Payload { get; set; }
    }
}
