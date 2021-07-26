using System;
using System.Collections.Generic;
using System.Linq;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace IMBDTgBot
{
    class Program
    {
        static string token = "";
        static ITelegramBotClient botClient;
        static List<Title> Titles = new List<Title>();

        static void Main()
        {
            botClient = new TelegramBotClient(token);
            using (imdbContext context = new imdbContext())
            {
                Titles = context.Titles.ToList();
            }
                var me = botClient.GetMeAsync().Result;
            Console.WriteLine(
              $"Hello, World! I am user {me.Id} and my name is {me.FirstName}."
            );

            botClient.OnMessage += Bot_OnMessage;
            botClient.StartReceiving();

            Console.WriteLine("Press any key to exit");
            Console.ReadKey();

            botClient.StopReceiving();
        }

        static async void Bot_OnMessage(object sender, MessageEventArgs e)
        {
            if (e.Message.Text != null)
            {
                
                    Console.WriteLine($"Received a text message in chat {e.Message.Chat.Id}.");
                    Console.WriteLine(e.Message.Text);
                    Random rnd = new Random();
                    var titles = Titles.Where(x => x.TitleType == "movie").ToList();
                    string title = titles[rnd.Next(0, titles.Count)].Tconst;

                    await botClient.SendTextMessageAsync(
                      chatId: e.Message.Chat,
                      text: "https://www.imdb.com/title/"+title

                    );
                
            }
        }
    }
}