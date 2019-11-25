using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace PersonalBlog.App.Services
{
    public interface IAuthService
    {
        Task<SignInResult> PasswordSignInAsync(string userName, string password);
        Task SignOutAsync();
    }
}
