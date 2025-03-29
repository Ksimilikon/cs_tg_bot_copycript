using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cs_tg_bot.Models
{
    class User
    {
        private long id;
        public Settings settings;
        public User(long id)
        {
            this.id = id;
        }

        public long getId()
        {
            return id;
        }
    }
}
