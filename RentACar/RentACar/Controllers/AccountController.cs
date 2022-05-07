using Microsoft.AspNetCore.Mvc;
using RentACar.Data.Entities;
using RentACar.Helpers;
using RentACar.Models;
using RentACar.Enums;
using Vereyon.Web;
using RentACar.Common;
using Microsoft.AspNetCore.Identity;
using RentACar.Data;

namespace RentACar.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserHelper _userHelper;
        private readonly DataContext _context;
        private readonly ICombosHelper _combosHelper;
        private readonly IBlobHelper _blobHelper;
        private readonly IMailHelper _mailHelper;
        private readonly IFlashMessage _flashMessage;
        public AccountController(IUserHelper userHelper, DataContext context, ICombosHelper combosHelper, IBlobHelper blobHelper, IMailHelper mailHelper, IFlashMessage flashMessage)
        {
            _userHelper = userHelper;
            _context = context;
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
                    if (Request.Query.Keys.Contains("ReturnUrl"))
                    {
                        return Redirect(Request.Query["ReturnUrl"].First());
                    }

                    return RedirectToAction("Index", "Home");

                }
                if (result.IsLockedOut)
                {
                    ModelState.AddModelError(string.Empty, "Ha superado el máximo número de intentos, su cuenta está bloqueada, intente de nuevo en 5 minutos.");
                }
                else if (result.IsNotAllowed)
                {
                    ModelState.AddModelError(string.Empty, "El usuario no ha sido habilitado, debes de seguir las instrucciones del correo enviado para poder habilitar el usuario.");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Email o contraseña incorrectos.");
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
                    "RentACar - Confirmación de Email",
                    $"<h1>RentACar - Confirmación de Email</h1>" +
                        $"Para habilitar el usuario por favor hacer click en el siguiente link:, " +
                        $"<hr/><br/><p><a style='color: blue' href = \"{tokenLink}\">Confirmar Email</a></p>");
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
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(token))
            {
                return NotFound();
            }

            User user = await _userHelper.GetUserAsync(new Guid(userId));
            if (user == null)
            {
                return NotFound();
            }

            IdentityResult result = await _userHelper.ConfirmEmailAsync(user, token);
            if (!result.Succeeded)
            {
                return NotFound();
            }

            return View();
        }
        public IActionResult NotAuthorized()
        {
            return View();
        }
        public JsonResult GetDocumentTypes(int documentTypeId)
        {
            DocumentType documentType = _context.DocumentTypes
                .FirstOrDefault(c => c.Id == documentTypeId);
            if (documentType == null)
            {
                return null;
            }
            return Json(documentType.Id);
        }
        public async Task<IActionResult> ChangeUser()
        {
            User user = await _userHelper.GetUserAsync(User.Identity.Name);
            if (user == null)
            {
                return NotFound();
            }

            EditUserViewModel model = new()
            {
                Address = user.Address,
                Document = user.Document,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                ImageId = user.ImageId,
                Id = user.Id,
                LicenceTypes = await _combosHelper.GetComboLicenceTypesAsync(),
                //LicenceTypeId = user.LicenceType.Id,
                Licence = user.Licence,
                DocumentTypes = await _combosHelper.GetComboDocumentTypesAsync(),
                //DocumentTypeId = user.DocumentType.Id
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeUser(EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                Guid imageId = model.ImageId;

                if (model.ImageFile != null)
                {
                    imageId = await _blobHelper.UploadBlobAsync(model.ImageFile, "users");
                }
                User user = await _userHelper.GetUserAsync(User.Identity.Name);

                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Address = model.Address;
                user.PhoneNumber = model.PhoneNumber;
                user.ImageId = imageId;
                user.Document = model.Document;


                await _userHelper.UpdateUserAsync(user);
                return RedirectToAction("Index", "Home");
            }
            model.DocumentTypes = await _combosHelper.GetComboDocumentTypesAsync();
            model.LicenceTypes = await _combosHelper.GetComboLicenceTypesAsync();
            return View(model);
        }
        public IActionResult ChangePassword()
        {
            return View();
        }
    }
}
