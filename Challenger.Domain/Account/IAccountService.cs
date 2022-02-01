using Challenger.Domain.Contracts.Account;
using Microsoft.AspNetCore.Identity;

namespace Challenger.Domain.Account
{
    public interface IAccountService
    {
        Task<IdentityResult?> Register(RegisterModel Input);

        Task<SignInResult?> Login(LoginModel loginModel);
    }
}