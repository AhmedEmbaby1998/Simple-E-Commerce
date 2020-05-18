using System;
using System.Collections.Generic;
using System.Security.Policy;
using System.Threading.Tasks;
using E_Commerce.Models.Data;
using E_Commerce.Models.FilesHelper;
using E_Commerce.Models.FormsData;
using E_Commerce.Models.Repositeries;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers
{
    public class ProductController:Controller
    {
        private IRepo<Product> _repo;
        private IWebHostEnvironment _environment;

        public ProductController(IRepo<Product> repo,IWebHostEnvironment environment)
        {
            _repo = repo;
            _environment = environment;
        }
        [HttpPost]
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

        public IActionResult Add()
        {
            var model = new ProductForm()
            {
                Categories = _repo.GetAllCategoriesId()
            };
            return View(model);
        }
    }
}