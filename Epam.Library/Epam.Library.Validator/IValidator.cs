using System.Collections.Generic;

namespace Epam.Library.ValidatorContracts
{
    public interface IValidator<T>
    {
        bool IsValid(T validationObject, out IList<string> validationErrorMessages);
    }
}
