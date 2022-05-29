using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentACar.Common;
using RentACar.Data;
using RentACar.Data.Entities;
using RentACar.Enums;
using RentACar.Helpers;
using RentACar.Models;
using Vereyon.Web;

namespace RentACar.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private readonly IUserHelper _userHelper;
        private readonly DataContext _context;
        private readonly ICombosHelper _combosHelper;
        private readonly IBlobHelper _blobHelper;
        private readonly IMailHelper _mailHelper;
        private readonly IFlashMessage _flashMessage;


        public UsersController(IUserHelper userHelper, DataContext context, ICombosHelper combosHelper, IBlobHelper blobHelper, IMailHelper mailHelper, IFlashMessage flashMessage)
        {
            _userHelper = userHelper;
            _context = context;
            _combosHelper = combosHelper;
            _blobHelper = blobHelper;
            _mailHelper = mailHelper;
            _flashMessage = flashMessage;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Users
                .Include(d => d.DocumentType)
                .Include(l => l.LicenceType)
                .ToListAsync());
        }

        public async Task<IActionResult> Create()
        {
            AddUserViewModel model = new AddUserViewModel
            {
                Id = Guid.Empty.ToString(),
                DocumentTypes = await _combosHelper.GetComboDocumenTypeAsync(),
                LicenceTypes = await _combosHelper.GetComboLicenceTypesAsync(),
                UserType = UserType.Admin,
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AddUserViewModel model)
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
                    //ModelState.AddModelError(string.Empty, "Este correo ya está siendo usado.");
                    _flashMessage.Danger("Este correo ya está siendo usado.");
                    model.DocumentTypes = await _combosHelper.GetComboDocumenTypeAsync();
                    model.LicenceTypes = await _combosHelper.GetComboLicenceTypesAsync();
                    // return View(model);
                    return RedirectToAction("Index", "Users");
                    // return Json(new { isValid = true, html = ModalHelper.RenderRazorViewToString(this, "_ViewAll", model) });
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
                        $"Para habilitar el usuario por favor hacer clicn en el siguiente link:, " +
                        $"<p><a href = \"{tokenLink}\">Confirmar Email</a></p>");
                if (response.IsSuccess)
                {
                    //ViewBag.Message = "Las instrucciones para habilitar el usuario han sido enviadas al correo.";
                    //return View(model);
                    _flashMessage.Info("Usuario registrado. Para poder ingresar al sistema, siga las instrucciones que han sido enviadas a su correo.");
                    // return View(model);
                    return RedirectToAction("Index", "Users");
                }

                ModelState.AddModelError(string.Empty, response.Message);
                return Json(new { isValid = true, html = ModalHelper.RenderRazorViewToString(this, "_ViewAll", model) });
            }

            model.DocumentTypes = await _combosHelper.GetComboDocumenTypeAsync();
            model.LicenceTypes = await _combosHelper.GetComboLicenceTypesAsync();
            //return View(model);
            return Json(new { isValid = false, html = ModalHelper.RenderRazorViewToString(this, "Create", model) });
        }


       





    }
}
