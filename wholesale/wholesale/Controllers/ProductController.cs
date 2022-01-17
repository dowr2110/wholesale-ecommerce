using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using wholesale.Models;


namespace wholesale.Controllers
{
    public class ProductController : Controller
    {
        public Models.ModelContext _db = new Models.ModelContext();

        public ActionResult Index(string id)
        {
            var outter = from dict in _db.Products select dict;//linq 


            ViewBag.Products = outter.OrderBy(u => u.Id).ToList();

            var outter22 = from dict in _db.Categories select dict;//linq 


            ViewBag.categories = outter22.OrderBy(u => u.Id).ToList();


            var tehnikcatregory = from dict in _db.Products where dict.CategoryId == 1 select dict;//linq 
            ViewBag.tehnikcatregory = tehnikcatregory.Count();

            var clothescatregory = from dict in _db.Products where dict.CategoryId == 2 select dict;//linq 
            ViewBag.clothescatregory = clothescatregory.Count();

            var fruitscatregory = from dict in _db.Products where dict.CategoryId == 3 select dict;//linq 
            ViewBag.fruitscatregory = fruitscatregory.Count();


          

            var wishlists = from dict in _db.WishLists select dict;//linq 
            ViewBag.wishlists = wishlists.OrderBy(u => u.Id).ToList();

            ViewBag.productCount = outter.Count();

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

        public ActionResult SingleProduct(string product_id, string user_id)
        {
            var outter = from dict in _db.Products select dict;//linq 
                                                               //Product product = outter.FirstOrDefault();

            var outter22 = from dict in _db.Categories select dict;//
            ViewBag.categories = outter22.OrderBy(u => u.Id).ToList();

            var users = from dict in _db.Users select dict;//
            ViewBag.users = users.OrderBy(u => u.Id).ToList();

            int prod_id = Convert.ToInt32(product_id);
            var comments = from dict in _db.Comments where dict.ProductId == prod_id select dict;//
            ViewBag.comments = comments.OrderBy(u => u.Id).ToList();

            string prod_name = "null";
            string price = "null";
            string discription = "null";
            int category_id = 1;
            int compane_id = 1;
            int id = 0;
            foreach (Product pr in outter)
            {
                if (pr.Id == Convert.ToInt32(product_id))
                {
                    prod_name = pr.Prod_name;
                    price = Convert.ToString(pr.Price);
                    id = pr.Id;
                    category_id = pr.CategoryId;
                    compane_id = pr.CompanyId;
                    discription = pr.Discription;
                }

            }
         
            ViewBag.prod_name = prod_name;
            ViewBag.price = price;
            ViewBag.discription = discription;
            ViewBag.category_id = category_id;
            ViewBag.compane_id = compane_id;
            ViewBag.id = id;


            ////////////////////////////////////////////////////////////////
            var outter2 = from dict in _db.Users select dict;//linq 
            string username = "Login";
            int user_idd = Convert.ToInt32(user_id);

            foreach (User pr in outter2)
            {
                if (pr.Id == Convert.ToInt32(user_id))
                {
                    username = pr.Username; 
                } 
            }
            ViewBag.username = username;
            ViewBag.user_id = user_idd;

            ////////////////////////////////////////////////////////////////


            ///////////////////////////////////////////////////
            var outterForCountInCard = from dict in _db.ShopCards select dict;//linq 
            int countInCart = 0;
            

            foreach (ShopCard pr in outterForCountInCard)
            {
                if (pr.UserId == Convert.ToInt32(user_id))
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
                if (pr.UserId == Convert.ToInt32(user_id))
                {
                    countInWish++;
                }
            }
            ViewBag.countInWish = countInWish;
            ////////////////////////////////////////////////////////////////////////
            var wishlists = from dict in _db.WishLists select dict;//linq 
            ViewBag.wishlists = wishlists.OrderBy(u => u.Id).ToList();

            return View();
        }
         
        public RedirectResult AddToCard(string product_id2, int count, int user_id)
        {
            var outter = from dict in _db.ShopCards select dict;//linq 
                                                                //Product product = outter.FirstOrDefault();

            
            // List<Spravochnik> m = outher.ToList(); ;
            int id = 0;
            foreach (ShopCard sp in outter)
            {
                if (sp.Id > id)
                {
                    id = sp.Id;
                }
            }

            var outter2 = from dict in _db.ShopCards where dict.UserId == user_id select dict;//linq 
            int flag = 0;
            foreach (ShopCard sp in outter2)
            {
                if (sp.ProductId == Convert.ToInt32(product_id2))
                {
                    sp.Count += Convert.ToInt32(count);

                    _db.Entry(sp).State = EntityState.Modified;
                    flag++;
                }
            }

            int prid = Convert.ToInt32(product_id2);
            Product product = _db.Products.FirstOrDefault(p => p.Id == prid);
              
            if (flag == 0)
            {
                var add = new ShopCard
                {
                    Id = id + 1,
                    ProductId = Convert.ToInt32(product_id2),
                    UserId = user_id,
                    Count = Convert.ToInt32(count),
                    Sum = product.Price * Convert.ToInt32(count)
                };

         

                var add2 = new ShopCartForOrder
                {
                    Id = id + 1,
                    ProductId = Convert.ToInt32(product_id2),
                    Id_User = user_id,
                    Count = Convert.ToInt32(count),
                    Sum = product.Price * Convert.ToInt32(count)
                };
                _db.ShopCartForOrders.Add(add2);//добавляем 
                _db.ShopCards.Add(add);//добавляем 
                _db.SaveChanges();//сохраняем
            }

            _db.SaveChanges();

            return Redirect("~/Product/SingleProduct?product_id="+product_id2+"&user_id="+user_id);
        }

        public RedirectResult AddToCard2(string product_id2, int count, int user_id)
        {
            var outter = from dict in _db.ShopCards select dict;//linq 
                                                                //Product product = outter.FirstOrDefault();


            // List<Spravochnik> m = outher.ToList(); ;
            int id = 0;
            foreach (ShopCard sp in outter)
            {
                if (sp.Id > id)
                {
                    id = sp.Id;
                }
            }

            var outter2 = from dict in _db.ShopCards where dict.UserId == user_id select dict;//linq 
            int flag = 0;
            foreach (ShopCard sp in outter2)
            {
                if (sp.ProductId == Convert.ToInt32(product_id2))
                {
                    sp.Count += Convert.ToInt32(count);

                    _db.Entry(sp).State = EntityState.Modified;
                    flag++;
                }
            }

            int prid = Convert.ToInt32(product_id2);
            Product product = _db.Products.FirstOrDefault(p => p.Id == prid);

            if (flag == 0)
            {
                var add = new ShopCard
                {
                    Id = id + 1,
                    ProductId = Convert.ToInt32(product_id2),
                    UserId = user_id,
                    Count = Convert.ToInt32(count),
                    Sum = product.Price * Convert.ToInt32(count)
                };



                var add2 = new ShopCartForOrder
                {
                    Id = id + 1,
                    ProductId = Convert.ToInt32(product_id2),
                    Id_User = user_id,
                    Count = Convert.ToInt32(count),
                    Sum = product.Price * Convert.ToInt32(count)
                };
                _db.ShopCartForOrders.Add(add2);//добавляем 
                _db.ShopCards.Add(add);//добавляем 
                _db.SaveChanges();//сохраняем
            }

            _db.SaveChanges();

            return Redirect("~/Product/Index?id=" + user_id);
        }
        public ActionResult Cart( int user_id)
        {

            var outter = from dict in _db.ShopCards where dict.UserId == user_id select dict;//linq 


            ViewBag.Products = outter.OrderBy(u => u.Id).ToList();

            ////////////////////////////////////////////////////////////////
            var outter2 = from dict in _db.Users select dict;//linq 
            string username = "Login";
            //int user_id = Convert.ToInt32(id);

            foreach (User pr in outter2)
            {
                if (pr.Id == Convert.ToInt32(user_id))
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
                if (pr.UserId == Convert.ToInt32(user_id))
                {
                    countInCart++;
                }

            }
            ViewBag.countInCart = countInCart;
            /////////////////////////////////////////////////////


            ViewBag.user_id = user_id;
            var allProducts = from dict in _db.Products select dict;//linq 
            ViewBag.allProducts = allProducts;

            //////////////////////////////////////////////////////
            var outterForCountInWish = from dict in _db.WishLists select dict;//linq 
            int countInWish = 0;

            foreach (Wishlist pr in outterForCountInWish)
            {
                if (pr.UserId == user_id)
                {
                    countInWish++;
                }
            }
            ViewBag.countInWish = countInWish;

            return View();
        }

        public ActionResult WishList(int user_id)
        {

            var outter = from dict in _db.WishLists where dict.UserId == user_id select dict;//linq 


            ViewBag.Products = outter.OrderBy(u => u.Id).ToList();

            ////////////////////////////////////////////////////////////////
            var outter2 = from dict in _db.Users select dict;//linq 
            string username = "Login";
            //int user_id = Convert.ToInt32(id);

            foreach (User pr in outter2)
            {
                if (pr.Id == Convert.ToInt32(user_id))
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
                if (pr.UserId == Convert.ToInt32(user_id))
                {
                    countInCart++;
                }

            }
            ViewBag.countInCart = countInCart;
            /////////////////////////////////////////////////////


            ViewBag.user_id = user_id;
            var allProducts = from dict in _db.Products select dict;//linq 
            ViewBag.allProducts = allProducts;

            //////////////////////////////////////////////////////////
            var outterForCountInWish = from dict in _db.WishLists select dict;//linq 
            int countInWish = 0;

            foreach (Wishlist pr in outterForCountInWish)
            {
                if (pr.UserId == Convert.ToInt32(user_id))
                {
                    countInWish++;
                }
            }
            ViewBag.countInWish = countInWish;

            return View();
        }


        public RedirectResult UpdSave(int user_id, int shopcardid, string count)
        {
            var outter = from dict in _db.ShopCards where dict.UserId == user_id select dict;//linq  
            foreach (ShopCard sp in outter)
            {
                if (sp.Id == shopcardid)
                {
                    sp.Count = Convert.ToInt32(count);
              
                    _db.Entry(sp).State = EntityState.Modified;
                }
            }
            var outter2 = from dict in _db.ShopCartForOrders where dict.Id_User == user_id select dict;//linq  
            foreach (ShopCartForOrder sp in outter2)
            {
                if (sp.Id == shopcardid)
                {
                    sp.Count = Convert.ToInt32(count);

                    _db.Entry(sp).State = EntityState.Modified;
                }
            }
            _db.SaveChanges();

            return Redirect("~/Product/Cart?user_id=" + user_id);
        }

        
        public RedirectResult DelSave(int user_id, int prod_id)
        {
            var outter = from dict in _db.ShopCards select dict;//linq  
            foreach (ShopCard sp in outter)
            {
                if (sp.Id == prod_id)
                {
                    _db.ShopCards.Remove(sp);
                }
            }

            var outter2 = from dict in _db.ShopCartForOrders select dict;//linq  
            foreach (ShopCartForOrder sp in outter2)
            {
                if (sp.Id == prod_id)
                {
                    _db.ShopCartForOrders.Remove(sp);
                }
            }
            _db.SaveChanges();

            return Redirect("~/Product/Cart?user_id="+user_id);
        }

        public RedirectResult DelSaveWish(int user_id, int prod_id)
        {
            var outter = from dict in _db.WishLists select dict;//linq  
            foreach (Wishlist sp in outter)
            {
                if (sp.Id == prod_id)
                {
                    _db.WishLists.Remove(sp);
                }
            }

           
            _db.SaveChanges();

            return Redirect("~/Product/WishList?user_id=" + user_id);
        }

        public ActionResult Checkout(int user_id)
        {

            var outter = from dict in _db.ShopCards where dict.UserId == user_id select dict;//linq 


            ViewBag.Products = outter.OrderBy(u => u.Id).ToList();

            ////////////////////////////////////////////////////////////////
            var outter2 = from dict in _db.Users select dict;//linq 
            string username = "Login";
            int user_idd = Convert.ToInt32(user_id);

            foreach (User pr in outter2)
            {
                if (pr.Id == Convert.ToInt32(user_id))
                {
                    username = pr.Username;
                }
            }
            ViewBag.username = username;
            ViewBag.user_id = user_idd;

            ////////////////////////////////////////////////////////////////


            ///////////////////////////////////////////////////
            var outterForCountInCard = from dict in _db.ShopCards select dict;//linq 
            int countInCart = 0;


            foreach (ShopCard pr in outterForCountInCard)
            {
                if (pr.UserId == Convert.ToInt32(user_id))
                {
                    countInCart++;
                }

            }
            ViewBag.countInCart = countInCart;
            /////////////////////////////////////////////////////

            var allProducts = from dict in _db.Products select dict;//linq 
            ViewBag.allProducts = allProducts;

            //////////////////////////////////////////////////////

            var outterForCountInWish = from dict in _db.WishLists select dict;//linq 
            int countInWish = 0;

            foreach (Wishlist pr in outterForCountInWish)
            {
                if (pr.UserId == user_id)
                {
                    countInWish++;
                }
            }
            ViewBag.countInWish = countInWish;


            return View();
        }

        public ActionResult Confirmation(int user_id, int productidInCart, string telno, string cartno, string company, string add1, string city, string country, string bank, int subtotal, string message)
        {
            var outter = from dict in _db.Orders select dict;//linq 
            //Product product = outter.FirstOrDefault();


            // List<Spravochnik> m = outher.ToList(); ;
            int id = 0;
            foreach (Order sp in outter)
            {
                if (sp.Id > id)
                {
                    id = sp.Id;
                }
            } 

            var add = new Order
            {
                Id = id + 1,
                UserId = user_id,
                Sum = subtotal,
                Date = Convert.ToString(DateTime.Now),
                Telno = telno,
                Cartno = cartno,
                Company = company,
                Add1 = add1,
                City = city,
                Country = country,
                Bank = bank,
              /*  ShopCard2_id = productidInCart,*/
                Message = message,
                Status = false
            };
                _db.Orders.Add(add);//добавляем 
                _db.SaveChanges();//сохраняем

            ////////////////////////////////////////////////////////////////////////////

            Order order = _db.Orders.FirstOrDefault(p => p.Id == id + 1);
           /* var orders = from dict in _db.Orders where dict.Id == id + 1 select dict;*/
            ViewBag.orders = order;

            /////////////////////////////////////////////// 
           
            var outt = from dict in _db.ShopCards where dict.UserId == order.UserId  select dict;//linq 
            ViewBag.shopcards = outt.OrderBy(u => u.Id).ToList();

            ////////////////////////////////////////////////////
            var products = from dict in _db.Products select dict;//linq 
            ViewBag.products = products;

            ViewBag.user_id = user_id;

            //////////////////////////////////////////////////////////
            var outter2 = from dict in _db.Users select dict;//linq 
            string username = "Login";
            int user_idd = Convert.ToInt32(user_id);

            foreach (User pr in outter2)
            {
                if (pr.Id == Convert.ToInt32(user_id))
                {
                    username = pr.Username;
                }
            }
            ViewBag.username = username;
            //////////////////////////////////////////////////////////

            var outterForCountInWish = from dict in _db.WishLists select dict;//linq 
            int countInWish = 0;

            foreach (Wishlist pr in outterForCountInWish)
            {
                if (pr.UserId == user_id)
                {
                    countInWish++;
                }
            }
            ViewBag.countInWish = countInWish;

            return View();
        }

        public RedirectResult DeleteCart(int user_id )
        {

            var outter2 = from dict in _db.ShopCards select dict;//linq  
            foreach (ShopCard sp in outter2)
            {
                if (sp.UserId == user_id)
                {
                    _db.ShopCards.Remove(sp);
                }
            }
            _db.SaveChanges();

            return Redirect("~/Product/Index?id=" + user_id);
        }
        public ActionResult HistoryOfUser(int user_id)
        {
            var outter = from dict in _db.Orders where dict.UserId == user_id select dict;//linq 
            ViewBag.orders = outter.OrderBy(u => u.Id).ToList();

             

            /////////////////////////////////////////////// products

            var outt = from dict in _db.ShopCartForOrders where dict.Id_User == user_id select dict;//linq 
            ViewBag.shopcards2 = outt.OrderBy(u => u.Id).ToList();

            ////////////////////////////////////////////////////для User_id
            var products = from dict in _db.Products select dict;//linq 
            ViewBag.products = products;

            ViewBag.user_id = user_id;

            //////////////////////////////////////////////////////////для вывода имени
            var outter2 = from dict in _db.Users select dict;//linq 
            string username = "Login";
            int user_idd = Convert.ToInt32(user_id);

            foreach (User pr in outter2)
            {
                if (pr.Id == Convert.ToInt32(user_id))
                {
                    username = pr.Username;
                }
            }
            ViewBag.username = username;
            //////////////////////////////////////////////////////////

            ///////////////////////////////////////////////////
            var outterForCountInCard = from dict in _db.ShopCards select dict;//linq 
            int countInCart = 0;

            foreach (ShopCard pr in outterForCountInCard)
            {
                if (pr.UserId == user_id)
                {
                    countInCart++;
                }

            }
            ViewBag.countInCart = countInCart;
            /////////////////////////////////////////////////////
            ///
            var outterForCountInWish = from dict in _db.WishLists select dict;//linq 
            int countInWish = 0;

            foreach (Wishlist pr in outterForCountInWish)
            {
                if (pr.UserId == user_id)
                {
                    countInWish++;
                }
            }
            ViewBag.countInWish = countInWish;

            return View();
        }

        public RedirectResult PostCommentSave(string product_id2, int user_id , string message)
        {
            var outter = from dict in _db.Comments select dict;//linq 
                                                                //Product product = outter.FirstOrDefault();


            // List<Spravochnik> m = outher.ToList(); ;
            int id = 0;
            foreach (Comment sp in outter)
            {
                if (sp.Id > id)
                {
                    id = sp.Id;
                }
            }

           

            int prid = Convert.ToInt32(product_id2);
        

                var add = new Comment
                {
                    Id = id + 1,
                    ProductId = Convert.ToInt32(product_id2),
                     UserId = user_id,
                    Text = message,
                    Date = Convert.ToString(DateTime.Now)
                };



               
                _db.Comments.Add(add);//добавляем 
                _db.SaveChanges();//сохраняем 

            return Redirect("~/Product/SingleProduct?product_id=" + product_id2 + "&user_id=" + user_id);
        }

        public RedirectResult AddToWishList(string product_id2, int user_id)
        {
            var outter = from dict in _db.WishLists select dict;//linq 
                                                                //Product product = outter.FirstOrDefault();


            // List<Spravochnik> m = outher.ToList(); ;
            int id = 0;
            int flaag = 0;
            foreach (Wishlist sp in outter)
            {
                if (sp.Id > id)
                {
                    id = sp.Id;
                }
                if (sp.ProductId == Convert.ToInt32(product_id2))
                {
                    flaag++;
                }

            }

            // var outter2 = from dict in _db.WishLists where dict.UserId == user_id select dict;//linq 
            if (flaag>0)
            {
                foreach (Wishlist sp in outter)
                {
                    if (sp.ProductId == Convert.ToInt32(product_id2))
                    {
                        _db.WishLists.Remove(sp);
                        
                    }
                }
                _db.SaveChanges();//сохраняем
            }
            else
            {

                var add = new Wishlist
                {
                    Id = id + 1,
                    ProductId = Convert.ToInt32(product_id2),
                    UserId = user_id

                };
                _db.WishLists.Add(add);//добавляем 
                _db.SaveChanges();//сохраняем
            }

               
                
            

            return Redirect("~/Product/Index?id=" + user_id);
        }

        public RedirectResult AddToWishList2(string product_id2, int user_id)
        {
            var outter = from dict in _db.WishLists select dict;//linq 
                                                                //Product product = outter.FirstOrDefault();


            // List<Spravochnik> m = outher.ToList(); ;
            int id = 0;
            int flaag = 0;
            foreach (Wishlist sp in outter)
            {
                if (sp.Id > id)
                {
                    id = sp.Id;
                }
                if (sp.ProductId == Convert.ToInt32(product_id2))
                {
                    flaag++;
                }

            }

            // var outter2 = from dict in _db.WishLists where dict.UserId == user_id select dict;//linq 
            if (flaag > 0)
            {
                foreach (Wishlist sp in outter)
                {
                    if (sp.ProductId == Convert.ToInt32(product_id2))
                    {
                        _db.WishLists.Remove(sp);

                    }
                }
                _db.SaveChanges();//сохраняем
            }
            else
            {

                var add = new Wishlist
                {
                    Id = id + 1,
                    ProductId = Convert.ToInt32(product_id2),
                    UserId = user_id

                };
                _db.WishLists.Add(add);//добавляем 
                _db.SaveChanges();//сохраняем
            }

            return Redirect("~/Product/SingleProduct?product_id="+product_id2+"&user_id="+user_id);
        }



    }
}