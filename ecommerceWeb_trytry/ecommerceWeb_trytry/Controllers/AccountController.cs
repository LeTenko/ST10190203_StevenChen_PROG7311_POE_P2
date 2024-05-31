using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ecommerceWeb_trytry_trail2.Models;

namespace ecommerceWeb_trytry_trail2.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }



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
                // Check if the user already exists
                var existingUser = await _userManager.FindByEmailAsync(model.Email);
                if (existingUser != null)
                {
                    ModelState.AddModelError(string.Empty, "An account with this email already exists.");
                    return View(model);
                }

                // Create the new user
                var user = new IdentityUser { UserName = model.Email, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    // Confirm the email
                    user.EmailConfirmed = true;
                    await _userManager.UpdateAsync(user);

                    await _userManager.AddToRoleAsync(user, model.Role);

                    // Optionally add a success message to ViewData or ModelState
                    ViewData["SuccessMessage"] = "Registration successful! You can now log in.";

                    // Return the same view with a cleared model
                    ModelState.Clear();
                    return View(new RegisterViewModel());
                }

                // If the registration failed, add errors to the ModelState
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }
    }
}