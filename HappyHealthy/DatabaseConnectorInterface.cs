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
    public abstract class DatabaseConnectorInterface
    {
        public virtual bool Insert()
        {
            return false;
        }
        public virtual bool Delete()
        {
            return false;
        }
        public virtual bool ViewAll()
        {
            return false;
        }
    }
}