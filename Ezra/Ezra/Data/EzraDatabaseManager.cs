using Ezra.Models;
using SQLite;
using SQLite.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ezra.Data
{
    public class EzraDatabaseManager
    {
        readonly SQLiteConnection database;
        public SQLiteConnection Database { get { return database; } }

        public EzraDatabaseManager(IDatabaseConnection databaseConnection)
        {
            this.database = databaseConnection.DbConnection();
            database.CreateTable<ReportItem>();
            database.CreateTable<Settings>();
            database.CreateTable<CounterTimestamp>();
        }
    }
}
