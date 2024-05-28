using BookStore.DataAccess.Data;
using BookStore.Models;
using BookStore.Models.ViewModel;
using BookStore.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.IdentityModel.Tokens;

namespace BookStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        #region WithUnitofWork
        private readonly IUnitOfWorks _unitOfWorks;
        public ProductController(IUnitOfWorks unitOfWorks)
        {
            _unitOfWorks = unitOfWorks;
        }
        public IActionResult Index()
        {
            //List<Product> products = _unitOfWorks.Product.GetAll().ToList();

            //add to show caetgory Name in product index
            List<Product> products = _unitOfWorks.Product.GetAll(includeProperties: "Category").ToList();
            return View(products);
        }

        public IActionResult Create()
        {
            // ViewBag simple way
            //List<Category> categories = _unitOfWorks.Category.GetAll().ToList();
            //ViewBag.Category = categories;

            // ViewBag another way
            //IEnumerable<SelectListItem> CategoryList = _unitOfWorks.Category.
            //    GetAll().Select(u => new SelectListItem
            //    {
            //        Text = u.Name,
            //        Value = u.Id.ToString()
            //    });
            //ViewBag.Category = CategoryList;

            // using ViewModel
            IEnumerable<SelectListItem> CategoryList = _unitOfWorks.Category.
                GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                });

            ProductVM productVM = new ProductVM
            {
                Product = new Product(),
                CategoryList = CategoryList
            };

            return View(productVM);
        }

        [HttpPost]
        public IActionResult Create(ProductVM obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWorks.Product.Add(obj.Product);
                _unitOfWorks.Save();
                TempData["Success"] = "Product Created";
                return RedirectToAction("Index", "Product");
            }
            else
            {
                //Populate Default if page is not validate
                IEnumerable<SelectListItem> CategoryList = _unitOfWorks.Category.
                GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                });
                //ViewBag.Category = CategoryList;

                ProductVM productVM = new ProductVM
                {
                    Product = new Product(),
                    CategoryList = CategoryList
                };

                return View(productVM);
            }
        }

        public IActionResult Edit(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }
            else
            {
                //Product? Product = _db.Categories.Find(Id); Always find with Primay key
                //Product? Product = _db.Categories.Where(c => c.Id == Id).FirstOrDefault(); Use if require some more filter condition
                Product? Product = _unitOfWorks.Product.Get(c => c.Id == Id);
                if (Product == null)
                {
                    return NotFound();
                }
                else
                {
                    return View(Product);
                }
            }
        }

        [HttpPost]
        public IActionResult Edit(Product obj)
        {
            if (obj != null)
            {
                if (!string.IsNullOrEmpty(obj.Title) && obj.Title.ToLower() == "test")
                {
                    ModelState.AddModelError("Name", "test is Not valid Product Title");
                }
                if (ModelState.IsValid)
                {
                    _unitOfWorks.Product.Update(obj);
                    _unitOfWorks.Save();
                    TempData["Success"] = "Product Updated";
                    return RedirectToAction("Index", "Product");
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
                Product? Product = _unitOfWorks.Product.Get(c => c.Id == Id);
                if (Product == null)
                {
                    return NotFound();
                }
                else
                {
                    return View(Product);
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
                Product? Product = _unitOfWorks.Product.Get(c => c.Id == Id);
                if (Product == null)
                {
                    return NotFound();
                }
                else
                {
                    _unitOfWorks.Product.Remove(Product);
                    _unitOfWorks.Save();
                    TempData["Success"] = "Product Deleted";
                    return RedirectToAction("Index", "Product");
                }
            }
        }


        public IActionResult Upsert(int? Id)
        {
            IEnumerable<SelectListItem> CategoryList = _unitOfWorks.Category.
                GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                });

            ProductVM productVM = new ProductVM
            {
                Product = new Product(),
                CategoryList = CategoryList
            };
            if (Id == null || Id == 0)
            {
                return View(productVM);
            }
            else
            {
                productVM.Product = _unitOfWorks.Product.Get(u => u.Id == Id);
                return View(productVM);
            }
        }

        [HttpPost]
        public IActionResult Upsert(ProductVM obj, IFormFile? ProductImage)
        {
            if (ModelState.IsValid)
            {
                if (ProductImage != null && ProductImage.Length > 0)
                {
                    var fileName = Path.GetFileName(ProductImage.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", @"Images\Product", fileName);

                    if (!string.IsNullOrEmpty(obj.Product.ImageURL))
                    {
                        //delete old image first
                        var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", obj.Product.ImageURL.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                            System.IO.File.Delete(oldImagePath);
                    }
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        ProductImage.CopyTo(fileStream);
                    }
                    obj.Product.ImageURL = @"\Images\Product\" + fileName;
                }

                if (obj.Product.Id == 0)
                    _unitOfWorks.Product.Add(obj.Product);
                else
                    _unitOfWorks.Product.Update(obj.Product);
                _unitOfWorks.Save();
                TempData["Success"] = "Product Created";
                return RedirectToAction("Index", "Product");
            }
            else
            {
                //Populate Default if page is not validate
                IEnumerable<SelectListItem> CategoryList = _unitOfWorks.Category.
                GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                });
                //ViewBag.Category = CategoryList;

                ProductVM productVM = new ProductVM
                {
                    Product = new Product(),
                    CategoryList = CategoryList
                };

                return View(productVM);
            }
        }

        #endregion

        #region APICalls
        [HttpGet]
        public IActionResult getAll()
        {
            List<Product> products = _unitOfWorks.Product.GetAll(includeProperties: "Category").ToList();
            return Json(new { Data = products });
        }

        [HttpDelete]
        public IActionResult DeleteAPI(int? id)
        {
            var productToBeDelete = _unitOfWorks.Product.Get(u => u.Id == id);
            if (productToBeDelete == null)
            {
                return Json(new { Success = false, message = "Error while deleting" });
            }

            //delete old image first
            var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", productToBeDelete.ImageURL.TrimStart('\\'));
            if (System.IO.File.Exists(oldImagePath))
                System.IO.File.Delete(oldImagePath);

            _unitOfWorks.Product.Remove(productToBeDelete);
            _unitOfWorks.Product.Save();

            return Json(new { Success = false, message = "Product deleted successfully" });
        }
        #endregion
    }
}
