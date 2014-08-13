using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcTEST.Models;
using System.Collections;

namespace MvcTEST.HtmlHelpers
{
    public static class LanguageExtensions
    {
        public static IDictionary Items { get { return System.Web.HttpContext.Current.Items; } }

        public static string Translate2(string keyword)
        {
            try
            {
                var user = (tblUser)System.Web.HttpContext.Current.Session["User"];
                var dictionary = (List<tblDictionary>)System.Web.HttpContext.Current.Application["language"];
                SpeedCourtConfigDbEntities db = new SpeedCourtConfigDbEntities();

                var prijevod = db.getNameFromDictionary(keyword, user.ID).FirstOrDefault();
                return prijevod;

               
                
            }


            catch
            {
                return keyword;
            }
        }

        public static string Translate(string keyword)
        {
            try
            {
                var user = (tblUser)System.Web.HttpContext.Current.Session["User"];
                var dictionary = (List<tblDictionary>)System.Web.HttpContext.Current.Application["language"];
                SpeedCourtConfigDbEntities db = new SpeedCourtConfigDbEntities();

                var prijevod = db.getNameFromDictionary(keyword, user.ID).FirstOrDefault();
                return prijevod;

                //switch (user.LanguageID)
                //{
                //    case 1:
                //        return dictionary.Where(d => d.Name==keyword).FirstOrDefault().Name;
                //    //break;
                //    case 2:
                //        return dictionary.Where(d => d.Name==keyword).FirstOrDefault().NameHR;
                //    //break;
                //    case 3:
                //        //return dictionary.Where(d => d.Name==keyword).FirstOrDefault().NameENG;
                //        var prijevod = db.getNameFromDictionary(keyword, user.ID).FirstOrDefault();
                //        return prijevod;
                //    //break;
                //    case null:
                //        return dictionary.Where(d => d.Name.StartsWith(keyword)).FirstOrDefault().Name;
                //    default:
                //        return dictionary.Where(d => d.Name.StartsWith(keyword)).FirstOrDefault().Name;

                //}
            }


            catch
            {
                return keyword;
            }
        }
        public static string Translate(this HtmlHelper helper, string keyword)
        {
            return Translate(keyword);
            //SpeedCourtConfigDbEntities db = null;

            //if (Items["db"] == null)
            //{
            //    db = new SpeedCourtConfigDbEntities();
            //    Items["db"] = db;
            //}
            //else
            //    db = (SpeedCourtConfigDbEntities)Items["db"];

            //var user = (tblUser)System.Web.HttpContext.Current.Session["User"];
            //var user1 = Items["User"];
            //return db.getNameFromDictionary(keyword, user.ID ).FirstOrDefault().Name  ;
            try
            {
                var user = (tblUser)System.Web.HttpContext.Current.Session["User"];
                var dictionary = (List<tblDictionary>)System.Web.HttpContext.Current.Application["language"];
                SpeedCourtConfigDbEntities db = new SpeedCourtConfigDbEntities();

                var prijevod = db.getNameFromDictionary(keyword, user.ID).FirstOrDefault();
                return prijevod;

                //switch (user.LanguageID)
                //{
                //    case 1:
                //        return dictionary.Where(d => d.Name==keyword).FirstOrDefault().Name;
                //    //break;
                //    case 2:
                //        return dictionary.Where(d => d.Name==keyword).FirstOrDefault().NameHR;
                //    //break;
                //    case 3:
                //        //return dictionary.Where(d => d.Name==keyword).FirstOrDefault().NameENG;
                //        var prijevod = db.getNameFromDictionary(keyword, user.ID).FirstOrDefault();
                //        return prijevod;
                //    //break;
                //    case null:
                //        return dictionary.Where(d => d.Name.StartsWith(keyword)).FirstOrDefault().Name;
                //    default:
                //        return dictionary.Where(d => d.Name.StartsWith(keyword)).FirstOrDefault().Name;

                //}
            }


            catch
            {
                return keyword;
            }
        }
        public static tblUser User(this HtmlHelper helper)
        {
            var user = (tblUser)System.Web.HttpContext.Current.Session["user"];

            //testing mode: user in cookie
            if( user == null )
            {
                if( HttpContext.Current.Request.Cookies[ "user" ] != null )
                {
                    SpeedCourtConfigDbEntities db = new SpeedCourtConfigDbEntities( );
                    int cookieId = Int32.Parse( HttpContext.Current.Request.Cookies[ "user" ].Value );
                    user = db.tblUsers.Where( x => x.ID == cookieId ).FirstOrDefault( );
                    HttpContext.Current.Session[ "user" ] = user;
                }
            }
            else
            {
                if( HttpContext.Current.Request.Cookies[ "user" ] == null )
                {
                    HttpCookie cookie = cookie = new HttpCookie( "user" );
                    cookie.Value = user.ID.ToString( );
                    cookie.Expires = DateTime.Now.AddDays( 30 );
                    cookie.Path = "/";
                    cookie.Secure = HttpContext.Current.Request.IsSecureConnection;
                    HttpContext.Current.Response.Cookies.Add( cookie );
                }
            }

            return user;
        }
    }
}