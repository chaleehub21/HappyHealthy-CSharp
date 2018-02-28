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
using Java.Interop;

namespace HappyHealthyCSharp
{
    [Activity]
    public class Add_Food : Activity
    {
        EditText foodname;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            SetTheme(Resource.Style.Base_Theme_AppCompat_Light);
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_add__food);
            // Create your application here
            foodname = FindViewById<EditText>(Resource.Id.f_name);
            var saveButton = FindViewById<ImageView>(Resource.Id.buttonsavefoodex);
            var backButton = FindViewById<ImageView>(Resource.Id.imageView46);
            saveButton.Click += RequestFood;
            backButton.Click += delegate { Finish(); };
        }

        private void RequestFood(object sender, EventArgs e)
        {
            var service = new HHCSService.HHCSService();
            var result = service.FoodRequest(Service.GetInstance.WebServiceAuthentication,foodname.Text);
            if (result == true)
                Extension.CreateDialogue(this, "รายการอาหารของคุณได้ทำการเพิ่มเข้าสู่ระบบพิจารณาจากผู้ดูแล", delegate
                {
                    Finish();
                }).Show();
            else
                Extension.CreateDialogue(this, "เกิดความผิดพลาดในการขอเพิ่มรายการอาหาร กรุณาลองใหม่อีกครั้งในภายหลัง").Show();
        }

        [Export("ClickAddFoodBack")]
        public void ClickAddFoodBack(View v)
        {
            this.Finish();
        }
    }
}