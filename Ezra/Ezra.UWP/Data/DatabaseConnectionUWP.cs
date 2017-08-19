using Ezra.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite.Net;
using Ezra.UWP.Data;
using System.IO;
using SQLite.Net.Platform.WinRT;

[assembly: Xamarin.Forms.Dependency(typeof(DatabaseConnectionUWP))]
namespace Ezra.UWP.Data
{
    public class DatabaseConnectionUWP : IDatabaseConnection
    {
        public SQLiteConnection DbConnection()
        {
            var dbName = "EzraDatabase.db3";
            string path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, dbName);
            var platform = new SQLitePlatformWinRT();
            return new SQLiteConnection(platform, path);
        }
    }
}
