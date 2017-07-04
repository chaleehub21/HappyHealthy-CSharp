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
using SQLite;
namespace HappyHealthyCSharp
{
    class Database
    {
        public static string DB_NAME = "HHSQLITE.db3";
        public static int DB_VERSION = 1;
        public Database(Context context)
        {

        }
    }
}