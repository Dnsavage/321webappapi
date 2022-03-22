using System.Collections.Generic;
using System.Data.SQLite;
using API.Models.Interfaces;
using MySql.Data.MySqlClient;

namespace API.Models
{
    public class ReadBookData : IGetAllBooks, IGetBook
    {
        public List<Book> GetAllBooks()
        {
            DBConnect db = new DBConnect();
            bool isOpen = db.OpenConnection();//check if the connection is open

            if(isOpen){
                MySqlConnection conn = db.GetConn();
                string stm = "SELECT * FROM books";
                MySqlCommand cmd = new MySqlCommand(stm, conn);

                List<Book> allBooks = new List<Book>();

                using (var rdr = cmd.ExecuteReader())
                {
                    while(rdr.Read()){
                        allBooks.Add(new Book(){ID=rdr.GetInt32(0), Title=rdr.GetString(1), Author=rdr.GetString(2)});
                    }
                }

                db.CloseConnection();
                return allBooks;
            } else{
                return new List<Book>();//If not open, return an empty list
            }

            /*
            List<Book> allBooks = new List<Book>();

            string cs = @"URI=file:C:\Users\Plati\Desktop\Spring2022\MIS321\repos\BookDatabase\book.db";
            using var con = new SQLiteConnection(cs);
            con.Open();

            string stm = "SELECT * FROM books";
            using var cmd = new SQLiteCommand(stm, con);

            using SQLiteDataReader rdr = cmd.ExecuteReader();

            while(rdr.Read())//while the reader is returning data
            {
                //Create a new book object then add it to the list
                Book temp = new Book(){ID=rdr.GetInt32(0), Title=rdr.GetString(1), Author=rdr.GetString(2)};
                allBooks.Add(temp);
            }

            return allBooks;
            */
        }

        public Book GetBook(int id)
        {
            string cs = @"URI=file:C:\Users\Plati\Desktop\Spring2022\MIS321\repos\BookDatabase\book.db";
            using var con = new SQLiteConnection(cs);
            con.Open();

            string stm = "SELECT * FROM books WHERE id = @id";
            using var cmd = new SQLiteCommand(stm, con);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();
            using SQLiteDataReader rdr = cmd.ExecuteReader();

            rdr.Read();
            return new Book(){ID = rdr.GetInt32(0), Title = rdr.GetString(1), Author = rdr.GetString(2)};
        }
    }
}