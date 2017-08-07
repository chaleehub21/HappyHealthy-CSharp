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
using Android.Support.V4.View;
using Java.Lang;

namespace HappyHealthyCSharp
{
    class AndroidImageAdapter : PagerAdapter
    {
        Context mContext;
        private int[] sliderImageId = new int[] {
            Resource.Drawable.infopre,
            Resource.Drawable.infokid,
            Resource.Drawable.infodia,
            Resource.Drawable.infodia1,
            Resource.Drawable.infodrink,
            Resource.Drawable.infoeat
        };
        public AndroidImageAdapter(Context c)
        {
            this.mContext = c;
        }
        public override int Count
        {
            get { return sliderImageId.Length; }
        }

        public override bool IsViewFromObject(View view, Java.Lang.Object objectValue)
        {
            return view == objectValue;
        }
        public override Java.Lang.Object InstantiateItem(ViewGroup containter,int position)
        {
            var imageView = new ImageView(mContext);
            imageView.SetImageResource(sliderImageId[position]);
            var viewPager = containter.JavaCast<ViewPager>();
            viewPager.AddView(imageView);
            return imageView;
        }
        public override void DestroyItem(View container, int position, Java.Lang.Object objectValue)
        {
            var viewPager = container.JavaCast<ViewPager>();
            viewPager.RemoveView(objectValue as View);
        }
    }
}