using Epam.Library.Core.Services;
using Epam.Library.Entities;
using Epam.Library.LogicContracts;
using Epam.Library.ViewModels.Display;
using PagedList;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Epam.Library.MVC.Controllers
{
    public class LibraryObjectController : Controller
    {
        private readonly GetAllService _getAllService;
        private List<DisplayLibraryObjectVM> _libraryObjects;
        private const int pageSize = 2;
        private readonly int maxPageNumber;

        // GET: LibraryObject
        private readonly ILibraryObjectLogic _libraryObjectLogic;
        public LibraryObjectController()
        {
            _getAllService = new GetAllService(Dependencies.DependencyResolver.Mapper);
            _libraryObjectLogic = Dependencies.DependencyResolver.LibraryObjectLogic;
            maxPageNumber = _getAllService.DisplayList(_libraryObjectLogic.GetAll()).Count;
            if (maxPageNumber % pageSize != 0)
            {
                maxPageNumber = (maxPageNumber / pageSize) + 1;
            }
            else
            {
                maxPageNumber /= pageSize;
            }
        }
        public ActionResult ShowLibraryObjects(int? page, bool? sortDirect, bool? sortReverse)
        {
            int pageNumber = page ?? 1;

            if (sortDirect != null && (bool)sortDirect)
            {
                _libraryObjects = _getAllService.DisplayList(_libraryObjectLogic.SelectLibraryObjectsForPageSortByYearDirect(pageNumber, pageSize));
                ViewBag.SortDirect = true;
            }
            else if (sortReverse != null && (bool)sortReverse)
            {
                _libraryObjects = _getAllService.DisplayList(_libraryObjectLogic.SelectLibraryObjectsForPageSortByYearReverse(pageNumber, pageSize));
                ViewBag.SortReverse = true;
            }
            else
            {
                _libraryObjects = _getAllService.DisplayList(_libraryObjectLogic.SelectLibraryObjectsForPage(pageNumber, pageSize));
                ViewBag.SortDirect = false;
                ViewBag.SortReverse = false;
            }
            ViewBag.MaxPage = maxPageNumber;
            ViewBag.CurrentPage = pageNumber;

            return View(_libraryObjects);
        }


        public ActionResult DeleteLibraryObject(int id)
        {
            if (_libraryObjectLogic.Delete(id))
            {
                return RedirectToAction(nameof(LibraryObjectController.ShowLibraryObjects));
            }
            else
            {
                return RedirectToAction(nameof(LibraryObjectController.ShowLibraryObjects));
            }
        }

        public ActionResult CheckLibraryObjectLikeDeleted(int id)
        {
            if (_libraryObjectLogic.CheckObjectLikeDeletedById(id))
            {
                return RedirectToAction(nameof(LibraryObjectController.ShowLibraryObjects));
            }
            else
            {
                return RedirectToAction(nameof(LibraryObjectController.ShowLibraryObjects));
            }
        }

        public ActionResult RestoreLibraryObject(int id)
        {
            if (_libraryObjectLogic.RestoreObject(id))
            {
                return RedirectToAction(nameof(LibraryObjectController.ShowLibraryObjects));
            }
            else
            {
                return RedirectToAction(nameof(LibraryObjectController.ShowLibraryObjects));
            }
        }

        [HttpPost]
        public ActionResult ShowByTitleLibraryObject(string title, int? page)
        {
            if (string.IsNullOrEmpty(title))
            {
                title = "No title";
            }
            int pageNumber = page ?? 1;
            _libraryObjects = _getAllService.DisplayList(_libraryObjectLogic.GetByTitle(title));
            ViewBag.MaxPage = maxPageNumber;
            ViewBag.CurrentPage = pageNumber;
            ViewBag.Title = title;

            return View(_libraryObjects);
        }

        public ActionResult GroupingByPublishingYear()
        {
            //return View(_showService.DisplayList(_libraryObjectLogic.GroupingByPublishingYear()));
            return View();
            //перевод
        }

        public ActionResult GetBooksPatentsByPerson(int personId)
        {
            return View(_getAllService.DisplayList(_libraryObjectLogic.GetBooksPatentsByPerson(personId)));
            //перевод
        }
    }
}