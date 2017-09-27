using Ezra.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ezra.Data
{
    public class SettingsDatabase : BaseDatabase
    {
        public Settings GetSettings()
        {
            return GetDatabase().Table<Settings>().FirstOrDefault();
        }
    }
}
