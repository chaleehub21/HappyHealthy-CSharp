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
    public class History_Diabetes : ListActivity
    {
        ListView listView;
        DiabetesTABLE diaTable;
        JavaList<IDictionary<string, object>> diabList;
        string[] Choice;
        string DateDiabetes, Level, CostStatus, People;
        int D_id, Cost1Diabetes;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            SetTheme(Resource.Style.Base_Theme_AppCompat_Light);
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_history_diabetes);
            //ListView = FindViewById<ListView>(Resource.Id.listView);
            diaTable = new DiabetesTABLE();
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
            diabList[e.Position].TryGetValue("fbs_fbs", out object fbsValue);
            diabList[e.Position].TryGetValue("fbs_id", out object fbsID);
            Extension.CreateDialogue(this, $@"The value for this one : {fbsValue.ToString()}",null,(EventHandler<DialogClickEventArgs>)delegate {
                var diaObject = new DiabetesTABLE();
                diaObject = diaObject.Select<DiabetesTABLE>($@"SELECT * FROM DiabetesTABLE WHERE fbs_id = {e.Position}")[0];
                var diabetesIntent = new Intent(this, typeof(Diabetes));
                //diabetesIntent.PutExtra("fbs_id", $"{diaObject.fbs_id}");
                //diabetesIntent.PutExtra("fbs_fbs", $"{diaObject.fbs_fbs}");
            },"OK","Edit").Show();
        }
        
        [Export("ClickAddDia")]
        public void ClickAddDia(View v)
        {
            StartActivity(new Intent(this, typeof(Diabetes)));
        }
        [Export("ClickBackHisDiaHome")]
        public void ClickBackHisDiaHome(View v)
        {
            this.Finish();
        }
        public void setDiabetesList()
        {
            diabList = diaTable.GetJavaList<DiabetesTABLE>($"SELECT * FROM DiabetesTABLE WHERE UD_ID = {Extension.getPreference("ud_id", 0, this)} ORDER BY FBS_TIME",new DiabetesTABLE().Column);
            ListAdapter = new SimpleAdapter(this, diabList, Resource.Layout.history_diabetes, new string[] { "fbs_time" }, new int[] { Resource.Id.date }); //"D_DateTime",date
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