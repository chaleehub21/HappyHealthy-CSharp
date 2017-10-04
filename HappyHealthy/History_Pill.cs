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
    [Activity(Label = "Pill")]
    public class History_Pill : ListActivity
    {
        PillTABLE pillTable;
        JavaList<IDictionary<string, object>> pillList;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            SetTheme(Resource.Style.Base_Theme_AppCompat_Light);
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_history_pill);
            // Create your application here
            var backbtt = FindViewById<ImageView>(Resource.Id.imageViewbackpill);
            backbtt.Click += delegate {
                this.Finish();
            };
            var addbtt = FindViewById<ImageView>(Resource.Id.imageViewAddPill);
            addbtt.Click += delegate
            {
                StartActivity(typeof(Pill));
            };
            ListView.ItemClick += onItemClick;
            pillTable = new PillTABLE();
        }
        protected override void OnResume()
        {
            base.OnResume();
            setPillList();
        }
        private void onItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            pillList[e.Position].TryGetValue("ma_name", out object pillValue);
            pillList[e.Position].TryGetValue("ma_id", out object pillID);
            GlobalFunction.CreateDialogue(this, $@"The value for this one : {pillValue.ToString()}", null, (EventHandler<DialogClickEventArgs>)delegate {
                GlobalFunction.CreateDialogue(this, "Do you want to delete this row?", (EventHandler<DialogClickEventArgs>)delegate {
                    var pillTable = new PillTABLE();
                    pillTable.Delete<PillTABLE>(Convert.ToInt32((string)pillID.ToString()));
                    setPillList();
                }, delegate { }, "Yes", "No").Show();
            }, "OK", "Delete").Show();
        }
        public void setPillList()
        {
            pillList = pillTable.GetJavaList<PillTABLE>($"SELECT * FROM PillTABLE WHERE UD_ID = {GlobalFunction.getPreference("ud_id", 0, this)}",new PillTABLE().Column);
            //pillList = pillTable.getPillList($"SELECT * FROM PillTABLE WHERE UD_ID = {GlobalFunction.getPreference("ud_id", "", this)}");
            ListAdapter = new SimpleAdapter(this, pillList, Resource.Layout.history_pill, new string[] { "ma_name","ma_desc" }, new int[] { Resource.Id.his_pill_name,Resource.Id.his_pill_desc }); //"D_DateTime",date
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