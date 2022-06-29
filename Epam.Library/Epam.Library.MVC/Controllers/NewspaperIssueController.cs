using AutoMapper;
using Epam.Library.Core.Services;
using Epam.Library.Entities;
using Epam.Library.LogicContracts;
using Epam.Library.ViewModels.Create;
using Epam.Library.ViewModels.Display;
using System.Web.Mvc;

namespace Epam.Library.MVC.Controllers
{
    public class NewspaperIssueController : Controller
    {
        private readonly IMapper _mapper;
        private readonly INewspaperIssueLogic _newspaperIssueLogic;
        private readonly INewspaperLogic _newspaperLogic;
        private readonly DisplayNewspaperService _newspaperService;
        private readonly CreateNewspaperIssueService _createNewspaperIssueService;
        public NewspaperIssueController()
        {
            _mapper = Dependencies.DependencyResolver.Mapper;
            _newspaperLogic = Dependencies.DependencyResolver.NewspaperLogic;
            _newspaperIssueLogic = Dependencies.DependencyResolver.NewspaperIssueLogic;
            _newspaperService = new DisplayNewspaperService(Dependencies.DependencyResolver.Mapper);
            _createNewspaperIssueService = new CreateNewspaperIssueService(Dependencies.DependencyResolver.Mapper);
        }
        // GET: Book
        public ActionResult Create()
        {
            SelectList newspaper = new SelectList(_newspaperService.GetNewspaperVMList(_newspaperLogic.GetAll()), nameof(DisplayLibraryObjectVM.Id), nameof(DisplayLibraryObjectVM.Information));
            ViewBag.Newspaper = newspaper;

            return View();
        }

        [HttpPost]
        public ActionResult Create(CreateNewspaperIssueVM createNewspaperIssue)
        {
            string errorMessage = _createNewspaperIssueService.ResultOfCreateNewspaperIssue(createNewspaperIssue);
            ViewBag.Error = errorMessage;
            if (!ModelState.IsValid || errorMessage != null)
            {
                SelectList newspapers = new SelectList(_newspaperService.GetNewspaperVMList(_newspaperLogic.GetAll()), nameof(DisplayLibraryObjectVM.Id), nameof(DisplayLibraryObjectVM.Information));
                ViewBag.Newspaper = newspapers;
                return View();
            }
            else
            {
                return RedirectToAction(nameof(LibraryObjectController.ShowLibraryObjects), nameof(LibraryObject));
            }
        }
    }
}