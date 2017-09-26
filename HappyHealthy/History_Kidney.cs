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
        KidneyTABLE kidneyTable;
        JavaList<IDictionary<string, object>> kidneyList;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            SetTheme(Resource.Style.Base_Theme_AppCompat_Light);
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_history_kidney);
            //ListView = FindViewById<ListView>(Resource.Id.listView);
            kidneyTable = new KidneyTABLE();
            ListView.ItemClick += onItemClick;

            // Create your application here
            setKidneyList();
        }
        private void onItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            kidneyList[e.Position].TryGetValue("ckd_gfr_level", out object gfrLevel);
            kidneyList[e.Position].TryGetValue("ckd_id", out object ckdID);
            GlobalFunction.CreateDialogue(this, $@"The value for this one : {gfrLevel.ToString()}", null,
                delegate {
                    GlobalFunction.CreateDialogue(this, "Do you want to delete this row?", delegate {
                        var ckdTable = new KidneyTABLE();
                        ckdTable.deleteKidneyFromSQL(ckdID.ToString());
                        setKidneyList();
                    }, delegate { }, "Yes", "No").Show();
                }, "OK", "Delete").Show();
        }
        protected override void OnResume()
        {
            base.OnResume();
            setKidneyList();
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
            kidneyList = kidneyTable.getKidneyList($"SELECT * FROM CKD WHERE UD_ID = {GlobalFunction.getPreference("ud_id","",this)}"); //must changed
            ListAdapter = new SimpleAdapter(this, kidneyList, Resource.Layout.history_kidney, new string[] { "ckd_time" }, new int[] { Resource.Id.dateKidney }); //"D_DateTime",date
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