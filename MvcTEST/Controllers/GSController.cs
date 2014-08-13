using MvcTEST.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MvcTEST.Controllers
{
    public class GSController : Controller
    {
        //
        // GET: /GS/
        public tblUser GetCurrentUser()
        {
            tblUser user = (tblUser)Session["user"];
            if( user == null )
            {
                if( Request.Cookies[ "user" ] != null )
                {
                    SpeedCourtConfigDbEntities db = new SpeedCourtConfigDbEntities( );
                    int cookieId = Int32.Parse(Request.Cookies[ "user" ].Value );
                    user = db.tblUsers.Where( x => x.ID == cookieId ).FirstOrDefault( );
                    Session[ "user" ] = user;
                }
            }
            else
            {
                if( Request.Cookies[ "user" ] == null )
                {
                    HttpCookie cookie = cookie = new HttpCookie( "user" );
                    cookie.Value = user.ID.ToString( );
                    cookie.Expires = DateTime.Now.AddDays( 30 );
                    cookie.Path = "/";
                    cookie.Secure = Request.IsSecureConnection;
                    Response.Cookies.Add( cookie );
                }
            }
            return user;
        }

        public static CultureInfo hrCulture = new CultureInfo("hr-HR");

        protected override void ExecuteCore()
        {
            //try
            //{
                Thread.CurrentThread.CurrentUICulture = hrCulture;
                Thread.CurrentThread.CurrentCulture = hrCulture;
                // try catch: cim istekne login, redirekcija na pocetni logon ekran !
                base.ExecuteCore();
            //}
            //catch
            //{
            //}
        }

        //protected override void OnActionExecuting(ActionExecutingContext filterContext)
        //{
        //    try
        //    {
        //        base.OnActionExecuting(filterContext);
        //    }
        //    catch
        //    {
        //        filterContext.Result = new RedirectToRouteResult(
        //        new RouteValueDictionary { { "controller", "Logon" }, { "action", "Index" } });
        //        base.OnActionExecuting(filterContext);
        //    }
        //}

        //protected override void OnResultExecuting(ResultExecutingContext filterContext)
        //{
        //    try
        //    {
        //        base.OnResultExecuting(filterContext);
        //    }
        //    catch
        //    {
        //        filterContext.Result = new RedirectToRouteResult(
        //        new RouteValueDictionary { { "controller", "Logon" }, { "action", "Index" } });
        //        base.OnResultExecuting(filterContext);
        //    }
        //}
    }
}
