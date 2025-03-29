using System;
using System.Reflection.Metadata.Ecma335;
using cs_tg_bot.Controller;
using cs_tg_bot.logger;
using cs_tg_bot.Models;
using cs_tg_bot.Routes;
using cs_tg_bot.Service;
using cs_tg_bot.Service.Enums;
using Telegram.Bot;
using Telegram.Bot.Types;
using User = cs_tg_bot.Models.User;
namespace main
{
    static class Program
    {
        public static List<User> users = new List<User>();

        private static string botToken;
        private static TelegramBotClient bot;
        private static MainRouts _rout = new MainRouts();

        public static void Main(string[] args)
        {
            Console.WriteLine("start run app");
            cs_tg_bot.Service.Path path = new cs_tg_bot.Service.Path();
            path.back(3);
            LocaleController lcController = new LocaleController();
            //Console.WriteLine(lcController.getJSON(Locales.ru, "dsd").list_locale[0]);
            botToken = Env.getProperty(path.getPathFile(".env"), "bot_token");
            if(botToken == "" || botToken == null)
            {
                Console.WriteLine("botToken error::token = {0}", botToken);
                return;
            }
            
            try 
            { 
                bot = new TelegramBotClient(botToken);
                Logger.Log("Program", "bot running");
            }
            catch
            {
                Logger.Log("Program", "bot running is failed");
                return;
            }
            
            bot.StartReceiving(Update,Error);
            Console.ReadLine();
        }
        async private static Task Update(ITelegramBotClient botClient, Update update, CancellationToken token)
        {
            var msg = update.Message;
            _rout.commandStart(botClient, msg);
            
        }
        async private static Task Error(ITelegramBotClient arg1, Exception arg2, CancellationToken arg3)
        {
            return;
        }
        
    }
}