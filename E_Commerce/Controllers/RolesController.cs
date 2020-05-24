using System.Linq;
using System.Threading.Tasks;
using E_Commerce.Models.FormsData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Controllers
{
    public class RolesController:Controller
    {
        private readonly RoleManager<IdentityRole> _rolesManager;

        public RolesController(RoleManager<IdentityRole> rolesController)
        {
            _rolesManager = rolesController;
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Add(CreateRolesViewModel model)
        {
            if (ModelState.IsValid)
            {
                var role = new IdentityRole
                {
                    Name = model.Role
                };
               var state= await _rolesManager.CreateAsync(role);
               return state!=null? (IActionResult) RedirectToAction("Products", "Product"):
                   View("Add");
            }

            return View("Add");
        }

        public IActionResult GetAll()
        {
            var l = _rolesManager.Roles.AsNoTracking().ToList();
            return View(l);
        }
        
    }
}