using Challenger.Domain.Contracts.Account;
using Challenger.Domain.Contracts.Repositories;
using Challenger.Domain.DbModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Challenger.Domain.Account
{
    public class AccountService : IAccountService
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUserStore<IdentityUser> _userStore;
        private readonly IUserRepository _userRepository;

        // private readonly IUserEmailStore<IdentityUser> _emailStore;
        private readonly ILogger<AccountService> _logger;
        //private readonly IEmailSender _emailSender;

        public AccountService(
            UserManager<IdentityUser> userManager,
            IUserStore<IdentityUser> userStore,
            SignInManager<IdentityUser> signInManager,
            IUserRepository userRepository,
            ILogger<AccountService> logger
            //IEmailSender emailSender
            )
        {
            _userManager = userManager;
            _userStore = userStore;
            _userRepository = userRepository;
            //_emailStore = GetEmailStore();
            _signInManager = signInManager;
            _logger = logger;
            //_emailSender = emailSender;
        }

        public async Task<IdentityResult?> Register(RegisterModel registerModel)
        {
            var user = CreateUser();

            await _userStore.SetUserNameAsync(user, registerModel.Email, CancellationToken.None);
            //await _emailStore.SetEmailAsync(user, Input.Email, CancellationToken.None);
            var result = await _userManager.CreateAsync(user, registerModel.Password);

            if (result.Succeeded)
            {
                _logger.LogInformation("User created a new account with password.");

                var domainUser = CreateDomainUser(registerModel);
                _userRepository.Add(domainUser);
                await _userRepository.SaveChanges();

                //var userId = await _userManager.GetUserIdAsync(user);
                //var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                //code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                //var callbackUrl = Url.Page(
                //    "/Account/ConfirmEmail",
                //    pageHandler: null,
                //    values: new { area = "Identity", userId = userId, code = code, returnUrl = returnUrl },
                //    protocol: Request.Scheme);

                //await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                //    $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                //await _signInManager.SignInAsync(user, isPersistent: false);
            }

            return result;
        }

        public async Task<SignInResult?> Login(LoginModel loginModel)
        {
            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, set lockoutOnFailure: true
            var result = await _signInManager.PasswordSignInAsync(loginModel.Email, loginModel.Password, loginModel.RememberMe, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                _logger.LogInformation("User logged in.");
            }
            //if (result.RequiresTwoFactor)
            //{
            //    return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
            //}
            //if (result.IsLockedOut)
            //{
            //    _logger.LogWarning("User account locked out.");
            //    return RedirectToPage("./Lockout");
            //}
            //else
            //{
            //    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            //    return Page();
            //}

            return result;
        }

        private IdentityUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<IdentityUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(IdentityUser)}'. " +
                    $"Ensure that '{nameof(IdentityUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }

        private User CreateDomainUser(RegisterModel Input)
        {
            try
            {
                return new User
                {
                    DateOfBirth = Input.BirthDate,
                    Email = Input.Email,
                    Height = Input.Height,
                    UserName = Input.Nickname,
                    Sex = Input.Sex,
                };
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(IdentityUser)}'. " +
                    $"Ensure that '{nameof(IdentityUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }

        private IUserEmailStore<IdentityUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<IdentityUser>)_userStore;
        }
    }
}
