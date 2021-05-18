using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Outfits.Data;
using Outfits.Models;

namespace Outfits.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public ProductsController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            this._hostEnvironment = hostEnvironment;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            return View(await _context.Product.ToListAsync());
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ImageFile,Name,Description,Price,Likes")] Product product)
        {
            if (ModelState.IsValid)
            {
                // Save image to wwwroot/image
                string wwwRootPath = _hostEnvironment.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(product.ImageFile.FileName);
                string extension = Path.GetExtension(product.ImageFile.FileName);
                product.ImageName = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                string path = Path.Combine(wwwRootPath + "/image/", fileName);

                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await product.ImageFile.CopyToAsync(fileStream);
                }

                    _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ImageFile,Name,Description,Price,Likes")] Product product)
        {
            string wwwRootPath = _hostEnvironment.WebRootPath;
            string fileName = Path.GetFileNameWithoutExtension(product.ImageFile.FileName);
            string extension = Path.GetExtension(product.ImageFile.FileName);
            product.ImageName = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            string path = Path.Combine(wwwRootPath + "/image/", fileName);

            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await product.ImageFile.CopyToAsync(fileStream);
            }
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!productExists(product.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Product.FindAsync(id);

            //delete image from wwroot/image
            var imagePath = Path.Combine(_hostEnvironment.WebRootPath, "image", product.ImageName);
            if (System.IO.File.Exists(imagePath))
                System.IO.File.Delete(imagePath);

            //delete the record
            _context.Product.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool productExists(int id)
        {
            return _context.Product.Any(e => e.Id == id);
        }

        public ActionResult AddToCart(int id)
        {
            Product update = _context.Product.ToList().Find(u => u.Id == id);
            update.IsInCart = 1;
            update.HasInCart += "*" + User.Identity.Name + "*";
            _context.SaveChanges();
            return RedirectToAction("ToCart");
        }
       
        public ActionResult RemoveFromCart(int id)
        {
            Product update = _context.Product.ToList().Find(u => u.Id == id);
            update.IsInCart = 0;
            update.HasInCart = update.WhoHasLiked.Replace("*" + User.Identity.Name + "*", "");
            _context.SaveChanges();
            return RedirectToAction("ToCart");
        }
    
        public ActionResult RemoveFromWishList(int id)
        {
            Product update = _context.Product.ToList().Find(u => u.Id == id);
            update.IsInWishList = 0;
            update.HasInWishList = update.WhoHasLiked.Replace("*" + User.Identity.Name + "*", "");
            _context.SaveChanges();
            return RedirectToAction("ToWishList");
        }

        public ActionResult Like(int id)
        {
            Product update = _context.Product.ToList().Find(u => u.Id == id);
            update.Likes += 1;
            update.Priority += "*" + User.Identity.Name + "*";
            update.WhoHasLiked += "*" + User.Identity.Name + "*";
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Dislike(int id)
        {
            Product update = _context.Product.ToList().Find(u => u.Id == id);
            update.Likes -= 1;
            update.WhoHasLiked = update.WhoHasLiked.Replace("*" + User.Identity.Name + "*", "");
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
  
        public ActionResult AddToWishList(int id)
        {
            Product update = _context.Product.ToList().Find(u => u.Id == id);
            update.IsInWishList = 1;
            update.Priority += "*" + User.Identity.Name + "*";
            update.HasInWishList += "*" + User.Identity.Name + "*";
            _context.SaveChanges();
            return RedirectToAction("ToWishList");
        }
 
        public async Task<IActionResult> ToCart()
        {
            return View("Cart", await _context.Product.ToListAsync());
        }

        public async Task<IActionResult> ToWishList()
        {
            return View("WishList", await _context.Product.ToListAsync());
        }

    }
}
