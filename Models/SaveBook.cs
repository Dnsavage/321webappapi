using System.Data.SQLite;
using API.Models.Interfaces;

namespace API.Models
{
    public class SaveBook : IInsertBook
    {
        public void InsertBook(Book value)
        {
            string cs = @"URI=file:C:\Users\Plati\Desktop\Spring2022\MIS321\repos\BookDatabase\book.db";
            using var con = new SQLiteConnection(cs);
            con.Open();

            string stm = @"INSERT INTO books(title, author) VALUES(@title, @author)";

            using var cmd = new SQLiteCommand(stm, con);
            cmd.Parameters.AddWithValue("@title",value.Title);
            cmd.Parameters.AddWithValue("@author",value.Author);
            cmd.Prepare();
            cmd.ExecuteNonQuery();
        }
    }
}