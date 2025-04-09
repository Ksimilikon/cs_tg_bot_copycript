using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using main;
using Telegram.Bot;
using Telegram.Bot.Types;
using cs_tg_bot.Models;
using User = cs_tg_bot.Models.User;
using cs_tg_bot.Service.Enums;
using Microsoft.VisualBasic;
using System.Runtime.CompilerServices;

namespace cs_tg_bot.Routes
{
    class MainRouts : Routs
    {
        
        async public Task Handler(ITelegramBotClient botClient, Message msg)
        {
            if (filterText(msg, "/start"))
            {
                string lang = msg.From.LanguageCode;
                Locales lc;
                if (lang == "ru")
                {
                    lc = Locales.ru;
                }
                else
                {
                    lc = Locales.en;
                }
                User.create(Program.dbConn, msg.Chat.Id, lc);
                await botClient.SendMessage(msg.Chat.Id, "Welcome");
                await logger.Logger.LogAsync($"{nameClass}::Handler()/start", $"event handled -- {msg.Chat.Id}::{msg.Chat.FirstName}");
            }
            if (filterText(msg, "/locale_en"))
            {
                await changeLocale(botClient, msg, Locales.en);
                await logger.Logger.LogAsync($"{nameClass}::Handler()/locale_en", $"event handled -- {msg.Chat.Id}::{msg.Chat.FirstName}");
            }
            if (filterText(msg, "/locale_ru"))
            {
                await changeLocale(botClient, msg, Locales.ru);
                await logger.Logger.LogAsync($"{nameClass}::Handler()/locale_ru", $"event handled -- {msg.Chat.Id}::{msg.Chat.FirstName}");
            }
        }
        
        async private Task changeLocale(ITelegramBotClient botClient, Message msg, Locales lc)
        {
            User user = User.find(Program.dbConn, msg.Chat.Id);
            if (user != null)
            {
                user.setLocale(Program.dbConn, lc);
                await botClient.SendMessage(msg.Chat.Id, $"Language is changed {lc}");

            }
            else
            {
                await botClient.SendMessage(msg.Chat.Id, "need authorize /start");
            }
        }
    }
}
