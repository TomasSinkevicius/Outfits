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
    public class OutfitPostsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public OutfitPostsController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            this._hostEnvironment = hostEnvironment;
        }

        // GET: OutfitPosts
        public async Task<IActionResult> Index()
        {
            return View(await _context.OutfitPost.ToListAsync());
        }

        // GET: OutfitPosts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var outfitPost = await _context.OutfitPost
                .FirstOrDefaultAsync(m => m.Id == id);
            if (outfitPost == null)
            {
                return NotFound();
            }

            return View(outfitPost);
        }

        // GET: OutfitPosts/Create
        public IActionResult Create()
        {

            ViewBag.Products = GetProducts().Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Name.ToString(),

            }).ToList();


            return View();
        }

        // POST: OutfitPosts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ImageFile,username,Description,Product1,Product2,Product3")] OutfitPost outfitPost)
        {
            ViewBag.Products = GetProducts().Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Name.ToString(),

            }).ToList();

            if (ModelState.IsValid)
            {
                // Save image to wwwroot/image
                string wwwRootPath = _hostEnvironment.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(outfitPost.ImageFile.FileName);
                string extension = Path.GetExtension(outfitPost.ImageFile.FileName);
                outfitPost.ImageName = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                string path = Path.Combine(wwwRootPath + "/image/", fileName);

                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await outfitPost.ImageFile.CopyToAsync(fileStream);
                }

                _context.Add(outfitPost);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(outfitPost);
        }

        // GET: OutfitPosts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var outfitPost = await _context.OutfitPost.FindAsync(id);
            if (outfitPost == null)
            {
                return NotFound();
            }
            return View(outfitPost);
        }

        // POST: OutfitPosts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ImageFile,username,Description")] OutfitPost outfitPost)
        {
            string wwwRootPath = _hostEnvironment.WebRootPath;
            string fileName = Path.GetFileNameWithoutExtension(outfitPost.ImageFile.FileName);
            string extension = Path.GetExtension(outfitPost.ImageFile.FileName);
            outfitPost.ImageName = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            string path = Path.Combine(wwwRootPath + "/image/", fileName);

            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await outfitPost.ImageFile.CopyToAsync(fileStream);
            }
            if (id != outfitPost.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(outfitPost);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OutfitPostExists(outfitPost.Id))
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
            return View(outfitPost);
        }

        // GET: OutfitPosts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var outfitPost = await _context.OutfitPost
                .FirstOrDefaultAsync(m => m.Id == id);
            if (outfitPost == null)
            {
                return NotFound();
            }

            return View(outfitPost);
        }

        // POST: OutfitPosts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var outfitPost = await _context.OutfitPost.FindAsync(id);

            //delete image from wwroot/image
            var imagePath = Path.Combine(_hostEnvironment.WebRootPath, "image", outfitPost.ImageName);
            if (System.IO.File.Exists(imagePath))
                System.IO.File.Delete(imagePath);

            //delete the record
            _context.OutfitPost.Remove(outfitPost);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public List<Product> GetProducts()
        {
            List<Product> products = new List<Product>();
            Product product = new Product();
            product.Name = "";
            products.Add(product);
            var allProducts = _context.Product.ToList();
            foreach (var data in allProducts)
            {
                products.Add(data);
            }
            return products;
        }
        private bool OutfitPostExists(int id)
        {
            return _context.OutfitPost.Any(e => e.Id == id);
        }

        public async Task<IActionResult> Recommendation(int? id)
        {
            ViewBag.Products = GetProducts().Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Name.ToString(),

            }).ToList();
            var outfitPost = await _context.OutfitPost
                .FirstOrDefaultAsync(m => m.Id == id);


            return View(outfitPost);
        }
  

        public ActionResult Like(int id)
        {
            OutfitPost update = _context.OutfitPost.ToList().Find(u => u.Id == id);
            update.Likes += 1;
            update.WhoHasLiked += "*" + User.Identity.Name + "*";
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Dislike(int id)
        {
            OutfitPost update = _context.OutfitPost.ToList().Find(u => u.Id == id);
            update.Likes -= 1;
            update.WhoHasLiked = update.WhoHasLiked.Replace("*" + User.Identity.Name + "*", "");
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
      

        public ActionResult SetRecommendation1(int id, string Recommendation1)
        {
            OutfitPost update = _context.OutfitPost.ToList().Find(u => u.Id == id);
            update.Recommendation1 = Recommendation1;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult SetRecommendation2(int id, string Recommendation2)
        {
            OutfitPost update = _context.OutfitPost.ToList().Find(u => u.Id == id);
            update.Recommendation2 = Recommendation2;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult SetRecommendation3(int id, string Recommendation3)
        {
            OutfitPost update = _context.OutfitPost.ToList().Find(u => u.Id == id);
            update.Recommendation3 = Recommendation3;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
