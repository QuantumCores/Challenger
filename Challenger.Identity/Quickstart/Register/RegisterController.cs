using Challenger.Email;
using Challenger.Identity.Migrations.IdentityServer.IdentityDb;
using IdentityServerHost.Quickstart.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Challenger.Identity.Quickstart.Register
{
    [AllowAnonymous]
    public class RegisterController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RegisterController> _logger;
        private readonly EmailBuilder _emailBuilder;
        private readonly IEmailSender _emailSender;

        public RegisterController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterController> logger,
            EmailBuilder emailBuilder,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailBuilder = emailBuilder;
            _emailSender = emailSender;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string returnUrl = null)
        {
            var vm = await BuildRegisterViewModelAsync(returnUrl);

            return View(vm);
        }

        /// <summary>
        /// Handle postback from username/password login
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(RegisterInputModel Input, string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            //ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { Email = Input.Email, UserName = Input.UserName };
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    try
                    {
                        using var client = new HttpClient();
                        var message = new HttpRequestMessage(HttpMethod.Post, $"https://localhost:5002/User/api/v1/create?correlationId={user.Id}");
                        var response = await client.SendAsync(message);

                        if (response.StatusCode != System.Net.HttpStatusCode.OK)
                        {
                            _logger.LogError(response.StatusCode.ToString());
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex.Message);
                    }

                    var callbackUrl = await GetEmailConfirmationUrl(user, returnUrl);
                    var values = new Dictionary<string, object>()
                    {
                        ["url"] = $"{HtmlEncoder.Default.Encode(callbackUrl)}",
                        ["userName"] = Input.UserName,
                    };
                    await SendEmail(Input.Email, values);

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToAction("RegisterConfirmation", new { email = Input.Email, returnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return View("Error", new ErrorViewModel
            {
                Error = new IdentityServer4.Models.ErrorMessage
                {
                    Error = ModelState.Root.Errors[0].ErrorMessage
                }
            });
        }

        [HttpGet]
        public async Task<IActionResult> RegisterConfirmation(string email, string returnUrl = null)
        {
            if (email == null)
            {
                return RedirectToPage("/Index");
            }

            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return NotFound($"Unable to load user with email '{email}'.");
            }

            var vm = new RegisterConfirmationViewModel
            {
                Email = email,
                ReturnUrl = returnUrl,
                DisplayConfirmAccountLink = false,
            };

            // Once you add a real email sender, you should remove this code that lets you confirm the account
            if (vm.DisplayConfirmAccountLink)
            {
                var userId = await _userManager.GetUserIdAsync(user);
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                vm.EmailConfirmationUrl = await GetEmailConfirmationUrl(user, returnUrl);
            }

            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string userId, string code, string returnUrl = null)
        {
            if (userId == null || code == null)
            {
                return RedirectToPage("/Index");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{userId}'.");
            }

            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            var result = await _userManager.ConfirmEmailAsync(user, code);

            if (result.Succeeded && !string.IsNullOrWhiteSpace(returnUrl))
            {
                return Redirect(returnUrl);
            }

            var vm = new ConfirmEmailViewModel
            {
                StatusMessage = result.Succeeded ? "Thank you for confirming your email." : "Error confirming your email.",
            };

            return View(vm);
        }

        private async Task<string> GetEmailConfirmationUrl(ApplicationUser user, string returnUrl = null)
        {
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            return Url.Action(
                nameof(ConfirmEmail),
                nameof(RegisterController).Replace(nameof(Controller), string.Empty),
                values: new { userId = user.Id, code, returnUrl },
                protocol: Request.Scheme);
        }

        private async Task<RegisterViewModel> BuildRegisterViewModelAsync(string returnUrl)
        {
            return new RegisterViewModel
            {
                ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList(),
                ReturnUrl = returnUrl = returnUrl ?? Url.Content("~/"),
            };

        }

        private async Task SendEmail(string email, Dictionary<string, object> values)
        {
            await _emailBuilder.Configure();
            await _emailSender.SendEmailAsync(
                email,
                _emailBuilder.BuildEmailSubject("Register"),
                _emailBuilder.BuildEmailMessage("Templates.Register.html", values));
        }
    }
}
