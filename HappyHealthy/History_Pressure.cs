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
            bpList[e.Position].TryGetValue("bp_up", out object bpValue);
            bpList[e.Position].TryGetValue("bp_id", out object bpID);
            GlobalFunction.createDialog(this, $@"The value for this one : {bpValue.ToString()}", null,(EventHandler<DialogClickEventArgs>)delegate {
                    GlobalFunction.createDialog(this, "Do you want to delete this row?", (EventHandler<DialogClickEventArgs>)delegate {
                        var bpTable = new PressureTABLE();
                        bpTable.deletePressureFromSQL((string)bpID.ToString());
                        setDiabetesList();
                    }, delegate { }, "Yes", "No").Show();
                }, "OK", "Delete").Show();
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
            bpList = bpTable.getPressureList("SELECT * FROM BP ORDER BY BP_TIME");
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