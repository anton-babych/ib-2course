using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Security.Principal;
using System.Text;
using System.Threading;
using lab1;
using lab4;

namespace lab11_12
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var user1 = Protector.Register("user1", "password1");
            var user2 = Protector.Register("user2", "password2");
            var user3 = Protector.Register("user3", "password3","Admins");
            var user4 = Protector.Register("user4", "password4");
            
            Protector.LogIn("user21415", "password3");
            Protector.LogIn("user2", "gqqegqg");
            
            Protector.LogIn("user3", "password3");
            Protector.DoAdminPart();
            Protector.LogIn("user4", "password4");
            Protector.DoAdminPart();
        }
    }

    public class User
    {
        public readonly string Login;
        public readonly string PasswordHash;
        public readonly string Salt;
        public readonly string[] Roles;

        public User(string login, string passwordHash, string salt, string[] roles)
        {
            Login = login;
            PasswordHash = passwordHash;
            Salt = salt;
            Roles = roles;
        }
    }

    public class Protector
    {
        private static Dictionary<string, User> _users = new Dictionary<string, 
            User>();

        public static User Register(string userName, string password, params string[]
            roles)
        {
            if (TryGetUserByName(userName, out User oldUser)) return oldUser;

            string salt = GenerateSalt();
            string passwordHash = ComputeHash(password, salt);

            User registeredUser = new User(userName, passwordHash, salt, roles);
            _users.Add(userName, registeredUser);
            
            return registeredUser;
        }

        private static bool CheckPassword(string userName, string password)
        {
            if (!TryGetUserByName(userName, out User user))
            {
                Console.WriteLine("no such user with name " + userName);
                return false;
            }

            var newHash = ComputeHash(password, user.Salt);
            return CompareHashes(newHash, user.PasswordHash);
        }

        private static bool CompareHashes(string newHash, string userPasswordHash) => 
            string.Equals(newHash, userPasswordHash);

        private static string ComputeHash(string password, string salt)
        {
            return Convert.ToBase64String(PBKDF2.HashPassword(
                Encoding.UTF8.GetBytes(password), 
                Encoding.UTF8.GetBytes(salt), 2));
        }

        private static bool TryGetUserByName(string userName, out User user) => 
            _users.TryGetValue(userName, out user);

        private static string GenerateSalt() => 
            Convert.ToBase64String(RandomNumbersGenerator.GenerateRandomBytes(32));

        public static void LogIn(string userName, string password)
        {
            if (!CheckPassword(userName, password))
            {
                Console.WriteLine("something gone wrong with " + userName);
                return;
            }
            
            var identity = new GenericIdentity(userName, "OIBAuth");
            var principal = new GenericPrincipal(identity, _users[userName].Roles);
            Thread.CurrentPrincipal = principal;
            Console.WriteLine("you ve logged as " + userName);
        }
        
        public static void DoAdminPart()
        {
            try
            {
                OnlyForAdminsFeature();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.GetType()}: {ex.Message}");
            }

        }
        
        public static void OnlyForAdminsFeature()
        {
            if (Thread.CurrentPrincipal == null)
                throw new SecurityException("Thread.CurrentPrincipal cannot be null.");
            if (!Thread.CurrentPrincipal.IsInRole("Admins"))
                throw new SecurityException("User must be a member of Admins to access this feature.");
            
            Console.WriteLine("You have access to this secure feature.");
        }
    }
}