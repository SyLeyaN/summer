using Epam.Library.Entities;
using System.Collections.Generic;

namespace Epam.Library.MemoryDal
{
    static class Memory
    {
        public static List<Person> Persons { get; set; }
        public static List<LibraryObject> LibraryObjects { get; set; }
        static Memory()
        {
            LibraryObjects = new List<LibraryObject>();
            Persons = new List<Person>();
        }
        private static int _idCounter = 0;
        public static int NextId => ++_idCounter;
    }
}
