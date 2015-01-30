using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using HealthPortal.Models;
using System.Web.SessionState;
using System.Security.Principal;
using System.Web.Security;
using HealthPortal.Controllers;

namespace HealthPortal
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {

            //Exception exception = Server.GetLastError();
            //HttpException httpException = exception as HttpException;
            //route.RouteHandler = new MyHttpControllerRouteHandler();
            //GlobalConfiguration.Configuration.Filters.Add(new UserDataModificationActionFilter());
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        //protected void Application_Error(object sender, EventArgs e)
        //{
        //    var httpContext = ((WebApiApplication)sender).Context;
        //    var currentController = " ";
        //    var currentAction = " ";
        //    var currentRouteData = RouteTable.Routes.GetRouteData(new HttpContextWrapper(httpContext));

        //    if (currentRouteData != null)
        //    {
        //        if (currentRouteData.Values["controller"] != null && !String.IsNullOrEmpty(currentRouteData.Values["controller"].ToString()))
        //        {
        //            currentController = currentRouteData.Values["controller"].ToString();
        //        }

        //        if (currentRouteData.Values["action"] != null && !String.IsNullOrEmpty(currentRouteData.Values["action"].ToString()))
        //        {
        //            currentAction = currentRouteData.Values["action"].ToString();
        //        }
        //    }

        //    var ex = Server.GetLastError();
        //    var controller = new ErrorController();
        //    var routeData = new RouteData();
        //    var action = "Index";

        //    if (ex is HttpException)
        //    {
        //        var httpEx = ex as HttpException;

        //        switch (httpEx.GetHttpCode())
        //        {
        //            case 404:
        //                action = "NotFound";
        //                break;

        //            case 401:
        //                action = "AccessDenied";
        //                break;
        //        }
        //    }

        //    httpContext.ClearError();
        //    httpContext.Response.Clear();
        //    httpContext.Response.StatusCode = ex is HttpException ? ((HttpException)ex).GetHttpCode() : 500;
        //    httpContext.Response.TrySkipIisCustomErrors = true;

        //    routeData.Values["controller"] = "Error";
        //    routeData.Values["action"] = action;

        //    controller.ViewData.Model = new HandleErrorInfo(ex, currentController, currentAction);
        //    ((IController)controller).Execute(new RequestContext(new HttpContextWrapper(httpContext), routeData));
        //}
        protected void Application_Error(object sender, EventArgs e)
        {
            Exception lastError = Server.GetLastError();
            Server.ClearError();

            int statusCode = 0;

            if (lastError.GetType() == typeof(HttpException))
            {
                statusCode = ((HttpException)lastError).GetHttpCode();
            }
            else
            {
                // Not an HTTP related error so this is a problem in our code, set status to
                // 500 (internal server error)
                statusCode = 500;
            }

            HttpContextWrapper contextWrapper = new HttpContextWrapper(this.Context);

            RouteData routeData = new RouteData();
            routeData.Values.Add("controller", "Error");
            routeData.Values.Add("action", "Index");
            routeData.Values.Add("statusCode", statusCode);
            routeData.Values.Add("exception", lastError);
            routeData.Values.Add("isAjaxRequet", contextWrapper.Request.IsAjaxRequest());

            IController controller = new ErrorController();

            RequestContext requestContext = new RequestContext(contextWrapper, routeData);

            controller.Execute(requestContext);
            Response.End();
            //Exception exception = Server.GetLastError();
            //Server.ClearError();

            //RouteData routeData = new RouteData();
            //routeData.Values.Add("controller", "Error");
            //routeData.Values.Add("action", "Index");
            //routeData.Values.Add("exception", exception);

            //if (exception.GetType() == typeof(HttpException))
            //{
            //    routeData.Values.Add("statusCode", ((HttpException)exception).GetHttpCode());
            //}
            //else
            //{
            //    routeData.Values.Add("statusCode", 500);
            //}

            //IController controller = new ErrorController();
            //controller.Execute(new RequestContext(new HttpContextWrapper(Context), routeData));
            //Response.End();
        }
    }
}