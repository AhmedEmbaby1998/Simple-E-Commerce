using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_Commerce.Models.Data;
using E_Commerce.Models.FormsData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Controllers
{
    public class RolesController:Controller
    {
        private readonly RoleManager<IdentityRole> _rolesManager;
        private UserManager<ApplicationUser> _userManager;
        private ECommerceContext _context;

        public RolesController(RoleManager<IdentityRole> rolesController,UserManager<ApplicationUser> userManager
        ,ECommerceContext context)
        {
            _rolesManager = rolesController;
            _userManager = userManager;
            _context = context;
        }
        [HttpGet]
        [Authorize(Roles="Admin1")]
        [Authorize(Roles = "admin2")]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles="Admin1")]
        [Authorize(Roles = "admin2")]
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
        [Authorize(Roles="Admin1")]
        [Authorize(Roles = "admin2")]
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
        [Authorize(Roles="Admin1")]
        [Authorize(Roles = "admin2")]
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
        [Route("Roles/AddOrDelete/{id}")]
        [HttpGet]
        public async Task<IActionResult> AddOrDelete(string id)
        {
            var users = _userManager.Users.ToList();
            var model = new List<AddOrDeleteUsersViewModel>();
            var role = await _rolesManager.FindByIdAsync(id);
            foreach (var user in users)
            {
                var y=await _userManager.IsInRoleAsync(user, role.Name);
                var x = new AddOrDeleteUsersViewModel
                {
                    UserName = user.Email,
                    IsSelected = y,
                    Id = user.Id
                };
                model.Add(x);
            }
            return View(model);
        }
        [HttpPost]

        public async Task<IActionResult> AddOrDelete(List<AddOrDeleteUsersViewModel> models, string id)
        {
            var role = await _rolesManager.FindByIdAsync(id);
            if (role== null)
                return RedirectToAction("Error", "Error");
            foreach (AddOrDeleteUsersViewModel addOrDeleteUsersViewModel in models)
            {
                var user =  await _userManager.FindByIdAsync(addOrDeleteUsersViewModel.Id);
                if (addOrDeleteUsersViewModel.IsSelected&&!await _userManager.IsInRoleAsync(user,role.Name))
                {
                    await _userManager.AddToRoleAsync(user, role.Name);
                }
                else if (!addOrDeleteUsersViewModel.IsSelected&&await _userManager.IsInRoleAsync(user,role.Name))
                { 
                    await _userManager.RemoveFromRoleAsync(user,role.Name );
                }
            }

            return RedirectToAction("Edit",new {id});
        }
        
    }
}