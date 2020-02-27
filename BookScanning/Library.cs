using System;
using System.Collections.Generic;
using System.Text;

namespace BookScanning
{
    public class Library
    {
        public int id { get; set; }
        public int BooksCount { get; set; }
        public int SignUp { get; set; }
        public int ShipCount { get; set; }
        public List<Book> Books { get; set; }
    }

    public class Book
    {
        public int id { get; set; }
        public int Score { get; set; }
    }

    public class LibResult
    {
        public int id { get; set; }
        public List<int> Books { get; set; }
    }
}
