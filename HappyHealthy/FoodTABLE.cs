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
using SQLite.Net.Platform.XamarinAndroid;
using SQLite.Net.Attributes;

namespace HappyHealthyCSharp
{
    class FoodTABLE
    {
        public string Food { get; set; }
        [PrimaryKey,AutoIncrement]
        public int Food_ID { get; set; }
        public string Food_NAME { get; set; }
        public string Food_AMT { get; set; }
        public double Food_CAL { get; set; }
        public string Food_UNIT { get; set; }
        public string Food_NET_WEIGHT { get; set; }
        public string Food_NET_UNIT { get; set; }
        public string Food_PROTEIN { get; set; }
        public string Food_FAT { get; set; }
        public string Food_CARBOHYDRATE { get; set; }
        public string Food_SUGAR { get; set; }
        public string Food_SODIUM { get; set; }
        public string Food_Detail { get; set; }
        //private string localPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
        //private string dbPath = "";
        public FoodTABLE()
        {
            var sqliteConn = new SQLite.Net.SQLiteConnection(new SQLitePlatformAndroid(), GlobalFunction.sqliteDBPath);
            sqliteConn.CreateTable<FoodTABLE>();
            sqliteConn.Close();
            //constructor - no need for args since naming convention for instances variable mapping can be use : CB
        }
        public bool InsertToSQL(FoodTABLE foodinstance)
        {
            try
            {
                var sqlconn = new MySqlConnection(GlobalFunction.remoteAccess);
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
        public JavaList<IDictionary<string,object>> getFoodList(string word,Context c = null)
        {
            var sqlconn = new MySqlConnection(GlobalFunction.remoteAccess);
            sqlconn.Open();
            var foodList = new JavaList<IDictionary<string, object>>();
            var query = $@"SELECT * FROM Food where Food_NAME LIKE '%{word}%'";
            var tickets = new DataSet();
            var adapter = new MySqlDataAdapter(query, sqlconn);
            adapter.Fill(tickets, "Food");
            foreach(DataRow x in tickets.Tables["Food"].Rows)
            {
                var food = new JavaDictionary<string, object>();
                food.Add("food_id", GlobalFunction.StringValidation(x[0].ToString()));
                food.Add("food_name", GlobalFunction.StringValidation(x[1].ToString()));
                food.Add("food_calories", GlobalFunction.StringValidation(x[3].ToString()));
                food.Add("food_unit", GlobalFunction.StringValidation(x[4].ToString()));
                food.Add("food_netweight", GlobalFunction.StringValidation(x[5].ToString()));
                food.Add("food_netunit", GlobalFunction.StringValidation(x[6].ToString()));
                food.Add("food_protein", GlobalFunction.StringValidation(x[7].ToString()));
                food.Add("food_fat", GlobalFunction.StringValidation(x[8].ToString()));
                food.Add("food_carbohydrate", GlobalFunction.StringValidation(x[9].ToString()));
                food.Add("food_sugars", GlobalFunction.StringValidation(x[10].ToString()));
                food.Add("food_sodium", GlobalFunction.StringValidation(x[11].ToString()));
                food.Add("food_detail", GlobalFunction.StringValidation(x[12].ToString()));
                foodList.Add(food);
            }
            sqlconn.Close();
            return foodList;
        }
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
            var sqlconn = new MySqlConnection(GlobalFunction.remoteAccess);
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
                   {"food_id", GlobalFunction.StringValidation(x[0].ToString()) },
                   {"food_name", GlobalFunction.StringValidation(x[1].ToString()) },
                   {"food_calories", GlobalFunction.StringValidation(x[3].ToString()) },
                   {"food_unit", GlobalFunction.StringValidation(x[4].ToString()) },
                   {"food_netweight", GlobalFunction.StringValidation(x[5].ToString()) },
                   {"food_netunit", GlobalFunction.StringValidation(x[6].ToString()) },
                   {"food_protein", GlobalFunction.StringValidation(x[7].ToString()) },
                   {"food_fat", GlobalFunction.StringValidation(x[8].ToString()) },
                   {"food_carbohydrate", GlobalFunction.StringValidation(x[9].ToString()) },
                   {"food_sugars", GlobalFunction.StringValidation(x[10].ToString()) },
                   {"food_sodium", GlobalFunction.StringValidation(x[11].ToString())},
                   { "food_detail", GlobalFunction.StringValidation(x[12].ToString())}
                };
            }
            sqlconn.Close();
            return retValue;
        }
    }
}