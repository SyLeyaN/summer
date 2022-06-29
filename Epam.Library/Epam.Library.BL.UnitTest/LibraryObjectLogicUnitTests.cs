using Epam.Library.DalContracts;
using Epam.Library.Entities;
using Epam.Library.Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Epam.Library.BL.UnitTests
{
    [TestClass]
    public class LibraryObjectLogicUnitTests
    {
        #region delete

        [TestMethod]
        public void DeleteWithCorrectId() 
        {
            var memoryDal = new Mock<ILibraryObjectDal>();
            memoryDal.Setup(dal => dal.Delete(It.IsAny<int>())).Returns(true);

            LibraryObjectLogic logic = new LibraryObjectLogic(memoryDal.Object);

            var result = logic.Delete(1);

            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void DeleteWithIncorrectId() 
        {
            var memoryDal = new Mock<ILibraryObjectDal>();
            memoryDal.Setup(dal => dal.Delete(It.IsAny<int>())).Returns(false);

            LibraryObjectLogic logic = new LibraryObjectLogic(memoryDal.Object);

            var result = logic.Delete(1);

            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void DeleteInEmptyCollection()
        {
            var memoryDal = new Mock<ILibraryObjectDal>();
            memoryDal.Setup(dal => dal.Delete(It.IsAny<int>())).Returns(false);

            LibraryObjectLogic logic = new LibraryObjectLogic(memoryDal.Object);

            var result = logic.Delete(1);

            Assert.AreEqual(false, result);
        }
        #endregion

        #region getAll

        [TestMethod]
        public void GetCorrectCollection()
        {
            LibraryObject libraryObject = new LibraryObject
            {
                Title = "Title",
                Note = "Note",
                NumberOfPages = 1,
                PublishingYear = DateTime.Now.Year
            };
            var memoryDal = new Mock<ILibraryObjectDal>();
            memoryDal.Setup(dal => dal.GetAll()).Returns(new List<LibraryObject> {libraryObject});

            LibraryObjectLogic logic = new LibraryObjectLogic(memoryDal.Object);

            var result = logic.GetAll().Count();

            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void GetEmptyCollection() 
        {
            var memoryDal = new Mock<ILibraryObjectDal>();
            memoryDal.Setup(dal => dal.GetAll()).Returns(new List<LibraryObject> {});

            LibraryObjectLogic logic = new LibraryObjectLogic(memoryDal.Object);

            var result = logic.GetAll().Count();

            Assert.AreEqual(0, result);
        }
        #endregion

        #region getBooksPatentsByPerson()

        [TestMethod]
        public void GetWithCorrectId() 
        {
            Book _correctBook = new Book
            {
                Title = "Title",
                NumberOfPages = 1,
                PublishingYear = DateTime.Now.Year,
                Note = "Note",
                PublishingCity = "City",
                PublishingHouse = "House",
                ISBN = ""
            };

            Patent _correctPatent = new Patent
            {
                Title = "Title",
                NumberOfPages = 1,
                ApplicationDate = DateTime.MinValue,
                PublishingDate = DateTime.Now,
                PublishingYear = DateTime.Now.Year,
                Country = "Country",
                Note = "Note",
                RegistrationNumber = "123456"
            };
            var memoryDal = new Mock<ILibraryObjectDal>();
            memoryDal.Setup(dal => dal.GetBooksPatentsByPerson(It.IsAny<int>())).Returns(new List<LibraryObject> { _correctBook, _correctPatent});

            LibraryObjectLogic logic = new LibraryObjectLogic(memoryDal.Object);

            var result = logic.GetBooksPatentsByPerson(1).Count();

            Assert.AreEqual(2, result);
        }

        [TestMethod]
        public void GetWithIncorrectId() 
        {
            var memoryDal = new Mock<ILibraryObjectDal>();
            memoryDal.Setup(dal => dal.GetBooksPatentsByPerson(It.IsAny<int>())).Returns(new List<LibraryObject> {});

            LibraryObjectLogic logic = new LibraryObjectLogic(memoryDal.Object);

            var result = logic.GetBooksPatentsByPerson(1).Count();

            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void GetInEmptyCollection()
        {
            var memoryDal = new Mock<ILibraryObjectDal>();
            memoryDal.Setup(dal => dal.GetBooksPatentsByPerson(It.IsAny<int>())).Returns(new List<LibraryObject> { });

            LibraryObjectLogic logic = new LibraryObjectLogic(memoryDal.Object);

            var result = logic.GetBooksPatentsByPerson(1).Count();

            Assert.AreEqual(0, result);
        }
        #endregion

        #region getByTitle

        [TestMethod]
        public void GetWithCorrectTitle()
        {
            Book _correctBook = new Book
            {
                Title = "Title",
                NumberOfPages = 1,
                PublishingYear = DateTime.Now.Year,
                Note = "Note",
                PublishingCity = "City",
                PublishingHouse = "House",
                ISBN = ""
            };

            Patent _correctPatent = new Patent
            {
                Title = "Title",
                NumberOfPages = 1,
                ApplicationDate = DateTime.MinValue,
                PublishingDate = DateTime.Now,
                PublishingYear = DateTime.Now.Year,
                Country = "Country",
                Note = "Note",
                RegistrationNumber = "123456"
            };
            var memoryDal = new Mock<ILibraryObjectDal>();
            memoryDal.Setup(dal => dal.GetByTitle(It.IsAny<string>())).Returns(new List<LibraryObject> { _correctBook, _correctPatent });

            LibraryObjectLogic logic = new LibraryObjectLogic(memoryDal.Object);

            var result = logic.GetByTitle("Title").Count();

            Assert.AreEqual(2, result);

        }

        [TestMethod]
        public void GetWithIncorrectTitle() 
        {
            var memoryDal = new Mock<ILibraryObjectDal>();
            memoryDal.Setup(dal => dal.GetByTitle(It.IsAny<string>())).Returns(new List<LibraryObject> { });

            LibraryObjectLogic logic = new LibraryObjectLogic(memoryDal.Object);

            var result = logic.GetByTitle("Title").Count();

            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void GetInEmptyCollectionByTitle()
        {
            var memoryDal = new Mock<ILibraryObjectDal>();
            memoryDal.Setup(dal => dal.GetByTitle(It.IsAny<string>())).Returns(new List<LibraryObject> { });

            LibraryObjectLogic logic = new LibraryObjectLogic(memoryDal.Object);

            var result = logic.GetByTitle("Title").Count();

            Assert.AreEqual(0, result);
        }

        #endregion

        #region GroupingByPublishingYear

        [TestMethod]
        public void GroupCorrectCollection()
        {
            Book _correctBook = new Book
            {
                Title = "Title",
                NumberOfPages = 1,
                PublishingYear = DateTime.Now.Year,
                Note = "Note",
                PublishingCity = "City",
                PublishingHouse = "House",
                ISBN = ""
            };

            Patent _correctPatent = new Patent
            {
                Title = "Title",
                NumberOfPages = 1,
                ApplicationDate = DateTime.MinValue,
                PublishingDate = DateTime.Now,
                PublishingYear = DateTime.Now.Year+1,
                Country = "Country",
                Note = "Note",
                RegistrationNumber = "123456"
            };

            List<LibraryObject> objs = new List<LibraryObject> { _correctBook, _correctPatent };
            var memoryDal = new Mock<ILibraryObjectDal>();
            memoryDal.Setup(dal => dal.GroupingByPublishingYear()).Returns(objs.ToLookup(b => b.PublishingYear, b => b));

            LibraryObjectLogic logic = new LibraryObjectLogic(memoryDal.Object);

            var result = logic.GroupingByPublishingYear().First().Key;

            Assert.AreEqual(DateTime.Now.Year, result);
        }

        [TestMethod]
        public void GroupEmptyCollection() 
        {
            List<LibraryObject> objs = new List<LibraryObject> {};
            var memoryDal = new Mock<ILibraryObjectDal>();
            memoryDal.Setup(dal => dal.GroupingByPublishingYear()).Returns(objs.ToLookup(b => b.PublishingYear, b => b));

            LibraryObjectLogic logic = new LibraryObjectLogic(memoryDal.Object);

            var result = logic.GroupingByPublishingYear().FirstOrDefault();

            Assert.AreEqual(null, result);
        }

        #endregion

        #region SortingByYearDirectOrder
        [TestMethod]
        public void SortCorrectCollectionDirect() 
        {
            Book _correctBook = new Book
            {
                Title = "Title",
                NumberOfPages = 1,
                PublishingYear = DateTime.Now.Year,
                Note = "Note",
                PublishingCity = "City",
                PublishingHouse = "House",
                ISBN = ""
            };

            Patent _correctPatent = new Patent
            {
                Title = "Title",
                NumberOfPages = 1,
                ApplicationDate = DateTime.MinValue,
                PublishingDate = DateTime.Now,
                PublishingYear = DateTime.Now.Year + 1,
                Country = "Country",
                Note = "Note",
                RegistrationNumber = "123456"
            };

            List<LibraryObject> objs = new List<LibraryObject> { _correctBook, _correctPatent };
            var memoryDal = new Mock<ILibraryObjectDal>();
            memoryDal.Setup(dal => dal.SortingByYearDirectOrder()).Returns(objs.OrderBy(p=>p.PublishingYear));

            LibraryObjectLogic logic = new LibraryObjectLogic(memoryDal.Object);

            var result = logic.SortingByYearDirectOrder().First().PublishingYear;

            Assert.AreEqual(DateTime.Now.Year, result);
        }

        [TestMethod]
        public void SortEmptyCollectionDirect()
        {
            List<LibraryObject> objs = new List<LibraryObject> { };

            var memoryDal = new Mock<ILibraryObjectDal>();
            memoryDal.Setup(dal => dal.SortingByYearDirectOrder()).Returns(objs);

            LibraryObjectLogic logic = new LibraryObjectLogic(memoryDal.Object);

            var result = logic.SortingByYearDirectOrder().FirstOrDefault();

            Assert.AreEqual(null, result);
        }

        #endregion

        #region SortingByYearReverseOrder
        [TestMethod]
        public void SortCorrectCollectionReverse()
        {
            Book _correctBook = new Book
            {
                Title = "Title",
                NumberOfPages = 1,
                PublishingYear = DateTime.Now.Year,
                Note = "Note",
                PublishingCity = "City",
                PublishingHouse = "House",
                ISBN = ""
            };

            Patent _correctPatent = new Patent
            {
                Title = "Title",
                NumberOfPages = 1,
                ApplicationDate = DateTime.MinValue,
                PublishingDate = DateTime.Now,
                PublishingYear = DateTime.Now.Year + 1,
                Country = "Country",
                Note = "Note",
                RegistrationNumber = "123456"
            };

            List<LibraryObject> objs = new List<LibraryObject> { _correctBook, _correctPatent };
            var memoryDal = new Mock<ILibraryObjectDal>();
            memoryDal.Setup(dal => dal.SortingByYearReverseOrder()).Returns(objs.OrderByDescending(p => p.PublishingYear));

            LibraryObjectLogic logic = new LibraryObjectLogic(memoryDal.Object);

            var result = logic.SortingByYearReverseOrder().First().PublishingYear;

            Assert.AreEqual(DateTime.Now.Year + 1, result);
        }

        [TestMethod]
        public void SortEmptyCollectionReverse()
        {
            List<LibraryObject> objs = new List<LibraryObject> { };

            var memoryDal = new Mock<ILibraryObjectDal>();
            memoryDal.Setup(dal => dal.SortingByYearReverseOrder()).Returns(objs);

            LibraryObjectLogic logic = new LibraryObjectLogic(memoryDal.Object);

            var result = logic.SortingByYearReverseOrder().FirstOrDefault();

            Assert.AreEqual(null, result);
        }

        #endregion

    }
}
