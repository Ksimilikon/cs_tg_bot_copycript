using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cs_tg_bot.Service
{
    static class Env
    {
        
        public static string getProperty(string pathFile, string property)
        {
            if (File.Exists(pathFile))
            {
                string result="";
                using (StreamReader sr = new StreamReader(pathFile))
                {
                    string line = "";
                    while (true)
                    {
                        line = sr.ReadLine();
                        if (line == null)
                        {
                            Console.WriteLine($"env {pathFile} property::{property} not found");
                            break;
                        }

                        string[] words = line.Split('=');
                        if (words[0] == property)
                        {
                            result = words[1];
                            break;
                        }
                    }
                    sr.Close();

                    return result;
                }
            }
        
            else
            {
                Console.WriteLine($"env file not found {pathFile}");
            }
            return "";
        }
        
    }
}
