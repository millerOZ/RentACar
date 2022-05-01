using Microsoft.AspNetCore.Mvc;
using RentACar.Data.Entities;
using RentACar.Helpers;
using RentACar.Models;
using RentACar.Enums;
using Vereyon.Web;
using RentACar.Common;

namespace RentACar.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserHelper _userHelper;
        private readonly ICombosHelper _combosHelper;
        private readonly IBlobHelper _blobHelper;
        private readonly IMailHelper _mailHelper;
        private readonly IFlashMessage _flashMessage;
        public AccountController(IUserHelper userHelper, ICombosHelper combosHelper, IBlobHelper blobHelper, IMailHelper mailHelper, IFlashMessage flashMessage)
        {
            _userHelper = userHelper;
            _combosHelper = combosHelper;
            _blobHelper = blobHelper;
            _mailHelper = mailHelper;
            _flashMessage = flashMessage;
        }
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            return View(new LoginViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                Microsoft.AspNetCore.Identity.SignInResult result = await _userHelper.LoginAsync(model);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
               
            }
            ModelState.AddModelError(string.Empty, "Email y contraseña incorrectos");

            return View(model);
        }
        public async Task<IActionResult> Logout()
        {
            await _userHelper.LogoutAsync();
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> Register()
        {
            AddUserViewModel model = new()
            {
                Id = Guid.Empty.ToString(),
                DocumentTypes = await _combosHelper.GetComboDocumentTypesAsync(),
                LicenceTypes = await _combosHelper.GetComboLicenceTypesAsync(),

                //Countries = await _combosHelper.GetComboCountriesAsync(),
                //States = await _combosHelper.GetComboStatesAsync(0),
                //Cities = await _combosHelper.GetComboCitiesAsync(0) ,
                UserType = UserType.User,
            };

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(AddUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                Guid imageId = Guid.Empty;

                if (model.ImageFile != null)
                {
                    imageId = await _blobHelper.UploadBlobAsync(model.ImageFile, "users");
                }

                model.ImageId = imageId;
                User user = await _userHelper.AddUserAsync(model);
                if (user == null)
                {
                    _flashMessage.Danger("Este correo ya está siendo usado.");
                    model.DocumentTypes = await _combosHelper.GetComboCategoriesAsync();
                    model.LicenceTypes = await _combosHelper.GetComboLicenceTypesAsync();
                    //model.States = await _combosHelper.GetComboStatesAsync(model.CountryId);
                    //model.Cities = await _combosHelper.GetComboCitiesAsync(model.StateId);
                    return View(model);
                }

                string myToken = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
                string tokenLink = Url.Action("ConfirmEmail", "Account", new
                {
                    userid = user.Id,
                    token = myToken
                }, protocol: HttpContext.Request.Scheme);

                Response response = _mailHelper.SendMail(
                    $"{model.FirstName} {model.LastName}",
                    model.Username,
                    "Shopping - Confirmación de Email",
                    $"<h1>Shopping - Confirmación de Email</h1>" +
                        $"Para habilitar el usuario por favor hacer click en el siguiente link:, " +
                        $"<hr/><br/><p><a href = \"{tokenLink}\">Confirmar Email</a></p>");
                if (response.IsSuccess)
                {
                    _flashMessage.Info("Usuario registrado. Para poder ingresar al sistema, siga las instrucciones que han sido enviadas a su correo.");
                    return RedirectToAction(nameof(Login));
                }

                ModelState.AddModelError(string.Empty, response.Message);
            }
            model.DocumentTypes = await _combosHelper.GetComboDocumentTypesAsync();
            model.LicenceTypes = await _combosHelper.GetComboLicenceTypesAsync();
            //model.Countries = await _combosHelper.GetComboCountriesAsync();
            //model.States = await _combosHelper.GetComboStatesAsync(model.CountryId);
            //model.Cities = await _combosHelper.GetComboCitiesAsync(model.StateId);

            return View(model);
        }
    }
}
