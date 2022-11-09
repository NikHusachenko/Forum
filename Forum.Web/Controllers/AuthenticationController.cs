using Forum.Services.AuthenticationServices;
using Forum.Services.AuthenticationServices.Models;
using Microsoft.AspNetCore.Mvc;

namespace Forum.Web.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpGet]
        public async Task<IActionResult> SignIn()
        {
            return View();
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> SignInAjax([FromBody]SignInViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _authenticationService.SignIn(vm.Login, vm.Password);
            if (response.IsError)
            {
                return BadRequest(new { responseMessage = response.ErrorMessage });
            }

            return Ok(new { redirectUrl = Url.Action("Index", "Home") });
        }

        [HttpGet]
        public async Task<IActionResult> SignOut()
        {
            var response = await _authenticationService.SignOut();
            if(response.IsError)
            {
                return View("UserFriendlyError", response.ErrorMessage);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> SignUp()
        {
            return View();
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> SignUpAjax([FromBody]SignUpViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _authenticationService.SignUp(vm);
            if (response.IsError)
            {
                return BadRequest(new { responseMessage = response.ErrorMessage });
            }

            return Ok(new { redirectUrl = Url.Action("Index", "Home") });
        }
    }
}
