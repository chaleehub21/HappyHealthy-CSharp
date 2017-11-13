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
using System.Threading.Tasks;
using Android.Content.Res;

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
        public static Bitmap Resize(Context mContext,int imageID,int width,int height)
        {
            BitmapFactory.Options options = BitmapHelpers.GetBitmapOptionsOfImage(mContext, imageID);
            Bitmap bitmapToDisplay = BitmapHelpers.LoadScaledDownBitmapForDisplay(mContext.Resources, options, imageID, width,height);
            return bitmapToDisplay;
        }
        private static int CalculateInSampleSize(BitmapFactory.Options options, int reqWidth, int reqHeight)
        {
            // Raw height and width of image
            float height = options.OutHeight;
            float width = options.OutWidth;
            double inSampleSize = 1D;

            if (height > reqHeight || width > reqWidth)
            {
                int halfHeight = (int)(height / 2);
                int halfWidth = (int)(width / 2);

                // Calculate a inSampleSize that is a power of 2 - the decoder will use a value that is a power of two anyway.
                while ((halfHeight / inSampleSize) > reqHeight && (halfWidth / inSampleSize) > reqWidth)
                {
                    inSampleSize *= 2;
                }

            }

            return (int)inSampleSize;
        }

        private static Bitmap LoadScaledDownBitmapForDisplay(Resources res, BitmapFactory.Options options,int resourceID, int reqWidth, int reqHeight)
        {
            // Calculate inSampleSize
            options.InSampleSize = CalculateInSampleSize(options, reqWidth, reqHeight);

            // Decode bitmap with inSampleSize set
            options.InJustDecodeBounds = false;

            return BitmapFactory.DecodeResource(res, resourceID, options);
        }

        public static BitmapFactory.Options GetBitmapOptionsOfImage(Context c,int resourceID)
        {
            BitmapFactory.Options options = new BitmapFactory.Options
            {
                InJustDecodeBounds = true
            };

            // The result will be null because InJustDecodeBounds == true.
            Bitmap result = BitmapFactory.DecodeResource(c.Resources, resourceID, options);


            int imageHeight = options.OutHeight;
            int imageWidth = options.OutWidth;
            return options;
        }
    }
}