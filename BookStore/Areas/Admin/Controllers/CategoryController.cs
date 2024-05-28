using BookStore.DataAccess.Data;
using BookStore.Models;
using BookStore.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace BookStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        #region WithoutReposiroty
        //private readonly ApplicationDBContext _db;
        //public CategoryController(ApplicationDBContext db)
        //{
        //    _db = db;
        //}
        //public IActionResult Index()
        //{
        //    List<Category> categories = _db.Categories.ToList();
        //    return View(categories);
        //}

        //public IActionResult Create()
        //{ return View(); }

        //[HttpPost]
        //public IActionResult Create(Category obj)
        //{
        //    if (obj != null)
        //    {
        //        if (!string.IsNullOrEmpty(obj.Name) && obj.Name.ToLower() == "test")
        //        {
        //            ModelState.AddModelError("Name", "test is Not valid Category Name");
        //        }
        //        if (!string.IsNullOrEmpty(obj.Name) && obj.Name == obj.DisplayOrder.ToString())
        //        {
        //            ModelState.AddModelError("", "Category Name and Display Order cannot be the same");
        //        }
        //        if (ModelState.IsValid)
        //        {
        //            _db.Categories.Add(obj);
        //            _db.SaveChanges();
        //            TempData["Success"] = "Category Created";
        //            return RedirectToAction("Index", "Category");
        //        }
        //    }
        //    return View();
        //}

        //public IActionResult Edit(int? Id)
        //{
        //    if (Id == null || Id == 0)
        //    {
        //        return NotFound();
        //    }
        //    else
        //    {
        //        //Category? category = _db.Categories.Find(Id); Always find with Primay key
        //        //Category? category = _db.Categories.Where(c => c.Id == Id).FirstOrDefault(); Use if require some more filter condition
        //        Category? category = _db.Categories.FirstOrDefault(c => c.Id == Id);
        //        if (category == null)
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            return View(category);
        //        }
        //    }
        //}

        //[HttpPost]
        //public IActionResult Edit(Category obj)
        //{
        //    if (obj != null)
        //    {
        //        if (!string.IsNullOrEmpty(obj.Name) && obj.Name.ToLower() == "test")
        //        {
        //            ModelState.AddModelError("Name", "test is Not valid Category Name");
        //        }
        //        if (!string.IsNullOrEmpty(obj.Name) && obj.Name == obj.DisplayOrder.ToString())
        //        {
        //            ModelState.AddModelError("", "Category Name and Display Order cannot be the same");
        //        }
        //        if (ModelState.IsValid)
        //        {
        //            _db.Categories.Update(obj);
        //            _db.SaveChanges();
        //            TempData["Success"] = "Category Updated";
        //            return RedirectToAction("Index", "Category");
        //        }
        //    }
        //    return View();
        //}

        //public IActionResult Delete(int? Id)
        //{
        //    if (Id == null || Id == 0)
        //    {
        //        return NotFound();
        //    }
        //    else
        //    {
        //        Category? category = _db.Categories.FirstOrDefault(c => c.Id == Id);
        //        if (category == null)
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            return View(category);
        //        }
        //    }
        //}

        //[HttpPost, ActionName("Delete")]
        //public IActionResult DeletePost(int? Id)
        //{
        //    if (Id == null || Id == 0)
        //    {
        //        return NotFound();
        //    }
        //    else
        //    {
        //        Category? category = _db.Categories.FirstOrDefault(c => c.Id == Id);
        //        if (category == null)
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            _db.Categories.Remove(category);
        //            _db.SaveChanges();
        //            TempData["Success"] = "Category Deleted";
        //            return RedirectToAction("Index", "Category");
        //        }
        //    }
        //}

        #endregion

        #region withRepository
        /*
        private readonly ICategoryRepository _categoryRepo;
        public CategoryController(ICategoryRepository category)
        {
            _categoryRepo = category;
        }
        public IActionResult Index()
        {
            List<Category> categories = _categoryRepo.GetAll().ToList();
            return View(categories);
        }

        public IActionResult Create()
        { return View(); }

        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if (obj != null)
            {
                if (!string.IsNullOrEmpty(obj.Name) && obj.Name.ToLower() == "test")
                {
                    ModelState.AddModelError("Name", "test is Not valid Category Name");
                }
                if (!string.IsNullOrEmpty(obj.Name) && obj.Name == obj.DisplayOrder.ToString())
                {
                    ModelState.AddModelError("", "Category Name and Display Order cannot be the same");
                }
                if (ModelState.IsValid)
                {
                    _categoryRepo.Add(obj);
                    _categoryRepo.Save();
                    TempData["Success"] = "Category Created";
                    return RedirectToAction("Index", "Category");
                }
            }
            return View();
        }

        public IActionResult Edit(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }
            else
            {
                //Category? category = _db.Categories.Find(Id); Always find with Primay key
                //Category? category = _db.Categories.Where(c => c.Id == Id).FirstOrDefault(); Use if require some more filter condition
                Category? category = _categoryRepo.Get(c => c.Id == Id);
                if (category == null)
                {
                    return NotFound();
                }
                else
                {
                    return View(category);
                }
            }
        }

        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            if (obj != null)
            {
                if (!string.IsNullOrEmpty(obj.Name) && obj.Name.ToLower() == "test")
                {
                    ModelState.AddModelError("Name", "test is Not valid Category Name");
                }
                if (!string.IsNullOrEmpty(obj.Name) && obj.Name == obj.DisplayOrder.ToString())
                {
                    ModelState.AddModelError("", "Category Name and Display Order cannot be the same");
                }
                if (ModelState.IsValid)
                {
                    _categoryRepo.Update(obj);
                    _categoryRepo.Save();
                    TempData["Success"] = "Category Updated";
                    return RedirectToAction("Index", "Category");
                }
            }
            return View();
        }

        public IActionResult Delete(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }
            else
            {
                Category? category = _categoryRepo.Get(c => c.Id == Id);
                if (category == null)
                {
                    return NotFound();
                }
                else
                {
                    return View(category);
                }
            }
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }
            else
            {
                Category? category = _categoryRepo.Get(c => c.Id == Id);
                if (category == null)
                {
                    return NotFound();
                }
                else
                {
                    _categoryRepo.Remove(category);
                    _categoryRepo.Save();
                    TempData["Success"] = "Category Deleted";
                    return RedirectToAction("Index", "Category");
                }
            }
        }
        */
        #endregion

        #region WithUnitofWork
        private readonly IUnitOfWorks _unitOfWorks;
        public CategoryController(IUnitOfWorks unitOfWorks)
        {
            _unitOfWorks = unitOfWorks;
        }
        public IActionResult Index()
        {
            List<Category> categories = _unitOfWorks.Category.GetAll().ToList();
            return View(categories);
        }

        public IActionResult Create()
        { return View(); }

        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if (obj != null)
            {
                if (!string.IsNullOrEmpty(obj.Name) && obj.Name.ToLower() == "test")
                {
                    ModelState.AddModelError("Name", "test is Not valid Category Name");
                }
                if (!string.IsNullOrEmpty(obj.Name) && obj.Name == obj.DisplayOrder.ToString())
                {
                    ModelState.AddModelError("", "Category Name and Display Order cannot be the same");
                }
                if (ModelState.IsValid)
                {
                    _unitOfWorks.Category.Add(obj);
                    _unitOfWorks.Save();
                    TempData["Success"] = "Category Created";
                    return RedirectToAction("Index", "Category");
                }
            }
            return View();
        }

        public IActionResult Edit(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }
            else
            {
                //Category? category = _db.Categories.Find(Id); Always find with Primay key
                //Category? category = _db.Categories.Where(c => c.Id == Id).FirstOrDefault(); Use if require some more filter condition
                Category? category = _unitOfWorks.Category.Get(c => c.Id == Id);
                if (category == null)
                {
                    return NotFound();
                }
                else
                {
                    return View(category);
                }
            }
        }

        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            if (obj != null)
            {
                if (!string.IsNullOrEmpty(obj.Name) && obj.Name.ToLower() == "test")
                {
                    ModelState.AddModelError("Name", "test is Not valid Category Name");
                }
                if (!string.IsNullOrEmpty(obj.Name) && obj.Name == obj.DisplayOrder.ToString())
                {
                    ModelState.AddModelError("", "Category Name and Display Order cannot be the same");
                }
                if (ModelState.IsValid)
                {
                    _unitOfWorks.Category.Update(obj);
                    _unitOfWorks.Save();
                    TempData["Success"] = "Category Updated";
                    return RedirectToAction("Index", "Category");
                }
            }
            return View();
        }

        public IActionResult Delete(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }
            else
            {
                Category? category = _unitOfWorks.Category.Get(c => c.Id == Id);
                if (category == null)
                {
                    return NotFound();
                }
                else
                {
                    return View(category);
                }
            }
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }
            else
            {
                Category? category = _unitOfWorks.Category.Get(c => c.Id == Id);
                if (category == null)
                {
                    return NotFound();
                }
                else
                {
                    _unitOfWorks.Category.Remove(category);
                    _unitOfWorks.Save();
                    TempData["Success"] = "Category Deleted";
                    return RedirectToAction("Index", "Category");
                }
            }
        }
        #endregion
    }
}
