using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cs_tg_bot.locale.structs
{
    struct UILocale
    {
        public string lc_ru;
        public string lc_en;
        public string lc_menu;
        public string services;
        public UILocale(string lc_ru, string lc_en, string lc_menu, string services)
        {
            this.lc_ru = lc_ru;
            this.lc_en = lc_en;
            this.lc_menu = lc_menu;
            this.services = services;
        }
    }
}
