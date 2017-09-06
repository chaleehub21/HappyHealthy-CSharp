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
    class KidneyTABLE
    {
        public KidneyTABLE()
        {
            //conn = new SQLiteConnection(GlobalFunction.dbPath);
            //conn.CreateTable<FoodTABLE>();
            //conn.Close();
            //constructor - no need for args since naming convention for instances variable mapping can be use : CB
        }
        public JavaList<IDictionary<string,object>> getKidneyList(string queryCustomized = "SELECT * FROM CKD")
        {
            //conn = new SQLiteConnection(GlobalFunction.dbPath);
            var sqlconn = new MySqlConnection(GlobalFunction.remoteaccess);
            sqlconn.Open();
            var ckdList = new JavaList<IDictionary<string, object>>();
            var query = queryCustomized;
            var tickets = new DataSet();
            var adapter = new MySqlDataAdapter(query, sqlconn);
            adapter.Fill(tickets, "CKD");
            foreach(DataRow x in tickets.Tables["CKD"].Rows)
            {
                var ckd = new JavaDictionary<string, object>();
                ckd.Add("ckd_id", GlobalFunction.stringValidation(x[0].ToString()));
                ckd.Add("ckd_time", GlobalFunction.stringValidation(x[1].ToString()));
                ckd.Add("ckd_gfr", GlobalFunction.stringValidation(x[2].ToString()));
                ckd.Add("ckd_gfr_level", GlobalFunction.stringValidation(x[3].ToString()));
                ckd.Add("ckd_creatinine", GlobalFunction.stringValidation(x[4].ToString()));
                ckd.Add("ckd_bun", GlobalFunction.stringValidation(x[5].ToString()));
                ckd.Add("ckd_sodium", GlobalFunction.stringValidation(x[6].ToString()));
                ckd.Add("ckd_potassium", GlobalFunction.stringValidation(x[7].ToString()));
                ckd.Add("ckd_albumin_blood", GlobalFunction.stringValidation(x[8].ToString()));
                ckd.Add("ckd_albumin_urine", GlobalFunction.stringValidation(x[9].ToString()));
                ckd.Add("ckd_phosphorus_blood", GlobalFunction.stringValidation(x[10].ToString()));
                //ckd.Add("", GlobalFunction.stringValidation(x[].ToString()));
                ckdList.Add(ckd);
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
            return ckdList;
        }
        public void deleteKidneyFromSQL(string id)
        {
            var sqlconn = new MySqlConnection(GlobalFunction.remoteaccess);
            sqlconn.Open();
            var command = sqlconn.CreateCommand();
            command.CommandText = $@"DELETE FROM CKD WHERE CKD_ID = {id}";
            command.ExecuteNonQuery();
            sqlconn.Close();
        }
        public void InsertKidneyToSQL(string gfr,string creatinine,string bun,string sodium,string potassium,string alb_blood,string alb_urine,string phos_blood,int userID)
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
            var gfrLevel = 0;
            sqlCommand.CommandText = $@"INSERT INTO CKD
                                    (`ckd_id`,
                                    `ckd_time`,
                                    `ckd_gfr`,
                                    `ckd_gfr_level`,
                                    `ckd_creatinine`,
                                    `ckd_bun`,
                                    `ckd_sodium`,
                                    `ckd_potassium`,
                                    `ckd_albumin_blood`,
                                    `ckd_albumin_urine`,
                                    `ckd_phosphorus_blood`,
                                    `ud_id`)
                                    VALUES
                                    (null,
                                    '{System.DateTime.Now.ToString("yyyy-MM-dd H:mm:ss")}',
                                    {gfr},
                                    {gfrLevel},
                                    {creatinine},
                                    {bun},
                                    {sodium},
                                    {potassium},
                                    {alb_blood },
                                    {alb_urine},
                                    {phos_blood},
                                    {userID});";
            sqlCommand.ExecuteNonQuery();
            conn.Close();

        }
    }
}