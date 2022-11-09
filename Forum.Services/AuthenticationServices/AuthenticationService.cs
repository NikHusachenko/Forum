using Forum.Common;
using Forum.Database.Entities;
using Forum.EntityFramework.Repositories;
using Forum.Services.AuthenticationServices.Models;
using Forum.Services.Response;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace Forum.Services.AuthenticationServices
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly ILogger<AuthenticationService> _logger;
        private readonly IRepository<UserEntity> _repository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthenticationService(ILogger<AuthenticationService> logger,
            IRepository<UserEntity> repository,
            IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _repository = repository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ServiceResponse> SignIn(string login, string password)
        {
            UserEntity dbRecord = await _repository.Table
                .FirstOrDefaultAsync(user => user.Login == login && 
                    user.Password == password);

            if (dbRecord == null)
            {
                return ServiceResponse.Error("This user not found");
            }

            ClaimsIdentity identity = new ClaimsIdentity(new List<Claim>()
            {
                new Claim(AuthenticationClaimTypes.ID, dbRecord.Id.ToString()),
                new Claim(AuthenticationClaimTypes.LOGIN, dbRecord.Login),
                new Claim(AuthenticationClaimTypes.EMAIL, dbRecord.Email),
            });
            ClaimsPrincipal principal = new ClaimsPrincipal(new List<ClaimsIdentity>()
            {
                identity,
            });

            try
            {
                await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
            }
            catch (Exception ex)
            {
                _logger.LogError($"AuthenticationService -> SignIn exception: {ex.Message}");
                return ServiceResponse.Error(ex.Message);
            }

            return ServiceResponse.Ok();
        }

        public async Task<ServiceResponse> SignOut()
        {
            try
            {
                await _httpContextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            }
            catch (Exception ex)
            {
                _logger.LogError($"AuthenticationService -> SignOut exception: {ex.Message}");
                return ServiceResponse.Error(ex.Message);
            }

            return ServiceResponse.Ok();
        }

        public async Task<ServiceResponse<long>> SignUp(SignUpViewModel vm)
        {
            UserEntity dbRecord = await _repository.Table
                .FirstOrDefaultAsync(user => !user.DeletedOn.HasValue &&
                    (user.Login == vm.Login ||
                    user.Email == vm.Email));

            if (dbRecord != null)
            {
                return ServiceResponse<long>.Error("User with such login or email was exists");
            }

            dbRecord = new UserEntity()
            {
                CreatedOn = DateTime.Now,
                Email = vm.Email,
                Login = vm.Login,
                Password = vm.Password,
            };

            try
            {
                await _repository.Add(dbRecord);
                await _repository.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError($"AuthenticationService -> SignUp exception: {ex.Message}");
                return ServiceResponse<long>.Error(ex.Message);
            }

            var signInResult = await SignIn(dbRecord.Login, dbRecord.Password);
            if (signInResult.IsError)
            {
                return ServiceResponse<long>.Error(signInResult.ErrorMessage);
            }

            return ServiceResponse<long>.Ok(dbRecord.Id);
        }
    }
}