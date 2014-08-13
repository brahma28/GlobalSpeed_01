using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Security;
using System.Data.Linq.Mapping;
using System.Web.Mvc;

namespace MvcTEST.Models
{
    public class GameData
    {

        public int userid { get; set;  }           
        public int gameid { get; set;  }     
    }

    public class MyViewModel
    {
        public string Id { get; set; }
        public IEnumerable<SelectListItem> Items { get; set; }
    }



}