﻿using System;
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
using MySql.Data.MySqlClient;
using System.IO;
using System.Data;

namespace HappyHealthyCSharp
{
    class DiabetesTABLE
    {
        private string strLastDate;
        public string Diabetes { get; set; }
        [PrimaryKey,AutoIncrement]
        public string D_Id { get; set; }
        public string D_DateTime { get; set; }
        public string D_CostSugar { get; set; }
        public string D_Level { get; set; }
        public string D_Status { get; set; }
        public string D_People { get; set; }
        public string User_Id { get; set; }

        public string Kidney { get; set; }
        public string K_Id { get; set; }
        public string K_DateTime { get; set; }
        public string K_CostGFR { get; set; }
        public string K_LevelCostGFR { get; set; }

        public string Pressure { get; set; }
        public string P_Id { get; set; }
        public string P_DateTime { get; set; }
        public string P_CostPressureDown { get; set; }
        public string P_CostPressureTop { get; set; }
        public string P_Cost_Level_Down { get; set; }
        public string P_Cost_Level_Top { get; set; }
        public string P_HeartRate { get; set; }
        public string P_HeartRate_Level { get; set; }
        SQLiteConnection conn = null;
        public DiabetesTABLE()
        {
            //conn = new SQLiteConnection(GlobalFunction.dbPath);
            //conn.CreateTable<FoodTABLE>();
            //conn.Close();
            //constructor - no need for args since naming convention for instances variable mapping can be use : CB
        }
        public JavaList<IDictionary<string,object>> getDiabetesList(string queryCustomized = "SELECT * FROM FBS")
        {
            //conn = new SQLiteConnection(GlobalFunction.dbPath);
            var sqlconn = new MySqlConnection(GlobalFunction.remoteaccess);
            sqlconn.Open();
            var fbsList = new JavaList<IDictionary<string, object>>();
            var query = queryCustomized;
            var tickets = new DataSet();
            var adapter = new MySqlDataAdapter(query, sqlconn);
            adapter.Fill(tickets, "FBS");
            foreach(DataRow x in tickets.Tables["FBS"].Rows)
            {
                var fbs = new JavaDictionary<string, object>();
                fbs.Add("fbs_id", GlobalFunction.stringValidation(x[0].ToString()));
                fbs.Add("fbs_time", GlobalFunction.stringValidation(x[1].ToString()));
                fbs.Add("fbs_fbs", GlobalFunction.stringValidation(x[2].ToString()));
                fbs.Add("fbs_fbs_lvl", GlobalFunction.stringValidation(x[3].ToString()));
                fbs.Add("ud_id", GlobalFunction.stringValidation(x[4].ToString()));
                fbsList.Add(fbs);
            }
            #region deprecated
            /*
            var query = $@"SELECT * FROM FoodTABLE where Food_NAME LIKE '%{word}%'";
            var backFromSQL = conn.Query<FoodTABLE>(query);
            backFromSQL.ForEach(x =>
            {
                var food = new JavaDictionary<string, object>();
                food.Add("food_id", GlobalFunction.stringValidation(x.Food_ID));
                food.Add("food_name", GlobalFunction.stringValidation(x.Food_NAME));
                food.Add("food_calories", GlobalFunction.stringValidation(x.Food_CAL));
                food.Add("food_unit", GlobalFunction.stringValidation(x.Food_UNIT));
                food.Add("food_netweight", GlobalFunction.stringValidation(x.Food_NET_WEIGHT));
                food.Add("food_netunit", GlobalFunction.stringValidation(x.Food_NET_UNIT));
                food.Add("food_protein", GlobalFunction.stringValidation(x.Food_PROTEIN));
                food.Add("food_fat", GlobalFunction.stringValidation(x.Food_FAT));
                food.Add("food_carbyhydrate", GlobalFunction.stringValidation(x.Food_CARBOHYDRATE));
                food.Add("food_sugars", GlobalFunction.stringValidation(x.Food_SUGAR));
                food.Add("food_sodium", GlobalFunction.stringValidation(x.Food_SODIUM));
                food.Add("food_detail", GlobalFunction.stringValidation(x.Food_Detail));
                foodList.Add(food);
            });
            conn.Close();
            */
            #endregion
            sqlconn.Close();
            return fbsList;
        }
        public void deleteFbsFromSQL(string id)
        {
            var sqlconn = new MySqlConnection(GlobalFunction.remoteaccess);
            sqlconn.Open();
            var command = sqlconn.CreateCommand();
            command.CommandText = $@"DELETE FROM FBS WHERE fbs_id = {id}";
            command.ExecuteNonQuery();
            sqlconn.Close();
        }
        public void InsertFbsToSQL(string BloodValue,int userID)
        {
            #region deprecated
            /*
            var conn = new SQLiteAsyncConnection(GlobalFunction.dbPath);
            await conn.CreateTableAsync<FoodTABLE>();
            int retRecord = await conn.InsertAsync(foodinstance);
            */
            #endregion
            var conn = new MySqlConnection(GlobalFunction.remoteaccess);
            conn.Open();
            var sqlCommand = conn.CreateCommand();
            sqlCommand.CommandText = $@"insert into fbs values(null,'{System.DateTime.Now.ToString("yyyy-MM-dd H:mm:ss")}',{BloodValue},1,{userID})";
            sqlCommand.ExecuteNonQuery();
            conn.Close();

        }
    }
}