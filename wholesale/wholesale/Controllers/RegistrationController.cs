using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using wholesale.Models;

namespace wholesale.Controllers
{
    public class RegistrationController : Controller
    {
        // GET: Registration
        public ActionResult Index()
        {
            return View();
        }

        public Models.ModelContext _db = new Models.ModelContext();

        public RedirectResult Signup()
        {
            //(HttpContext.Request.Form["surname"], HttpContext.Request.Form["number"]);

            var outher = from dict in _db.Users select dict;
            // List<Spravochnik> m = outher.ToList(); ;
            int id = 0;
            foreach (User sp in outher)
            {
                if (sp.Id > id)
                {
                    id = sp.Id;
                }
            }

            var add = new User
            {
                Id = id + 1,
                Username = HttpContext.Request.Form["username"],
                Password = HttpContext.Request.Form["password"],    
               
                Email = HttpContext.Request.Form["email"]                
            };
            _db.Users.Add(add);//добавляем 
            _db.SaveChanges();//сохраняем

            return Redirect("~/Login/Index");//переадресация в Index
        }
    }
}