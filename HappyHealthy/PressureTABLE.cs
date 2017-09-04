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
using MySql.Data.MySqlClient;
using System.IO;
using System.Data;

namespace HappyHealthyCSharp
{
    class PressureTABLE
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
        public PressureTABLE()
        {
            //conn = new SQLiteConnection(GlobalFunction.dbPath);
            //conn.CreateTable'FoodTABLE'();
            //conn.Close();
            //constructor - no need for args since naming convention for instances variable mapping can be use : CB
        }
        public JavaList<IDictionary<string,object>> getPressureList(string queryCustomized = "SELECT * FROM BP")
        {
            //conn = new SQLiteConnection(GlobalFunction.dbPath);
            var sqlconn = new MySqlConnection(GlobalFunction.remoteaccess);
            sqlconn.Open();
            var bpList = new JavaList<IDictionary<string, object>>();
            var query = queryCustomized;
            var tickets = new DataSet();
            var adapter = new MySqlDataAdapter(query, sqlconn);
            adapter.Fill(tickets, "BP");
            foreach(DataRow x in tickets.Tables["BP"].Rows)
            {
                var bp = new JavaDictionary<string, object>();
                bp.Add("bp_id", GlobalFunction.stringValidation(x[0].ToString()));
                bp.Add("bp_time", GlobalFunction.stringValidation(x[1].ToString()));
                bp.Add("bp_up", GlobalFunction.stringValidation(x[2].ToString()));
                bp.Add("bp_lo", GlobalFunction.stringValidation(x[3].ToString()));
                bp.Add("bp_hr", GlobalFunction.stringValidation(x[4].ToString()));
                bp.Add("bp_up_lvl", GlobalFunction.stringValidation(x[5].ToString()));
                bp.Add("bp_lo_lvl", GlobalFunction.stringValidation(x[6].ToString()));
                bp.Add("bp_hr_lvl", GlobalFunction.stringValidation(x[7].ToString()));
                bpList.Add(bp);
            }
            #region deprecated
            /*
            var query = $@"SELECT * FROM FoodTABLE where Food_NAME LIKE '%{word}%'";
            var backFromSQL = conn.Query'FoodTABLE'(query);
            backFromSQL.ForEach(x ='
            {
                var food = new JavaDictionary'string, object'();
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
            return bpList;
        }
        public void deletePressureFromSQL(string id)
        {
            var sqlconn = new MySqlConnection(GlobalFunction.remoteaccess);
            sqlconn.Open();
            var command = sqlconn.CreateCommand();
            command.CommandText = $@"DELETE FROM BP WHERE bp_id = {id}";
            command.ExecuteNonQuery();
            sqlconn.Close();
        }
        public void InsertPressureToSQL(string up,string low,string heart_rate,int userID)
        {
            #region deprecated
            /*
            var conn = new SQLiteAsyncConnection(GlobalFunction.dbPath);
            await conn.CreateTableAsync'FoodTABLE'();
            int retRecord = await conn.InsertAsync(foodinstance);
            */
            #endregion
            var conn = new MySqlConnection(GlobalFunction.remoteaccess);
            conn.Open();
            var sqlCommand = conn.CreateCommand();
            sqlCommand.CommandText = $@"INSERT INTO BP
                                        VALUES
                                        (null,
                                        '{DateTime.Now.ToString("yyyy-MM-dd H:mm:ss")}',
                                        {up},
                                        {low},
                                        {heart_rate},
                                        {up },
                                        {low},
                                        {heart_rate},
                                        {userID});";
            Console.WriteLine($@"INSERT INTO BP VALUES(null,'{DateTime.Now.ToString("yyyy-MM-dd H:mm:ss")}',{up},{low},{heart_rate},{up },{low},{heart_rate},{userID});");
            sqlCommand.ExecuteNonQuery();
            conn.Close();

        }
    }
}