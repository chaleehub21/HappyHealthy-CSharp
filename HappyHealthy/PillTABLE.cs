using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MySql.Data.MySqlClient;
using SQLite;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace HappyHealthyCSharp
{
    class PillTABLE
    {
        public PillTABLE()
        {
            //conn = new SQLiteConnection(GlobalFunction.dbPath);
            //conn.CreateTable<FoodTABLE>();
            //conn.Close();
            //constructor - no need for args since naming convention for instances variable mapping can be use : CB
        }
        public JavaList<IDictionary<string,object>> getPillList(string queryCustomized = "SELECT * FROM MED_ALERT")
        {
            //conn = new SQLiteConnection(GlobalFunction.dbPath);
            var sqlconn = new MySqlConnection(GlobalFunction.remoteAccess);
            sqlconn.Open();
            var pillList = new JavaList<IDictionary<string, object>>();
            var query = queryCustomized;
            var tickets = new DataSet();
            var adapter = new MySqlDataAdapter(query, sqlconn);
            adapter.Fill(tickets, "MED_ALERT");
            foreach(DataRow x in tickets.Tables["MED_ALERT"].Rows)
            {
                var pill = new JavaDictionary<string, object>();
                /*
                pill.Add("ma_id", GlobalFunction.StringValidation(x[0].ToString()));
                pill.Add("ma_name", GlobalFunction.StringValidation(x[1].ToString()));
                pill.Add("ma_desc", GlobalFunction.StringValidation(x[2].ToString()));
                pill.Add("ma_set_time", GlobalFunction.StringValidation(x[3].ToString()));
                pill.Add("ma_pic", GlobalFunction.StringValidation(x[4].ToString()));
                */
                pill.Add("ma_id", x[0].ToString().StringValidation());
                pill.Add("ma_name", x[1].ToString().StringValidation());
                pill.Add("ma_desc", x[2].ToString().StringValidation());
                pill.Add("ma_set_time", x[3].ToString().StringValidation());
                pill.Add("ma_pic", x[4].ToString().StringValidation());
                pillList.Add(pill);
            }
            sqlconn.Close();
            return pillList;
        }
        public void deletePillFromSQL(string id)
        {
            var sqlconn = new MySqlConnection(GlobalFunction.remoteAccess);
            sqlconn.Open();
            var command = sqlconn.CreateCommand();
            command.CommandText = $@"DELETE FROM MED_ALERT WHERE MA_ID = {id}";
            command.ExecuteNonQuery();
            sqlconn.Close();
        }
        public void InsertPillToSQL(string medName,string medDesc,DateTime medTime,string picPath,string userID)
        {
            #region deprecated
            /*
            var conn = new SQLiteAsyncConnection(GlobalFunction.dbPath);
            await conn.CreateTableAsync<FoodTABLE>();
            int retRecord = await conn.InsertAsync(foodinstance);
            */
            #endregion
            var conn = new MySqlConnection(GlobalFunction.remoteAccess);
            conn.Open();
            var sqlCommand = conn.CreateCommand();
            sqlCommand.CommandText = $@"insert into med_alert values(null,'{medName}','{medDesc}','{medTime.ToThaiLocale().ToString("H:mm:ss")}','{picPath}',{userID})";
            sqlCommand.ExecuteNonQuery();
            conn.Close();

        }
    }
}