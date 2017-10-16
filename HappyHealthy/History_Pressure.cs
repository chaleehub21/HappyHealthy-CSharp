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
using Java.Interop;
using Newtonsoft.Json;

namespace HappyHealthyCSharp
{
    [Activity(Label = "History_Diabetes")]
    public class History_Pressure : ListActivity
    {
        ListView listView;
        PressureTABLE bpTable;
        JavaList<IDictionary<string, object>> bpList;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            SetTheme(Resource.Style.Base_Theme_AppCompat_Light);
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_history_pressure);
            //ListView = FindViewById<ListView>(Resource.Id.listView);
            bpTable = new PressureTABLE();
            ListView.ItemClick += onItemClick;
            setDiabetesList();
        }
        protected override void OnResume()
        {
            base.OnResume();
            setDiabetesList();
        }
        private void onItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            //bpList[e.Position].TryGetValue("bp_up", out object bpValue);
            bpList[e.Position].TryGetValue("bp_id", out object bpID);
            var pressureObject = new PressureTABLE();
            pressureObject = pressureObject.Select<PressureTABLE>($"SELECT * From PressureTABLE where bp_id = {bpID}")[0];
            Extension.CreateDialogue(this, $@"The value for this one : {pressureObject.bp_hr}", null, delegate
            {
                var jsonObject = JsonConvert.SerializeObject(pressureObject);
                var Intent = new Intent(this, typeof(Pressure));
                Intent.PutExtra("targetObject", jsonObject);
                StartActivity(Intent);
            }, "OK", "Edit").Show();
        }

        [Export("ClickAddPre")]
        public void ClickAddDia(View v)
        {
            StartActivity(new Intent(this, typeof(Pressure)));
        }
        [Export("ClickBackHisPreHome")]
        public void ClickBackHisDiaHome(View v)
        {
            this.Finish();
        }
        public void setDiabetesList()
        {
            bpList = bpTable.GetJavaList<PressureTABLE>($"SELECT * FROM PressureTABLE WHERE UD_ID = {Extension.getPreference("ud_id", 0, this)} ORDER BY BP_TIME",bpTable.Column);
            //bpList = bpTable.getPressureList($"SELECT * FROM PressureTABLE WHERE UD_ID = {GlobalFunction.getPreference("ud_id", "", this)} ORDER BY BP_TIME");
            ListAdapter = new SimpleAdapter(this, bpList, Resource.Layout.history_diabetes, new string[] { "bp_time" }, new int[] { Resource.Id.date }); //"D_DateTime",date
            ListView.Adapter = ListAdapter;
            /* for reference on how to work with simpleadapter (it's ain't simple as its name, fuck off)
            var data = new JavaList<IDictionary<string, object>>();
            data.Add(new JavaDictionary<string, object> {
                {"name","Bruce Banner" },{ "status","Bruce Banner feels like SMASHING!"}
            });
            var adapter = new SimpleAdapter(this, data, Android.Resource.Layout.SimpleListItem1, new[] { "name","status" }, new[] { Android.Resource.Id.Text1,Android.Resource.Id.Text2 });
            ListView.Adapter = adapter;
            */
        }
    }
}