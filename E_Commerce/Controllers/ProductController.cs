
using System;
using System.Threading.Tasks;
using E_Commerce.Models.Data;
using E_Commerce.Models.FilesHelper;
using E_Commerce.Models.FormsData;
using E_Commerce.Models.Repositeries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers
{
    public class ProductController:Controller
    {
        private IProductRepo _repo;
        private IWebHostEnvironment _environment;

        public ProductController(IProductRepo repo,IWebHostEnvironment environment)
        {
            _repo = repo;
            _environment = environment;
        }
        [HttpPost]
        [Authorize]
        public IActionResult Create(ProductForm form)
        {
            var up = new FilesUploading(_environment,"images");
            up.StageRange(form.Images);
            up.Save();
            _repo.Insert(new Product
            {
                CategoryId = form.CategoryId,
                Images = up.GetFilesObjects<Image>(),
                Name = form.Name,
                Price = form.Price,
                Quantity = form.Quantity
            });
            
            return RedirectToAction("Add");
        }
        [Authorize]
        public IActionResult Add()
        {
            var model = new ProductForm()
            {
                Categories = _repo.GetAllCategoriesId()
            };
            return View(model);
        }
        [Route("Product/Modify/{id?}")]
        [HttpGet]
        [Authorize]
        public IActionResult Modify(int? id)
        {
            var product = _repo.Get(id??1);
            if (product==null)
            {
                Response.StatusCode = 404;
                return View("ProductError",id.Value);
            }

            return View(new ModifyProductCustomViewModel
            {
                Price = product.Price,
                ProductId=product.Id,
                OldImages =product.Images,
                Name = product.Name,
                CategoriesId = _repo.GetAllCategoriesId(),
            });
        }
        [HttpPost]
        [Authorize]
        public IActionResult Modify(ModifyProductCustomViewModel form)
        {
            var modifProduct=new Product();
            if (form.newImages.Count!=0)
            {
                foreach (var image in _repo.GetImages(form.ProductId))
                {
                    System.IO.File.Delete($"C:/Users/Mr/RiderProjects/E_Commerce/E_Commerce/wwwroot/images/{image.Path}");
                }
                var up=new FilesUploading(_environment,"images");
                up.StageRange(form.newImages);
                up.Save();
                modifProduct.Images = up.GetFilesObjects<Image>();
                _repo.DeleteAllImages(form.ProductId);
            }

            var oldQuantity = _repo.Get(form.ProductId).Quantity;
            modifProduct.CategoryId = form.CategoryId;
            modifProduct.Name = form.Name;
            modifProduct.Id = form.ProductId;
            modifProduct.Price = form.Price;
            modifProduct.Quantity = oldQuantity;
            _repo.Update(modifProduct);
            return RedirectToAction("Add");
        }
        [Route("products")]
        [Route("/")]
        [Route("index")]
        [Authorize]

        public IActionResult Products()
        {
            var product = _repo.GetAll();
            foreach (var aProduct in product)
            {
               Console.WriteLine("sssssssssssssssssssssssssssssss   "+aProduct.Images.Count);
            }
            return View(product);
        }
 

    
    }
}