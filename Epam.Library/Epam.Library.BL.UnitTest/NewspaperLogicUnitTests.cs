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
    public class NewspaperLogicUnitTests
    {
        #region add

        [TestMethod]
        public void AddCorrect()
        {
            Newspaper _correctNewspaper = new Newspaper
            {
                Title = "Title",
                NumberOfPages = 1,
                PublishingYear = DateTime.Now.Year,
                Note = "Note",
                PublishingCity = "City",
                PublishingHouse = "House",
                ISSN = ""
            };


            var validator = new Mock<IValidator<Newspaper>>();
            validator.Setup(s => s.IsValid(It.IsAny<Newspaper>(), out It.Ref<IList<string>>.IsAny)).Returns(true);

            var memoryDal = new Mock<INewspaperDal>();
            memoryDal.Setup(dal => dal.Add(It.IsAny<Newspaper>())).Returns(1);

            NewspaperLogic logic = new NewspaperLogic(memoryDal.Object, validator.Object);

            Assert.AreEqual(1, logic.Add(_correctNewspaper));
        }

        public delegate void CallbackValid(Newspaper person, out IList<string> errorList);

        [TestMethod]
        public void AddIncorrect()
        {
            Newspaper _inCorrectNewspaper = new Newspaper
            {
                Title = "Title",
                NumberOfPages = 1,
                PublishingYear = DateTime.Now.Year + 1,
                Note = "Note",
                PublishingCity = "city",
                PublishingHouse = "House",
                ISSN = ""
            };

            var validator = new Mock<IValidator<Newspaper>>();
            validator.Setup(v => v.IsValid(It.IsAny<Newspaper>(), out It.Ref<IList<string>>.IsAny))
                         .Callback(new CallbackValid((Newspaper person, out IList<string> errorsList) =>
                         {
                             errorsList = new List<string>() { "Error 1", "Error 2" };
                         }))
                         .Returns(false);

            var memoryDal = new Mock<INewspaperDal>();
            memoryDal.Setup(dal => dal.Add(It.IsAny<Newspaper>())).Returns(0);

            NewspaperLogic logic = new NewspaperLogic(memoryDal.Object, validator.Object);

            IList<string> validationErrors = null;

            try
            {
                logic.Add(_inCorrectNewspaper);
            }
            catch (ObjectNotValidateException e)
            {
                validationErrors = e.BackMessageValidate;
            }

            Assert.IsNotNull(validationErrors);
            Assert.AreEqual(2, validationErrors.Count);
        }

        #endregion

        #region getAll 
        [TestMethod]
        public void GetCorrect()
        {
            Newspaper _correctNewspaper = new Newspaper
            {
                Title = "Title",
                NumberOfPages = 1,
                PublishingYear = DateTime.Now.Year,
                Note = "Note",
                PublishingCity = "City",
                PublishingHouse = "House",
                ISSN = ""
            };

            var validator = new Mock<IValidator<Newspaper>>();

            var memoryDal = new Mock<INewspaperDal>();
            memoryDal.Setup(dal => dal.GetAll()).Returns(new List<Newspaper> {_correctNewspaper, _correctNewspaper});

            NewspaperLogic logic = new NewspaperLogic(memoryDal.Object, validator.Object);

            var result = logic.GetAll().Count();

            Assert.AreEqual(2, result);
        }

        [TestMethod]
        public void GetEmptyCollection()
        {
            var validator = new Mock<IValidator<Newspaper>>();

            var memoryDal = new Mock<INewspaperDal>();
            memoryDal.Setup(dal => dal.GetAll()).Returns(new List<Newspaper> { });

            NewspaperLogic logic = new NewspaperLogic(memoryDal.Object, validator.Object);

            var result = logic.GetAll().Count();

            Assert.AreEqual(0, result);
        }

        #endregion

        #region getById
        [TestMethod]
        public void GetByCorrectId() 
        {
            Newspaper _correctNewspaper = new Newspaper
            {
                Title = "Title",
                NumberOfPages = 1,
                PublishingYear = DateTime.Now.Year,
                Note = "Note",
                PublishingCity = "City",
                PublishingHouse = "House",
                ISSN = ""
            };

            var validator = new Mock<IValidator<Newspaper>>();

            var memoryDal = new Mock<INewspaperDal>();
            memoryDal.Setup(dal => dal.GetById(It.IsAny<int>())).Returns(_correctNewspaper);

            NewspaperLogic logic = new NewspaperLogic(memoryDal.Object, validator.Object);

            var result = logic.GetById(1);

            Assert.AreEqual(_correctNewspaper, result);
        }

        [TestMethod]
        public void GetByInorrectId() 
        {
            var validator = new Mock<IValidator<Newspaper>>();

            var memoryDal = new Mock<INewspaperDal>();
            memoryDal.Setup(dal => dal.GetById(It.IsAny<int>())).Returns((Newspaper)null);

            NewspaperLogic logic = new NewspaperLogic(memoryDal.Object, validator.Object);

            var result = logic.GetById(1);

            Assert.AreEqual(null, result);
        }
        #endregion

    }
}
