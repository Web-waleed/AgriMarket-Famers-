﻿using AgriMarket.Data;
using AgriMarket.Models;
using AgriMarket.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AgriMarket.Controllers
{
    public class AccountController : Controller
    {
        #region Configuration
        private AppDbContext context;
        private UserManager<IdentityUser> userManager;
        private SignInManager<IdentityUser> signInManager;
        private RoleManager<IdentityRole> roleManager;
        public AccountController(UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            RoleManager<IdentityRole> roleManager, 
            AppDbContext context)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
            this.context = context;
        }
        #endregion

        #region USer
        [HttpGet]
        
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityUser user = new IdentityUser
                {

                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    UserName = model.Email
                };
                var farmer = new Farmer
                {
                    FarmerEmail = model.Email, 
                    FarmerName = model.Email, 
                    FarmerNumber = model.PhoneNumber 
                };
                context.Farmers.Add(farmer);
                await context.SaveChangesAsync();
                var result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                foreach (var err in result.Errors)
                {
                    ModelState.AddModelError("", err.Description);
                }
            }
            return View(model);
        }
        [HttpGet]

        public IActionResult Login()
        {
           return View();
        }
        [HttpPost]

        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(model.Email,model.Password, false,false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Invalid User or Password");
                return View(model);
            }
            return View(model);
        }
        [HttpGet]

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        public IActionResult UserStatistics()
        {
            ViewBag.TotalUsers = userManager.Users.Count();  
            return View();  
        }
        #endregion

        #region Role
        [HttpGet]
        public async Task<IActionResult> RolesList()
        {
            return View(await roleManager.Roles.ToListAsync());
        }
        [HttpGet]
        public IActionResult CreateRoles()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateRoles(CreateRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityRole role = new IdentityRole
                {
                    Name = model.RoleName
                };
                var result = await roleManager.CreateAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(RolesList));
                }
                foreach (var err in result.Errors)
                {
                    ModelState.AddModelError("", err.Description);
                }
                return View(model);
            }

            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> EditRoles(String id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var Role = await roleManager.FindByIdAsync(id);
            if (Role == null) { return NotFound(); }
            EditRoleViewModel model = new EditRoleViewModel
            {
                RoleName = Role.Name!,
                RoleId = Role.Id
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> editRoles(EditRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var x = await roleManager.FindByIdAsync(model.RoleId);
                if (x == null) { return NotFound(); }
                x.Name = model.RoleName;
                var result = await roleManager.UpdateAsync(x);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(RolesList));
                }
                foreach (var Err in result.Errors)
                {
                    ModelState.AddModelError(Err.Code, Err.Description);
                }
                return View(model);
            }
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> DeleteRole(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var role = await roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }

            DeleteRoleViewModel model = new DeleteRoleViewModel
            {
                Id = role.Id,
                RoleName = role.Name!
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteRole(DeleteRoleViewModel model)
        {
            var role = await roleManager.FindByIdAsync(model.Id);

            if (role != null)
            {
                var result = await roleManager.DeleteAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(RolesList));
                }
                else
                {
                    ModelState.AddModelError("", "Error deleting role");
                }
            }
            return View(model);
        }
        #endregion

    }
}