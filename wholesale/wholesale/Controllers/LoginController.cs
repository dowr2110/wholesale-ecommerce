using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using wholesale.Models;

namespace wholesale.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ErrorLogin()
        {
            ViewBag.error = "Incorrect email or password";
            return View();
        }
        
        public Models.ModelContext _db = new Models.ModelContext();
        
        public RedirectResult Signin()
        {
            var outter = from dict in _db.Users select dict ;//linq  
            int t = 0;
            int id = 0;
            foreach (User sp in outter)
            {
                if (sp.Username == HttpContext.Request.Form["username"] && sp.Password == HttpContext.Request.Form["password"])
                {
                    id = sp.Id;
                    t++;
                }
                
            }

            if (t>0)
            {
                return Redirect("~/Product/Index?id="+id);
            }
            else
            {
                return Redirect("~/Login/ErrorLogin");
            }
             
        }



       
    }
}