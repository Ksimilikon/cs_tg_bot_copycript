using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;

namespace cs_tg_bot.UI
{
    class KeyboardStart
    {
        public static ReplyKeyboardMarkup keyboard()
        {
            return new ReplyKeyboardMarkup();
        }
    }
}
