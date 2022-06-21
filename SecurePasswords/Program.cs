using SecurePasswords.Models;
using SecurePasswords.Service;
using System;
using System.Security.Authentication;

namespace SecurePasswords
{
    internal class Program
    {
        static IUserService userService = new UserService();

        static void Main()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("* Secure Passwords");
                Console.WriteLine("Login press L or Create account press C");
                Console.Write("Choice: ");
                ConsoleKeyInfo consoleKeyInfo = Console.ReadKey();
                if (consoleKeyInfo.Key == ConsoleKey.L)
                {
                    LoginView();
                }
                else if (consoleKeyInfo.Key == ConsoleKey.C)
                {
                    CreateAccountView();
                }
            }
        }

        static void LoginView()
        {
            Console.Clear();
            Console.WriteLine("* Login");

            Console.Write("Username: ");
            string username = Console.ReadLine();
            Console.Write("Password: ");
            string password = Console.ReadLine();

            try
            {
                User user = userService.GetUserByLogin(username, password);
                Console.WriteLine("You are logged in as " + user.Username);
            }
            catch (AuthenticationException ex)
            {
                Console.WriteLine("Failed to login: " + ex.Message);
            }
            Console.ReadKey();
        }

        static void CreateAccountView()
        {
            Console.Clear();
            Console.WriteLine("* Create user");

            User user = new User();
            Console.Write("Username: ");
            user.Username = Console.ReadLine();
            Console.Write("Password: ");
            user.Password = Console.ReadLine();

            if (!userService.CreateUser(user))
            {
                Console.WriteLine("Failed to create user...");
            }
            else
            {
                Console.WriteLine("New user created");
            }
            Console.ReadKey();
        }
    }
}
