using System;
using System.Collections.Generic;

namespace Epam.Library.Entities.Exceptions
{
    public class ObjectNotValidateException : Exception
    {
        public IList<string> BackMessageValidate { get; set; }
        public ObjectNotValidateException(IList<string> backMessage)
            : base()
        {
            BackMessageValidate = backMessage;
        }
    }
}
