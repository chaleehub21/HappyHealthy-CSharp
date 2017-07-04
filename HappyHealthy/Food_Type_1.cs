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
        List<string> foodList;
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
            foodTable = new FoodTABLE();
            btn_search.Click += delegate {
                word_search = txt_search.Text;
                setListFood(word_search);
            };
            // Create your application here

        }
        public void setListFood(string what_to_search)
        {
            foodList = foodTable.getFoodList(what_to_search);
            var datasource = foodList.ToArray();
            ListAdapter = new ArrayAdapter<string>(this, global::Android.Resource.Layout.SimpleListItem1,datasource);
            ListView.Adapter = ListAdapter;
        }
    }
}