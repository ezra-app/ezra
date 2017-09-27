using SQLite.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ezra.Data
{
    public class BaseDatabase
    {
        public SQLiteConnection GetDatabase()
        {
            return App.DatabaseManager.Database;
        }

        public void Save(object obj)
        {
            GetDatabase().Insert(obj);
        }

        public void Update(object obj)
        {
            GetDatabase().Update(obj);
        }
    }
}
