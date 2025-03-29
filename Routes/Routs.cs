using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using cs_tg_bot.logger;

namespace cs_tg_bot.Routes
{
    abstract class Routs
    {
        protected string nameClass = "Routs";
        protected virtual bool filterText(Telegram.Bot.Types.Message msg, string text)
        {
            Logger.Log("update:filterText()", $"get message -- {msg.Chat.Id}::{msg.Chat.FirstName}");
            if (msg.Text != null)
            {
                if(msg.Text.ToLower() == text)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
