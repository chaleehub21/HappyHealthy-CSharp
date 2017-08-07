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
    class FoodTABLE
    {
        public string Food { get; set; }
        [PrimaryKey,AutoIncrement]
        public int Food_ID { get; set; }
        public string Food_NAME { get; set; }
        public double Food_CAL { get; set; }
        public string Food_AMT { get; set; }
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
        SQLiteConnection conn = null;
        public FoodTABLE()
        {
            conn = new SQLiteConnection(GlobalFunction.dbPath);
            conn.CreateTable<FoodTABLE>();
            conn.Close();
            //constructor - no need for args since naming convention for instances variable mapping can be use : CB
        }
        public int InsertToSQL(FoodTABLE foodinstance)
        {
            var conn = new SQLiteConnection(GlobalFunction.dbPath);
            conn.CreateTable<FoodTABLE>();
            return conn.Insert(foodinstance);
        }
        public JavaList<IDictionary<string,object>> getFoodList(string word,Context c = null)
        {
            //conn = new SQLiteConnection(GlobalFunction.dbPath);
            var sqlconn = new MySqlConnection(GlobalFunction.remoteaccess);
            sqlconn.Open();
            var foodList = new JavaList<IDictionary<string, object>>();
            var query = $@"SELECT * FROM Food where Food_NAME LIKE '%{word}%'";
            var tickets = new DataSet();
            var adapter = new MySqlDataAdapter(query, sqlconn);
            adapter.Fill(tickets, "Food");
            foreach(DataRow x in tickets.Tables["Food"].Rows)
            {
                var food = new JavaDictionary<string, object>();
                food.Add("food_id", GlobalFunction.stringValidation(x[0].ToString()));
                food.Add("food_name", GlobalFunction.stringValidation(x[1].ToString()));
                food.Add("food_calories", GlobalFunction.stringValidation(x[3].ToString()));
                food.Add("food_unit", GlobalFunction.stringValidation(x[4].ToString()));
                food.Add("food_netweight", GlobalFunction.stringValidation(x[5].ToString()));
                food.Add("food_netunit", GlobalFunction.stringValidation(x[6].ToString()));
                food.Add("food_protein", GlobalFunction.stringValidation(x[7].ToString()));
                food.Add("food_fat", GlobalFunction.stringValidation(x[8].ToString()));
                food.Add("food_carbyhydrate", GlobalFunction.stringValidation(x[9].ToString()));
                food.Add("food_sugars", GlobalFunction.stringValidation(x[10].ToString()));
                food.Add("food_sodium", GlobalFunction.stringValidation(x[11].ToString()));
                food.Add("food_detail", GlobalFunction.stringValidation(x[12].ToString()));
                foodList.Add(food);
            }
            #region SQLiteMethodology
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
        public async void InsertToSQL(FoodTABLE foodinstance,string just_to_make_overload)
        {
            var conn = new SQLiteAsyncConnection(GlobalFunction.dbPath);
            await conn.CreateTableAsync<FoodTABLE>();
            int retRecord = await conn.InsertAsync(foodinstance);
            
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
            return retValue;
        }
    }
}