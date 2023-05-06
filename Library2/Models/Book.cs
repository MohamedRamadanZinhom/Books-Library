namespace Library2.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public int Currentcount { get; set; }
        public int Maxcount { get; set; }
    }
}
