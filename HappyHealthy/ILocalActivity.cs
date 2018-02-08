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
    interface ILocalActivity
    {
        void SaveValue(object sender, EventArgs e);
        void UpdateValue(object sender, EventArgs e);
        void DeleteValue(object sender, EventArgs e);
        void InitialForUpdateEvent();

    }
}