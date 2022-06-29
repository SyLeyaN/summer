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
    public class PatentLogicUnitTests
    {

        #region add

        [TestMethod]
        public void AddCorrect()
        {
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

            IList<string> errorMessage = new List<string>();
            var validator = new Mock<IValidator<Patent>>();
            validator.Setup(s => s.IsValid(It.IsAny<Patent>(), out It.Ref<IList<string>>.IsAny)).Returns(true);

            var memoryDal = new Mock<IPatentDal>();
            memoryDal.Setup(dal => dal.Add(It.IsAny<Patent>())).Returns(1);


            PatentLogic logic = new PatentLogic(memoryDal.Object, validator.Object);

            Assert.AreEqual(1, logic.Add(_correctPatent));
        }

        public delegate void CallbackValid(Patent person, out IList<string> errorList);

        [TestMethod]
        public void AddIncorrect()
        {
            Patent _inCorrectPatent = new Patent
            {
                Title = "Title",
                NumberOfPages = -1,
                ApplicationDate = DateTime.MinValue,
                PublishingDate = DateTime.Now,
                PublishingYear = DateTime.Now.Year,
                Country = "-Country",
                Note = "Note",
                RegistrationNumber = "123456"
            };

            IList<string> validationErrors = null;

            var validator = new Mock<IValidator<Patent>>();
            validator.Setup(v => v.IsValid(It.IsAny<Patent>(), out It.Ref<IList<string>>.IsAny))
                         .Callback(new CallbackValid((Patent person, out IList<string> errorsList) =>
                         {
                             errorsList = new List<string>() { "Error 1", "Error 2" };
                         }))
                         .Returns(false);

            var memoryDal = new Mock<IPatentDal>();
            memoryDal.Setup(dal => dal.Add(It.IsAny<Patent>())).Returns(0);

            PatentLogic logic = new PatentLogic(memoryDal.Object, validator.Object);

            try
            {
                logic.Add(_inCorrectPatent);
            }
            catch (ObjectNotValidateException e)
            {
                validationErrors = e.BackMessageValidate;
            }

            Assert.IsNotNull(validationErrors);
            Assert.AreEqual(2, validationErrors.Count);
        }
        #endregion

        #region getByInventor
        [TestMethod]
        public void GetAndGroupByCorrectInventorId() 
        {
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

            IList<string> errorMessage = new List<string>();
            var validator = new Mock<IValidator<Patent>>();

            var memoryDal = new Mock<IPatentDal>();
            memoryDal.Setup(dal => dal.GetByInventor(It.IsAny<int>())).Returns(new List<Patent> { _correctPatent });

            PatentLogic logic = new PatentLogic(memoryDal.Object, validator.Object);

            var result = logic.GetByInventor(1).Count();

            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void GetAndGroupByInorrectInventorId() 
        {
            IList<string> errorMessage = new List<string>();
            var validator = new Mock<IValidator<Patent>>();

            var memoryDal = new Mock<IPatentDal>();
            memoryDal.Setup(dal => dal.GetByInventor(It.IsAny<int>())).Returns((List<Patent>)null);

            PatentLogic logic = new PatentLogic(memoryDal.Object, validator.Object);

            var result = logic.GetByInventor(1);

            Assert.AreEqual(null, result);
        }

        #endregion

        #region getAll 
        [TestMethod]
        public void GetCorrect() 
        {
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

            IList<string> errorMessage = new List<string>();
            var validator = new Mock<IValidator<Patent>>();

            var memoryDal = new Mock<IPatentDal>();
            memoryDal.Setup(dal => dal.GetAll()).Returns(new List<Patent> {_correctPatent, _correctPatent});

            PatentLogic logic = new PatentLogic(memoryDal.Object, validator.Object);

            Assert.AreEqual(2, logic.GetAll().Count());
        }

        [TestMethod]
        public void GetEmptyCollection() 
        {
            IList<string> errorMessage = new List<string>();
            var validator = new Mock<IValidator<Patent>>();

            var memoryDal = new Mock<IPatentDal>();
            memoryDal.Setup(dal => dal.GetAll()).Returns((IEnumerable<Patent>)null);

            PatentLogic logic = new PatentLogic(memoryDal.Object, validator.Object);

            Assert.AreEqual(null, logic.GetAll());
        }
        #endregion

        #region getById
        [TestMethod]
        public void GetByCorrectId() 
        {
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

            IList<string> errorMessage = new List<string>();
            var validator = new Mock<IValidator<Patent>>();

            var memoryDal = new Mock<IPatentDal>();
            memoryDal.Setup(dal => dal.GetById(It.IsAny<int>())).Returns(_correctPatent);

            PatentLogic logic = new PatentLogic(memoryDal.Object, validator.Object);

            var result = logic.GetById(1);

            Assert.AreEqual(_correctPatent, result);
        }

        [TestMethod]
        public void GetByInorrectId() 
        {
            IList<string> errorMessage = new List<string>();
            var validator = new Mock<IValidator<Patent>>();

            var memoryDal = new Mock<IPatentDal>();
            memoryDal.Setup(dal => dal.GetById(It.IsAny<int>())).Returns((Patent)null);

            PatentLogic logic = new PatentLogic(memoryDal.Object, validator.Object);

            var result = logic.GetById(1);

            Assert.AreEqual(null, result);
        }
        #endregion
    }
}
