using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite.Net;
using System.IO;
using SQLite.Net.Platform.XamarinAndroid;
using Ezra.Data;
using Ezra.Droid.Data;

[assembly: Xamarin.Forms.Dependency(typeof(DatabaseConnectionAndroid))]
namespace Ezra.Droid.Data
{
    class DatabaseConnectionAndroid : IDatabaseConnection
    {
        public SQLiteConnection DbConnection()
        {
            var dbName = "EzraDatabase.db3";
            string documentsFolder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            string path = Path.Combine(documentsFolder, dbName);
            var platform = new SQLitePlatformAndroid();
            return new SQLiteConnection(platform, path);
        }
    }
}