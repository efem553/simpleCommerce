using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using simpleCommerce_DataAccess.Repository.Interface;
using simpleCommerce_Models;
using simpleCommerce_Models.ViewModels;
using simpleCommerce_Utility;

namespace simpleCommerce.Controllers
{
    public class AboutController : Controller
    {
        private readonly IAboutRepository _aboutRepo;
        private readonly IFacultyLogoRepository _facultyLogoRepo;
        public AboutController(IAboutRepository aboutRepo, IFacultyLogoRepository facultyLogoRepo)
        {
            _aboutRepo = aboutRepo;
            _facultyLogoRepo = facultyLogoRepo;
        }
        public IActionResult Index()
        {
            AboutVM aboutVM = new AboutVM();
            aboutVM.About = _aboutRepo.GetAll().First();
            aboutVM.FacultyLogos = _facultyLogoRepo.GetAll(x=>x.About==aboutVM.About);
            return View(aboutVM);
        }
        [Authorize(Roles = WC.AdminRole)]
        [HttpGet]
        
        public IActionResult Add()
        {
            if (!_aboutRepo.GetAll().Any())
            {
                return View();
            }
            else
            {
                Guid aboutArticleId = _aboutRepo.GetAll().First().AboutArticleId;
                return RedirectToAction("Edit", new { aboutArticleId = aboutArticleId });
            }
        }

