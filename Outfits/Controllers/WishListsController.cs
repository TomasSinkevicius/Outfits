using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Outfits.Controllers
{
    public class WishListsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
