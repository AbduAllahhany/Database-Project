using CSharpFunctionalExtensions;
using hospital.management.system.BLL.Models;
using hospital.management.system.BLL.Models.Accounts;


namespace hospital.management.system.BLL.Services.IServices
{
    public interface IAccountService
    {
        Task<Result<LoginResponseModel, DomainError>> LoginAsync(LoginModel model);
        Task<Result<RegisterResponseModel, DomainError>> RegisterAsync(RegisterModel model);
        Task<Result<string>> ForgetPasswordAsync(ForgotPasswordModel model);
        Task<Result<string, DomainError>> ResetPasswordAsync(ResetPasswordModel model);
        Task ConfirmEmailAysnc(string Email);
        Task<Result<string, DomainError>> ConfirmEmailAysnc(ConfirmEmailModel model);
        Task<Result<TwoFactorAuthenticationResponseModel, DomainError>> EnableTwoFactorAuthenticationAsync(TwoFactorAuthenticationModel model);
        Task<Result<GetAuthenticatorKeyResponseModel, DomainError>> GetAuthenticatorTokenAysnc(GetAuthenticatorTokenModel model);
        Task SignOutAsync();
        Task<Result> GetTwoFactorAuthenticationUserAsync(GetTwoFactorAuthenticationUserModel model);
        Task<Result<VerifyAuthenticatorResponseModel>> TwoFactorAuthenticatorSignInAsync(VerifyAuthenticatorModel model);
        Task<Result<ProfileModel>> GetProfileDataAsync(string? UserName);
         
     }
}
