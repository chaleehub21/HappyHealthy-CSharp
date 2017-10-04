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

namespace HappyHealthyCSharp
{
    abstract class DatabaseHelper
    {
        virtual public List<T> Select<T>(string query) where T : class {
            var conn = new SQLiteConnection(new SQLitePlatformAndroid(), GlobalFunction.sqliteDBPath);
            var result = conn.Query<T>(query);
            conn.Close();
            return result;
        }
        virtual public bool Insert<T>(T data) where T : class {
            try
            {
                var conn = new SQLiteConnection(new SQLitePlatformAndroid(), GlobalFunction.sqliteDBPath);
                var result = conn.Insert(data);
                conn.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }
        virtual public bool Update<T>(T data) where T : class
        {
            try
            {
                var conn = new SQLiteConnection(new SQLitePlatformAndroid(), GlobalFunction.sqliteDBPath);
                var result = conn.Update(data);
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
                var conn = new SQLiteConnection(new SQLitePlatformAndroid(), GlobalFunction.sqliteDBPath);
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
            var conn = new SQLiteConnection(new SQLitePlatformAndroid(), GlobalFunction.sqliteDBPath);
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
}