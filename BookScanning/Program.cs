using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BookScanning
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"C:\Users\User\Downloads";
            Console.WriteLine("Input char a-f: ");
            string str = Console.ReadLine();

            //for (int k = 0; k < 6; k++)
            //{
            string PathToFile = "", OutputPath = "";
            char k = str[0];

            switch (k)
            {
                case 'a':
                    {
                        PathToFile = path + "\\a_example.txt";
                        OutputPath = path + "\\a_out_";
                        break;
                    }
                case 'b':
                    {
                        PathToFile = path + "\\b_read_on.txt";
                        OutputPath = path + "\\b_out_";
                        break;
                    }
                case 'c':
                    {
                        PathToFile = path + "\\c_incunabula.txt";
                        OutputPath = path + "\\c_out_";
                        break;
                    }
                case 'd':
                    {
                        PathToFile = path + "\\d_tough_choices.txt";
                        OutputPath = path + "\\d_out_";
                        break;
                    }
                case 'e':
                    {
                        PathToFile = path + "\\e_so_many_books.txt";
                        OutputPath = path + "\\e_out_";
                        break;
                    }
                case 'f':
                    {
                        PathToFile = path + "\\f_libraries_of_the_world.txt";
                        OutputPath = path + "\\f_out_";
                        break;
                    }
                default:
                    break;
            }


            int LibNumber;
            int booksNumber;
            int ScanDays;
            List<int> Score = new List<int>();
            List<Library> libs = new List<Library>();
            int count = 0;
            var Result = new List<LibResult>();
            int score = 0;
            var uniqBooks = new List<int>();

            using (StreamReader sr = new StreamReader(PathToFile))
            {
                var line = sr.ReadLine();
                var Parts = line.Split(' ');
                booksNumber = int.Parse(Parts[0]);
                LibNumber = int.Parse(Parts[1]);
                ScanDays = int.Parse(Parts[2]);

                line = sr.ReadLine();
                Parts = line.Split(' ');
                for (int i = 0; i < booksNumber; i++)
                {
                    Score.Add(int.Parse(Parts[i]));
                }

                for (int i = 0; i < LibNumber; i++)
                {
                    line = sr.ReadLine();
                    Parts = line.Split(' ');

                    var lib = new Library();
                    lib.id = count;
                    lib.BooksCount = int.Parse(Parts[0]);
                    lib.SignUp = int.Parse(Parts[1]);
                    lib.ShipCount = int.Parse(Parts[2]);
                    lib.Books = new List<Book>();

                    line = sr.ReadLine();
                    Parts = line.Split(' ');

                    for (int j = 0; j < lib.BooksCount; j++)
                    {
                        var newBook = new Book
                        {
                            id = int.Parse(Parts[j]),
                            Score = Score[int.Parse(Parts[j])]
                        };
                        lib.Books.Add(newBook);
                    }

                    libs.Add(lib);
                    count++;
                }
            }


            var libsAcs = libs.OrderBy(x => x.SignUp).ThenByDescending(x => x.Books.Sum(s => s.Score));
            //var libsAcs = libs.OrderByDescending(x => x.Books.Count()); // good on D

            foreach (var item in libsAcs)
            {
                var libRes = new LibResult();
                libRes.Books = new List<int>();
                libRes.id = item.id;
                var c = ScanDays - item.SignUp;
                var ship = item.ShipCount;
                var maxBookCount = c * ship;
                //var BCount = c / item.ShipCount;

                var sortedBooks = item.Books.OrderByDescending(x => x.Score).ToArray();

                int i = 0;
                while (maxBookCount > 0)
                {
                    if (uniqBooks.Any(b => b == sortedBooks[i].id) == false)
                    {
                        uniqBooks.Add(sortedBooks[i].id);
                        libRes.Books.Add(sortedBooks[i].id);
                        score = score + sortedBooks[i].Score;
                    }
                    if (i < sortedBooks.Count() - 1)
                        i++;
                    else
                        break;


                }


                Result.Add(libRes);
            }
          //  var libsArr = libs.ToArray();

            var newResult = new List<LibResult>(Result);
            Result = new List<LibResult>();

            foreach (var item in newResult)
            {
                if (item.Books.Count() > 0)
                {
                    Result.Add(item);
                    //var bookArr = item.Books.ToArray();
                    //for (int j = 0; j < item.Books.Count(); j++)
                    //{
                    //    score = score + libsArr[item.id].Books.Where(x => x.id == bookArr[j]).FirstOrDefault().Score;
                    //}
                }
            }

            using (StreamWriter sw = new StreamWriter(OutputPath + score.ToString() + ".txt"))
            {
                sw.WriteLine(Result.Count());
                foreach (var item in Result)
                {
                    sw.Write(item.id);
                    sw.Write(' ');
                    sw.Write(item.Books.Count());
                    sw.WriteLine();
                    foreach (var b in item.Books)
                    {
                        sw.Write(b);
                        sw.Write(' ');
                    }
                    sw.WriteLine();
                }
                sw.WriteLine();
            }
            //}
            Console.WriteLine("Done!");

            Console.ReadKey();

        }
    }
}