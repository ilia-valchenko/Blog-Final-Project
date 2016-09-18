using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ORM;

namespace ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new BlogDbContext())
            {
                Console.Write("Data from blog:\n");

                foreach(var user in db.Users)
                {
                    Console.WriteLine($"Id: {user.UserId}, Nickname: {user.Nickname}");
                }

                Console.ReadKey(true);
            }
        }
    }
}
