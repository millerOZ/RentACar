using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentACar.Common;
using RentACar.Data;
using RentACar.Data.Entities;
using RentACar.Enums;
using RentACar.Helpers;
using RentACar.Models;

namespace RentACar.Controllers
{
    public class UsersController : Controller
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;
        private readonly IBlobHelper _blobHelper;
        private readonly ICombosHelper _combosHelper;
        private readonly IMailHelper _mailHelper;

        public UsersController(DataContext context, IUserHelper userHelper, IBlobHelper blobHelper, ICombosHelper combosHelper, IMailHelper mailHelper)
        {
            _context = context;
            _userHelper = userHelper;
            _blobHelper = blobHelper;
            _combosHelper = combosHelper;
            _mailHelper = mailHelper;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Users
                .Include(u => u.Reserve)
             
                .ToListAsync());
        }
        public async Task<IActionResult> Create()
        {
            AddUserViewModel model = new()
            {
                Id = Guid.Empty.ToString(),
               // Rese = await _combosHelper.GetComboCountriesAsync(),
                LicenceTypes = await _combosHelper.GetComboLicenceTypesAsync(),
                DocumentTypes = await _combosHelper.GetComboDocumentTypesAsync(),
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
                    ModelState.AddModelError(string.Empty, "Este correo ya está siendo usado.");
                   // model.Countries = await _combosHelper.GetComboCountriesAsync();
                    model.LicenceTypes = await _combosHelper.GetComboLicenceTypesAsync();
                    model.DocumentTypes = await _combosHelper.GetComboDocumentTypesAsync();
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
                    ViewBag.Message = "Las instrucciones para habilitar el administrador han sido enviadas al correo.";
                    return View(model);
                }

                ModelState.AddModelError(string.Empty, response.Message);
            }

           // model.Countries = await _combosHelper.GetComboCountriesAsync();
            model.LicenceTypes = await _combosHelper.GetComboLicenceTypesAsync();
            model.DocumentTypes = await _combosHelper.GetComboDocumentTypesAsync();
            return View(model);
        }
    }
}
