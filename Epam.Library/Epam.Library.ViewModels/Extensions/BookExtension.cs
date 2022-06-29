using Epam.Library.Entities;
using System.Linq;
using System.Text;

namespace Epam.Library.Core.Extensions
{
    public static class BookExtension
    {
        public static string DisplayBookInformationInTable(this Book book)
        {
            StringBuilder result = new StringBuilder();
            if (book.Authors.Any())
            {
                foreach (Person author in book.Authors)
                {
                    result.Append($" {author.Name[0]}.{author.Surname}, ");
                }
                result.Remove(result.Length - 2, 2);
                result.Append(" - ");
            }
            result.Append($"{book.Title}/({book.PublishingYear})");
            return result.ToString();
        }
    }
}