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
        public bool InsertToSQL(FoodTABLE foodinstance)
        {
            #region deprecated
            /*
            var conn = new SQLiteConnection(GlobalFunction.dbPath);
            conn.CreateTable<FoodTABLE>();
            return conn.Insert(foodinstance);
            */
            #endregion
            try
            {
                var sqlconn = new MySqlConnection(GlobalFunction.remoteaccess);
                var command = sqlconn.CreateCommand();
                command.CommandText = $@"INSERT INTO FOOD VALUES(null,'{foodinstance.Food_NAME}',null,{foodinstance.Food_CAL},'{foodinstance.Food_UNIT}',{foodinstance.Food_NET_WEIGHT},'{foodinstance.Food_NET_UNIT}',{foodinstance.Food_PROTEIN},{foodinstance.Food_FAT},{foodinstance.Food_CARBOHYDRATE},{foodinstance.Food_SUGAR},{foodinstance.Food_SODIUM},'{foodinstance.Food_Detail}');";
                sqlconn.Open();
                command.ExecuteNonQuery();
                return true;
            }
            catch {
                return false;
            }
        }
        public JavaList<IDictionary<string,object>> getDiabetesList(Context c = null)
        {
            //conn = new SQLiteConnection(GlobalFunction.dbPath);
            var sqlconn = new MySqlConnection(GlobalFunction.remoteaccess);
            sqlconn.Open();
            var foodList = new JavaList<IDictionary<string, object>>();
            var query = $@"SELECT * FROM Food";
            var tickets = new DataSet();
            var adapter = new MySqlDataAdapter(query, sqlconn);
            adapter.Fill(tickets, "Food");
            foreach(DataRow x in tickets.Tables["Food"].Rows)
            {
                var food = new JavaDictionary<string, object>();
                food.Add("D_DateTime", GlobalFunction.stringValidation(DateTime.Now));
                food.Add("food_id", GlobalFunction.stringValidation(x[0].ToString()));
                food.Add("food_name", GlobalFunction.stringValidation(x[1].ToString()));
                food.Add("food_calories", GlobalFunction.stringValidation(x[3].ToString()));
                food.Add("food_unit", GlobalFunction.stringValidation(x[4].ToString()));
                food.Add("food_netweight", GlobalFunction.stringValidation(x[5].ToString()));
                food.Add("food_netunit", GlobalFunction.stringValidation(x[6].ToString()));
                food.Add("food_protein", GlobalFunction.stringValidation(x[7].ToString()));
                food.Add("food_fat", GlobalFunction.stringValidation(x[8].ToString()));
                food.Add("food_carbohydrate", GlobalFunction.stringValidation(x[9].ToString()));
                food.Add("food_sugars", GlobalFunction.stringValidation(x[10].ToString()));
                food.Add("food_sodium", GlobalFunction.stringValidation(x[11].ToString()));
                food.Add("food_detail", GlobalFunction.stringValidation(x[12].ToString()));
                foodList.Add(food);
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
            return foodList;
        }
        [System.Obsolete]
        public void InsertToSQL(string insert_statement)
        {
            #region deprecated
            /*
            var conn = new SQLiteAsyncConnection(GlobalFunction.dbPath);
            await conn.CreateTableAsync<FoodTABLE>();
            int retRecord = await conn.InsertAsync(foodinstance);
            */
            #endregion
            var sqlconn = new MySqlConnection(GlobalFunction.remoteaccess);
            var command = sqlconn.CreateCommand();
            command.CommandText = $@"INSERT INTO FOOD VALUES()";
            sqlconn.Open();
            command.ExecuteNonQuery();

        } //experiment method
        public Dictionary<string,string> selectDetailByID(int id)
        {
            #region deprecated
            /*
            conn = new SQLiteConnection(GlobalFunction.dbPath);
            var data = conn.Query<FoodTABLE>($@"SELECT * FROM FoodTABLE where Food_ID = {id}");
            var retValue = new Dictionary<string, string>() {
                {"food_id", GlobalFunction.stringValidation(data[0].Food_ID) },
                { "food_name", GlobalFunction.stringValidation(data[0].Food_NAME)},
                {"food_calories", GlobalFunction.stringValidation(data[0].Food_CAL) },
                {"food_unit", GlobalFunction.stringValidation(data[0].Food_UNIT)},
                {"food_netweight", GlobalFunction.stringValidation(data[0].Food_NET_WEIGHT)},
                {"food_netunit", GlobalFunction.stringValidation(data[0].Food_NET_UNIT)},
                {"food_protein", GlobalFunction.stringValidation(data[0].Food_PROTEIN)},
                {"food_fat", GlobalFunction.stringValidation(data[0].Food_FAT)},
                {"food_carbohydrate", GlobalFunction.stringValidation(data[0].Food_CARBOHYDRATE)},
                {"food_sugars", GlobalFunction.stringValidation(data[0].Food_SUGAR)},
                {"food_sodium", GlobalFunction.stringValidation(data[0].Food_SODIUM)},
                {"food_detail", GlobalFunction.stringValidation(data[0].Food_Detail)}

            };
            conn.Close();
            return retValue;
            */
            #endregion
            var sqlconn = new MySqlConnection(GlobalFunction.remoteaccess);
            sqlconn.Open();
            var query = $@"SELECT * FROM Food where Food_ID = {id}";
            var tickets = new DataSet();
            var adapter = new MySqlDataAdapter(query, sqlconn);
            adapter.Fill(tickets, "Food");
            var retValue = new Dictionary<string, string>();
            foreach (DataRow x in tickets.Tables["Food"].Rows)
            {
                retValue = new Dictionary<string, string>()
                {
                   {"food_id", GlobalFunction.stringValidation(x[0].ToString()) },
                   {"food_name", GlobalFunction.stringValidation(x[1].ToString()) },
                   {"food_calories", GlobalFunction.stringValidation(x[3].ToString()) },
                   {"food_unit", GlobalFunction.stringValidation(x[4].ToString()) },
                   {"food_netweight", GlobalFunction.stringValidation(x[5].ToString()) },
                   {"food_netunit", GlobalFunction.stringValidation(x[6].ToString()) },
                   {"food_protein", GlobalFunction.stringValidation(x[7].ToString()) },
                   {"food_fat", GlobalFunction.stringValidation(x[8].ToString()) },
                   {"food_carbohydrate", GlobalFunction.stringValidation(x[9].ToString()) },
                   {"food_sugars", GlobalFunction.stringValidation(x[10].ToString()) },
                   {"food_sodium", GlobalFunction.stringValidation(x[11].ToString())},
                   { "food_detail", GlobalFunction.stringValidation(x[12].ToString())}
                };
            }
            sqlconn.Close();
            return retValue;
        }
    }
}