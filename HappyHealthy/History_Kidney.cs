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
    public class History_Kidney : ListActivity
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
            SetContentView(Resource.Layout.activity_history_kidney);
            //ListView = FindViewById<ListView>(Resource.Id.listView);
            diaTable = new DiabetesTABLE();
            ListView.ItemClick += onItemClick;

            // Create your application here
            setKidneyList();
        }

        private void onItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            GlobalFunction.createDialog(this, e.Position.ToString()).Show();
        }

        [Export("ClickAddKid")]
        public void ClickAddKid(View v)
        {
            StartActivity(new Intent(this, typeof(Kidney)));
        }
        [Export("ClickBackHisKidHome")]
        public void ClickBackHisKidHome(View v)
        {
            this.Finish();
        }
        public void setKidneyList()
        {
            diabList = diaTable.getDiabetesList(); //must changed
            ListAdapter = new SimpleAdapter(this, diabList, Resource.Layout.history_kidney, new string[] { "D_DateTime" }, new int[] { Resource.Id.dateKidney }); //"D_DateTime",date
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