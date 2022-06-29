using Epam.Library.Dependencies;
using Epam.Library.Entities;
using Epam.Library.Entities.Exceptions;
using Epam.Library.LogicContracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IntegrationTests
{
    [TestClass]
    public class BookIntegrationTests
    {
        private IBookLogic _bookLogic = DependencyResolver.BookLogic;
        private IPersonLogic _personLogic = DependencyResolver.PersonLogic;
        private Book _correctBook;
        private Person _correctPerson;
        private ILibraryObjectLogic libraryObjectLogic = DependencyResolver.LibraryObjectLogic;

        [TestInitialize]
        public void Correct()
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

            _correctPerson = new Person
            {
                Name = "Name",
                Surname = "Surname"
            };
        }

        #region add
        [TestMethod]
        public void AddBookCorrectValue()
        {
            int? newId = null;
            try
            {
                 newId = _bookLogic.Add(_correctBook);

                Assert.IsTrue(newId >= 1);
            }
            finally
            {
                if (newId != null)
                {
                    libraryObjectLogic.Delete((int)newId);
                }
            }
        }

        [TestMethod]
        public void AddTwoDifferentBooks()
        {
            int? firstId = null;
            int? secondId = null;

            try
            {
                firstId = _bookLogic.Add(_correctBook);
                secondId = _bookLogic.Add(new Book
                {
                    Title = "NewTitlee",
                    NumberOfPages = 1,
                    PublishingYear = DateTime.Now.Year,
                    Note = "Note",
                    PublishingCity = "Cityy",
                    PublishingHouse = "Housee",
                    ISBN = ""
                });
            }
            finally
            {
                if (firstId != null)
                {
                    libraryObjectLogic.Delete((int)firstId);
                }
                if (secondId != null)
                {
                    libraryObjectLogic.Delete((int)secondId);
                }
            }
            Assert.IsTrue(firstId >= 1);
            Assert.IsTrue(secondId > firstId);

        }

        [TestMethod]
        public void AddTwoSameBooksExpectException()
        {
            Book secondCorrectBook = new Book
            {
                Title = "Title",
                NumberOfPages = 1,
                PublishingYear = DateTime.Now.Year,
                Note = "Note",
                PublishingCity = "City",
                PublishingHouse = "House",
                ISBN = "ISBN 6-1256-1356-9"
            };

            int? firstId = null;
            string error = null;
            int? secondId = null;

            try
            {
                firstId = _bookLogic.Add(_correctBook);
                secondId = _bookLogic.Add(secondCorrectBook);
            }
            catch (ObjectNotUniqueException e)
            {
                error = e.Message;
            }
            finally
            {
                if (firstId != null)
                {
                    libraryObjectLogic.Delete((int)firstId);
                }

                if (secondId != null)
                {
                    libraryObjectLogic.Delete((int)secondId);
                }
            }

            Assert.IsNotNull(error);

        }

        [TestMethod]
        public void AddTwoBooksSameISBNExpectException()
        {
            int? firstId = null;
            string error = null;
            try
            {
                firstId = _bookLogic.Add(_correctBook);
                int secondId = _bookLogic.Add(new Book
                {
                    Title = "Titlee",
                    NumberOfPages = 1,
                    PublishingYear = DateTime.Now.Year,
                    Note = "Note",
                    PublishingCity = "City",
                    PublishingHouse = "House",
                    ISBN = "ISBN 6-1256-1356-9"
                });

                libraryObjectLogic.Delete(secondId);
            }
            catch (ObjectNotUniqueException e)
            {
                error = e.Message;
            }
            finally
            {
                if (firstId != null)
                {
                    libraryObjectLogic.Delete((int)firstId);
                }
            }
            Assert.IsNotNull(error);

        }

        [TestMethod]
        public void AddTwoBooksSameTitleYearAuthorsExpectException()
        {
            int? firstId = null;
            string error = null;
            int? newPersonId = null;
            try
            {
                newPersonId = _personLogic.Add(_correctPerson);
                _correctBook.Authors.Add(_correctPerson);

                firstId = _bookLogic.Add(_correctBook);
                int secondId = _bookLogic.Add(new Book
                {
                    Title = "Title",
                    NumberOfPages = 1,
                    PublishingYear = DateTime.Now.Year,
                    Note = "Note",
                    PublishingCity = "City",
                    PublishingHouse = "House",
                    ISBN = "ISBN 6-1256-1356-9",
                    Authors = {_correctPerson}
                });
            }
            catch (ObjectNotUniqueException e)
            {
                error = e.Message;
            }
            finally
            {
                if (firstId != null)
                {
                    libraryObjectLogic.Delete((int)firstId);
                }

                _personLogic.Delete((int)newPersonId);
            }

            Assert.IsNotNull(error);

        }
        #endregion

        #region GetByAuthor
        [TestMethod]
        public void GetByCorrectAuthor()
        {
            int? firstId = null;
            int? newPersonId = null;
            List<Book> books = new List<Book>();
            try
            {
                newPersonId = _personLogic.Add(_correctPerson);
                _correctBook.Authors.Add(_correctPerson);

                firstId = _bookLogic.Add(_correctBook);
                books = _bookLogic.GetByAuthor((int)newPersonId).ToList();
                
            }
            finally 
            {
                if (firstId != null)
                {
                    libraryObjectLogic.Delete((int)firstId);
                }
                _personLogic.Delete((int)newPersonId);
            }

            Assert.IsTrue(books.Any(p => p.Id == firstId));

        }

        [TestMethod]
        public void GetByIncorrectAuthor()
        {
            int? firstId = null;
            int? newPersonId = null;
            List<Book> books = new List<Book>();
            try
            {
                newPersonId = _personLogic.Add(_correctPerson);
                firstId = _bookLogic.Add(_correctBook);
                books = _bookLogic.GetByAuthor((int)newPersonId + 1).ToList();
            }
            finally
            {
                if (firstId != null)
                {
                    libraryObjectLogic.Delete((int)firstId);
                }
                _personLogic.Delete((int)newPersonId);
            }

            Assert.IsTrue(!books.Any(p => p.Id == firstId));

        }

        #endregion

        #region GetAndGroupByPublishingHouse
        [TestMethod]
        public void GetAndGroupByCorrectPublishingHouse()
        {
            int? firstId = null;
            try
            {
               firstId = _bookLogic.Add(_correctBook);

                ILookup<string, Book> books = _bookLogic.GetAndGroupByPublishingHouse(_correctBook.PublishingHouse);

                Assert.IsTrue(books.Any());
            }
            finally
            {
                if (firstId != null)
                {
                    libraryObjectLogic.Delete((int)firstId);
                }
            }
        }

        [TestMethod]
        public void GetAndGroupByIncorrectPublishingHouse()
        {
            int? firstId = null;
            ILookup<string, Book> books;
            try
            {
                firstId = _bookLogic.Add(_correctBook);
                books = _bookLogic.GetAndGroupByPublishingHouse("NocorrectHOUSE");
            }
            finally
            {
                if (firstId != null)
                {
                    libraryObjectLogic.Delete((int)firstId);
                }
            }
            Assert.IsTrue(!books.Any());
        }
        #endregion

        #region GetAll
        [TestMethod]
        public void GetAllInNotEmptyCollection()
        {

            int? firstId = null;
            List<Book> books = new List<Book>();
            try
            {
                firstId = _bookLogic.Add(_correctBook);
                books = _bookLogic.GetAll().ToList();
            }
            finally
            {
                if (firstId != null)
                {
                    libraryObjectLogic.Delete((int)firstId);
                }
            }

            Assert.IsTrue(books.Any(p => p.Id == firstId));

        }

        #endregion

        #region GetById
        [TestMethod]
        public void GetByCorrectId()
        {
            int? firstId = null;
            Book book = new Book(); 
            try
            {
                firstId =  _bookLogic.Add(_correctBook);
                book = _bookLogic.GetById((int)firstId);
            }
            finally
            {
                if (firstId != null)
                {
                    libraryObjectLogic.Delete((int)firstId);
                }
            }
            Assert.IsTrue(book.Id == firstId);
            Assert.IsNotNull(book);

        }

        [TestMethod]
        public void GetByIncorrectId()
        {
            int? firstId = null;
            Book book = null;
            try
            {
                firstId = _bookLogic.Add(_correctBook);
                book = _bookLogic.GetById((int)firstId + 1);
            }
            finally
            {
                if (firstId != null)
                {
                    libraryObjectLogic.Delete((int)firstId);
                }
            }

            Assert.IsTrue(book.Id == 0);

        }
        #endregion
    }
}
