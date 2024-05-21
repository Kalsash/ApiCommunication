using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Eventing.Reader;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


internal class Program
{
    static readonly HttpClient client = new HttpClient();
    private static TelegramBotClient botClient;
    
    static async Task Main(string[] args)
    {
        var token = System.IO.File.ReadAllText(@"../../../TGToken.txt");

        var receiverOptions = new ReceiverOptions
        {
            AllowedUpdates = Array.Empty<UpdateType>()
        };
        botClient = new TelegramBotClient(token);
        Console.WriteLine("Запускается сервер...");

        botClient.StartReceiving(
            HandleUpdateAsync,
            HandleErrorAsync,
            receiverOptions);

        Console.WriteLine("Сервер запущен!");
        Console.ReadLine();

    }
    private static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        var message = update.Message;

        if (message != null && message.Text != null)
        {
            Console.WriteLine("Логи");
            Console.WriteLine($"{message.Chat.FirstName}    |    {message.Text}");

            if (message.Text.ToLower().Contains("/menu"))
            {
                var menu = await GetMenuAsync();
                if (menu != null)
                {
                    await botClient.SendTextMessageAsync(message.Chat.Id, FormatMenu(menu));
                }
                else
                {
                    await botClient.SendTextMessageAsync(message.Chat.Id, "Ошибка!");
                }
            }
        }
    }
    //запрос меню
    static async Task<JArray> GetMenuAsync()
    {
        var response = await client.GetStringAsync("http://localhost:8080/api/menu");
        if (!string.IsNullOrEmpty(response))
        {
            return JArray.Parse(response);
        }

        return null;
    }
    //Приведение ответа из Json формата к строке
    static string FormatMenu(JArray menu)
    {
        var result = new System.Text.StringBuilder();
        foreach (var item in menu)
        {
            var name = (string)item["name"];
            var price = (decimal)item["price"];

            result.AppendLine($"{name} - {price:C}");
        }
        return result.ToString();
    }
    //отработка исключений
    private static Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {
        Console.WriteLine("Сервер не работает!");
        return Task.CompletedTask;
    }

}