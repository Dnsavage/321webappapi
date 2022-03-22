namespace API.Models
{
    public class Book
    {
        public int ID {get; set;}
        public string Title {get; set;}
        public string Author {get; set;}
        
        public override string ToString()
        {
            return ID + " " + Title + " " + Author;
        }
    }
}