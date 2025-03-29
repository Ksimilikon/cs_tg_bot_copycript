using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace cs_tg_bot.logger
{
    class Logger
    {
        public static void Log(string place, string message)
        {
            Console.WriteLine($"{DateTime.UtcNow} -- {place}::{message}");
        }
    }
}
