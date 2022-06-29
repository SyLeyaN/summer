using AutoMapper;
using Epam.Library.DalContracts;
using Epam.Library.DatabaseDal;
using Epam.Library.Entities;
using Epam.Library.Logic;
using Epam.Library.LogicContracts;
using Epam.Library.Mappers;
using Epam.Library.Validator;
using Epam.Library.ValidatorContracts;

namespace Epam.Library.Dependencies
{
    public static class DependencyResolver
    {
        //statics delete
        #region library object
        public static ILibraryObjectDal LibraryObjectDal => new LibraryObjectDatabaseDal();

        public static ILibraryObjectLogic LibraryObjectLogic => new LibraryObjectLogic(LibraryObjectDal);

        #endregion

        #region book
        private static IBookDal _iBookDal;
        public static IBookDal BookDal => _iBookDal ?? (_iBookDal = new BookDatabaseDal());

        private static IBookLogic _iBookLogic;
        private static IValidator<Book> _iBookValidator = new BookValidator();
        public static IBookLogic BookLogic => _iBookLogic ?? (_iBookLogic = new BookLogic(BookDal, _iBookValidator));


        #endregion   
              
             

        #region person

        private static IPersonDal _iPersonDal;
        public static IPersonDal PersonDal => _iPersonDal ?? (_iPersonDal = new PersonDatabaseDal());

        private static IPersonLogic _iPersonLogic;

        private static IValidator<Person> _iPersonValidator = new PersonValidator();
        public static IPersonLogic PersonLogic => _iPersonLogic ?? (_iPersonLogic = new PersonLogic(PersonDal, _iPersonValidator));

        #endregion

        public static IMapper Mapper => new Mapper(new MapperConfiguration(cfg => { cfg.AddProfile(new MapperConfig()); }));
    }
}
