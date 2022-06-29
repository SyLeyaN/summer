using AutoMapper;
using Epam.Library.Core.Services;
using Epam.Library.Entities;
using Epam.Library.LogicContracts;
using Epam.Library.ViewModels.Create;
using Epam.Library.ViewModels.Display;
using System.Web.Mvc;

namespace Epam.Library.MVC.Controllers
{
    public class BookController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IBookLogic _bookLogic;
        private readonly IPersonLogic _personLogic;
        private readonly DisplayPersonService _personService;
        private readonly CreateBookService _createBookService;
        public BookController()
        {
            _mapper = Dependencies.DependencyResolver.Mapper;
            _bookLogic = Dependencies.DependencyResolver.BookLogic;
            _personLogic = Dependencies.DependencyResolver.PersonLogic;
            _personService = new DisplayPersonService(Dependencies.DependencyResolver.Mapper);
            _createBookService = new CreateBookService(Dependencies.DependencyResolver.Mapper);
        }
        // GET: Book
        public ActionResult Create()
        {
            MultiSelectList authors = new MultiSelectList(_personService.GetPersonVMList(_personLogic.GetAll()), nameof(DisplayPersonVM.Id), nameof(DisplayPersonVM.NameSurname));
            ViewBag.Authors = authors;

            return View();
        }
        [HttpPost]
        public ActionResult Create(CreateBookVM createBook)
        {
            string errorMessage = _createBookService.ResultOfCreateBook(createBook);
            ViewBag.Error = errorMessage;
            if (!ModelState.IsValid || ViewBag.Error != null)
            {
                MultiSelectList authors = new MultiSelectList(_personService.GetPersonVMList(_personLogic.GetAll()), nameof(DisplayPersonVM.Id), nameof(DisplayPersonVM.NameSurname));
                ViewBag.Authors = authors;
                return View();
            }
            else
            {
                return RedirectToAction(nameof(LibraryObjectController.ShowLibraryObjects), nameof(LibraryObject));
            }
            
        }


    }
}