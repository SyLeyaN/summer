using AutoMapper;
using Epam.Library.Core.Services;
using Epam.Library.Entities;
using Epam.Library.LogicContracts;
using Epam.Library.ViewModels.Create;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Epam.Library.MVC.Controllers
{
    public class NewspaperController : Controller
    {
        private readonly IMapper _mapper;
        private readonly INewspaperLogic _NewspaperLogic;
        private readonly CreateNewspaperService _createNewspaperService;
        public NewspaperController()
        {
            _mapper = Dependencies.DependencyResolver.Mapper;
            _NewspaperLogic = Dependencies.DependencyResolver.NewspaperLogic;
            _createNewspaperService = new CreateNewspaperService(Dependencies.DependencyResolver.Mapper);
        }
        // GET: Newspaper
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(CreateNewspaperVM createNewspaper)
        {
            string errorMessage = _createNewspaperService.ResultOfCreateNewspaper(createNewspaper);
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