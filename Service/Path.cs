using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cs_tg_bot.Service
{
    class Path
    {
        private string path;
        public Path()
        {
            path = Directory.GetCurrentDirectory();
        }

        public void back()
        {
            path = Directory.GetParent(path).FullName;
        }
        public void back(int count)
        {
            for(int i = 0; i < count; i++)
            {
                path = Directory.GetParent(path).FullName;
            }
        }
        public void forward(string dir)
        {
            path += @"\" + dir;
        }

        public string get()
        {
            return path;
        }
        public string getPathFile(string nameFile)
        {
            return path + @"\" + nameFile;
        }
    }
}
