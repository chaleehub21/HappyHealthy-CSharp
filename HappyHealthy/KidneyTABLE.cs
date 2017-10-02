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
using SQLiteNetExtensions.Attributes;
using SQLite.Net.Platform.XamarinAndroid;
using SQLite.Net.Attributes;
using SQLite.Net;

namespace HappyHealthyCSharp
{
    class KidneyTABLE
    {
        [PrimaryKey, AutoIncrement]
        public int ckd_id { get; set; }
        public DateTime ckd_time { get; set; }
        public decimal ckd_gfr { get; set; }
        public int ckd_gfr_level { get; set; }
        public decimal ckd_creatinine { get; set; }
        public decimal ckd_bun { get; set; }
        public decimal ckd_sodium { get; set; }
        public decimal ckd_potassium { get; set; }
        public decimal ckd_albumin_blood { get; set; }
        public decimal ckd_albumin_urine { get; set; }
        public decimal ckd_phosphorus_blood { get; set; }
        [ForeignKey(typeof(UserTABLE))]
        public int ud_id { get; set; }
        [ManyToOne]
        public UserTABLE UserTABLE { get; set; }
        //reconstruct of sqlite keys + attributes
        public KidneyTABLE()
        {
            var sqliteConn = new SQLite.Net.SQLiteConnection(new SQLitePlatformAndroid(), GlobalFunction.sqliteDBPath);
            sqliteConn.CreateTable<KidneyTABLE>();
            sqliteConn.Close();
            //constructor - no need for args since naming convention for instances variable mapping can be use : CB
        }
        public JavaList<IDictionary<string,object>> getKidneyList(string queryCustomized = "SELECT * FROM KidneyTABLE")
        {
            /*
            var sqlconn = new MySqlConnection(GlobalFunction.remoteAccess);
            sqlconn.Open();
            var ckdList = new JavaList<IDictionary<string, object>>();
            var query = queryCustomized;
            var tickets = new DataSet();
            var adapter = new MySqlDataAdapter(query, sqlconn);
            adapter.Fill(tickets, "CKD");
            foreach(DataRow x in tickets.Tables["CKD"].Rows)
            {
                var ckd = new JavaDictionary<string, object>();
                ckd.Add("ckd_id", GlobalFunction.StringValidation(x[0].ToString()));
                ckd.Add("ckd_time", GlobalFunction.StringValidation(x[1].ToString()));
                ckd.Add("ckd_gfr", GlobalFunction.StringValidation(x[2].ToString()));
                ckd.Add("ckd_gfr_level", GlobalFunction.StringValidation(x[3].ToString()));
                ckd.Add("ckd_creatinine", GlobalFunction.StringValidation(x[4].ToString()));
                ckd.Add("ckd_bun", GlobalFunction.StringValidation(x[5].ToString()));
                ckd.Add("ckd_sodium", GlobalFunction.StringValidation(x[6].ToString()));
                ckd.Add("ckd_potassium", GlobalFunction.StringValidation(x[7].ToString()));
                ckd.Add("ckd_albumin_blood", GlobalFunction.StringValidation(x[8].ToString()));
                ckd.Add("ckd_albumin_urine", GlobalFunction.StringValidation(x[9].ToString()));
                ckd.Add("ckd_phosphorus_blood", GlobalFunction.StringValidation(x[10].ToString()));
                //ckd.Add("", GlobalFunction.stringValidation(x[].ToString()));
                ckdList.Add(ckd);
            }
            sqlconn.Close();
            */
            var ckdList = new JavaList<IDictionary<string, object>>();
            var conn = new SQLiteConnection(new SQLitePlatformAndroid(),GlobalFunction.sqliteDBPath);
            var query = queryCustomized;
            var backFromSQL = conn.Query<KidneyTABLE>(query);
            backFromSQL.ForEach(x =>
            {
                var ckd = new JavaDictionary<string, object>();
                ckd.Add("ckd_id", GlobalFunction.StringValidation(x.ckd_id));
                ckd.Add("ckd_time", GlobalFunction.StringValidation(x.ckd_time.ToLocalTime()));
                ckd.Add("ckd_gfr", GlobalFunction.StringValidation(x.ckd_gfr));
                ckd.Add("ckd_gfr_level", GlobalFunction.StringValidation(x.ckd_gfr_level));
                ckd.Add("ckd_creatinine", GlobalFunction.StringValidation(x.ckd_creatinine));
                ckd.Add("ckd_bun", GlobalFunction.StringValidation(x.ckd_bun));
                ckd.Add("ckd_sodium", GlobalFunction.StringValidation(x.ckd_sodium));
                ckd.Add("ckd_potassium", GlobalFunction.StringValidation(x.ckd_potassium));
                ckd.Add("ckd_albumin_blood", GlobalFunction.StringValidation(x.ckd_albumin_blood));
                ckd.Add("ckd_albumin_urine", GlobalFunction.StringValidation(x.ckd_albumin_urine));
                ckd.Add("ckd_phosphorus_blood", GlobalFunction.StringValidation(x.ckd_phosphorus_blood));
                ckdList.Add(ckd);
            });
            conn.Close();
            return ckdList;
        }
        public void deleteKidneyFromSQL(string id)
        {
            /*
            var sqlconn = new MySqlConnection(GlobalFunction.remoteAccess);
            sqlconn.Open();
            var command = sqlconn.CreateCommand();
            command.CommandText = $@"DELETE FROM CKD WHERE CKD_ID = {id}";
            command.ExecuteNonQuery();
            sqlconn.Close();
            */
            var conn = new SQLiteConnection(new SQLitePlatformAndroid(), GlobalFunction.sqliteDBPath);
            conn.Delete<KidneyTABLE>(id);
            conn.Close();
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
            var conn = new MySqlConnection(GlobalFunction.remoteAccess);
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
                                    '{System.DateTime.Now.ToThaiLocale().ToString("yyyy-MM-dd H:mm:ss")}',
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
        public void InsertKidneyToSQL(KidneyTABLE data)
        {
            var conn = new SQLite.Net.SQLiteConnection(new SQLitePlatformAndroid(), GlobalFunction.sqliteDBPath);
            conn.Insert(data);
            try
            {
                InsertKidneyToSQL(data.ckd_gfr, data.ckd_creatinine, data.ckd_bun, data.ckd_sodium, data.ckd_potassium, data.ckd_albumin_blood, data.ckd_albumin_urine, data.ckd_phosphorus_blood, data.ud_id);
            }
            catch
            {
                //pass
            }
            conn.Close();
        }

        private void InsertKidneyToSQL(decimal ckd_gfr, decimal ckd_creatinine, decimal ckd_bun, decimal ckd_sodium, decimal ckd_potassium, decimal ckd_albumin_blood, decimal ckd_albumin_urine, decimal ckd_phosphorus_blood, int ud_id)
        {
            var conn = new MySqlConnection(GlobalFunction.remoteAccess);
            conn.Open();
            var sqlCommand = conn.CreateCommand();
            var ckd_gfr_lvl = 0;
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
                                    '{System.DateTime.Now.ToThaiLocale().ToString("yyyy-MM-dd H:mm:ss")}',
                                    {ckd_gfr},
                                    {ckd_gfr_lvl},
                                    {ckd_creatinine},
                                    {ckd_bun},
                                    {ckd_sodium},
                                    {ckd_potassium},
                                    {ckd_albumin_blood },
                                    {ckd_albumin_urine},
                                    {ckd_phosphorus_blood},
                                    {ud_id});";
            sqlCommand.ExecuteNonQuery();
            conn.Close();
        }
    }
}