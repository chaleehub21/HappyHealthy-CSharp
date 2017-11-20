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
    class TEMP_DiabetesTABLE
    {
        public int      fbs_id_pointer  { get; set; }
        public DateTime fbs_time_new    { get; set; }
        public DateTime fbs_time_old    { get; set; }
        public int      fbs_fbs_new     { get; set; }
        public int      fbs_fbs_old     { get; set; }
        public int      fbs_fbs_lvl_new { get; set; }
        public int      fbs_fbs_lvl_old { get; set; }
        public string   mode            { get; set; }
        public TEMP_DiabetesTABLE() { }
    }
}