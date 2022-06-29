using Epam.Library.Dependencies;
using Epam.Library.Entities;
using Epam.Library.LogicContracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IntegrationTests
{
    [TestClass]
    public class LibraryObjectIntegrationTests
    {
        private Book _correctBook;
        private ILibraryObjectLogic libraryObjectLogic = DependencyResolver.LibraryObjectLogic;
        private IBookLogic bookLogic = DependencyResolver.BookLogic;
        private IPatentLogic patentLogic = DependencyResolver.PatentLogic;
        private Patent _correctPatent;
        private IPersonLogic personLogic = DependencyResolver.PersonLogic;

        [TestInitialize]
        public void CorrectValues()
        {
            _correctBook = new Book
            {
                Title = "Title",
                NumberOfPages = 1,
                PublishingYear = DateTime.Now.Year,
                Note = "Note",
                PublishingCity = "City",
                PublishingHouse = "House",
                ISBN = "ISBN 6-1256-1356-9"
            };

            _correctPatent = new Patent
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
        }

        #region Delete
        [TestMethod]
        public void DeleteByCorrectId()
        {
            int? bookId = null;

            try
            {
                bookId = bookLogic.Add(_correctBook);
            }
            finally
            {
                bool delete = libraryObjectLogic.Delete((int)bookId);
                Assert.IsTrue(delete);
            }
        }

        [TestMethod]
        public void DeleteByIncorrectId()
        {
            int? bookId = null;
            bool notDelete = true;
            try
            {
                bookId = bookLogic.Add(_correctBook);
                notDelete = libraryObjectLogic.Delete((int)bookId + 1);
            }
            finally
            {
                bool delete = libraryObjectLogic.Delete((int)bookId);
                Assert.IsTrue(delete);
            }
            Assert.IsFalse(notDelete);
        }
        #endregion

        #region GetBooksPatentsByPerson
        [TestMethod]
        public void GetByCorrectPerson()
        {
            Person _correctPerson = new Person
            {
                Name = "Name",
                Surname = "Surname"
            };

            int personid = personLogic.Add(_correctPerson);
            _correctPerson.Id = personid;

            _correctBook.Authors.Add(_correctPerson);
            _correctPatent.Inventors.Add(_correctPerson);

            int? bookId = null;
            int? patentId = null;
            List<LibraryObject> objs = new List<LibraryObject>();

            try
            {
                bookId = bookLogic.Add(_correctBook);
                patentId = patentLogic.Add(_correctPatent);
                objs = libraryObjectLogic.GetBooksPatentsByPerson(personid).ToList();

                Assert.IsTrue(objs.Any(p => p.Id == bookId));
                Assert.IsTrue(objs.Any(p => p.Id == patentId));
            }
            finally
            {
                if (bookId != null)
                {
                    libraryObjectLogic.Delete((int)bookId);
                }
                if (patentId != null)
                {
                    libraryObjectLogic.Delete((int)patentId);
                }
                personLogic.Delete(personid);
            }

        }

        [TestMethod]
        public void GetByIncorrectPerson()
        {

            Person _correctPerson = new Person
            {
                Name = "Name",
                Surname = "Surname"
            };

            int personid = personLogic.Add(_correctPerson);
            _correctPerson.Id = personid;

            _correctBook.Authors.Add(_correctPerson);
            _correctPatent.Inventors.Add(_correctPerson);

            int? bookId = null;
            int? patentId = null;
            List<LibraryObject> objs = new List<LibraryObject>();

            try
            {
                bookId = bookLogic.Add(_correctBook);
                patentId = patentLogic.Add(_correctPatent);
                objs = libraryObjectLogic.GetBooksPatentsByPerson(personid + 1).ToList();

                Assert.IsTrue(!objs.Any(p => p.Id == bookId));
                Assert.IsTrue(!objs.Any(p => p.Id == patentId));
            }
            finally
            {
                if (bookId != null)
                {
                    libraryObjectLogic.Delete((int)bookId);
                }
                if (patentId != null)
                {
                    libraryObjectLogic.Delete((int)patentId);
                }
                personLogic.Delete(personid);
            }
        }

        #endregion

        #region GroupingByPublishingYear
        [TestMethod]
        public void GroupingByPublishingYearInNotEmptyCollection()
        {
            int? bookId = null;
            int? patentId = null;

            ILookup<int, LibraryObject> objects;

            try
            {
                bookId = bookLogic.Add(_correctBook);
                patentId = patentLogic.Add(_correctPatent);
                objects = libraryObjectLogic.GroupingByPublishingYear();
            }
            finally
            {
                libraryObjectLogic.Delete((int)bookId);
                libraryObjectLogic.Delete((int)patentId);
            }
            Assert.IsTrue(objects.Any());
            Assert.IsTrue(objects[DateTime.Now.Year].Any());
            Assert.IsTrue(objects[DateTime.Now.Year].Any(p => p.Id == bookId));
            Assert.IsTrue(objects[DateTime.Now.Year].Any(p => p.Id == patentId));

        }
        #endregion

        #region GetAll

        [TestMethod]
        public void GetAllInNotEmptyCollection()
        {
            int? bookId = null;
            int? patentId = null;

            List<LibraryObject> objects = new List<LibraryObject>();

            try
            {
                bookId = bookLogic.Add(_correctBook);
                patentId = patentLogic.Add(_correctPatent);

                objects = libraryObjectLogic.GetAll().ToList();
            }
            finally
            {
                libraryObjectLogic.Delete((int)bookId);
                libraryObjectLogic.Delete((int)patentId);
            }
            
            Assert.IsTrue(objects.Any(p => p.Id == bookId));
            Assert.IsTrue(objects.Any(p => p.Id == patentId));
        }

        #endregion

        #region GetByTitle
        [TestMethod]
        public void GetByCorrectTitle()
        {
            int? bookId = null;
            int? patentId = null;

            List<LibraryObject> objects = new List<LibraryObject>();

            try
            {
                bookId = bookLogic.Add(_correctBook);
                patentId = patentLogic.Add(_correctPatent);
                objects = libraryObjectLogic.GetByTitle("Title").ToList();

                Assert.IsTrue(objects.Any(p => p.Id == bookId));
                Assert.IsTrue(objects.Any(p => p.Id == patentId));
            }
            finally
            {
                libraryObjectLogic.Delete((int)bookId);
                libraryObjectLogic.Delete((int)patentId);
            }
        }

        [TestMethod]
        public void GetByIncorrectTitle()
        {
            int? bookId = null;
            int? patentId = null;

            List<LibraryObject> objects = new List<LibraryObject>();

            try
            {
                bookId = bookLogic.Add(_correctBook);
                patentId = patentLogic.Add(_correctPatent);
                objects = libraryObjectLogic.GetByTitle("Titleee").ToList();

                Assert.IsTrue(!objects.Any(p => p.Id == bookId));
                Assert.IsTrue(!objects.Any(p => p.Id == patentId));
            }
            finally
            {
                libraryObjectLogic.Delete((int)bookId);
                libraryObjectLogic.Delete((int)patentId);
            }
        }
        #endregion

        #region SortingByYearDirectOrder
        [TestMethod]
        public void SortingByYearDirectOrderInNotEmptyCollection()
        {
            int? bookId = null;
            int? patentId = null;

            List<LibraryObject> objects = new List<LibraryObject>();

            try
            {
                bookId = bookLogic.Add(_correctBook);
                _correctPatent.PublishingYear -= 1;
                patentId = patentLogic.Add(_correctPatent);
                objects = libraryObjectLogic.SortingByYearDirectOrder().ToList();

                Assert.IsTrue(objects.Any());
                Assert.IsTrue(objects.Any(p => p.Id == bookId));
                Assert.IsTrue(objects.Any(p => p.Id == patentId));
            }
            finally
            {
                libraryObjectLogic.Delete((int)bookId);
                libraryObjectLogic.Delete((int)patentId);
                _correctPatent.PublishingYear += 1;
            }

        }

        #endregion

        #region SortingByYearReverseOrder
        [TestMethod]
        public void SortingByYearReverseOrderInNotEmptyCollection()
        {

            int? bookId = null;
            int? patentId = null;

            List<LibraryObject> objects = new List<LibraryObject>();

            try
            {
                bookId = bookLogic.Add(_correctBook);
                _correctPatent.PublishingYear -= 1;
                patentId = patentLogic.Add(_correctPatent);
                objects = libraryObjectLogic.SortingByYearReverseOrder().ToList();

                Assert.IsTrue(objects.Any());
                Assert.IsTrue(objects.Any(p => p.Id == bookId));
                Assert.IsTrue(objects.Any(p => p.Id == patentId));
            }
            finally
            {
                libraryObjectLogic.Delete((int)bookId);
                libraryObjectLogic.Delete((int)patentId);
                _correctPatent.PublishingYear += 1;

            }

        }
        #endregion
    }
}
