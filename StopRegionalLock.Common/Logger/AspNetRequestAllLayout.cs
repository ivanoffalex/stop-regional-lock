using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLog.LayoutRenderers;
using System.Web;
using System.Web.SessionState;

namespace StopRegionalLock.Common.Logger
{
    [LayoutRenderer("aspnet-request-all")]
    public class AspNetRequestAllLayout : LayoutRenderer
    {
        protected override void Append(StringBuilder builder, NLog.LogEventInfo logEvent)
        {
            AddpendRequestAll(builder);
        }

        internal static void AddpendRequestAll(StringBuilder builder)
        {
            HttpRequest currentRequest = HttpContext.Current != null ? HttpContext.Current.Request : null;
            if (currentRequest != null)
            {
                builder.AppendFormat("Url: {0}\n", currentRequest.Url);
                if (currentRequest.UrlReferrer != null)
                {
                    builder.AppendFormat("From: {0}\n", currentRequest.UrlReferrer);
                }
                foreach (string s in currentRequest.QueryString)
                {
                    builder.AppendFormat("QueryString: {0}: {1}\n", s, currentRequest.QueryString[s]);
                }
                foreach (string s in currentRequest.Form)
                {
                    if (!string.IsNullOrEmpty(s) && !s.StartsWith("_"))
                    {
                        builder.AppendFormat("Form: {0}: {1}\n", s, currentRequest.Form[s]);
                    }
                }
                foreach (string cookieKey in currentRequest.Cookies.Keys)
                {
                    if (!cookieKey.StartsWith("_") &&
                        !cookieKey.StartsWith("ASP.NET"))
                    {
                        builder.AppendFormat("Cookie: {0}: {1}\n", cookieKey, currentRequest.Cookies[cookieKey].Value);
                    }
                }
                if (!string.IsNullOrEmpty(currentRequest.Headers["X-AjaxPro-Method"]))
                {
                    builder.AppendFormat("AjaxPro Method: {0}\n", currentRequest.Headers["X-AjaxPro-Method"]);
                    builder.AppendFormat("AjaxPro JSON: {0}\n", HttpContext.Current.Items["AjaxPro.JSON"]);
                }

                builder.AppendFormat("UserAgent: {0}\n", currentRequest.UserAgent);
                builder.AppendFormat("UserHostAddress: {0}\n", currentRequest.UserHostAddress);
                builder.AppendFormat("UserHostName: {0}\n", currentRequest.UserHostName);
            }
        }
    }
}
