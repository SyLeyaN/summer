using Epam.Library.DalContracts;
using Epam.Library.Entities;
using Epam.Library.Entities.Exceptions;
using Epam.Library.Logic;
using Epam.Library.ValidatorContracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace Epam.Library.BL.UnitTests
{
    [TestClass]
    public class PersonLogicUnitTests
    {

        #region add

        [TestMethod]
        public void AddCorrect()
        {
            Person _correctPerson = new Person
            {
                Name = "Name",
                Surname = "Surname"
            };

            IList<string> errorMessage = new List<string>();
            var validator = new Mock<IValidator<Person>>();
            validator.Setup(s => s.IsValid(It.IsAny<Person>(), out It.Ref<IList<string>>.IsAny)).Returns(true);

            var memoryDal = new Mock<IPersonDal>();
            memoryDal.Setup(dal => dal.Add(It.IsAny<Person>())).Returns(1);


            PersonLogic logic = new PersonLogic(memoryDal.Object, validator.Object);

            var result = logic.Add(_correctPerson);

            Assert.AreEqual(1, result);
        }

        public delegate void CallbackValid(Person person, out IList<string> errorList);

        [TestMethod]
        public void AddIncorrect()
        {
            Person _inCorrectPerson = new Person
            {
                Name = "name",
                Surname = "surname"
            };

            IList<string> validationErrors = null;

            var validator = new Mock<IValidator<Person>>();
            validator.Setup(v => v.IsValid(It.IsAny<Person>(), out It.Ref<IList<string>>.IsAny))
                         .Callback(new CallbackValid((Person person, out IList<string> errorsList) =>
                         {
                             errorsList = new List<string>() { "Error 1", "Error 2" };
                         }))
                         .Returns(false);

            var memoryDal = new Mock<IPersonDal>();
            memoryDal.Setup(dal => dal.Add(It.IsAny<Person>())).Returns(0);

            PersonLogic logic = new PersonLogic(memoryDal.Object, validator.Object);

            try
            {
                logic.Add(_inCorrectPerson);
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
        public void CorrectGetAll() 
        {
            Person _correctPerson = new Person
            {
                Name = "Name",
                Surname = "Surname"
            };

            IList<string> errorMessage = new List<string>();
            var validator = new Mock<IValidator<Person>>();

            var memoryDal = new Mock<IPersonDal>();
            memoryDal.Setup(dal => dal.GetAll()).Returns(new List<Person> {_correctPerson});

            PersonLogic logic = new PersonLogic(memoryDal.Object, validator.Object);

            var result = logic.GetAll().Count();

            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void GetAllEmpty() 
        {
            IList<string> errorMessage = new List<string>();
            var validator = new Mock<IValidator<Person>>();

            var memoryDal = new Mock<IPersonDal>();
            memoryDal.Setup(dal => dal.GetAll()).Returns((List<Person>)null);

            PersonLogic logic = new PersonLogic(memoryDal.Object, validator.Object);

            var result = logic.GetAll();

            Assert.AreEqual(null, result);
        }
        #endregion

        #region getById

        [TestMethod]
        public void CorrectId() 
        {
            Person _correctPerson = new Person
            {
                Name = "Name",
                Surname = "Surname"
            };

            IList<string> errorMessage = new List<string>();
            var validator = new Mock<IValidator<Person>>();

            var memoryDal = new Mock<IPersonDal>();
            memoryDal.Setup(dal => dal.GetById(It.IsAny<int>())).Returns(_correctPerson);

            PersonLogic logic = new PersonLogic(memoryDal.Object, validator.Object);

            var result = logic.GetById(1);

            Assert.AreEqual(_correctPerson, result);
        }

        [TestMethod]
        public void NoCorrectId() 
        {
            IList<string> errorMessage = new List<string>();
            var validator = new Mock<IValidator<Person>>();

            var memoryDal = new Mock<IPersonDal>();
            memoryDal.Setup(dal => dal.GetById(It.IsAny<int>())).Returns((Person)null);

            PersonLogic logic = new PersonLogic(memoryDal.Object, validator.Object);

            var result = logic.GetById(1);

            Assert.AreEqual(null, result);
        }
        #endregion

        #region delete
        [TestMethod]
        public void DeleteByCorrectId()
        {
            Person _correctPerson = new Person
            {
                Name = "Name",
                Surname = "Surname"
            };

            IList<string> errorMessage = new List<string>();
            var validator = new Mock<IValidator<Person>>();

            var memoryDal = new Mock<IPersonDal>();
            memoryDal.Setup(dal => dal.Delete(It.IsAny<int>())).Returns(true);

            PersonLogic logic = new PersonLogic(memoryDal.Object, validator.Object);

            var result = logic.Delete(1);

            Assert.IsTrue( result);
        }

        [TestMethod]
        public void DeleteByIncorrectId()
        {
            IList<string> errorMessage = new List<string>();
            var validator = new Mock<IValidator<Person>>();

            var memoryDal = new Mock<IPersonDal>();
            memoryDal.Setup(dal => dal.Delete(It.IsAny<int>())).Returns(false);

            PersonLogic logic = new PersonLogic(memoryDal.Object, validator.Object);

            var result = logic.Delete(1);

            Assert.IsTrue(!result);
        }
        #endregion
    }
}
