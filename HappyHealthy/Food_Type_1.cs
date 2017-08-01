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
using Android.Support.V7.App;

namespace HappyHealthyCSharp
{
    [Activity]
    public class Food_Type_1 : ListActivity
    {
        ListView listViewFood1;
        FoodTABLE foodTable;
        //List<Dictionary<string, string>> foodList;
        JavaList<IDictionary<string, object>> foodList;
        string foodName, foodUnit, foodNetUnit, foodDetail;
        double foodCalories, foodNetweight, foodProtein, foodFat, foodCarbohydrate, foodSugars, foodSodium;
        int foodAmt, foodId;
        Button btn_search;
        EditText txt_search;
        string word_search;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            SetTheme(Resource.Style.Base_Theme_AppCompat_Light);
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_food__type_1);
            btn_search = FindViewById<Button>(Resource.Id.bFoodSearch);
            txt_search = FindViewById<EditText>(Resource.Id.tv_Sfood);
            var img_back = FindViewById<ImageView>(Resource.Id.imageView44);
           
            foodTable = new FoodTABLE();
            btn_search.Click += delegate {
                word_search = txt_search.Text;
                setListFood(word_search);
            };
            img_back.Click += delegate {
                //StartActivity(new Intent(this, typeof(MainActivity)));
                this.Finish();
            };
            base.ListView.ItemClick += onItemClick;
            setListFood("");
            // Create your application here

        }
        private void onItemClick(object sender,AdapterView.ItemClickEventArgs e)
        {
            foodList[e.Position].TryGetValue("food_id", out object FoodID);
            var intFoodID = Convert.ToInt32(FoodID);
            //GlobalFunction.createDialog(this, intFoodID.ToString()).Show(); //for debugging only
            var foodDetailIntent = new Intent(this, typeof(FoodDetail));
            foodDetailIntent.PutExtra("food_id", intFoodID);
            foodDetailIntent.AddFlags(ActivityFlags.ClearTop);
            StartActivity(foodDetailIntent);


        }
        public void setListFood(string what_to_search)
        {
            foodList = foodTable.getFoodList(what_to_search,this);
            ListAdapter = new SimpleAdapter(this, foodList, Resource.Layout.food_1, new string[] { "food_name", "food_calories", "food_unit", "food_detail" }, new int[] { Resource.Id.food_name,Resource.Id.food_calories,Resource.Id.food_unit,Resource.Id.food_detail});
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