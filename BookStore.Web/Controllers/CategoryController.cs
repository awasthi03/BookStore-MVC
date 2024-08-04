using Book.DataAccess.Data;
using Book.Models.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Web.Controllers
{
    public class CategoryController : Controller
        
    {
        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            List<Category> objCategoryList = _db.Categories.ToList();
            return View(objCategoryList);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category obj)
        {
            // We don't want our Name and Display Order to be same;
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "Name and Display Order can not be same");
            }

            if (ModelState.IsValid)
            {
                _db.Categories.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Category Created Successfully";
                TempData["success1"] = "Done";
                TempData["ToastrSuccess"] = "Category Created Successfully";
                return RedirectToAction("Index", "Category");
            }
            return View();
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? category = _db.Categories.Find(id);
            //Category? obj = _db.Categories.FirstOrDefault(u => u.Id == id);
            //Category? obj = _db.Categories.Where(u => u.Id == id).FirstOrDefault();
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost]
        public IActionResult Edit(Category updatedCategory)
        {

            /*if (updatedCategory == null)
            {
                return NotFound();
            }
            // We don't want our Name and Display Order to be same;
            if (updatedCategory.Name == updatedCategory.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "Name and Display Order can not be same");
            }*/

            if (ModelState.IsValid)
            {
                /*Category? oldCategory = _db.Categories.Find(updatedCategory.Id);
                oldCategory.Name = updatedCategory.Name;
                oldCategory.DisplayOrder = updatedCategory.DisplayOrder;*/
                _db.Categories.Update(updatedCategory);
                _db.SaveChanges();
                TempData["success"] = "Category Updated Successfully";
                TempData["success2"] = "Done";
                TempData["ToastrSuccess"] = "Category Updated Successfully";
                return RedirectToAction("Index", "Category");
            }
            return View();
        }

        public IActionResult Delete (int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? category = _db.Categories.Find(id);
            
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }
        [HttpPost]
        public IActionResult Delete(Category category)
        {
            _db.Categories.Remove(category);
            _db.SaveChanges();
            TempData["success"] = "Category Deleted Successfully";
            TempData["success3"] = "Done";
            TempData["ToastrSuccess"] = "Category Deleted Successfully";
            return RedirectToAction("Index", "Category");
        }



    }
}
