using System.Data.SQLite;
using API.Models.Interfaces;
using MySql.Data.MySqlClient;

namespace API.Models
{
    public class SaveBook : IInsertBook
    {
        public void InsertBook(Book value)
        {
            //string cs = @"URI=file:C:\Users\Plati\Desktop\Spring2022\MIS321\repos\BookDatabase\book.db";
            //using var con = new SQLiteConnection(cs);
            //con.Open();

            //string stm = @"INSERT INTO books(title, author) VALUES(@title, @author)";

            DBConnect db = new DBConnect();
            bool isOpen = db.OpenConnection();

            if(isOpen){
                MySqlConnection conn = db.GetConn();
                MySqlCommand cmd = new MySqlCommand();

                cmd.Connection = conn;

                cmd.CommandText = @"INSERT INTO books(title, author) VALUES(@title, @author)";
                cmd.Parameters.AddWithValue("@title",value.Title);
                cmd.Parameters.AddWithValue("@author",value.Author);
                cmd.Prepare();
                cmd.ExecuteNonQuery();

                db.CloseConnection();
            }
        }
    }
}