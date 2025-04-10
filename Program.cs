using System;
using System.Data.SQLite;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
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
        public static SQLiteConnection dbConn = null;

        private static string botToken;
        private static TelegramBotClient bot;
        private static MainRouts _rout = new MainRouts();

        public static async Task Main(string[] args)
        {
            Console.WriteLine("start run app");
            Console.WriteLine("DB connection");
            cs_tg_bot.Service.Path pathEnv = new cs_tg_bot.Service.Path();
            pathEnv.back(3);
            cs_tg_bot.Service.Path pathDB = new cs_tg_bot.Service.Path();
            pathDB.back(3);
            string dbName = Env.getProperty(pathEnv.getPathFile(".env"), "db_name");
            dbName = pathDB.getPathFile(dbName);
            if (!File.Exists(dbName))
            {
                Console.WriteLine("db not found::error");
                System.Environment.Exit(-1);
            }
            dbConn = new SQLiteConnection("Data source="+dbName+";");
            try
            {
                dbConn.Open();
                Console.WriteLine("db connected");
            }
            catch
            {
                Console.WriteLine("db dont connect");
                System.Environment.Exit(-10);
            }

            //using (HttpClient client = new HttpClient())
            //{
            //    client.BaseAddress = new Uri(@"https://api-v3.raydium.io/");
            //    HttpResponseMessage response = await client.GetAsync(@"main/migrate-lp");
            //    string responseData = await response.Content.ReadAsStringAsync();
            //    Console.WriteLine(responseData);
            //}




                LocaleController lcController = new LocaleController();
            //Console.WriteLine(lcController.getJSON(Locales.ru, "dsd").list_locale[0]);
            botToken = Env.getProperty(pathEnv.getPathFile(".env"), "bot_token");
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
            Console.WriteLine("Closing db");
            dbConn.Close();
            Console.WriteLine("DB is closed");
        }
        async private static Task Update(ITelegramBotClient botClient, Update update, CancellationToken token)
        {
            Message msg = update.Message;
            await _rout.Handler(botClient, msg);
            
        }
        async private static Task Error(ITelegramBotClient arg1, Exception arg2, CancellationToken arg3)
        {
            return;
        }
        
    }
}