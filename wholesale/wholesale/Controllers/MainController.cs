using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using wholesale.Models;

namespace wholesale.Controllers
{
    public class MainController : Controller
    {
        // GET: Main
        public Models.ModelContext _db = new Models.ModelContext();

        public ActionResult Index(string id)
        {
            ////////////////////////////////////////////////////////////////
            var outter2 = from dict in _db.Users select dict;//linq 
            string username = "Login";
            int user_id = Convert.ToInt32(id);

            foreach (User pr in outter2)
            {
                if (pr.Id == Convert.ToInt32(id))
                {
                    username = pr.Username;

                }

            }
            ViewBag.username = username;

            ////////////////////////////////////////////////////////////////

            ///////////////////////////////////////////////////
            var outterForCountInCard = from dict in _db.ShopCards select dict;//linq 
            int countInCart = 0;

            foreach (ShopCard pr in outterForCountInCard)
            {
                if (pr.UserId == Convert.ToInt32(id))
                {
                    countInCart++;
                }

            }
            ViewBag.countInCart = countInCart;
            /////////////////////////////////////////////////////
            var outterForCountInWish = from dict in _db.WishLists select dict;//linq 
            int countInWish = 0;

            foreach (Wishlist pr in outterForCountInWish)
            {
                if (pr.UserId == Convert.ToInt32(id))
                {
                    countInWish++;
                }

            }
            ViewBag.countInWish = countInWish;
            /////////////////////////////////////////////////////

            ViewBag.user_id = user_id;

            ////////////////////////////////////////////////////////////
           
            //////////////////////////////////////////////////////////////
            var outter = from dict in _db.Products select dict;//linq 
            ViewBag.Products = outter.OrderBy(u => u.Id).ToList();
            //////////////////////////////////////////////////////////////////
            var outter22 = from dict in _db.Categories select dict;//linq 
            ViewBag.category = outter22.OrderBy(u => u.Id).ToList();

            var outternews = from dict in _db.Newses select dict;//linq 
            ViewBag.outternews = outternews.OrderBy(u => u.Id).ToList();



            return View();
        }
    }
}