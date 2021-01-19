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
    class Program
    {
        public static readonly TelegramBotClient Bot = new TelegramBotClient("1235309007:AAEB5lhIyfACtp4wy_i6j3LU1igqvHvBnio");
        public static bool shutdown;
        public static List<int> canVip = new List<int>();

        static void Main()
        {
            Bot.OnMessage += Bot_OnMessage;
            Bot.OnMessageEdited += Bot_OnMessage;

            Console.WriteLine("Bot iniciado.");
            Bot.StartReceiving();
            Bot.SendTextMessageAsync(651939005, "Bot operando.");

            while (true) { }
        }
        private static void Bot_OnMessage(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {
            try
            {
                if (e.Message.Type == Telegram.Bot.Types.Enums.MessageType.Text)
                {
                    var args = e.Message.Text.Split(" ").ToList();

                    try { args[1].ToString(); } catch { args.Add(""); }
                    try { args[2].ToString(); } catch { args.Add(""); }
                    try { args[3].ToString(); } catch { args.Add(""); }

                    //START:
                    if (e.Message.Text == "/start" || e.Message.Text == "/start@CellereumBot")
                    {
                        if (Cellereum.IsRegistered(e.Message.From.Id))
                        {
                            if (Cellereum.GetLanguage(e.Message.From.Id) == "English") Bot.SendTextMessageAsync(e.Message.Chat.Id, "Thanks for using our bot. If you are new here, check /help, or change your language at /language.\n\nVersion 1.1 RR");
                            else Bot.SendTextMessageAsync(e.Message.Chat.Id, "Obrigado por utilizar nosso bot. Se você é novo aqui, cheque /help, ou mude sua linguagem em /language.\n\nVersão 1.1 RR");
                        }
                        else Bot.SendTextMessageAsync(e.Message.Chat.Id, "Thanks for using our bot. If you are new here, check /help, or change your language at /language.\n\nVersion 1.1 RR");
                    }
                    //VERSION:
                    else if (e.Message.Text == "/version" || e.Message.Text == "/version@CellereumBot")
                    {
                        if (Cellereum.IsRegistered(e.Message.From.Id))
                        {
                            if (Cellereum.GetLanguage(e.Message.From.Id) == "English") Bot.SendTextMessageAsync(e.Message.Chat.Id, "Version 1.1 Remastered Remake");
                            else Bot.SendTextMessageAsync(e.Message.Chat.Id, "Versão 1.1 Remastered Remake");
                        }
                        else Bot.SendTextMessageAsync(e.Message.Chat.Id, "Version 1.1 Remastered Remake");
                    }
                    //HELP:
                    else if (e.Message.Text == "/help" || e.Message.Text == "/help@CellereumBot")
                    {
                        if (Cellereum.IsRegistered(e.Message.From.Id))
                        {
                            if (Cellereum.GetLanguage(e.Message.From.Id) == "English") Bot.SendTextMessageAsync(e.Message.Chat.Id, "/help : Show this list\n/version : Show the latest bot version\n/language : Change the bot language\n/register : Creates an account\n/account : Show info about your account\n/balance : Show your Cellereum balance\n/send : Send Cellereums to an user\n/rain : Send Cellereums to all registered users\n/supply : Show the Cellereum Supply\n/transactions : Show how many transactions Cellereum has\n/vip : Show the VIP subscriptions");
                            else Bot.SendTextMessageAsync(e.Message.Chat.Id, "/help : Mostra esta lista\n/version : Mostra a versão atual do bot\n/language : Mude a linguagem do bot\n/register : Crie uma conta\n/account : Mostra informações da sua conta\n/balance : Mostra seu saldo em Cellereums\n/send : Envie Cellereums para um usuário\n/rain : Envie Cellereums para todos os usuários registrados\n/supply : Mostra o supply da Cellereum\n/transactions : Mostra quantas transações a Cellereum tem\n/vip : Mostra os planos VIP");
                        }
                        else Bot.SendTextMessageAsync(e.Message.Chat.Id, "/help : Show this list\n/version : Show the latest bot version\n/language : Change the bot language\n/register : Creates an account\n/account : Show info about your account\n/balance : Show your Cellereum balance\n/send : Send Cellereums to an user\n/rain : Send Cellereums to all registered users\n/supply : Show the Cellereum Supply\n/transactions : Show how many transactions Cellereum has\n/vip : Show the VIP subscriptions");
                    }
                    //LANGUAGE:
                    else if (args[0] == "/language" || args[0] == "/language@CellereumBot")
                    {
                        if (!string.IsNullOrEmpty(args[1]))
                        {
                            if (Cellereum.IsRegistered(e.Message.From.Id))
                            {
                                if (args[1].ToLower() == "english" || args[1].ToLower() == "ingles" || args[1].ToLower() == "inglês") { Cellereum.SetLanguage(e.Message.From.Id, "English"); Bot.SendTextMessageAsync(e.Message.Chat.Id, "Language was setted to english."); }
                                else if (args[1].ToLower() == "portuguese" || args[1].ToLower() == "portugues" || args[1].ToLower() == "português") { Cellereum.SetLanguage(e.Message.From.Id, "Portuguese"); Bot.SendTextMessageAsync(e.Message.Chat.Id, "Linguagem selecionada para português."); }
                                else
                                {
                                    if (Cellereum.GetLanguage(e.Message.From.Id) == "English") Bot.SendTextMessageAsync(e.Message.Chat.Id, "Incorrect parameters. Usage: /language [Portugues/English].");
                                    else Bot.SendTextMessageAsync(e.Message.Chat.Id, "Parâmetros incorretos. Uso: /language [Portuguese/English].");
                                }
                            }
                            else Bot.SendTextMessageAsync(e.Message.Chat.Id, "You need to be registered to use this command.");
                        }
                        else
                        {
                            if (Cellereum.IsRegistered(e.Message.From.Id))
                            {
                                if (Cellereum.GetLanguage(e.Message.From.Id) == "English") Bot.SendTextMessageAsync(e.Message.Chat.Id, "Incorrect parameters. Usage: /language [Portugues/English].");
                                else Bot.SendTextMessageAsync(e.Message.Chat.Id, "Parâmetros incorretos. Uso: /language [Portuguese/English].");
                            }
                            else Bot.SendTextMessageAsync(e.Message.Chat.Id, "Incorrect parameters. Usage: /language [Portugues/English].");
                        }
                    }
                    //REGISTER:
                    else if (e.Message.Text == "/register" || e.Message.Text == "/register@CellereumBot")
                    {
                        if (!Cellereum.IsRegistered(e.Message.From.Id))
                        {
                            if (!String.IsNullOrEmpty(e.Message.From.Username))
                            {
                                Cellereum.Register(e.Message.From.Username, e.Message.From.Id);

                                Bot.SendTextMessageAsync(e.Message.Chat.Id, "Registered successfully!\nRemember that you can change your language with /language.");

                                Console.WriteLine($"New register: {e.Message.From.Id} | @{e.Message.From.Username}");
                            }
                            else Bot.SendTextMessageAsync(e.Message.Chat.Id, "You need to have an username in order to register, otherwise other people couldn't tip you!");
                        }
                        else
                        {
                            if (Cellereum.GetLanguage(e.Message.From.Id) == "English") Bot.SendTextMessageAsync(e.Message.Chat.Id, "You are already registered!");
                            else Bot.SendTextMessageAsync(e.Message.Chat.Id, "Você ja esta registrado!");
                        }
                    }
                    //BALANCE:
                    else if (e.Message.Text == "/balance" || e.Message.Text == "/balance@CellereumBot")
                    {
                        if (Cellereum.IsRegistered(e.Message.From.Id))
                        {
                            if (Cellereum.GetLanguage(e.Message.From.Id) == "English") Bot.SendTextMessageAsync(e.Message.Chat.Id, $"Your balance: {Cellereum.GetBalance(e.Message.From.Id)} CLRs.");
                            else Bot.SendTextMessageAsync(e.Message.Chat.Id, $"Seu saldo: {Cellereum.GetBalance(e.Message.From.Id)} CLRs.");
                        }
                        else Bot.SendTextMessageAsync(e.Message.Chat.Id, "You need to be registered to use this command.");
                    }
                    //TRANSACTIONS:
                    else if (e.Message.Text == "/transactions" || e.Message.Text == "/transactions@CellereumBot")
                    {
                        int transactions = Directory.GetFiles(@$"{Cellereum.RootPath}\Register").Length;

                        double size = 0;

                        foreach (var file in Directory.GetFiles(@$"{Cellereum.RootPath}\Register")) size += file.Length;

                        size /= 1024;
                        size = Math.Round(size);

                        if (Cellereum.IsRegistered(e.Message.From.Id))
                        {
                            if (Cellereum.GetLanguage(e.Message.From.Id) == "English") Bot.SendTextMessageAsync(e.Message.Chat.Id, $"Cellereum has {transactions} transactions at the moment ({size}Kb).");
                            else Bot.SendTextMessageAsync(e.Message.Chat.Id, $"A Cellereum tem {transactions} transações no momento ({size}Kb).");
                        }
                        else Bot.SendTextMessageAsync(e.Message.Chat.Id, $"Cellereum has {transactions} transactions at the moment ({size}Kb).");
                    }
                    //ACCOUNT:
                    else if (e.Message.Text == "/account" || e.Message.Text == "/account@CellereumBot")
                    {
                        if (Cellereum.IsRegistered(e.Message.From.Id))
                        {
                            if (Cellereum.GetLanguage(e.Message.From.Id) == "English") Bot.SendTextMessageAsync(e.Message.Chat.Id, "This command is on maintenance.");
                            else Bot.SendTextMessageAsync(e.Message.Chat.Id, "Este comando esta em manutenção.");
                        }
                        else Bot.SendTextMessageAsync(e.Message.Chat.Id, "This command is on maintenance.");
                    }
                    //SUPPLY:
                    else if (e.Message.Text == "/supply" || e.Message.Text == "/supply@CellereumBot")
                    {
                        if (Cellereum.IsRegistered(e.Message.From.Id))
                        {
                            if (Cellereum.GetLanguage(e.Message.From.Id) == "English") Bot.SendTextMessageAsync(e.Message.Chat.Id, "This command is on maintenance.");
                            else Bot.SendTextMessageAsync(e.Message.Chat.Id, "Este comando esta em manutenção.");
                        }
                        else Bot.SendTextMessageAsync(e.Message.Chat.Id, "This command is on maintenance.");
                    }
                    //SEND:
                    else if (args[0] == "/send" || args[0] == "/send@CellereumBot")
                    {
                        Stopwatch sw = new Stopwatch();
                        sw.Start();

                        if (!string.IsNullOrEmpty(args[1]))
                        {
                            bool replied = true;
                            try { e.Message.ReplyToMessage.From.Id.ToString(); } catch { replied = false; }

                            if (!string.IsNullOrEmpty(args[2]) || replied)
                            {
                                if (!args[2].Contains("@") && replied)
                                {
                                    args[2] = "@"+Cellereum.GetUsername(e.Message.ReplyToMessage.From.Id);
                                }

                                if (Cellereum.IsRegistered(e.Message.From.Id))
                                {
                                    if (e.Message.From.Username != null)
                                    {
                                        if (Cellereum.IsRegistered(Cellereum.GetID(args[2].Substring(1))))
                                        {
                                            if (e.Message.From.Id != Cellereum.GetID(args[2].Substring(1)))
                                            {
                                                try { Convert.ToDouble(args[1]); }
                                                catch
                                                {
                                                    if (Cellereum.GetLanguage(e.Message.From.Id) == "English") Bot.SendTextMessageAsync(e.Message.Chat.Id, "Incorrect parameters. Usage: /send {amount} {username}.");
                                                    else Bot.SendTextMessageAsync(e.Message.Chat.Id, "Parâmetros incorretos. Uso: /send {amount} {username}.");
                                                    return;
                                                }

                                                args[1] = args[1].Replace(".", ",");

                                                bool vipTransaction = Cellereum.IsVIP(e.Message.From.Id) && Convert.ToDouble(args[1]) >= 0.01;

                                                if (Convert.ToDouble(args[1]) >= 1 || vipTransaction)
                                                {
                                                    if (Math.Round(Convert.ToDouble(args[1]), 2) <= Cellereum.GetBalance(e.Message.From.Id))
                                                    {
                                                        Transactions.Send(e.Message.From.Id, Math.Round(Convert.ToDouble(args[1]), 2), args[2].Substring(1));

                                                        sw.Stop();

                                                        if (e.Message.Chat.Id != e.Message.From.Id)
                                                        {
                                                            if (Cellereum.GetLanguage(e.Message.From.Id) == "English") Bot.SendTextMessageAsync(e.Message.From.Id, $"You have sent {Math.Round(Convert.ToDouble(args[1]), 2)} CLRs to {args[2]}.");
                                                            else Bot.SendTextMessageAsync(e.Message.From.Id, $"Você enviou {Math.Round(Convert.ToDouble(args[1]), 2)} CLRs para {args[2]}.");
                                                        }
                                                        if (Math.Round(Convert.ToDouble(args[1]), 2) >= 1)
                                                        {
                                                            if (Cellereum.GetLanguage(Cellereum.GetID(args[2].Substring(1))) == "English") Bot.SendTextMessageAsync(Cellereum.GetID(args[2]), $"You have received {Math.Round(Convert.ToDouble(args[1]), 2)} CLRs from @{e.Message.From.Username}.");
                                                            else Bot.SendTextMessageAsync(Cellereum.GetID(args[2].Substring(1)), $"Você recebeu {Math.Round(Convert.ToDouble(args[1]), 2)} CLRs de @{e.Message.From.Username}.");
                                                        }

                                                        if (Cellereum.GetLanguage(e.Message.From.Id) == "English") Bot.SendTextMessageAsync(e.Message.Chat.Id, $"Transaction confirmed!\n\nDetails:\nSender: @{e.Message.From.Username}\nReceiver: {args[2]}\nAmount: {Math.Round(Convert.ToDouble(args[1]), 2)} CLRs\nDuration: {Math.Round(Decimal.Divide(sw.ElapsedMilliseconds, 1000), 2)} Seconds\n{DateTime.Now}");
                                                        else Bot.SendTextMessageAsync(e.Message.Chat.Id, $"Transação confirmada!\n\nDetalhes:\nRemetente: @{e.Message.From.Username}\nDestinatário: {args[2]}\nQuantidade: {Math.Round(Convert.ToDouble(args[1]), 2)} CLRs\nDuração: {Math.Round(Decimal.Divide(sw.ElapsedMilliseconds, 1000), 2)} Segundos\n{DateTime.Now}");
                                                    }
                                                    else
                                                    {
                                                        if (Cellereum.GetLanguage(e.Message.From.Id) == "English") Bot.SendTextMessageAsync(e.Message.Chat.Id, $"You have {Cellereum.GetBalance(e.Message.From.Id)} CLRs, but this transaction would cost you {Math.Round(Convert.ToDouble(args[1]), 2)} CLRs.");
                                                        else Bot.SendTextMessageAsync(e.Message.Chat.Id, $"Você tem {Cellereum.GetBalance(e.Message.From.Id)} CLRs, mas esta transação te custaria {Math.Round(Convert.ToDouble(args[1]), 2)} CLRs.");
                                                    }
                                                }
                                                else
                                                {
                                                    if (Cellereum.GetLanguage(e.Message.From.Id) == "English") Bot.SendTextMessageAsync(e.Message.Chat.Id, "Your transaction needs to be higher than 1. Vips can send transactions of 0.01 CLR.");
                                                    else Bot.SendTextMessageAsync(e.Message.Chat.Id, "Sua transação precisa ser maior do que 1. Vips podem enviar transações de 0.01 CLR.");
                                                }
                                            }
                                            else
                                            {
                                                if (Cellereum.GetLanguage(e.Message.From.Id) == "English") Bot.SendTextMessageAsync(e.Message.Chat.Id, "You can't send Cellereums to yourself!");
                                                else Bot.SendTextMessageAsync(e.Message.Chat.Id, "Você não pode enviar Cellereums para você mesmo!");
                                            }
                                        }
                                        else
                                        {
                                            if (Cellereum.GetLanguage(e.Message.From.Id) == "English") Bot.SendTextMessageAsync(e.Message.Chat.Id, "This user is not registered yet.");
                                            else Bot.SendTextMessageAsync(e.Message.Chat.Id, "Este usuário não está registrado ainda.");
                                        }
                                    }
                                    else
                                    {
                                        if (Cellereum.GetLanguage(e.Message.From.Id) == "English") Bot.SendTextMessageAsync(e.Message.Chat.Id, "You need to have an username to send Cellereums.");
                                        else Bot.SendTextMessageAsync(e.Message.Chat.Id, "Você precisa ter um nome de usuário para enviar Cellereums.");
                                    }
                                }
                                else Bot.SendTextMessageAsync(e.Message.Chat.Id, "You need to be registered to use this command.");
                            }
                            else
                            {
                                if (Cellereum.IsRegistered(e.Message.From.Id))
                                {
                                    if (Cellereum.GetLanguage(e.Message.From.Id) == "English") Bot.SendTextMessageAsync(e.Message.Chat.Id, "Incorrect parameters. Usage: /send {amount} {username}.");
                                    else Bot.SendTextMessageAsync(e.Message.Chat.Id, "Parâmetros incorretos. Uso: /send {amount} {username}.");
                                }
                                else Bot.SendTextMessageAsync(e.Message.Chat.Id, "Incorrect parameters. Usage: /send {amount} {username}.");
                            }
                        }
                        else
                        {
                            if (Cellereum.IsRegistered(e.Message.From.Id))
                            {
                                if (Cellereum.GetLanguage(e.Message.From.Id) == "English") Bot.SendTextMessageAsync(e.Message.Chat.Id, "Incorrect parameters. Usage: /send {amount} {username}.");
                                else Bot.SendTextMessageAsync(e.Message.Chat.Id, "Parâmetros incorretos. Uso: /send {amount} {username}.");
                            }
                            else Bot.SendTextMessageAsync(e.Message.Chat.Id, "Incorrect parameters. Usage: /send {amount} {username}.");
                        }
                        sw.Stop();
                    }
                    //RAIN:
                    else if (args[0] == "/rain" || args[0] == "/rain@CellereumBot")
                    {
                        Stopwatch sw = new Stopwatch();
                        sw.Start();

                        if (!string.IsNullOrEmpty(args[1]))
                        {
                            if (Cellereum.IsRegistered(e.Message.From.Id))
                            {
                                if (e.Message.From.Username != null)
                                {
                                    try { Convert.ToDouble(args[1]); }
                                    catch
                                    {
                                        if (Cellereum.GetLanguage(e.Message.From.Id) == "English") Bot.SendTextMessageAsync(e.Message.Chat.Id, "Incorrect parameters. Usage: /send {amount} {username}.");
                                        else Bot.SendTextMessageAsync(e.Message.Chat.Id, "Parâmetros incorretos. Uso: /send {amount} {username}.");
                                        return;
                                    }

                                    args[1] = args[1].Replace(".", ",");

                                    int usersCount = Directory.GetFiles(@$"{Cellereum.RootPath}\Accounts\IDs").Length-1;
                                    double roundedAmount = Cellereum.RemoveDecimals(Convert.ToDouble(args[1]),2);

                                    double eachAmount = (double)roundedAmount / usersCount;
                                    eachAmount = Cellereum.RemoveDecimals(eachAmount, 2);
                                    double amount = eachAmount * usersCount;

                                    bool vipTransaction = Cellereum.IsVIP(e.Message.From.Id) && eachAmount >= 0.01;

                                    if (eachAmount >= 1 || vipTransaction)
                                    {
                                        if (Cellereum.GetBalance(e.Message.From.Id) >= amount)
                                        {
                                            DirectoryInfo folder = new DirectoryInfo(@$"{Cellereum.RootPath}\Accounts\IDs");

                                            foreach (var file in folder.GetFiles())
                                            {
                                                if (file.Name != e.Message.From.Id+".txt")
                                                {
                                                    Transactions.Send(e.Message.From.Id, eachAmount, Cellereum.GetUsername(Convert.ToInt32(file.Name.Substring(0, file.Name.Length - 4))));

                                                    string msg = "";

                                                    if (eachAmount >= 1)
                                                    {
                                                        if (args[2] != "anon")
                                                        {
                                                            if (Cellereum.GetLanguage(Convert.ToInt32(file.Name.Substring(0, file.Name.Length - 4))) == "English") msg = $"You have received {eachAmount} CLRs from @{e.Message.From.Username}.";
                                                            else msg = $"Você recebeu {eachAmount} CLRs de @{e.Message.From.Username}.\n";
                                                        }
                                                        else
                                                        {
                                                            if (Cellereum.GetLanguage(Convert.ToInt32(file.Name.Substring(0, file.Name.Length - 4))) == "English") msg = $"You have received {eachAmount} CLRs anons.";
                                                            else msg = $"Você recebeu {eachAmount} CLRs anônimas.\n";
                                                        }

                                                        Thread notify = new Thread(() => Bot.SendTextMessageAsync(Convert.ToInt32(file.Name.Substring(0, file.Name.Length - 4)), msg));
                                                        notify.Start();
                                                    }
                                                }
                                            }
                                            sw.Stop();

                                            if (args[2] == "anon")
                                            {
                                                if (e.Message.Chat.Id != e.Message.From.Id)
                                                {
                                                    if (Cellereum.GetLanguage(e.Message.From.Id) == "English") Bot.SendTextMessageAsync(e.Message.From.Id, $"You have rained {eachAmount} CLRs anons to {usersCount} users.");
                                                    else Bot.SendTextMessageAsync(e.Message.From.Id, $"Você fez uma rain anônima de {eachAmount} CLRs para {usersCount} usuários.");
                                                }

                                                if (Cellereum.GetLanguage(e.Message.From.Id) == "English") Bot.SendTextMessageAsync(e.Message.Chat.Id, $"Rain confirmed!\n\nDetails:\nSender: anon\nReceivers: {usersCount}\nAmount Each: {eachAmount} CLRs\nTotal Amount: {Math.Round(amount, 2)} CLRs\nDuration: {Math.Round(Decimal.Divide(sw.ElapsedMilliseconds, 1000), 2)} Seconds\n{DateTime.Now}");
                                                else Bot.SendTextMessageAsync(e.Message.Chat.Id, $"Rain confirmada!\n\nDetalhes:\nRemetente: anon\nDestinatários: {usersCount}\nQuantidade Cada: {eachAmount} CLRs\nQuantidade Total: {Math.Round(amount, 2)} CLRs\nDuração: {Math.Round(Decimal.Divide(sw.ElapsedMilliseconds, 1000), 2)} Segundos\n{DateTime.Now}");
                                            }
                                            else
                                            {
                                                if (e.Message.Chat.Id != e.Message.From.Id)
                                                {
                                                    if (Cellereum.GetLanguage(e.Message.From.Id) == "English") Bot.SendTextMessageAsync(e.Message.From.Id, $"You have rained {eachAmount} CLRs to {usersCount} users.");
                                                    else Bot.SendTextMessageAsync(e.Message.From.Id, $"Você fez uma rain de {eachAmount} CLRs para {usersCount} usuários.");
                                                }

                                                if (Cellereum.GetLanguage(e.Message.From.Id) == "English") Bot.SendTextMessageAsync(e.Message.Chat.Id, $"Rain confirmed!\n\nDetails:\nSender: @{e.Message.From.Username}\nReceivers: {usersCount}\nAmount Each: {eachAmount} CLRs\nTotal Amount: {Math.Round(amount, 2)} CLRs\nDuration: {Math.Round(Decimal.Divide(sw.ElapsedMilliseconds, 1000), 2)} Seconds\n{DateTime.Now}");
                                                else Bot.SendTextMessageAsync(e.Message.Chat.Id, $"Rain confirmada!\n\nDetalhes:\nRemetente: @{e.Message.From.Username}\nDestinatários: {usersCount}\nQuantidade Cada: {eachAmount} CLRs\nQuantidade Total: {Math.Round(amount, 2)} CLRs\nDuração: {Math.Round(Decimal.Divide(sw.ElapsedMilliseconds, 1000), 2)} Segundos\n{DateTime.Now}");
                                            }
                                        }
                                        else
                                        {
                                            if (Cellereum.GetLanguage(e.Message.From.Id) == "English") Bot.SendTextMessageAsync(e.Message.Chat.Id, $"You have {Cellereum.GetBalance(e.Message.From.Id)} CLRs, but this rain would cost you {Math.Round(amount,2)} CLRs.");
                                            else Bot.SendTextMessageAsync(e.Message.Chat.Id, $"Você tem {Cellereum.GetBalance(e.Message.From.Id)} CLRs, mas esta rain te custaria {Math.Round(amount,2)} CLRs.");
                                        }
                                    }
                                    else
                                    {
                                        if (Cellereum.GetLanguage(e.Message.From.Id) == "English") Bot.SendTextMessageAsync(e.Message.Chat.Id, $"You need to rain at least 1 CLR to each user. ({usersCount} CLRs)\nVips can send rains of 0.01 CLR to each user.");
                                        else Bot.SendTextMessageAsync(e.Message.Chat.Id, $"Você precisa fazer uma rain de pelo menos 1 CLR para cada usuário. ({usersCount} CLRs)\nVips podem enviar rains de 0.01 CLR para cada usuário.");
                                    }
                                }
                                else
                                {
                                    if (Cellereum.GetLanguage(e.Message.From.Id) == "English") Bot.SendTextMessageAsync(e.Message.Chat.Id, "You need to have an username to send Cellereums.");
                                    else Bot.SendTextMessageAsync(e.Message.Chat.Id, "Você precisa ter um nome de usuário para enviar Cellereums.");
                                }
                            }
                            else Bot.SendTextMessageAsync(e.Message.Chat.Id, "You need to be registered to use this command.");
                        }
                        else
                        {
                            if (Cellereum.IsRegistered(e.Message.From.Id))
                            {
                                if (Cellereum.GetLanguage(e.Message.From.Id) == "English") Bot.SendTextMessageAsync(e.Message.Chat.Id, "Incorrect parameters. Usage: /send {amount} {username}.");
                                else Bot.SendTextMessageAsync(e.Message.Chat.Id, "Parâmetros incorretos. Uso: /send {amount} {username}.");
                            }
                            else Bot.SendTextMessageAsync(e.Message.Chat.Id, "Incorrect parameters. Usage: /send {amount} {username}.");
                        }
                        sw.Stop();
                    }
                    else if (args[0] == "/vip" || args[0] == "/vip@CellereumBot")
                    {
                        if (Cellereum.IsRegistered(e.Message.From.Id))
                        {
                            if (Cellereum.GetLanguage(e.Message.From.Id) == "English") Bot.SendTextMessageAsync(e.Message.Chat.Id, "With VIP, you will get a lot of advantages and acess to exclusive commands in our bots, and will support our developers to produce and make more.\n\nPrice cost:\n5000 Cellereums (Month)\nOR\n30000 Cellereums (Annual)\n\nYour plan won't be updated automaticaly at the end.\nType /buyvip month or /buyvip annual.");
                            else Bot.SendTextMessageAsync(e.Message.Chat.Id, "Com VIP, você receberá varias vantagens e acesso a comandos exclusivos, e irá ajudar nossos desenvolvedores á produzir e fazer mais.\n\nCusto:\n5000 Cellereums (Mensal)\nOU\n30000 Cellereums (Anual)\n\nSeu plano não irá ser atualizado automaticamente ao fim.\nDigite /buyvip month ou /buyvip annual.");
                        }
                        else Bot.SendTextMessageAsync(e.Message.Chat.Id, "With VIP, you will get a lot of advantages and acess to exclusive commands in our bots, and will support our developers to produce and make more.\n\nPrice cost:\n5000 Cellereums (Month)\nOR\n30000 Cellereums (Annual)\n\nYour plan won't be updated automaticaly at the end.\nType /buyvip month or /buyvip annual.");

                        canVip.Add(e.Message.From.Id);
                    }
                    else if (args[0] == "/buyvip" || args[0] == "/buyvip@CellereumBot")
                    {
                        if (Cellereum.IsRegistered(e.Message.From.Id))
                        {
                            if (!Cellereum.IsVIP(e.Message.From.Id))
                            {
                                if (canVip.Contains(e.Message.From.Id))
                                {
                                    if (args[1].ToLower() == "month")
                                    {
                                        if (Cellereum.GetBalance(e.Message.From.Id) >= 5000)
                                        {
                                            Transactions.Send(e.Message.From.Id, 5000, "JgBr123");
                                            Cellereum.SetVIP(e.Message.From.Id, 30);

                                            Console.WriteLine($"New VIP! {e.Message.From.Id}");

                                            if (Cellereum.GetLanguage(e.Message.From.Id) == "English") Bot.SendTextMessageAsync(e.Message.Chat.Id, "You bought VIP successfully! Remember that your subscription won't update automaticaly at the end.");
                                            else Bot.SendTextMessageAsync(e.Message.Chat.Id, "Você comprou VIP com sucesso! Lembre que sua inscrição não atualizará automaticamente ao fim.");

                                            if (Cellereum.GetLanguage(e.Message.From.Id) == "English") Bot.SendTextMessageAsync(e.Message.From.Id, "You are now a VIP user!");
                                            else Bot.SendTextMessageAsync(e.Message.From.Id, "Você agora é um usuário VIP!");
                                        }
                                        else
                                        {
                                            if (Cellereum.GetLanguage(e.Message.From.Id) == "English") Bot.SendTextMessageAsync(e.Message.Chat.Id, $"You have {Cellereum.GetBalance(e.Message.From.Id)} CLRs, but this transaction would cost you 5000 CLRs.");
                                            else Bot.SendTextMessageAsync(e.Message.Chat.Id, $"Você tem {Cellereum.GetBalance(e.Message.From.Id)} CLRs, mas esta transação te custaria 5000 CLRs.");
                                        }
                                    }
                                    else if (args[1].ToLower() == "annual")
                                    {
                                        if (Cellereum.GetBalance(e.Message.From.Id) >= 30000)
                                        {
                                            Transactions.Send(e.Message.From.Id, 30000, "JgBr123");
                                            Cellereum.SetVIP(e.Message.From.Id, 360);

                                            Console.WriteLine($"New VIP! {e.Message.From.Id}");

                                            if (Cellereum.GetLanguage(e.Message.From.Id) == "English") Bot.SendTextMessageAsync(e.Message.Chat.Id, "You bought VIP successfully! Remember that your subscription won't update automaticaly at the end.");
                                            else Bot.SendTextMessageAsync(e.Message.Chat.Id, "Você comprou VIP com sucesso! Lembre que sua inscrição não atualizará automaticamente ao fim.");

                                            if (Cellereum.GetLanguage(e.Message.From.Id) == "English") Bot.SendTextMessageAsync(e.Message.From.Id, "You are now a VIP user!");
                                            else Bot.SendTextMessageAsync(e.Message.From.Id, "Você agora é um usuário VIP!");
                                        }
                                        else
                                        {
                                            if (Cellereum.GetLanguage(e.Message.From.Id) == "English") Bot.SendTextMessageAsync(e.Message.Chat.Id, $"You have {Cellereum.GetBalance(e.Message.From.Id)} CLRs, but this transaction would cost you 30000 CLRs.");
                                            else Bot.SendTextMessageAsync(e.Message.Chat.Id, $"Você tem {Cellereum.GetBalance(e.Message.From.Id)} CLRs, mas esta transação te custaria 30000 CLRs.");
                                        }
                                    }
                                    else
                                    {
                                        if (Cellereum.GetLanguage(e.Message.From.Id) == "English") Bot.SendTextMessageAsync(e.Message.Chat.Id, "Incorrect parameters. Usage: /buyvip {month/annual}.");
                                        else Bot.SendTextMessageAsync(e.Message.Chat.Id, "Parâmetros incorretos. Uso: /buyvip {month/annual}.");
                                    }
                                }
                                else
                                {
                                    if (Cellereum.GetLanguage(e.Message.From.Id) == "English") Bot.SendTextMessageAsync(e.Message.Chat.Id, "Type /vip first.");
                                    else Bot.SendTextMessageAsync(e.Message.Chat.Id, "Digite /vip primeiro.");
                                }
                            }
                            else
                            {
                                if (Cellereum.GetLanguage(e.Message.From.Id) == "English") Bot.SendTextMessageAsync(e.Message.Chat.Id, "You are already a VIP!");
                                else Bot.SendTextMessageAsync(e.Message.Chat.Id, "Você ja é um VIP!");
                            }
                        }
                        else Bot.SendTextMessageAsync(e.Message.Chat.Id, "You need to be registered to use this command.");

                        canVip.Add(e.Message.From.Id);
                    }
                    else if (e.Message.Text.StartsWith("/broadcast") && e.Message.Chat.Id == 651939005)
                    {
                        DirectoryInfo folder = new DirectoryInfo(@$"{Cellereum.RootPath}\Accounts\IDs");

                        foreach (var file in folder.GetFiles())
                        {
                            Bot.SendTextMessageAsync(file.Name.Substring(0, file.Name.Length - 4), e.Message.Text.Replace("/broadcast", ""));
                        }
                    }
                    else if (args[0] == "/shutdown" && e.Message.Chat.Id == 651939005)
                    {
                        if (shutdown)
                        {
                            Console.WriteLine("Bot stopped receiving updates.");
                            Bot.StopReceiving();
                        }
                        else shutdown = true;
                    }
                    else if (args[0] == "/tes" && e.Message.Chat.Id == 651939005)
                    {
                        Thread a = new Thread(() => Cellereum.SetBalance(e.Message.From.Id, Cellereum.GetBalance(e.Message.From.Id)));
                        Thread b = new Thread(() => Cellereum.SetBalance(e.Message.From.Id, Cellereum.GetBalance(e.Message.From.Id)));

                        a.Start();
                        b.Start();
                    }
                }
            }
            catch (Exception ex)
            {
                Bot.SendTextMessageAsync(e.Message.Chat.Id, "An unknown error occured. Please tag or message @JgBr123.");
                Console.WriteLine(ex);
            }
        }
    }
}