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
using Android.Graphics;

namespace HappyHealthyCSharp
{
    public static class BitmapHelpers
    {
        public static Bitmap LoadBitmap(this string fileName, int width, int height)
        {
            var options = new BitmapFactory.Options
            {
                InJustDecodeBounds = true
            };
            BitmapFactory.DecodeFile(fileName, options);
            int outHeight = options.OutHeight;
            int outWidth = options.OutWidth;
            int inSampleSize = 1;
            if (outHeight > height || outWidth > width)
            {
                inSampleSize = outWidth > outHeight ? outHeight / height : outWidth / width;
            }
            options.InSampleSize = inSampleSize;
            options.InJustDecodeBounds = false;
            var resizedBitmap = BitmapFactory.DecodeFile(fileName, options);
            return resizedBitmap;
        }
    }
}