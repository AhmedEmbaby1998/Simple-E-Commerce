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
using Microsoft.AspNetCore.Routing;
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
 
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
     
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
        [Route("Roles")]
        [Authorize]
        public IActionResult GetAll()
        {
            var l = _rolesManager.Roles.AsNoTracking().ToList();
            return View(l);
        }
        
        [HttpGet]
  
        public async Task<IActionResult> Edit(string id)
        {
            var role = await _rolesManager.FindByIdAsync(id);
            if (role==null)
            {
                return RedirectToAction("Error", "Error");
            }
            var model = new EditViewModel
            {
                RoleId = role.Id,
                Name = role.Name,
                Users = _userManager.GetUsersInRoleAsync(role.Name).Result.ToList()
            };
            return View(model);
        }

        public async Task<IActionResult> DeleteRole(string id)
        {
            var state = await _rolesManager.DeleteAsync(await _rolesManager.FindByIdAsync(id));
            return state.Succeeded ? RedirectToAction("GetAll") : RedirectToAction("Error", "Error");
        }

        [HttpPost]
     
        public async Task<IActionResult> Edit(EditViewModel model)
        {
            var role = await _rolesManager.FindByIdAsync(model.RoleId);
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
                    UserId = user.Id
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
                var user =  await _userManager.FindByIdAsync(addOrDeleteUsersViewModel.UserId);
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
        [Route("users")]
        public  IActionResult GetAllUsers()
        {
            return  View(_userManager.Users.ToList());
        }

        [HttpGet]
        [Route("Roles/EditUser/{id}")]
        public async Task<IActionResult> EditUser(string id)
        {
            var user =await _userManager.FindByIdAsync(id);
            
            var claims = await _userManager.GetClaimsAsync(user);
            return View(new EditUserViwModel
            {
                City = user.City,
                UserId = user.Id,
                Email = user.Email,
                UserName = user.UserName,
                Roles = await _userManager.GetRolesAsync(user),
                Claims =claims.Select(claim => claim.Value).ToList(),
                ConcurrencyStamp = user.ConcurrencyStamp
            });
        }
        [HttpPost]
        public async Task<IActionResult> EditaUser(EditUserViwModel user)
        {
            var state=await _userManager.UpdateAsync(new ApplicationUser
            {
                Id = user.UserId,
                City = user.City,
                Email = user.Email,
                UserName = user.UserName,
                ConcurrencyStamp = user.ConcurrencyStamp
                
            });
            return state.Succeeded ? RedirectToAction("GetAllUsers") : RedirectToAction("Products","Product");
        }

        public async Task<IActionResult> DeleteUser(string id)
        {
            var state = await _userManager.DeleteAsync(await _userManager.FindByIdAsync(id));
            return state.Succeeded ? RedirectToAction("GetAllUsers") : RedirectToAction("Error", "Error");
        }
        [HttpGet]
        public async Task<IActionResult> AddOrDeleteRolesOfAUser(string id)
        {
            var roles = _rolesManager.Roles.ToList();
            var viewModels=new List<AddOrDeleteRolesToAUserViewModel>();
            var user = await _userManager.FindByIdAsync(id);
            foreach (var role in roles)
            {
                viewModels.Add(new AddOrDeleteRolesToAUserViewModel
                {
                    Name = role.Name,
                    RoleId = role.Id,
                    Checked =await _userManager.IsInRoleAsync(user,role.Name)
                });
            }

            return View(viewModels);
        }

        [HttpPost]
        public async Task<IActionResult> AddOrDeleteRolesOfAUser(string id,List<AddOrDeleteRolesToAUserViewModel> model)
        {
            var user =await  _userManager.FindByIdAsync(id);
            var state=await _userManager.AddToRolesAsync(user,
                model.Where( viewModel =>
                        viewModel.Checked && ! _userManager.IsInRoleAsync(user, viewModel.Name).Result)
                    .Select(viewModel => viewModel.Name));
            
            await _userManager.RemoveFromRolesAsync(user,
                model.Where( viewModel =>
                        !viewModel.Checked &&  _userManager.IsInRoleAsync(user, viewModel.Name).Result)
                    .Select(viewModel => viewModel.Name));
            return RedirectToAction("AddOrDeleteRolesOfAUser",new{id});
        }
        
       
    }
}