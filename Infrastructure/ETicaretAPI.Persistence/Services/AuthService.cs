using ETicaretAPI.Application.Abstractions.Services;
using ETicaretAPI.Application.Abstractions.Token;
using ETicaretAPI.Application.DTOs;
using ETicaretAPI.Application.Exceptions;
using ETicaretAPI.Application.Features.Commands.AppUser.LoginUser;
using ETicaretAPI.Domain.Entities.Identity;
using Google.Apis.Auth;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Text;

namespace ETicaretAPI.Persistence.Services
{
    public class AuthService : IAuthService
    {

        readonly IConfiguration _configuration;
        readonly UserManager<Domain.Entities.Identity.AppUser> _userManager;
        readonly ITokenHandler _tokenHandler;
        readonly SignInManager<Domain.Entities.Identity.AppUser> _signInManager;
        readonly IUserService _userService;
        readonly IMailService _mailService;

        public AuthService(IConfiguration configuration, UserManager<AppUser> userManager, ITokenHandler tokenHandler, SignInManager<AppUser> signInManager, IUserService userService, IMailService mailService)
        {

            _configuration=configuration;
            _userManager=userManager;
            _tokenHandler=tokenHandler;
            _signInManager=signInManager;
            _userService=userService;
            _mailService=mailService;
        }

        public async Task<Token> GoogleLoginAsync(string idToken, int accesTokenLifeTime)
        {
            var settings = new GoogleJsonWebSignature.ValidationSettings()
            {
                Audience= new List<string> { _configuration["Google:Client_Id"] }

            };
            var payload = await GoogleJsonWebSignature.ValidateAsync(idToken, settings);
            var info = new UserLoginInfo("GOOGLE", payload.Subject, "GOOGLE");
            Domain.Entities.Identity.AppUser user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);

            bool result = user!=null;
            if (user == null)
            {
                user=new()
                {
                    Id=Guid.NewGuid().ToString(),
                    Email=payload.Email,
                    UserName=payload.Name,

                };
                var identityresult = await _userManager.CreateAsync(user);
                result=identityresult.Succeeded;
            }
            if (result)
            {
                await _userManager.AddLoginAsync(user, info);
                Token token = _tokenHandler.CreateAccessToken(190, user);
                await _userService.UpdateRefreshTokenAsync(token.RefreshToken, user, token.Expiration, 5);
                return token;
            }
            else
            {
                throw new Exception("Invalid external authentication");
            }

        }

        public async Task<Token> LoginAsync(string username, string password, int accesTokenLifeTime)
        {
            Domain.Entities.Identity.AppUser user = await _userManager.FindByNameAsync(username);
            if (user == null)
                throw new NotFoundUserException();

            SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, password, false);
            if (result.Succeeded)
            {
                Token token = _tokenHandler.CreateAccessToken(accesTokenLifeTime,user);
                await _userService.UpdateRefreshTokenAsync(token.RefreshToken, user, token.Expiration, 5);
                return token;

            }
            //return new LoginUserErrorCommandResponse()
            //{
            //    Message="Kullanıcı adı veya şifre hatalı."
            //};
            throw new AuthenticationErrorException();
        }

        public async Task PasswordResetAsync(string email)
        {
             AppUser user=await _userManager.FindByEmailAsync(email);
            if(user != null)
            {
                string resetToken=await _userManager.GeneratePasswordResetTokenAsync(user);
                byte[] tokenBytes=Encoding.UTF8.GetBytes(resetToken);
                resetToken=WebEncoders.Base64UrlEncode(tokenBytes);
                await _mailService.SendPasswordResetMailAsync(email, user.Id, resetToken);
            }
        }

        public async Task<Token> RefreshTokenLoginAsync(string refreshToken)
        {
            AppUser? user = await _userManager.Users.FirstOrDefaultAsync(u => u.RefreshToken==refreshToken);
            if (user != null && user?.RefreshTokenEndDate>DateTime.UtcNow)
            {
                Token token = _tokenHandler.CreateAccessToken(15,user);
                await _userService.UpdateRefreshTokenAsync(token.RefreshToken, user, token.Expiration, 15);
                return token;
            }
            else
            throw new NotFoundUserException();
        }

        public async Task<bool> VerifyResetTokenAsync(string resetToken, string userId)
        {
            AppUser user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                byte[] tokenBytes=WebEncoders.Base64UrlDecode(resetToken);
                 resetToken=Encoding.UTF8.GetString(tokenBytes);

                return await _userManager.VerifyUserTokenAsync(user,_userManager.Options.Tokens.PasswordResetTokenProvider,"ResetPassword",resetToken);
               

            }
            return false;
        }
    }
}
