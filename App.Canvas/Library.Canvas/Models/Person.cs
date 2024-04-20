namespace Library.Canvas.Models
{
    public class Person
    {
        public int Id { get; private set; }
        private static int lastId = 0;
        public string Name { get; set; }



        public Person()
        {
            Name = string.Empty;
            Id = ++lastId;
        }

        public override string ToString()
        {
            return $"[{Id}] {Name}";
        }
    }


}
