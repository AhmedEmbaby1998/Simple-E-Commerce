using System.Linq;
using System.Threading.Tasks;
using E_Commerce.Models.Data;
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
        private UserManager<ApplicationUser> _userManager;

        public RolesController(RoleManager<IdentityRole> rolesController,UserManager<ApplicationUser> userManager)
        {
            _rolesManager = rolesController;
            _userManager = userManager;
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
        
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Edit(string id)
        {
            var role = await _rolesManager.FindByIdAsync(id);
            if (role==null)
            {
                return RedirectToAction("Error", "Error");
            }
            var model = new EditViewModel
            {
                Id = role.Id,
                Name = role.Name,
                Users = _userManager.GetUsersInRoleAsync(role.Name).Result.ToList()
            };
            return View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(EditViewModel model)
        {
            var role = await _rolesManager.FindByIdAsync(model.Id);
            role.Name = model.Name;
            var state=await _rolesManager.UpdateAsync(role);
            if (state.Succeeded)
            {
                return RedirectToAction("GetAll");
            }
            return RedirectToAction("Edit");
        }
        
    }
}