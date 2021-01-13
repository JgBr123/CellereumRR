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
    class Transactions
    {
        public static void Send(int id, double amount, string userReceiver)
        {
            int idReceiver = Cellereum.GetID(userReceiver);

            Thread sender = new Thread(() => Cellereum.SetBalance(id, Cellereum.GetBalance(id) - amount));
            Thread receiver = new Thread(() => Cellereum.SetBalance(idReceiver, Cellereum.GetBalance(idReceiver) + amount));

            sender.Start();
            receiver.Start();

            var datetime = DateTime.Now;
            string milisecond = datetime.Millisecond.ToString();

            while (milisecond.Length != 3)
            {
                milisecond = "0" + milisecond;
            }

            string fileName = $"{datetime.Day}.{datetime.Month}.{datetime.Year}-{datetime.Hour}.{datetime.Minute}.{milisecond}";

            using (var write = new StreamWriter(@$"{Cellereum.RootPath}\Register\{fileName}.data"))
            {
                write.WriteLine($"Sender: {id}");
                write.WriteLine($"Amount: {amount}");
                write.WriteLine($"Receiver: {idReceiver}");
            }

            Console.WriteLine($"New Transaction: Sender: {id} / Amount: {amount} / Receiver: {idReceiver}");

            while (sender.IsAlive || receiver.IsAlive) { }
        }
    }
}
