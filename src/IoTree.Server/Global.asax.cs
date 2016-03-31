using NLog;
using System;
using System.Diagnostics;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace IoTree.Server
{
    public class WebApiApplication : HttpApplication
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private static int requestCounter = 0;

        protected void Application_Start()
        {
            logger.Info("Application starting.");
            AreaRegistration.RegisterAllAreas();

            UnityConfig.RegisterComponents();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            logger.Info("Application started.");
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            int requestId = Interlocked.Increment(ref requestCounter);
            Request.RequestContext.HttpContext.Items.Add("RequestId", requestId);
            Request.RequestContext.HttpContext.Items.Add("RequestTime", Stopwatch.GetTimestamp());
            logger.Debug("Req {0} ({1}): Received \"{2} {3}\"",
                requestId, Request.UserHostName, Request.HttpMethod, Request.Url.PathAndQuery);
        }

        protected void Application_EndRequest(object sender, EventArgs e)
        {
            int requestId = (int)Request.RequestContext.HttpContext.Items["RequestId"];
            long startTime = (long)Request.RequestContext.HttpContext.Items["RequestTime"];
            long endTime = Stopwatch.GetTimestamp();
            var duration = TimeSpan.FromTicks(endTime - startTime);
            logger.Debug("Req {0} ({1}): {2} ({3} ms)",
                requestId, Request.UserHostName, Response.Status, (int)duration.TotalMilliseconds);
        }

        protected void Application_Error()
        {
            logger.Fatal(Server.GetLastError());
        }
    }
}