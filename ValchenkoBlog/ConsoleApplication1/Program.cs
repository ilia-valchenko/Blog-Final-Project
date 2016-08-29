using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ORM.Models;
using ORM;
using Ninject;
using BLL.Services;
using BLL.Interfacies.Entities;
using BLL.Interfacies.Services;
using System.Diagnostics;

namespace ConsoleApplication1
{
    class Program
    {
        public static IKernel appKernel;

        static void Main(string[] args)
        {
            /*using (var context = new BlogDbContext())
            {
                Console.WriteLine("Users from Database:");

                foreach (var user in context.Users)
                {
                    Console.WriteLine($"Id: {user.UserId}, Nickname: {user.Nickname}, Roles: ");

                    foreach (var role in user.Roles)
                        Console.WriteLine(role.Name + " ");
                }                 
            }*/

            // User interface

            appKernel = new StandardKernel(new CustomNinjectModule());

            var userService = appKernel.Get<IUserService>();

            var bllUser = new UserEntity
            {
                Id = 101,
                Nickname = "Kirill",
                Password = "Qwerty_123"
            };

            userService.Create(bllUser);

            using (var context = new BlogDbContext())
            {
                Console.WriteLine("Users from Database:");

                foreach (var user in context.Users)
                {
                    Console.WriteLine($"Id: {user.UserId}, Nickname: {user.Nickname}, Roles: ");

                    foreach (var role in user.Roles)
                        Console.WriteLine(role.Name + " ");
                }
            }

            Console.WriteLine("\n\nTap to continue...");
            Console.ReadKey(true);
        }
    }
}
