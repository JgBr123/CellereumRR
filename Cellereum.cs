using System;
using System.IO;
using System.Linq;
using System.Diagnostics;
using System.Collections.Generic;
using Telegram.Bot;
using System.Text;
using System.Threading;

namespace Cellereum_RR
{
    class Cellereum
    {
        public static string RootPath = @"D:\Cellereum";
        public static void Register(string username, int id)
        {
            using (var write = new StreamWriter($@"{RootPath}\Accounts\IDs\{id}.txt"))
            {
               write.WriteLine(0);
               write.WriteLine("English");
               write.WriteLine(username);
            }
        
            using (var write = new StreamWriter($@"{RootPath}\Accounts\Usernames\@{username}.txt"))
            { 
               write.WriteLine(id);
            }
        }
        public static bool IsRegistered(int id)
        {
            return File.Exists($@"{RootPath}\Accounts\IDs\{id}.txt");
        }
        public static void Link(string username, int id)
        {
            string oldUsername = GetUsername(id);

            //WriteSafetyBlock
            using (var write = new StreamWriter($@"{RootPath}\Accounts\SafetyBlock\@{oldUsername}.txt"))
            {
                write.WriteLine(id);
            }

            double balance;
            string language;

            //ReadProfile
            using (var read = new StreamReader($@"{RootPath}\Accounts\IDs\{id}.txt"))
            {
                balance = Convert.ToDouble(read.ReadLine());
                language = read.ReadLine();
            }

            //WriteProfile
            using (var write = new StreamWriter($@"{RootPath}\Accounts\IDs\{id}.txt"))
            {
                write.WriteLine(balance);
                write.WriteLine(language);
                write.WriteLine(username);
            }

            File.Delete($@"{RootPath}\Accounts\Usernames\@{username}.txt");
            File.Move($@"{RootPath}\Accounts\Usernames\@{oldUsername}.txt", $@"{RootPath}\Accounts\Usernames\@{username}.txt");
        }
        public static void SetLanguage(int id, string language)
        {
            double balance;
            string username;

            using (var read = new StreamReader($@"{RootPath}\Accounts\IDs\{id}.txt"))
            {
                balance = Convert.ToDouble(read.ReadLine());
                read.ReadLine();
                username = read.ReadLine();
            }

            using (var write = new StreamWriter($@"{RootPath}\Accounts\IDs\{id}.txt"))
            {
                write.WriteLine(balance);
                write.WriteLine(language);
                write.WriteLine(username);
            }
        }
        public static string GetLanguage(int id)
        {
            using (var read = new StreamReader($@"{RootPath}\Accounts\IDs\{id}.txt"))
            {
                read.ReadLine();
                return read.ReadLine();
            }
        }
        public static void SetBalance(int id, double newBalance)
        {
            string language;
            string username;

            //ReadProfile
            using (var read = new StreamReader($@"{RootPath}\Accounts\IDs\{id}.txt"))
            {
                read.ReadLine();
                language = read.ReadLine();
                username = read.ReadLine();
            }

            //WriteProfile
            using (var write = new StreamWriter($@"{RootPath}\Accounts\IDs\{id}.txt"))
            {
                write.WriteLine(Math.Round(newBalance,2));
                write.WriteLine(language);
                write.WriteLine(username);
            }
        }
        public static double GetBalance(int id)
        {
            using (var read = new StreamReader($@"{RootPath}\Accounts\IDs\{id}.txt"))
            {
                return Math.Round(Convert.ToDouble(read.ReadLine()),2);
            }
        }
        public static string GetUsername(int id)
        {
            try
            {
                using (var read = new StreamReader($@"{RootPath}\Accounts\IDs\{id}.txt"))
                {
                    read.ReadLine();
                    read.ReadLine();
                    return read.ReadLine();
                }
            }
            catch { return ""; }
        }
        public static int GetID(string username)
        {
            try
            {
                using (var read = new StreamReader($@"{RootPath}\Accounts\Usernames\@{username}.txt"))
                {
                    return Convert.ToInt32(read.ReadLine());
                }
            }
            catch { return 0; }
        }
        public static bool WasCreatedOneDayAgo(string path, int hours=24)
        {
            var threshold = DateTime.Now.AddHours(-hours);
            return System.IO.File.GetCreationTime(path) <= threshold;
        }
        public static double RemoveDecimals(double number, int maxDecimals)
        {
            string numberString = Convert.ToString(number);

            if (numberString.Contains(","))
            {
                string[] sides = numberString.Split(',');
                string decimals = Convert.ToString(sides[1]);

                while (decimals.Length > maxDecimals) decimals = decimals.Substring(0, decimals.Length - 1);
                return Convert.ToDouble(sides[0] + "," + decimals);
            }
            else return number;
        }
    }
}
