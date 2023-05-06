namespace Library2.Models
{
    public class Borrowed_Book
    {
        public int Id { get; set; }
       
        public int client_id { get; set; }
        public string User_id { get; set; }

        public int Book_id { get; set; }
        public string Book_Title { get; set; }
        public string Start_Date { get; set; } = DateTime.Now.Date.ToString();
        public string End_Date { get; set; }
    }
}
