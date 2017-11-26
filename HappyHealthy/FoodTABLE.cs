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
        private string Food { get; set; }
        [SQLite.Net.Attributes.PrimaryKey, SQLite.Net.Attributes.AutoIncrement]
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
            //constructor - no need for args since naming convention for instances variable mapping can be use : CB
        }
        public bool InsertToSQL(FoodTABLE foodinstance)
        {
            try
            {
                var sqlconn = new MySqlConnection(string.Empty);
                var command = sqlconn.CreateCommand();
                command.CommandText = $@"INSERT INTO FoodTABLE VALUES(null,'{foodinstance.Food_NAME}',null,{foodinstance.Food_CAL},'{foodinstance.Food_UNIT}',{foodinstance.Food_NET_WEIGHT},'{foodinstance.Food_NET_UNIT}',{foodinstance.Food_PROTEIN},{foodinstance.Food_FAT},{foodinstance.Food_CARBOHYDRATE},{foodinstance.Food_SUGAR},{foodinstance.Food_SODIUM},'{foodinstance.Food_Detail}');";
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
            var sqlconn = new MySqlConnection(string.Empty);
            sqlconn.Open();
            var foodList = new JavaList<IDictionary<string, object>>();
            var query = $@"SELECT * FROM FoodTABLE where Food_NAME LIKE '%{word}%'";
            var tickets = new DataSet();
            var adapter = new MySqlDataAdapter(query, sqlconn);
            adapter.Fill(tickets, "Food");
            foreach(DataRow x in tickets.Tables["Food"].Rows)
            {
                var food = new JavaDictionary<string, object>();
                food.Add("food_id", Extension.StringValidation(x[0].ToString()));
                food.Add("food_name", Extension.StringValidation(x[1].ToString()));
                food.Add("food_calories", Extension.StringValidation(x[3].ToString()));
                food.Add("food_unit", Extension.StringValidation(x[4].ToString()));
                food.Add("food_netweight", Extension.StringValidation(x[5].ToString()));
                food.Add("food_netunit", Extension.StringValidation(x[6].ToString()));
                food.Add("food_protein", Extension.StringValidation(x[7].ToString()));
                food.Add("food_fat", Extension.StringValidation(x[8].ToString()));
                food.Add("food_carbohydrate", Extension.StringValidation(x[9].ToString()));
                food.Add("food_sugars", Extension.StringValidation(x[10].ToString()));
                food.Add("food_sodium", Extension.StringValidation(x[11].ToString()));
                food.Add("food_detail", Extension.StringValidation(x[12].ToString()));
                foodList.Add(food);
            }
            sqlconn.Close();
            return foodList;
        }
        public Dictionary<string,string> selectDetailByID(int id)
        {
            var sqlconn = new MySqlConnection(string.Empty);
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
                   {"food_id", Extension.StringValidation(x[0].ToString()) },
                   {"food_name", Extension.StringValidation(x[1].ToString()) },
                   {"food_calories", Extension.StringValidation(x[3].ToString()) },
                   {"food_unit", Extension.StringValidation(x[4].ToString()) },
                   {"food_netweight", Extension.StringValidation(x[5].ToString()) },
                   {"food_netunit", Extension.StringValidation(x[6].ToString()) },
                   {"food_protein", Extension.StringValidation(x[7].ToString()) },
                   {"food_fat", Extension.StringValidation(x[8].ToString()) },
                   {"food_carbohydrate", Extension.StringValidation(x[9].ToString()) },
                   {"food_sugars", Extension.StringValidation(x[10].ToString()) },
                   {"food_sodium", Extension.StringValidation(x[11].ToString())},
                   { "food_detail", Extension.StringValidation(x[12].ToString())}
                };
            }
            sqlconn.Close();
            return retValue;
        }
    }
}