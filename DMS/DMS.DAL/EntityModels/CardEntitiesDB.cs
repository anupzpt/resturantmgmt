using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.DAL.EntityModels
{
    public class CardEntitiesDB
    {
        public static void getDetail(out string server, out string user, out string pass, out string db)
        {
            server = Properties.Settings.Default.server;
            user = Properties.Settings.Default.user;
            pass = Properties.Settings.Default.pass;
            db = Properties.Settings.Default.db;
        }
        public static string getConnectionString()
        {
            string server; string user; string pass; string db;
            getDetail(out server, out user, out pass, out db);
            if (server == "" && user == "")
            { return ""; }

            return getConnectionString(server, user, pass, db);
        }
        public static string getConnectionString(string Server, string User, string Pass, String DB)
        {
            return string.Format("Data Source={0};Initial Catalog={1};User ID={2};Password={3}",
                Server, DB, User, Pass);
        }
    }
}
