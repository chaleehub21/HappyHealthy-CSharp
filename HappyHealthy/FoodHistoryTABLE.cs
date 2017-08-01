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
using SQLite;
namespace HappyHealthyCSharp
{
    class FoodHistoryTABLE
    {
        /* unknown
        private MyDatabase myDatabase;
        private SQLiteDatabase writeSQLite, readSQLite;
        */
        private SQLiteConnection conn = null;
        public static string Food_History = "Food_History";
        public static string History_Food_Id = "History_Food_Id";
        public static string History_Food_Date = "History_Food_Date";
        public static string Food_TotalAmount = "Food_TotalAmount";
        public static string User_Id = "User_Id";
        public static string Food = "Food";
        public static string Food_Id = "Food_Id";
        public static string Food_Unit = "Food_Unit";
        public static string Food_Name = "Food_Name";
        public static string Food_Detail = "Food_Detail";
        public static string Food_Calories = "Food_Calories";
        public static string Food_Protein = "Food_Protein";
        public static string Food_Fat = "Food_Fat";
        public static string Food_Carbohydrate = "Food_Carbohydrate";
        public static string Food_Sugars = "Food_Sugars";
        public static string Food_Sodium = "Food_Sodium";
        public static string Exercise_History = "Exercise_History";
        public static string History_Exercise_Id = "History_Exercise_Id";
        public static string History_Exercise_Date = "History_Exercise_Date";
        public static string Exercise_TotalDuration = "Exercise_TotalDuration";
        public static string Exercise = "Exercise";
        public static string Exercise_Id = "Exercise_Id";
        public static string Exercise_Name = "Exercise_Name";
        public static string Exercise_Calories = "Exercise_Calories";
        public static string Exercise_Duration = "Exercise_Duration";
        public static string Exercise_Disease = "Exercise_Disease";
        public static string Exercise_Detail = "Exercise_Detail";
        public static string Exercise_Description = "Exercise_Description";
        public static string SUM_EX_Cal = "exc";
        public static string SUM_Food_Cal = "fcal";
        public static string SUM_pro = "fpro";
        public static string SUM_fat = "ffat";
        public static string SUM_car = "fcar";
        public static string SUM_sugar = "fsug";
        public static string SUM_sodium = "fsod";
        public static string Total_Cal = "TotalCal";
        
        string HisDate;
        int FoodId;
        double FoodAmount;

        
        public FoodHistoryTABLE()
        {
            conn = new SQLiteConnection(GlobalFunction.dbPath);
        }
        
    }
}