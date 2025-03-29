using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using main;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace cs_tg_bot.Routes
{
    class MainRouts : Routs
    {
        
        async public void commandStart(ITelegramBotClient botClient, Message msg)
        {
            if (filterText(msg, "/start"))
            {
                bool isExistUser = false;
                foreach (var user in Program.users)
                {
                    if (user.getId() == msg.Chat.Id)
                    {
                        isExistUser = true;
                        break;
                    } 
                }
                if (!isExistUser)
                {
                    Program.users.Add(new cs_tg_bot.Models.User(msg.Chat.Id));
                }
                await botClient.SendMessage(msg.Chat.Id, "start text");
                logger.Logger.Log($"{nameClass}::commandStart()", $"event handled -- {msg.Chat.Id}::{msg.Chat.FirstName}");
            }
        }
    }
}
