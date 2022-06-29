using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Epam.Library.Entities
{
    public class Book : PrintedProduct
    {
        public List<Person> Authors { get; set; } = new List<Person>();
        public string ISBN { get; set; }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder($"\nID {Id} \nНазвание {Title} \nЧисло страниц {NumberOfPages} " +
                $"\nГод публикации {PublishingYear} \nЗаметка {Note} \nГород издания {PublishingCity} " +
                $"\nИздательство {PublishingHouse} \nISBN {ISBN}");
            if (Authors.FirstOrDefault() != null)
            {
                result.Append("\nАвтор(ы):");
                foreach (Person author in Authors)
                {
                    result.Append($"\n{author}");
                }
            }
            else
            {
                result.Append("\nАвтора(ов) нет");
            }
            return result.ToString();
        }
    }
}
