using Forum.Services.AuthenticationServices.Models;
using Forum.Services.Response;

namespace Forum.Services.AuthenticationServices
{
    public interface IAuthenticationService
    {
        Task<ServiceResponse> SignIn(string login, string password);
        Task<ServiceResponse<long>> SignUp(SignUpViewModel vm);
        Task<ServiceResponse> SignOut();
    }
}