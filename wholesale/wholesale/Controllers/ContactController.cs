using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using wholesale.Models;

namespace wholesale.Controllers
{
    public class ContactController : Controller
    {
        // GET: Contact

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

            return View();
        }

        public RedirectResult PostContactMessage(string Username, string Message, string Subject, string Email)
        {


            var outter = from dict in _db.ContactMessages select dict;//linq 

            int id = 0;
            foreach (ContactMessage sp in outter)
            {
                if (sp.Id > id)
                {
                    id = sp.Id;
                }
            }

            var add = new ContactMessage
            {
                Id = id + 1,
               Username = Username,
               Message = Message,
               Subject = Subject,
               Email = Email,
                Date = Convert.ToString(DateTime.Now)
            };

            _db.ContactMessages.Add(add);//добавляем 
            _db.SaveChanges();//сохраняем 




            return Redirect("~/Contact/Index");
        }
    }
}