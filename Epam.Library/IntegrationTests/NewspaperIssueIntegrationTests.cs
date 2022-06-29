using Epam.Library.Dependencies;
using Epam.Library.Entities;
using Epam.Library.Entities.Exceptions;
using Epam.Library.LogicContracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace IntegrationTests
{
    [TestClass]
    public class NewspaperIssueIntegrationTests
    {
        private INewspaperIssueLogic _newspaperIssueLogic = DependencyResolver.NewspaperIssueLogic;
        private NewspaperIssue _correctNewspaperIssue;
        private ILibraryObjectLogic libraryObjectLogic = DependencyResolver.LibraryObjectLogic;
        private Newspaper _correctNewspaper;
        private INewspaperLogic _newspaperLogic = DependencyResolver.NewspaperLogic;

        [TestInitialize]
        public void CorrectNewspaper()
        {
            _correctNewspaper = new Newspaper
            {
                Title = "Title",
                NumberOfPages = 1,
                PublishingYear = DateTime.Now.Year,
                Note = "Note",
                PublishingCity = "City",
                PublishingHouse = "House",
                ISSN = ""
            };
            _correctNewspaperIssue = new NewspaperIssue
            {
                Number = 1,
                PublishingDate = DateTime.Now
            };

        }

        #region add
        [TestMethod]
        public void AddNewspaperIssueCorrectValue()
        {
            int? id = null;
            int? newId = null;
            try
            {
                 id = _newspaperLogic.Add(_correctNewspaper);
                _correctNewspaper.Id = (int)id;

                newId = _newspaperIssueLogic.Add(_correctNewspaper, _correctNewspaperIssue);
            }
            finally
            {
                _newspaperIssueLogic.Delete((int)id, (int)newId);
                libraryObjectLogic.Delete((int)id);
            }
            Assert.IsTrue((int)newId > 0);

        }

        [TestMethod]
        public void AddTwoDifferentNewspaperIssues()
        {
            int? id = null;

            int? firstId = null;
            int? secondId = null;
            try
            {
                id = _newspaperLogic.Add(_correctNewspaper);
                _correctNewspaper.Id = (int)id;

                firstId = _newspaperIssueLogic.Add(_correctNewspaper, _correctNewspaperIssue);
                secondId = _newspaperIssueLogic.Add(_correctNewspaper, new NewspaperIssue { Number = 2, PublishingDate = DateTime.Now.AddDays(-1) });
            }
            finally
            {
                _newspaperIssueLogic.Delete((int)id, (int)firstId);
                if (secondId != null)
                    _newspaperIssueLogic.Delete((int)id, (int)secondId);
                libraryObjectLogic.Delete((int)id);
            }

            Assert.IsTrue(firstId > 0);
            Assert.IsTrue(secondId > firstId);
        }

        [TestMethod]
        public void AddTwoSameNewspaperIssuesExpectException()
        {
            int? id = null;

            int? firstId = null;
            string error = null;
            int? secondId = null;

            try
            {
                id = _newspaperLogic.Add(_correctNewspaper);
                _correctNewspaper.Id = (int)id;

                firstId = _newspaperIssueLogic.Add(_correctNewspaper, _correctNewspaperIssue);
                
                secondId = _newspaperIssueLogic.Add(_correctNewspaper, new NewspaperIssue
                {
                    Number = 1,
                    PublishingDate = DateTime.Now
                });
            }
            catch (ObjectNotUniqueException e)
            {
                error = e.Message;
            }
            finally
            {
                _newspaperIssueLogic.Delete((int)id, (int)firstId);
                if (secondId != null)
                    _newspaperIssueLogic.Delete((int)id, (int)secondId);
                libraryObjectLogic.Delete((int)id);
            }

            Assert.IsNotNull(error);

        }
        #endregion
    }
}
