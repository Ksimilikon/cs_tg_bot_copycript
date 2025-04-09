using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cs_tg_bot.Service.Enums;
using main;

namespace cs_tg_bot.Models
{
    class User
    {
        public readonly long id;
        public Locales lang { get { return _lang; } }
        private Locales _lang;

        public User(long id, Locales lang)
        {
            this.id = id;
            _lang = lang;
        }
        public void setLocale(SQLiteConnection conn ,Locales lang)
        {
            _lang = lang;
            SQLiteCommand patch = new SQLiteCommand("update users set lang=@lang where tg_id=@id", conn);
            patch.Parameters.AddWithValue("@lang", lang);
            patch.Parameters.AddWithValue("@id", this.id);
            try
            {
                patch.ExecuteNonQuery();
                logger.Logger.Log("User::class::setLocale()", "change locale success");
            }
            catch
            {
                logger.Logger.Log("User::class::setLocale()", "change locale FAIL");
            }
        }
        public static User find(SQLiteConnection conn, long idUserTg)
        {
            SQLiteCommand select = new SQLiteCommand("select tg_id, lang from users where tg_id=@id", conn);
            select.Parameters.AddWithValue("@id", idUserTg);
            using (SQLiteDataReader r = select.ExecuteReader())
            {
                if (r.Read())
                {
                    return new User(r.GetInt64(0), (Locales)Enum.Parse<Locales>(r.GetString(1)));
                }
            }
            return null;
        }

        public static User create(SQLiteConnection conn,long idUser, Locales lang)
        {
            SQLiteCommand checkExist = new SQLiteCommand("select tg_id, lang from users where tg_id=@id", conn);
            checkExist.Parameters.AddWithValue("@id", idUser);
            using (SQLiteDataReader r = checkExist.ExecuteReader())
            {
                if (r.Read())
                {
                    return new User(r.GetInt64(0), (Locales)Enum.Parse<Locales>(r.GetString(1)));
                }
            }
            SQLiteCommand insert = new SQLiteCommand("insert into users (tg_id, lang, timestamp) values(@id,@lang,@timestamp)", conn);
            insert.Parameters.AddWithValue("@id", idUser);
            insert.Parameters.AddWithValue("@lang", lang);
            insert.Parameters.AddWithValue("@timestamp", DateTime.UtcNow);

            try
            {
                insert.ExecuteNonQuery();
                logger.Logger.Log("User::class::create()", "new user registration");
            }
            catch
            {
                logger.Logger.Log("User::class::create()", "error create new user in db");
            }
            return new User(idUser, lang);
        }
    }
}
