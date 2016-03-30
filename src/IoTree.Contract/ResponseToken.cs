using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IoTree.Contract
{
    public class ResponseToken
    {
        public string Method { get; set; }

        public string[] MethodParameters { get; set; }

        public int ErrorCode { get; set; }

        public string ErrorInfo { get; set; }

        public static ResponseToken Ok(string method, params object[] methodParameters)
        {
            return new ResponseToken
            {
                Method = method,
                MethodParameters = methodParameters.Select(o => o.ToString()).ToArray(),
                ErrorCode = 0
            };
        }

        public static ResponseToken<T> OkData<T>(T payload, string method, params object[] methodParameters)
        {
            return new ResponseToken<T>
            {
                Method = method,
                MethodParameters = methodParameters.Select(o => o.ToString()).ToArray(),
                ErrorCode = 0,
                Payload = payload
            };
        }

        public static ResponseToken Error(Exception e, string method, params object[] methodParameters)
        {
            return new ResponseToken
            {
                Method = method,
                MethodParameters = methodParameters.Select(o => o.ToString()).ToArray(),
                ErrorCode = -1,
                ErrorInfo = e.ToString()
            };
        }

        public static ResponseToken Error(string message, string method, params object[] methodParameters)
        {
            return new ResponseToken
            {
                Method = method,
                MethodParameters = methodParameters.Select(o => o.ToString()).ToArray(),
                ErrorCode = -1,
                ErrorInfo = message
            };
        }

        public static ResponseToken<T> ErrorData<T>(Exception e, T payload, string method, params object[] methodParameters)
        {
            return new ResponseToken<T>
            {
                Method = method,
                MethodParameters = methodParameters.Select(o => o.ToString()).ToArray(),
                ErrorCode = -1,
                ErrorInfo = e.ToString(),
                Payload = payload
            };
        }

        public static ResponseToken<T> ErrorData<T>(string message, T payload, string method, params object[] methodParameters)
        {
            return new ResponseToken<T>
            {
                Method = method,
                MethodParameters = methodParameters.Select(o => o.ToString()).ToArray(),
                ErrorCode = -1,
                ErrorInfo = message,
                Payload = payload
            };
        }
    }

    public class ResponseToken<TPayload> : ResponseToken
    {
        public TPayload Payload { get; set; }
    }
}
