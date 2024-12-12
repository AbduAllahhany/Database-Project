

using System.Security.Claims;
using hospital.management.system.BLL.Models.Accounts;
using hospital.management.system.BLL.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace hospital.management.system.Web.Controllers;

[Authorize]
public class AccountController : Controller
{
    private readonly IAccountService accountService;

    public AccountController(IAccountService accountService)
    {
        this.accountService = accountService;
    }

    [AllowAnonymous]
    [HttpGet]
    public IActionResult Register(string returnurl = null)
    {
        ViewData["ReturnUrl"] = returnurl;
        RegisterModel registerViewModel = new();
        return View(registerViewModel);

    }

    [HttpGet]
    public async Task<IActionResult> LogOff()
    {
        await accountService.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }

    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterModel model, string returnurl = null)
    {
        ViewData["ReturnUrl"] = returnurl;
        returnurl = returnurl ?? Url.Content("~/");
        if (!ModelState.IsValid) return View(model);

        var result = await accountService.RegisterAsync(model);

        if (result.IsFailure)
        {
            foreach (var error in result.Error.errors)
                ModelState.AddModelError(string.Empty, error);


            return View(model);
        }

        return RedirectToAction("Index", "Home");
    }


    [HttpGet]
    [Authorize]
    public async Task<IActionResult> ConfirmEmailMessage()
    {
        await accountService.ConfirmEmailAysnc(User.FindFirstValue(ClaimTypes.Email));
        return View(true);
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> ConfirmEmail(ConfirmEmailModel model)
    {
        if (!ModelState.IsValid) return View("Error");
        var result = await accountService.ConfirmEmailAysnc(model);
        return result.IsSuccess ? View() : View("Error");

    }

    [AllowAnonymous]
    public IActionResult Login(string returnurl = null)
    {
        ViewData["ReturnUrl"] = returnurl;
        return View();

    }

    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginModel model, string returnurl = null)
    {
        ViewData["ReturnUrl"] = returnurl;
        returnurl = returnurl ?? Url.Content("~/");
        if (!ModelState.IsValid) return View(model);

        var result = await accountService.LoginAsync(model);

        if (result.IsFailure)
        {
            foreach (var error in result.Error.errors)
                ModelState.AddModelError(string.Empty, error);
            return View(model);
        }

        if (result.Value.RequiresTwoFactor)
            return RedirectToAction(nameof(VerifyAuthenticatorCode),
                new GetTwoFactorAuthenticationUserModel { returnUrl = returnurl, rememberMe = model.RememberMe });

        if (result.Value.IsLockedOut)
            return View("Lockout");

        return LocalRedirect(returnurl);
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> VerifyAuthenticatorCode(GetTwoFactorAuthenticationUserModel model)
    {
        var result = await accountService.GetTwoFactorAuthenticationUserAsync(model);
        if (result.IsFailure)
            return View("Error");

        ViewData["ReturnUrl"] = model.returnUrl;

        return View(new VerifyAuthenticatorModel { ReturnUrl = model.returnUrl, RememberMe = model.rememberMe });

    }

    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> VerifyAuthenticatorCode(VerifyAuthenticatorModel model)
    {
        model.ReturnUrl = model.ReturnUrl ?? Url.Content("~/");
        if (!ModelState.IsValid) return View(model);

        var result = await accountService.TwoFactorAuthenticatorSignInAsync(model);

        if (!result.IsSuccess)
        {

            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return View(model);
        }

        if (result.Value.IsLockedOut)
            return View("Lockout");
        return LocalRedirect(model.ReturnUrl);

    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult ForgotPassword()
    {
        return View();
    }

    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ForgotPassword(ForgotPasswordModel model)
    {
        if (!ModelState.IsValid) return View(model);
        var result = await accountService.ForgetPasswordAsync(model);
        return RedirectToAction(nameof(ForgotPasswordConfirmation));
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult ResetPassword(string Token = null)
    {
        return Token == null ? View("Error") : View();
    }

    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ResetPassword(ResetPasswordModel model)
    {
        if (!ModelState.IsValid) return View(model);
        var result = await accountService.ResetPasswordAsync(model);
        return result.IsSuccess ? RedirectToAction(nameof(ResetPasswordConfirmation)) : View("Error");



    }


    [HttpGet]
    [AllowAnonymous]
    public IActionResult ResetPasswordConfirmation()
    {
        return View();
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult ForgotPasswordConfirmation()
    {
        return View();
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> EnableAuthenticator()
    {
        var result = await accountService.GetAuthenticatorTokenAysnc(new GetAuthenticatorTokenModel
        {
            UserName = User.Identity.Name,

        });
        if (result.IsFailure) return View("Error");
        var responseModel = result.Value;
        var model = new TwoFactorAuthenticationModel()
            { Token = responseModel.Token, QRCodeUrl = responseModel.QRCodeUrl };
        return View(model);
    }

    [HttpPost]
    [Authorize]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EnableAuthenticator(TwoFactorAuthenticationModel model)
    {
        if (!ModelState.IsValid) return View("Error");
        var result = await accountService.EnableTwoFactorAuthenticationAsync(model);
        if (result.IsFailure)
        {
            ModelState.AddModelError("Verify", "Your two factor auth code could not be validated.");
            return View(model);

        }

        return RedirectToAction(nameof(AuthenticatorConfirmation));
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult AuthenticatorConfirmation()
    {
        return View();
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult Lockout()
    {
        return View();
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult NoAccess()
    {
        return View();
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult Error()
    {
        return View();
    }



}

