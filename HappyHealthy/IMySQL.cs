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

namespace HappyHealthyCSharp
{
    interface IMySQL
    {
        bool MySQLInsert(DatabaseHelper data);
        bool MySQLDelete(DatabaseHelper data);
        List<T> MySQLSelect<T>(string data);
        JavaList<IDictionary<string, object>> MySQLGetJavaList(DatabaseHelper data);
    }
}