using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using cs_tg_bot.locale;
using cs_tg_bot.locale.structs;
using cs_tg_bot.Service;
using cs_tg_bot.Service.Enums;
using Path = cs_tg_bot.Service.Path;

namespace cs_tg_bot.Controller
{
    class LocaleController : Controller
    {
        protected override string nameClass { get; } = "LocaleConroller";
        private Path path = new Path();
        
        public LocaleController()
        {
            path.back(3);
            path.forward("locale");
            
        }
        
        public DataLocale getLocale(Locales lc)
        {
            string file = path.getPathFile(Convert.ToString(lc)+".lc");
            //using StreamReader sr = new StreamReader(file)
            //{

            //}
            return new DataLocale();
        }


    }
}
