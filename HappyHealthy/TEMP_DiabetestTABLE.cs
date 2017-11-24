﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Xml.Serialization;
using System.Runtime.Serialization;
namespace HappyHealthyCSharp
{
    [Serializable]
    [DataContract]
    class TEMP_DiabetesTABLE : DatabaseHelper
    {
        [DataMember]
        public int      fbs_id_pointer  { get; set; }
        [DataMember]
        public DateTime fbs_time_new    { get; set; }
        [DataMember]
        public string   fbs_time_string_new { get; set; }
        [DataMember]
        public DateTime fbs_time_old    { get; set; }
        [DataMember]
        public int      fbs_fbs_new     { get; set; }
        [DataMember]
        public int      fbs_fbs_old     { get; set; }
        [DataMember]
        public int      fbs_fbs_lvl_new { get; set; }
        [DataMember]
        public int      fbs_fbs_lvl_old { get; set; }
        [DataMember]
        public string   mode            { get; set; }

        public override List<string> Column => throw new NotImplementedException();

        public TEMP_DiabetesTABLE() { }
    }
}