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

        static void Main()
        {
            Bot.OnMessage += Bot_OnMessage;
            Bot.OnMessageEdited += Bot_OnMessage;

            Console.WriteLine("Bot iniciado e operando");
            Bot.StartReceiving();

            while (true)
            {
                Console.ReadKey();
            }
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
                            if (Cellereum.GetLanguage(e.Message.From.Id) == "English") Bot.SendTextMessageAsync(e.Message.Chat.Id, "Thanks for using our bot. If you are new here, check /help, or change your language at /language.\n\nVersion 1.0 RR");
                            else Bot.SendTextMessageAsync(e.Message.Chat.Id, "Obrigado por utilizar nosso bot. Se você é novo aqui, cheque /help, ou mude sua linguagem em /language.\n\nVersão 1.0 RR");
                        }
                        else Bot.SendTextMessageAsync(e.Message.Chat.Id, "Thanks for using our bot. If you are new here, check /help, or change your language at /language.\n\nVersion 1.0 RR");
                    }
                    //VERSION:
                    if (e.Message.Text == "/version" || e.Message.Text == "/version@CellereumBot")
                    {
                        if (Cellereum.IsRegistered(e.Message.From.Id))
                        {
                            if (Cellereum.GetLanguage(e.Message.From.Id) == "English") Bot.SendTextMessageAsync(e.Message.Chat.Id, "Version 1.0 Remastered Remake");
                            else Bot.SendTextMessageAsync(e.Message.Chat.Id, "Versão 1.0 Remastered Remake");
                        }
                        else Bot.SendTextMessageAsync(e.Message.Chat.Id, "Version 1.0 Remastered Remake");
                    }
                    //HELP:
                    if (e.Message.Text == "/help" || e.Message.Text == "/help@CellereumBot")
                    {
                        if (Cellereum.IsRegistered(e.Message.From.Id))
                        {
                            if (Cellereum.GetLanguage(e.Message.From.Id) == "English") Bot.SendTextMessageAsync(e.Message.Chat.Id, "/help : Show this list\n/version : Show the latest bot version\n/language : Change the bot language\n/register : Creates an account\n/account : Show info about your account\n/balance : Show your Cellereum balance\n/send : Send Cellereums to an user\n/rain : Send Cellereums to all registered users\n/supply : Show the Cellereum Supply\n/transactions : Show how many transactions Cellereum has");
                            else Bot.SendTextMessageAsync(e.Message.Chat.Id, "/help : Mostra esta lista\n/version : Mostra a versão atual do bot\n/language : Mude a linguagem do bot\n/register : Crie uma conta\n/account : Mostra informações da sua conta\n/balance : Mostra seu saldo em Cellereums\n/send : Envie Cellereums para um usuário\n/rain : Envie Cellereums para todos os usuários registrados\n/supply : Mostra o supply da Cellereum\n/transactions : Mostra quantas transações a Cellereum tem");
                        }
                        else Bot.SendTextMessageAsync(e.Message.Chat.Id, "/help : Show this list\n/version : Show the latest bot version\n/language : Change the bot language\n/register : Creates an account\n/account : Show info about your account\n/balance : Show your Cellereum balance\n/send : Send Cellereums to an user\n/rain : Send Cellereums to all registered users\n/supply : Show the Cellereum Supply\n/transactions : Show how many transactions Cellereum has");
                    }
                    //LANGUAGE:
                    if (args[0] == "/language" || args[0] == "/language@CellereumBot")
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
                    if (e.Message.Text == "/register" || e.Message.Text == "/register@CellereumBot")
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
                    if (e.Message.Text == "/balance" || e.Message.Text == "/balance@CellereumBot")
                    {
                        if (Cellereum.IsRegistered(e.Message.From.Id))
                        {
                            if (Cellereum.GetLanguage(e.Message.From.Id) == "English") Bot.SendTextMessageAsync(e.Message.Chat.Id, $"Your balance: {Cellereum.GetBalance(e.Message.From.Id)} CLRs.");
                            else Bot.SendTextMessageAsync(e.Message.Chat.Id, $"Seu saldo: {Cellereum.GetBalance(e.Message.From.Id)} CLRs.");
                        }
                        else Bot.SendTextMessageAsync(e.Message.Chat.Id, "You need to be registered to use this command.");
                    }
                    //TRANSACTIONS:
                    if (e.Message.Text == "/transactions" || e.Message.Text == "/transactions@CellereumBot")
                    {
                        int transactions = Directory.GetFiles(@$"{Cellereum.RootPath}\Register").Length;

                        if (Cellereum.IsRegistered(e.Message.From.Id))
                        {
                            if (Cellereum.GetLanguage(e.Message.From.Id) == "English") Bot.SendTextMessageAsync(e.Message.Chat.Id, $"Cellereum has {transactions} transactions at the moment.");
                            else Bot.SendTextMessageAsync(e.Message.Chat.Id, $"A Cellereum tem {transactions} transações no momento.");
                        }
                        else Bot.SendTextMessageAsync(e.Message.Chat.Id, $"Cellereum has {transactions} transactions at the moment.");
                    }
                    //ACCOUNT:
                    if (e.Message.Text == "/account" || e.Message.Text == "/account@CellereumBot")
                    {
                        if (Cellereum.IsRegistered(e.Message.From.Id))
                        {
                            if (Cellereum.GetLanguage(e.Message.From.Id) == "English") Bot.SendTextMessageAsync(e.Message.Chat.Id, "This command is on maintenance.");
                            else Bot.SendTextMessageAsync(e.Message.Chat.Id, "Este comando esta em manutenção.");
                        }
                        else Bot.SendTextMessageAsync(e.Message.Chat.Id, "This command is on maintenance.");
                    }
                    //SUPPLY:
                    if (e.Message.Text == "/supply" || e.Message.Text == "/supply@CellereumBot")
                    {
                        if (Cellereum.IsRegistered(e.Message.From.Id))
                        {
                            if (Cellereum.GetLanguage(e.Message.From.Id) == "English") Bot.SendTextMessageAsync(e.Message.Chat.Id, "This command is on maintenance.");
                            else Bot.SendTextMessageAsync(e.Message.Chat.Id, "Este comando esta em manutenção.");
                        }
                        else Bot.SendTextMessageAsync(e.Message.Chat.Id, "This command is on maintenance.");
                    }
                    //SEND:
                    if (args[0] == "/send" || args[0] == "/send@CellereumBot")
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

                                                if (Convert.ToDouble(args[1]) >= 1)
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
                                                        if (Cellereum.GetLanguage(Cellereum.GetID(args[2].Substring(1))) == "English") Bot.SendTextMessageAsync(Cellereum.GetID(args[2]), $"You have received {Math.Round(Convert.ToDouble(args[1]), 2)} CLRs from @{e.Message.From.Username}.");
                                                        else Bot.SendTextMessageAsync(Cellereum.GetID(args[2].Substring(1)), $"Você recebeu {Math.Round(Convert.ToDouble(args[1]), 2)} CLRs de @{e.Message.From.Username}.");

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
                                                    if (Cellereum.GetLanguage(e.Message.From.Id) == "English") Bot.SendTextMessageAsync(e.Message.Chat.Id, "Your transaction needs to be higher than 1.");
                                                    else Bot.SendTextMessageAsync(e.Message.Chat.Id, "Sua transação precisa ser maior do que 1.");
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
                    if (args[0] == "/rain" || args[0] == "/rain@CellereumBot")
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

                                    if (eachAmount >= 1)
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

                                                    if (Cellereum.GetLanguage(Convert.ToInt32(file.Name.Substring(0, file.Name.Length - 4))) == "English") msg = $"You have received {eachAmount} CLRs from @{e.Message.From.Username}.";
                                                    else msg = $"Você recebeu {eachAmount} CLRs de @{e.Message.From.Username}.\n";

                                                    Thread notify = new Thread(() => Bot.SendTextMessageAsync(Convert.ToInt32(file.Name.Substring(0, file.Name.Length - 4)),msg));
                                                    notify.Start();
                                                }
                                            }
                                            sw.Stop();

                                            if (e.Message.Chat.Id != e.Message.From.Id)
                                            {
                                                if (Cellereum.GetLanguage(e.Message.From.Id) == "English") Bot.SendTextMessageAsync(e.Message.From.Id, $"You have rained {eachAmount} CLRs to {usersCount} users.");
                                                else Bot.SendTextMessageAsync(e.Message.From.Id, $"Você fez uma rain de {eachAmount} CLRs para {usersCount} usuários.");
                                            }

                                            if (Cellereum.GetLanguage(e.Message.From.Id) == "English") Bot.SendTextMessageAsync(e.Message.Chat.Id, $"Rain confirmed!\n\nDetails:\nSender: @{e.Message.From.Username}\nReceivers: {usersCount}\nAmount Each: {eachAmount} CLRs\nTotal Amount: {amount} CLRs\nDuration: {Math.Round(Decimal.Divide(sw.ElapsedMilliseconds, 1000), 2)} Seconds\n{DateTime.Now}");
                                            else Bot.SendTextMessageAsync(e.Message.Chat.Id, $"Rain confirmada!\n\nDetalhes:\nRemetente: @{e.Message.From.Username}\nDestinatários: {usersCount}\nQuantidade Cada: {eachAmount} CLRs\nQuantidade Total: {amount} CLRs\nDuração: {Math.Round(Decimal.Divide(sw.ElapsedMilliseconds, 1000), 2)} Segundos\n{DateTime.Now}");
                                        }
                                        else
                                        {
                                            if (Cellereum.GetLanguage(e.Message.From.Id) == "English") Bot.SendTextMessageAsync(e.Message.Chat.Id, $"You have {Cellereum.GetBalance(e.Message.From.Id)} CLRs, but this rain would cost you {amount} CLRs.");
                                            else Bot.SendTextMessageAsync(e.Message.Chat.Id, $"Você tem {Cellereum.GetBalance(e.Message.From.Id)} CLRs, mas esta rain te custaria {amount} CLRs.");
                                        }
                                    }
                                    else
                                    {
                                        if (Cellereum.GetLanguage(e.Message.From.Id) == "English") Bot.SendTextMessageAsync(e.Message.Chat.Id, "You need to rain at least 1 CLR to each user.");
                                        else Bot.SendTextMessageAsync(e.Message.Chat.Id, "Você precisa fazer uma rain de pelo menos 1 CLR para cada usuário.");
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