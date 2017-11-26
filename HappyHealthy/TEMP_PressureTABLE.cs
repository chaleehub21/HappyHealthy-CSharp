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
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace HappyHealthyCSharp
{
    [Serializable]
    [DataContract]
    class TEMP_PressureTABLE : DatabaseHelper
    {
        [DataMember]
        public int      bp_id_pointer   { get; set; }
        [DataMember]
        public DateTime bp_time_new     { get; set; }
        [DataMember]
        public DateTime bp_time_old     { get; set; }
        [DataMember]
        public string   bp_time_string_new { get; set; }
        [DataMember]
        public int      bp_up_new     { get; set; }
        [DataMember]
        public int      bp_up_old     { get; set; }
        [DataMember]
        public int      bp_lo_new     { get; set; }
        [DataMember]
        public int      bp_lo_old     { get; set; }
        [DataMember]
        public int      bp_hr_new     { get; set; }
        [DataMember]
        public int      bp_hr_old     { get; set; }
        [DataMember]
        public int      bp_up_lvl_new { get; set; }
        [DataMember]
        public int      bp_up_lvl_old { get; set; }
        [DataMember]
        public int      bp_lo_lvl_new { get; set; }
        [DataMember]
        public int      bp_lo_lvl_old { get; set; }
        [DataMember]
        public int      bp_hr_lvl_new { get; set; }
        [DataMember]
        public int      bp_hr_lvl_old { get; set; }
        [DataMember]
        public string   mode          { get; set; }

        public override List<string> Column => throw new NotImplementedException();
    }
}