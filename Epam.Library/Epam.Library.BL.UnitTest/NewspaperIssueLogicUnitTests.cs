using Epam.Library.DalContracts;
using Epam.Library.Entities;
using Epam.Library.Entities.Exceptions;
using Epam.Library.Logic;
using Epam.Library.ValidatorContracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;

namespace Epam.Library.BL.UnitTests
{
    [TestClass]
    public class NewspaperIssueIssueLogicUnitTests
    {
        Newspaper newspaper;

        [TestInitialize]
        public void TestNewspaper()
        {
            newspaper = new Newspaper
            {
                Id = 1,
                Title = "Title",
                NumberOfPages = 1,
                PublishingYear = DateTime.Now.Year,
                Note = "Note",
                PublishingCity = "City",
                PublishingHouse = "House",
                ISSN = ""
            };
        }

        #region add

        [TestMethod]
        public void AddCorrect()
        {
            NewspaperIssue _correctNewspaperIssue = new NewspaperIssue
            {
                Number = 1,
                PublishingDate = DateTime.Now
            };

            IList<string> errorMessage = new List<string>();
            var validator = new Mock<INewspaperIssueValidator>();
            validator.Setup(s => s.IsValid(It.IsAny<NewspaperIssue>(), It.IsAny<Newspaper>(), out It.Ref<IList<string>>.IsAny)).Returns(true);

            var memoryDal = new Mock<INewspaperIssueDal>();
            memoryDal.Setup(dal => dal.Add(newspaper.Id, It.IsAny<NewspaperIssue>())).Returns(1);

            NewspaperIssueLogic logic = new NewspaperIssueLogic(memoryDal.Object, validator.Object);

            var result = logic.Add(newspaper, _correctNewspaperIssue);

            Assert.AreEqual(1, result);
        }

        public delegate void CallbackValid(NewspaperIssue person, Newspaper newspaper, out IList<string> errorList);

        [TestMethod]
        public void AddIncorrect()
        {
            NewspaperIssue _inCorrectNewspaperIssue = new NewspaperIssue
            {
                Number = -1,
                PublishingDate = DateTime.Now.AddDays(100)
            };

            IList<string> validationErrors = null;

            var validator = new Mock<INewspaperIssueValidator>();
            validator.Setup(v => v.IsValid(_inCorrectNewspaperIssue, It.IsAny<Newspaper>(), out It.Ref<IList<string>>.IsAny))
                         .Callback(new CallbackValid((NewspaperIssue person, Newspaper newspaper, out IList<string> errorsList) =>
                         {
                             errorsList = new List<string>() { "Error 1", "Error 2" };
                         }))
                         .Returns(false);

            var memoryDal = new Mock<INewspaperIssueDal>();
            memoryDal.Setup(dal => dal.Add(newspaper.Id, It.IsAny<NewspaperIssue>())).Returns(0);

            NewspaperIssueLogic logic = new NewspaperIssueLogic(memoryDal.Object, validator.Object);

            try
            {
                logic.Add(newspaper, _inCorrectNewspaperIssue);
            }
            catch (ObjectNotValidateException e)
            {
                validationErrors = e.BackMessageValidate;
            }

            Assert.IsNotNull(validationErrors);
            Assert.AreEqual(2, validationErrors.Count);
        }

        #endregion
    }
}