        [Authorize(Roles = WC.AdminRole)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(CreateAboutArticleVM createAboutArticleVM)
        {
            if (ModelState.IsValid)
            {
                
                //Checking Store Logo File and if its not null adding it to Model
                if (createAboutArticleVM.StoreLogoFile != null)
                {

                    string imageBase64Data;
                    string contentType;
                    FileInfo fi = new FileInfo(createAboutArticleVM.StoreLogoFile.Name);
                    MemoryStream ms = new MemoryStream();
                    createAboutArticleVM.StoreLogoFile.CopyTo(ms);
                    imageBase64Data = Convert.ToBase64String(ms.ToArray());
                    contentType = string.Format("data:image/{0};base64,", createAboutArticleVM.StoreLogoFile.ContentType.Split("/")[1]);
                    if (!String.IsNullOrEmpty(contentType) || !String.IsNullOrEmpty(imageBase64Data))
                    {
                        imageBase64Data = contentType + imageBase64Data;
                    }
                    ms.Close();
                    ms.Dispose();
                    createAboutArticleVM.About.BaseIconBase64 = imageBase64Data;
                    _aboutRepo.Add(createAboutArticleVM.About);
                    _aboutRepo.Save();
                }
                if(createAboutArticleVM.FacultyLogoFiles.Count()>0)
                {
                    List<FacultyLogo> facultyLogos = new List<FacultyLogo>();
                    foreach (var file in createAboutArticleVM.FacultyLogoFiles)
                        {
                            
                            string imageBase64Data;
                            string contentType;
                            FileInfo fi = new FileInfo(file.Name);
                            MemoryStream ms = new MemoryStream();
                            file.CopyTo(ms);
                            imageBase64Data = Convert.ToBase64String(ms.ToArray());
                            contentType = string.Format("data:image/{0};base64,", file.ContentType.Split("/")[1]);
                            if (!String.IsNullOrEmpty(contentType) || !String.IsNullOrEmpty(imageBase64Data))
                            {
                                imageBase64Data = contentType + imageBase64Data;
                            }
                            ms.Close();
                            ms.Dispose();
                            facultyLogos.Add(new FacultyLogo
                            {
                                FacultyName = file.FileName.Split('.')[0],
                                LogoBase64 = imageBase64Data,
                                About = createAboutArticleVM.About,
                                
                            });
                        }
                    _facultyLogoRepo.AddRange(facultyLogos);
                    _facultyLogoRepo.Save();
                }
                
                return View("Add");
            }
            else
            {
                TempData[WC.Error] = "Something went wrong while saving your About Page content";
                return RedirectToAction("Add");
            }
        }

        [Authorize(Roles = WC.AdminRole)]
        [HttpGet]
        public IActionResult Edit(Guid aboutArticleId)
        {
            if (aboutArticleId != null && aboutArticleId != Guid.Empty)
            {
                UpdateAboutArticleVM updateAboutArticleVM = new UpdateAboutArticleVM();
                updateAboutArticleVM.About = _aboutRepo.Find(aboutArticleId);

                return View(updateAboutArticleVM);
            }
            else
            {
                TempData[WC.Error] = "Something went wrong while getting you About Article";
                return RedirectToAction("Index", "Admin");
            }
        }

        [Authorize(Roles = WC.AdminRole)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(UpdateAboutArticleVM updateAboutArticleVM)
        {
            if (ModelState.IsValid)
            {

                //Checking Store Logo File and if its not null adding it to Model
                if (updateAboutArticleVM.StoreLogoFile != null)
                {

                    string imageBase64Data;
                    string contentType;
                    FileInfo fi = new FileInfo(updateAboutArticleVM.StoreLogoFile.Name);
                    MemoryStream ms = new MemoryStream();
                    updateAboutArticleVM.StoreLogoFile.CopyTo(ms);
                    imageBase64Data = Convert.ToBase64String(ms.ToArray());
                    contentType = string.Format("data:image/{0};base64,", updateAboutArticleVM.StoreLogoFile.ContentType.Split("/")[1]);
                    if (!String.IsNullOrEmpty(contentType) || !String.IsNullOrEmpty(imageBase64Data))
                    {
                        imageBase64Data = contentType + imageBase64Data;
                    }
                    ms.Close();
                    ms.Dispose();
                    updateAboutArticleVM.About.BaseIconBase64 = imageBase64Data;
                    _aboutRepo.Update(updateAboutArticleVM.About);
                    _aboutRepo.Save();
                }
                if (updateAboutArticleVM.FacultyLogoFiles?.Count() > 0)
                {
                    _facultyLogoRepo.RemoveRange(_facultyLogoRepo.GetAll(x=>x.About== updateAboutArticleVM.About));
                    _facultyLogoRepo.Save();
                    List<FacultyLogo> facultyLogos = new List<FacultyLogo>();
                    foreach (var file in updateAboutArticleVM.FacultyLogoFiles)
                    {

                        string imageBase64Data;
                        string contentType;
                        FileInfo fi = new FileInfo(file.Name);
                        MemoryStream ms = new MemoryStream();
                        file.CopyTo(ms);
                        imageBase64Data = Convert.ToBase64String(ms.ToArray());
                        contentType = string.Format("data:image/{0};base64,", file.ContentType.Split("/")[1]);
                        if (!String.IsNullOrEmpty(contentType) || !String.IsNullOrEmpty(imageBase64Data))
                        {
                            imageBase64Data = contentType + imageBase64Data;
                        }
                        ms.Close();
                        ms.Dispose();
                        facultyLogos.Add(new FacultyLogo
                        {
                            FacultyName = file.FileName.Split('.')[0],
                            LogoBase64 = imageBase64Data,
                            AboutArticleId = updateAboutArticleVM.About.AboutArticleId,

                        });
                    }
                    _facultyLogoRepo.AddRange(facultyLogos);
                    _facultyLogoRepo.Save();
                }
                if (!(updateAboutArticleVM.FacultyLogoFiles?.Count() > 0 && updateAboutArticleVM.StoreLogoFile != null))
                {
                    About about = _aboutRepo.Find(updateAboutArticleVM.About.AboutArticleId);
                    if (about != null)
                    {
                        about.StoreName = updateAboutArticleVM.About.StoreName;
                        about.Article1Header = updateAboutArticleVM.About.Article1Header;
                        about.Article1Content = updateAboutArticleVM.About.Article1Content;
                        _aboutRepo.Update(about);
                        _aboutRepo.Save();
                    }
                }
                
                return RedirectToAction("Index","About");
            }
            else
            {
                TempData[WC.Error] = "Something went wrong while saving your About Page content";
                return RedirectToAction("Add");
            }
        }
    }
}
