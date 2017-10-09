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
using SQLite.Net;
using SQLite.Net.Platform.XamarinAndroid;
using MySql.Data.MySqlClient;
using System.Data;

namespace HappyHealthyCSharp
{
    abstract class DatabaseHelper
    {
        public abstract List<string> Column { get; }
        virtual public List<T> Select<T>(string query) where T : class {
            var conn = new SQLiteConnection(new SQLitePlatformAndroid(), Extension.sqliteDBPath);
            var result = conn.Query<T>(query);
            conn.Close();
            return result;
        }
        virtual public bool Insert<T>(T data) where T : class {
            try
            {
                var conn = new SQLiteConnection(new SQLitePlatformAndroid(), Extension.sqliteDBPath);
                var result = conn.Insert(data);
                conn.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }   
        virtual public bool Delete<T>(int id) {
            try
            {
                var conn = new SQLiteConnection(new SQLitePlatformAndroid(), Extension.sqliteDBPath);
                var result = conn.Delete<T>(id);
                conn.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }
        virtual public JavaList<IDictionary<string, object>> GetJavaList<T>(string queryCustomized,List<string> ColumnTags) where T : class
        {
            #region MySQL
            /*
            var sqlconn = new MySqlConnection(GlobalFunction.remoteAccess);
            sqlconn.Open();
            var fbsList = new JavaList<IDictionary<string, object>>();
            var query = queryCustomized;
            var tickets = new DataSet();
            var adapter = new MySqlDataAdapter(query, sqlconn);
            adapter.Fill(tickets, "FBS");
            foreach(DataRow x in tickets.Tables["FBS"].Rows)
            {
                var fbs = new JavaDictionary<string, object>();
                fbs.Add("fbs_id", GlobalFunction.StringValidation(x[0].ToString()));
                fbs.Add("fbs_time", GlobalFunction.StringValidation(x[1].ToString()));
                Console.WriteLine(GlobalFunction.StringValidation(((DateTime)x[1]).ToThaiLocale().ToString()));
                fbs.Add("fbs_fbs", GlobalFunction.StringValidation(x[2].ToString()));
                fbs.Add("fbs_fbs_lvl", GlobalFunction.StringValidation(x[3].ToString()));
                fbs.Add("ud_id", GlobalFunction.StringValidation(x[4].ToString()));
                fbsList.Add(fbs);
            }
            sqlconn.Close();
            */
            #endregion
            var dataList = new JavaList<IDictionary<string, object>>();
            var conn = new SQLiteConnection(new SQLitePlatformAndroid(), Extension.sqliteDBPath);
            var query = queryCustomized;
            var backFromSQL = conn.Query<T>(query);
            backFromSQL.ForEach(x =>
            {
                var data = new JavaDictionary<string, object>();
                foreach(var attr in ColumnTags)
                {
                    if(x.GetType().GetProperty(attr).PropertyType == typeof(DateTime))
                    {
                        data.Add(attr, ((DateTime)x.GetType().GetProperty(attr).GetValue(x)).ToLocalTime());
                    }
                    else
                    {
                        data.Add(attr, x.GetType().GetProperty(attr).GetValue(x));
                    }
                }
                dataList.Add(data);
            });
            conn.Close();
            return dataList;
        }
    }
    static class DatabaseHelperExtension
    {
        public static bool Update(this DatabaseHelper data)
        {
            try
            {
                var conn = new SQLiteConnection(new SQLitePlatformAndroid(), Extension.sqliteDBPath);
                var result = conn.Update(data);
                conn.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static bool CreateSQLiteTableIfNotExists()
        {
            try
            {
                var sqliteConn = new SQLiteConnection(new SQLitePlatformAndroid(), Extension.sqliteDBPath);
                sqliteConn.CreateTable<DiabetesTABLE>();
                sqliteConn.CreateTable<FoodTABLE>();
                sqliteConn.CreateTable<KidneyTABLE>();
                sqliteConn.CreateTable<MedicineTABLE>();
                sqliteConn.CreateTable<PressureTABLE>();
                sqliteConn.CreateTable<UserTABLE>();
                sqliteConn.CreateTable<SummaryDiabetesTABLE>();
                CreateTriggers(sqliteConn);
                sqliteConn.Close();
                return true;
            }catch(Exception e)
            {
                Console.WriteLine(e.ToString());
                return false;
            }
        }
        /// <summary>
        /// Create Triggers for Initial Tables.
        /// </summary>
        private static void CreateTriggers(SQLiteConnection c)
        {
            c.Execute("CREATE TABLE `TEMP_DiabetesTABLE` ( `fbs_time_new` bigint, `fbs_time_old` bigint, `fbs_fbs_new` float, `fbs_fbs_old` float, `fbs_fbs_lvl_new` integer, `fbs_fbs_lvl_old` integer, `MODE` TEXT )");
            c.Execute("CREATE TABLE `TEMP_KidneyTABLE` ( `ckd_time_new` bigint, `ckd_time_old` bigint, `ckd_gfr_new` float, `ckd_gfr_old` float, `ckd_gfr_level_new` INTEGER, `ckd_gfr_level_old` INTEGER, `ckd_creatinine_new` float, `ckd_creatinine_old` float, `ckd_bun_new` float, `ckd_bun_old` float, `ckd_sodium_new` float, `ckd_sodium_old` float, `ckd_potassium_new` float, `ckd_potassium_old` float, `ckd_albumin_blood_new` float, `ckd_albumin_blood_old` float, `ckd_albumin_urine_new` float, `ckd_albumin_urine_old` float, `ckd_phosphorus_blood_new` Float, `ckd_phosphorus_blood_old` Float, `MODE` TEXT )");
            c.Execute("CREATE TABLE `TEMP_PressureTABLE`( `bp_time_new` bigint , `bp_time_old` bigint , `bp_up_new` float , `bp_up_old` float , `bp_lo_new` float , `bp_lo_old` float , `bp_hr_new` integer , `bp_hr_old` integer , `bp_up_lvl_new` integer , `bp_up_lvl_old` integer , `bp_lo_lvl_new` integer , `bp_lo_lvl_old` integer , `bp_hr_lvl_new` integer , `bp_hr_lvl_old` integer , MODE TEXT)");
            c.Execute("CREATE TABLE `TEMP_SummaryDiabetesTABLE` ( `sfbs_time_new` bigint, `sfbs_time_old` bigint, `sfbs_sfbs_new` float, `sfbs_sfbs_old` float, `sfbs_sfbs_lvl_new` INT, `sfbs_sfbs_lvl_old` INT, `MODE` TEXT )");
            c.Execute("CREATE TRIGGER DiabetesTABLE_After_Delete_Trigger AFTER DELETE ON DiabetesTABLE BEGIN insert into TEMP_DiabetesTABLE( fbs_time_old, fbs_fbs_old, fbs_fbs_lvl_old, MODE) values( OLD.fbs_time, OLD.fbs_fbs, OLD.fbs_fbs_lvl, 'D'); END");
            c.Execute("CREATE TRIGGER DiabetesTABLE_After_Insert_Trigger AFTER INSERT ON DiabetesTABLE BEGIN insert into TEMP_DiabetesTABLE( fbs_time_new, fbs_fbs_new, fbs_fbs_lvl_new, MODE) values( NEW.fbs_time, NEW.fbs_fbs, NEW.fbs_fbs_lvl, 'I'); END");
            c.Execute("CREATE TRIGGER DiabetesTABLE_After_Update_Trigger AFTER UPDATE ON DiabetesTABLE BEGIN insert into TEMP_DiabetesTABLE( fbs_time_old, fbs_fbs_old, fbs_fbs_lvl_old, fbs_time_new, fbs_fbs_new, fbs_fbs_lvl_new, MODE) values( OLD.fbs_time, OLD.fbs_fbs, OLD.fbs_fbs_lvl, NEW.fbs_time, NEW.fbs_fbs, NEW.fbs_fbs_lvl, 'U'); END");
            c.Execute("CREATE TRIGGER KidneyTABLE_After_DELETE_Trigger AFTER DELETE ON KidneyTABLE BEGIN INSERT INTO `TEMP_KidneyTABLE`( `ckd_time_old` , `ckd_gfr_old` , `ckd_gfr_level_old` , `ckd_creatinine_old` , `ckd_bun_old` , `ckd_sodium_old` , `ckd_potassium_old` , `ckd_albumin_blood_old` , `ckd_albumin_urine_old` , `ckd_phosphorus_blood_old` , `MODE`) VALUES( OLD.ckd_time , OLD.ckd_gfr , OLD.ckd_gfr_level , OLD.ckd_creatinine , OLD.ckd_bun , OLD.ckd_sodium , OLD.ckd_potassium , OLD.ckd_albumin_blood , OLD.ckd_albumin_urine , OLD.ckd_phosphorus_blood , 'D' ); END");
            c.Execute("CREATE TRIGGER KidneyTABLE_After_Insert_Trigger AFTER INSERT ON KidneyTABLE BEGIN INSERT INTO `TEMP_KidneyTABLE`( `ckd_time_new` , `ckd_gfr_new` , `ckd_gfr_level_new` , `ckd_creatinine_new` , `ckd_bun_new` , `ckd_sodium_new` , `ckd_potassium_new` , `ckd_albumin_blood_new` , `ckd_albumin_urine_new` , `ckd_phosphorus_blood_new` , `MODE`) VALUES( NEW.ckd_time , NEW.ckd_gfr , NEW.ckd_gfr_level , NEW.ckd_creatinine , NEW.ckd_bun , NEW.ckd_sodium , NEW.ckd_potassium , NEW.ckd_albumin_blood , NEW.ckd_albumin_urine , NEW.ckd_phosphorus_blood , 'I' ); END");
            c.Execute("CREATE TRIGGER KidneyTABLE_After_UPDATE_Trigger AFTER UPDATE ON KidneyTABLE BEGIN INSERT INTO `TEMP_KidneyTABLE`( `ckd_time_new` , `ckd_time_old` , `ckd_gfr_new` , `ckd_gfr_old` , `ckd_gfr_level_new` , `ckd_gfr_level_old` , `ckd_creatinine_new` , `ckd_creatinine_old` , `ckd_bun_new` , `ckd_bun_old` , `ckd_sodium_new` , `ckd_sodium_old` , `ckd_potassium_new` , `ckd_potassium_old` , `ckd_albumin_blood_new` , `ckd_albumin_blood_old` , `ckd_albumin_urine_new` , `ckd_albumin_urine_old` , `ckd_phosphorus_blood_new` , `ckd_phosphorus_blood_old` , `MODE`) VALUES( NEW.ckd_time , OLD.ckd_time , NEW.ckd_gfr , OLD.ckd_gfr , NEW.ckd_gfr_level , OLD.ckd_gfr_level , NEW.ckd_creatinine , OLD.ckd_creatinine , NEW.ckd_bun , OLD.ckd_bun , NEW.ckd_sodium , OLD.ckd_sodium , NEW.ckd_potassium , OLD.ckd_potassium , NEW.ckd_albumin_blood , OLD.ckd_albumin_blood , NEW.ckd_albumin_urine , OLD.ckd_albumin_urine , NEW.ckd_phosphorus_blood , OLD.ckd_phosphorus_blood , 'U' ); END");
            c.Execute("CREATE TRIGGER PressureTABLE_After_DELETE_Trigger AFTER DELETE ON PressureTABLE BEGIN Insert into TEMP_PressureTABLE( bp_time_old ,bp_up_old ,bp_lo_old ,bp_hr_old ,bp_up_lvl_old ,bp_lo_lvl_old ,bp_hr_lvl_old ,MODE) values( OLD.bp_time ,OLD.bp_up ,OLD.bp_lo ,OLD.bp_hr ,OLD.bp_up_lvl ,OLD.bp_lo_lvl ,OLD.bp_hr_lvl ,'D'); END");
            c.Execute("CREATE TRIGGER PressureTABLE_After_Insert_Trigger AFTER INSERT ON PressureTABLE BEGIN insert into TEMP_PressureTABLE( bp_time_new ,bp_up_new ,bp_lo_new ,bp_hr_new ,bp_up_lvl_new ,bp_lo_lvl_new ,bp_hr_lvl_new ,MODE) values( NEW.bp_time ,NEW.bp_up ,NEW.bp_lo ,NEW.bp_hr ,NEW.bp_up_lvl ,NEW.bp_lo_lvl ,NEW.bp_hr_lvl ,'I'); END");
            c.Execute("CREATE TRIGGER PressureTABLE_After_UPDATE_Trigger AFTER UPDATE ON PressureTABLE BEGIN Insert into TEMP_PressureTABLE( bp_time_old ,bp_up_old ,bp_lo_old ,bp_hr_old ,bp_up_lvl_old ,bp_lo_lvl_old ,bp_hr_lvl_old ,bp_time_new ,bp_up_new ,bp_lo_new ,bp_hr_new ,bp_up_lvl_new ,bp_lo_lvl_new ,bp_hr_lvl_new ,MODE) values( OLD.bp_time ,OLD.bp_up ,OLD.bp_lo ,OLD.bp_hr ,OLD.bp_up_lvl ,OLD.bp_lo_lvl ,OLD.bp_hr_lvl ,NEW.bp_time ,NEW.bp_up ,NEW.bp_lo ,NEW.bp_hr ,NEW.bp_up_lvl ,NEW.bp_lo_lvl ,NEW.bp_hr_lvl ,'U'); END");
            c.Execute("CREATE TRIGGER SummaryDiabetesTABLE_After_Delete_Trigger AFTER DELETE ON SummaryDiabetesTABLE BEGIN insert into TEMP_SummaryDiabetesTABLE( sfbs_time_old, sfbs_sfbs_old, sfbs_sfbs_lvl_old, MODE) values( OLD.sfbs_time, OLD.sfbs_sfbs, OLD.sfbs_sfbs_lvl, 'D'); END");
            c.Execute("CREATE TRIGGER SummaryDiabetesTABLE_After_Insert_Trigger AFTER INSERT ON SummaryDiabetesTABLE BEGIN insert into TEMP_SummaryDiabetesTABLE( sfbs_time_new, sfbs_sfbs_new, sfbs_sfbs_lvl_new, MODE) values( NEW.sfbs_time, NEW.sfbs_sfbs, NEW.sfbs_sfbs_lvl, 'I'); END");
            c.Execute("CREATE TRIGGER SummaryDiabetesTABLE_After_Update_Trigger AFTER UPDATE ON SummaryDiabetesTABLE BEGIN insert into TEMP_SummaryDiabetesTABLE( sfbs_time_old, sfbs_sfbs_old, sfbs_sfbs_lvl_old, sfbs_time_new, sfbs_sfbs_new, sfbs_sfbs_lvl_new, MODE) values( OLD.sfbs_time, OLD.sfbs_sfbs, OLD.sfbs_sfbs_lvl, NEW.sfbs_time, NEW.sfbs_sfbs, NEW.sfbs_sfbs_lvl, 'U'); END");
        }
    }
}