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
            WaitFileAvailable($@"{RootPath}\Accounts\IDs\{id}.txt");
            using (var write = new StreamWriter($@"{RootPath}\Accounts\IDs\{id}.txt"))
            {
                write.WriteLine(0);
                write.WriteLine("English");
                write.WriteLine(username);
                write.WriteLine("null");
            }

            WaitFileAvailable($@"{RootPath}\Accounts\Usernames\@{username}.txt");
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
            WaitFileAvailable($@"{RootPath}\Accounts\SafetyBlock\@{oldUsername}.txt");
            using (var write = new StreamWriter($@"{RootPath}\Accounts\SafetyBlock\@{oldUsername}.txt"))
            {
                write.WriteLine(id);
            }

            double balance;
            string language;
            string vip;

            //ReadProfile
            using (var read = new StreamReader($@"{RootPath}\Accounts\IDs\{id}.txt"))
            {
                balance = Convert.ToDouble(read.ReadLine());
                language = read.ReadLine();
                read.ReadLine();
                vip = read.ReadLine();
            }

            //WriteProfile
            WaitFileAvailable($@"{RootPath}\Accounts\IDs\{id}.txt");
            using (var write = new StreamWriter($@"{RootPath}\Accounts\IDs\{id}.txt"))
            {
                write.WriteLine(balance);
                write.WriteLine(language);
                write.WriteLine(username);
                write.WriteLine(vip);
            }

            File.Delete($@"{RootPath}\Accounts\Usernames\@{username}.txt");
            File.Move($@"{RootPath}\Accounts\Usernames\@{oldUsername}.txt", $@"{RootPath}\Accounts\Usernames\@{username}.txt");
        }
        public static void SetLanguage(int id, string language)
        {
            double balance;
            string username;
            string vip;

            //ReadProfile
            using (var read = new StreamReader($@"{RootPath}\Accounts\IDs\{id}.txt"))
            {
                balance = Convert.ToDouble(read.ReadLine());
                read.ReadLine();
                username = read.ReadLine();
                vip = read.ReadLine();
            }

            //WriteProfile
            WaitFileAvailable($@"{RootPath}\Accounts\IDs\{id}.txt");
            using (var write = new StreamWriter($@"{RootPath}\Accounts\IDs\{id}.txt"))
            {
                write.WriteLine(balance);
                write.WriteLine(language);
                write.WriteLine(username);
                write.WriteLine(vip);
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
            string vip;

            //ReadProfile
            using (var read = new StreamReader($@"{RootPath}\Accounts\IDs\{id}.txt"))
            {
                read.ReadLine();
                language = read.ReadLine();
                username = read.ReadLine();
                vip = read.ReadLine();
            }

            //WriteProfile
            WaitFileAvailable($@"{RootPath}\Accounts\IDs\{id}.txt");
            using (var write = new StreamWriter($@"{RootPath}\Accounts\IDs\{id}.txt"))
            {
                write.WriteLine(Math.Round(newBalance,2));
                write.WriteLine(language);
                write.WriteLine(username);
                write.WriteLine(vip);
            }
        }
        public static double GetBalance(int id)
        {
            using (var read = new StreamReader($@"{RootPath}\Accounts\IDs\{id}.txt"))
            {
                return Math.Round(Convert.ToDouble(read.ReadLine()), 2);
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
        public static bool IsVIP(int id)
        {
            try
            {
                using (var read = new StreamReader($@"{RootPath}\Accounts\IDs\{id}.txt"))
                {
                    read.ReadLine();
                    read.ReadLine();
                    read.ReadLine();

                    string vip = read.ReadLine();

                    if (vip != "null")
                    {
                        DateTime vipTime = Convert.ToDateTime(vip);

                        return vipTime > DateTime.Now;
                    }
                    else return false;
                }
            }
            catch { return false; }
        }
        public static void SetVIP(int id, int days)
        {
            string balance;
            string language;
            string username;

            DateTime vipTime = DateTime.Now.AddDays(days);

            //ReadProfile
            using (var read = new StreamReader($@"{RootPath}\Accounts\IDs\{id}.txt"))
            {
                balance = read.ReadLine();
                language = read.ReadLine();
                username = read.ReadLine();
            }

            //WriteProfile
            WaitFileAvailable($@"{RootPath}\Accounts\IDs\{id}.txt");
            using (var write = new StreamWriter($@"{RootPath}\Accounts\IDs\{id}.txt"))
            {
                write.WriteLine(balance);
                write.WriteLine(language);
                write.WriteLine(username);
                write.WriteLine(vipTime);
            }
        }
        public static bool WasCreatedOneDayAgo(string path, int hours=24)
        {
            var threshold = DateTime.Now.AddHours(-hours);
            return System.IO.File.GetCreationTime(path) >= threshold;
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
        public static bool isBlocked(string username, int id)
        {
            if (File.Exists(@$"{RootPath}\Accounts\SafetyBlock\@{username}.txt"))
            {
                if (WasCreatedOneDayAgo(@$"{RootPath}\Accounts\SafetyBlock\@{username}.txt"))
                {
                    int allowedId;

                    using (var read = new StreamReader(@$"{RootPath}\Accounts\SafetyBlock\@{username}.txt"))
                    {
                        allowedId = Convert.ToInt32(read.ReadLine());
                    }

                    if (allowedId == id) return false;
                    else return true;
                }
                else
                {
                    File.Delete(@$"{RootPath}\Accounts\SafetyBlock\@{username}.txt");
                    return false;
                }
            }
            else return false;
        }
        public static void WaitFileAvailable(string path)
        {
            int tries = 0;

            var file = new FileInfo(path);

            while (true)
            {
                try
                {
                    using (FileStream stream = file.Open(FileMode.Open, FileAccess.Read, FileShare.None))
                    {
                        stream.Close();
                    }
                    break;
                }
                catch (IOException) { tries++; Thread.Sleep(10); }
                if (tries > 500) break;
            }
        }
    }
}
