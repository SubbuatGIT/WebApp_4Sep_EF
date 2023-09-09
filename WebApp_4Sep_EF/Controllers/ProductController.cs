using DBFirst;
using DBFirst.Entities;
using Microsoft.AspNetCore.Mvc;

namespace WebApp_4Sep_EF.Controllers
{
    public class ProductController : Controller
    {
        AppDBContext _dbContext;
        public ProductController(AppDBContext appDBContext)
        {
            _dbContext = appDBContext;
        }
        public IActionResult Index()
        {
            var products = _dbContext.Products.ToList();
            return View(products);
        }

        public IActionResult Create()
        {
           ViewBag.Categories = _dbContext.Categories.ToList();
            return View();
        }
        [HttpPost]
        public IActionResult Create(Product model )
        {
            if(ModelState.IsValid)
            {
                _dbContext.Products.Add(model);
                _dbContext.SaveChanges();
                return RedirectToAction("Index"); 
            }
            ViewBag.Categories = _dbContext.Categories.ToList();
            return View();
        }
        public IActionResult Edit(int id)
        {
            Product model = _dbContext.Products.Find(id);
            ViewBag.Categories = _dbContext.Categories.ToList();
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(Product model)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Products.Update(model);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Categories = _dbContext.Categories.ToList();
            return View();
        }

        public IActionResult Delete(int id)
        {
            Product model = _dbContext.Products.Find(id);
            if(model != null)
            {
                _dbContext.Products.Remove(model);
                _dbContext.SaveChanges(true);
            }
            return RedirectToAction("Index");
        }
    }
}
