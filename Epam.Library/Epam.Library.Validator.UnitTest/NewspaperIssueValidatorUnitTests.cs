using Epam.Library.Entities;
using Epam.Library.ValidatorContracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Library.Validator.UnitTest
{
    [TestClass]
    public class NewspaperIssueValidatorUnitTests
    {
        private INewspaperIssueValidator _validator = new NewspaperIssueValidator();
        private NewspaperIssue _newspaperIssue;
        private Newspaper _newspaper;

        [TestInitialize]
        public void InitializeCorrectPerson()
        {
            _newspaper = new Newspaper
            {
                PublishingYear = DateTime.Now.Year
            };
            _newspaperIssue = new NewspaperIssue
            {
                Number = 1,
                PublishingDate = DateTime.Now
            };
        }
        #region number
        [TestMethod]
        public void AddWithCorrectNumber()
        {
            NewspaperIssue newNewspaperIssue = new NewspaperIssue
            {
                Number = _newspaperIssue.Number,
                PublishingDate = _newspaperIssue.PublishingDate
            };

            bool isValid = _validator.IsValid(newNewspaperIssue, _newspaper, out IList<string> errorMessages);
            Assert.AreEqual(true, isValid);
            Assert.AreEqual(0, errorMessages.Count);
        }

        [TestMethod]
        public void AddWithNumberLess0()
        {
            NewspaperIssue newNewspaperIssue = new NewspaperIssue
            {
                Number = -1,
                PublishingDate = _newspaperIssue.PublishingDate
            };

            bool isValid = _validator.IsValid(newNewspaperIssue, _newspaper, out IList<string> errorMessages);
            Assert.AreEqual(false, isValid);
            Assert.AreEqual(1, errorMessages.Count);
            Assert.AreEqual(true, errorMessages.Any(e => e == nameof(newNewspaperIssue.Number)));
        }


        #endregion

        #region publishing date
        [TestMethod]
        public void AddWithCorrectPublishingDate()
        {
            NewspaperIssue newNewspaperIssue = new NewspaperIssue
            {
                Number = _newspaperIssue.Number,
                PublishingDate = _newspaperIssue.PublishingDate
            };

            bool isValid = _validator.IsValid(newNewspaperIssue, _newspaper, out IList<string> errorMessages);
            Assert.AreEqual(true, isValid);
            Assert.AreEqual(0, errorMessages.Count);
        }


        [TestMethod]
        public void AddWithIncorrectPublishingDateYear()
        {
            NewspaperIssue newNewspaperIssue = new NewspaperIssue
            {
                Number = _newspaperIssue.Number,
                PublishingDate = _newspaperIssue.PublishingDate.AddDays(367)
            };

            bool isValid = _validator.IsValid(newNewspaperIssue, _newspaper, out IList<string> errorMessages);
            Assert.AreEqual(false, isValid);
            Assert.AreEqual(1, errorMessages.Count);
            Assert.AreEqual(true, errorMessages.Any(e => e == nameof(newNewspaperIssue.PublishingDate)));
        }

        #endregion
    }
}
