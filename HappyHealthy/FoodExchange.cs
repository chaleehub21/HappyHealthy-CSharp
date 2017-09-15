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

namespace HappyHealthyCSharp
{
    [Activity(Label = "FoodExchange")]
    public class FoodExchange : ListActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            SetTheme(Resource.Style.Base_Theme_AppCompat_Light);
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_food_exchange);
            var backBtt = FindViewById<ImageView>(Resource.Id.backBttFoodExchange);
            backBtt.Click +=delegate{
                Finish();
            };
            setListFood("");
            // Create your application here
        }
        public void setListFood(string what_to_search)
        {
            var foodList = new FoodTABLE().getFoodList(what_to_search, this);
            ListAdapter = new SimpleAdapter(this, foodList, Resource.Layout.food_1, new string[] { "food_name", "food_calories", "food_unit", "food_detail" }, new int[] { Resource.Id.food_name, Resource.Id.food_calories, Resource.Id.food_unit, Resource.Id.food_detail });
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