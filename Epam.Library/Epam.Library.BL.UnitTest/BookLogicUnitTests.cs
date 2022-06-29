using Epam.Library.DalContracts;
using Epam.Library.Entities;
using Epam.Library.Entities.Exceptions;
using Epam.Library.Logic;
using Epam.Library.ValidatorContracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Epam.Library.BL.UnitTests
{
    [TestClass]
    public class BookLogicUnitTests
    {
        #region add

        [TestMethod]
        public void AddCorrect()
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

            IList<string> errorMessage = new List<string>();
            var validator = new Mock<IValidator<Book>>();
            validator.Setup(s => s.IsValid(It.IsAny<Book>(), out It.Ref<IList<string>>.IsAny)).Returns(true);

            var memoryDal = new Mock<IBookDal>();
            memoryDal.Setup(dal => dal.Add(It.IsAny<Book>())).Returns(1);


            BookLogic logic = new BookLogic(memoryDal.Object, validator.Object);

            var result = logic.Add(_correctBook);

            Assert.AreEqual(1, result);
        }

        public delegate void CallbackValid(Book person, out IList<string> errorList);

        [TestMethod]
        public void AddIncorrect()
        {
            Book _inCorrectBook = new Book
            {
                Title = "Title",
                NumberOfPages = 1,
                PublishingYear = DateTime.Now.Year + 1,
                Note = "Note",
                PublishingCity = "city",
                PublishingHouse = "House",
                ISBN = ""
            };

            IList<string> validationErrors = null;

            var validator = new Mock<IValidator<Book>>();
            validator.Setup(s => s.IsValid(It.IsAny<Book>(), out It.Ref<IList<string>>.IsAny))
                         .Callback(new CallbackValid((Book person, out IList<string> errorsList) =>
                         {
                             errorsList = new List<string>() { "Error 1", "Error 2" };
                         }))
                         .Returns(false);

            var memoryDal = new Mock<IBookDal>();
            memoryDal.Setup(dal => dal.Add(It.IsAny<Book>())).Returns(0);

            BookLogic logic = new BookLogic(memoryDal.Object, validator.Object);

            try
            {
                logic.Add(_inCorrectBook);
            }
            catch (ObjectNotValidateException e)
            {
                validationErrors = e.BackMessageValidate;
            }

            Assert.IsNotNull(validationErrors);
            Assert.AreEqual(2, validationErrors.Count);
        }

        #endregion

        #region getAndGroupByPublishingHouse
        [TestMethod]
        public void GetAndGroupByCorrectPublishingHouse() 
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

            Book _correctBook2 = new Book
            {
                Title = "Title",
                NumberOfPages = 1,
                PublishingYear = DateTime.Now.Year,
                Note = "Note",
                PublishingCity = "City",
                PublishingHouse = "House2",
                ISBN = ""
            };

            IList<string> errorMessage = new List<string>();
            var validator = new Mock<IValidator<Book>>();

            List<Book> books = new List< Book> { _correctBook, _correctBook, _correctBook2 };
            var memoryDal = new Mock<IBookDal>();
            memoryDal.Setup(dal => dal.GetAndGroupByPublishingHouse(It.IsAny<string>())).Returns(books.ToLookup(b => b.PublishingHouse, b => b));


            BookLogic logic = new BookLogic(memoryDal.Object, validator.Object);

            var result = logic.GetAndGroupByPublishingHouse("House").Count();

            Assert.AreEqual(2, result);
        }

        [TestMethod]
        public void GetAndGroupByInorrectPublishingHouse() 
        {
            IList<string> errorMessage = new List<string>();
            var validator = new Mock<IValidator<Book>>();

            List<Book> books = new List<Book> {};

            var memoryDal = new Mock<IBookDal>();
            memoryDal.Setup(dal => dal.GetAndGroupByPublishingHouse(It.IsAny<string>())).Returns(books.ToLookup(b => b.PublishingHouse, b => b));

            BookLogic logic = new BookLogic(memoryDal.Object, validator.Object);

            var result = logic.GetAndGroupByPublishingHouse("").Count();

            Assert.AreEqual(0, result);
        }
        #endregion

        #region getByAuthor
        [TestMethod]
        public void GetAndGroupByCorrectAuthorId() 
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

            IList<string> errorMessage = new List<string>();
            var validator = new Mock<IValidator<Book>>();

            var memoryDal = new Mock<IBookDal>();
            memoryDal.Setup(dal => dal.GetByAuthor(It.IsAny<int>())).Returns(new List<Book> { _correctBook });

            BookLogic logic = new BookLogic(memoryDal.Object, validator.Object);

            var result = logic.GetByAuthor(1).Count();

            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void GetAndGroupByInorrectAuthorId() 
        {
            IList<string> errorMessage = new List<string>();
            var validator = new Mock<IValidator<Book>>();

            var memoryDal = new Mock<IBookDal>();
            memoryDal.Setup(dal => dal.GetByAuthor(It.IsAny<int>())).Returns(new List<Book> { });

            BookLogic logic = new BookLogic(memoryDal.Object, validator.Object);

            var result = logic.GetByAuthor(1).Count();

            Assert.AreEqual(0, result);
        }
        #endregion

        #region getAll 
        [TestMethod]
        public void GetCorrect() 
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

            IList<string> errorMessage = new List<string>();
            var validator = new Mock<IValidator<Book>>();

            var memoryDal = new Mock<IBookDal>();
            memoryDal.Setup(dal => dal.GetAll()).Returns(new List<Book> { _correctBook });

            BookLogic logic = new BookLogic(memoryDal.Object, validator.Object);

            var result = logic.GetAll().Count();

            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void GetEmptyCollection() 
        {
            IList<string> errorMessage = new List<string>();
            var validator = new Mock<IValidator<Book>>();

            var memoryDal = new Mock<IBookDal>();
            memoryDal.Setup(dal => dal.GetAll()).Returns(new List<Book> { });

            BookLogic logic = new BookLogic(memoryDal.Object, validator.Object);

            var result = logic.GetAll().Count();

            Assert.AreEqual(0, result);
        }
        #endregion

        #region getById
        [TestMethod]
        public void GetByCorrectId() 
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

            IList<string> errorMessage = new List<string>();
            var validator = new Mock<IValidator<Book>>();

            var memoryDal = new Mock<IBookDal>();
            memoryDal.Setup(dal => dal.GetById(It.IsAny<int>())).Returns(_correctBook);

            BookLogic logic = new BookLogic(memoryDal.Object, validator.Object);

            var result = logic.GetById(1);

            Assert.AreEqual(_correctBook, result);
        }

        [TestMethod]
        public void GetByInorrectId()
        {
            IList<string> errorMessage = new List<string>();
            var validator = new Mock<IValidator<Book>>();

            var memoryDal = new Mock<IBookDal>();
            memoryDal.Setup(dal => dal.GetById(It.IsAny<int>())).Returns((Book) null);

            BookLogic logic = new BookLogic(memoryDal.Object, validator.Object);

            var result = logic.GetById(1);

            Assert.AreEqual(null, result);
        }
        #endregion

    }
}
