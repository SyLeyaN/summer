namespace Epam.Library.Entities
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public override string ToString() => $"\nID {Id} \nИмя {Name} \nФамилия {Surname}";
    }
}
