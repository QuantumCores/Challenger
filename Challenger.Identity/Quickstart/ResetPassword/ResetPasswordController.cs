using Challenger.Identity.Migrations.IdentityServer.IdentityDb;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using QuantumCore.Email.Builders;
using System.Collections.Generic;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Challenger.Identity.Quickstart.ResetPassword
{
    public class ResetPasswordController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly EmailBuilder _emailBuilder;
        private readonly IEmailSender _emailSender;

        public ResetPasswordController(
            UserManager<ApplicationUser> userManager,
            EmailBuilder emailBuilder,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _emailBuilder = emailBuilder;
            _emailSender = emailSender;
        }

        [HttpGet]
        public IActionResult Index(string returnUrl = null)
        {
            return View(new ForgotPasswordViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Index(ForgotPasswordViewModel Input)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(Input.Email);
                if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return RedirectToAction("ForgotPasswordConfirmation");
                }

                // For more information on how to enable account confirmation and password reset please 
                // visit https://go.microsoft.com/fwlink/?LinkID=532713
                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

                //?????
                var callbackUrl = Url.Action(
                    "ResetPassword",
                    "ResetPassword",
                    values: new { code },
                    protocol: Request.Scheme);

                var values = new Dictionary<string, object>()
                {
                    ["url"] = $"{HtmlEncoder.Default.Encode(callbackUrl)}",
                    ["userName"] = user.UserName,
                };
                await SendEmail(Input.Email, values);

                return RedirectToAction("ForgotPasswordConfirmation");
            }

            return View();
        }

        [HttpGet]
        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ResetPassword(string code = null)
        {
            if (code == null)
            {
                return BadRequest("A code must be supplied for password reset.");
            }
            else
            {
                var vm = new ResetPasswordViewModel
                {
                    Code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code))
                };
                return View(vm);
            }
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel Input)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var user = await _userManager.FindByEmailAsync(Input.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction("ResetPasswordConfirmation");
            }

            var decoded = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(Input.Code));

            var result = await _userManager.ResetPasswordAsync(user, decoded, Input.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View();
        }

        [HttpGet]
        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }


        private async Task SendEmail(string email, Dictionary<string, object> values)
        {
            await _emailBuilder.Configure();
            await _emailSender.SendEmailAsync(
                email,
                _emailBuilder.BuildEmailSubject("ResetPassword"),
                _emailBuilder.BuildEmailMessage("Templates.ResetPassword.html", values));
        }
    }
}
