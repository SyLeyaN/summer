using AutoMapper;
using Epam.Library.Core.Services;
using Epam.Library.Entities;
using Epam.Library.LogicContracts;
using Epam.Library.ViewModels.Create;
using System.Web.Mvc;

namespace Epam.Library.MVC.Controllers
{
    public class PatentController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IPatentLogic _PatentLogic;
        private readonly CreatePatentService _createPatentService;
        public PatentController()
        {
            _mapper = Dependencies.DependencyResolver.Mapper;
            _PatentLogic = Dependencies.DependencyResolver.PatentLogic;
            _createPatentService = new CreatePatentService(Dependencies.DependencyResolver.Mapper);
        }
        // GET: Patent
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(CreatePatentVM createPatent)
        { 
            string errorMessage = _createPatentService.ResultOfCreatePatent(createPatent);
            ViewBag.Error = errorMessage;
            if (!ModelState.IsValid || errorMessage != null)
                return View();
            else
            {
                return RedirectToAction(nameof(LibraryObjectController.ShowLibraryObjects), nameof(LibraryObject));
            }
        }
    }
}