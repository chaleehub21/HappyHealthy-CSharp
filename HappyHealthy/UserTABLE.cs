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
    class UserTABLE
    {
        public int ud_id { get; set; }
        public string ud_iden_number { get; set; }
        public string ud_name { get; set; }
        public char ud_gender { get; set; }
        public DateTime ud_birthdate { get; set; }
        public DateTime ud_bf_time { get; set; }
        public DateTime ud_lu_time { get; set; }
        public DateTime ud_dn_time { get; set; }
        public DateTime ud_usually_meal_time { get; set; }
        public string ud_email { get; set; }
        public UserTABLE()
        {
            
        }
        public UserTABLE getUserDetail(object ud_id)
        {
            var conn = new MySqlConnection(GlobalFunction.remoteAccess);
            var sql = $"select * from user_detail where ud_id = @ud_id";
            var cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("ud_id", (string)ud_id);
            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    var rUser = new UserTABLE();
                    rUser.ud_id = reader.IsDBNull(0) ? 0 : Convert.ToInt32(reader.GetString(0));
                    rUser.ud_iden_number = reader.IsDBNull(1)?null: reader.GetString(1);
                    rUser.ud_name = reader.IsDBNull(2) ? null : reader.GetString(2);
                    rUser.ud_gender = reader.IsDBNull(3) ? ' ' : Convert.ToChar(reader.GetString(3));
                    rUser.ud_birthdate = reader.IsDBNull(4)? DateTime.Now: DateTime.Parse(reader.GetString(4));
                    rUser.ud_bf_time = reader.IsDBNull(5) ? DateTime.Now : DateTime.Parse(reader.GetString(5));
                    rUser.ud_dn_time = reader.IsDBNull(6) ? DateTime.Now : DateTime.Parse(reader.GetString(6));
                    rUser.ud_lu_time = reader.IsDBNull(7) ? DateTime.Now : DateTime.Parse(reader.GetString(7));
                    rUser.ud_usually_meal_time = reader.IsDBNull(8) ? DateTime.Now : DateTime.Parse(reader.GetString(8));
                    rUser.ud_email = reader.IsDBNull(9) ? null : reader.GetString(9);
                    return rUser;
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return this;
        }
        public void deleteUserFromSQL(string id)
        {
            var sqlconn = new MySqlConnection(GlobalFunction.remoteAccess);
            sqlconn.Open();
            var command = sqlconn.CreateCommand();
            command.CommandText = $@"DELETE FROM USER_DETAIL WHERE USER_DETAIL = @ud_id";
            command.Parameters.AddWithValue("ud_id", id);
            command.ExecuteNonQuery();
            sqlconn.Close();
        }
        public static bool InsertUserToSQL(string name, char gender, string birthdate, string email, string password, Context c)
        {
            var conn = new MySqlConnection(GlobalFunction.remoteAccess);
            conn.Open();
            var sqlCommand = conn.CreateCommand();
            //sqlCommand.CommandText = $@"insert into fbs values(null,'{System.DateTime.Now.ToString("yyyy-MM-dd H:mm:ss")}',{BloodValue},1,{userID})";
            sqlCommand.CommandText = $@"INSERT INTO `user_detail`(`ud_iden_number`,`ud_name`,`ud_gender`,`ud_birthdate`,`ud_email`,`ud_pass`)VALUES(0,'{name}','{gender}','{birthdate}','{email}','{password}');";
            try
            {
                Console.WriteLine(sqlCommand.CommandText);
                sqlCommand.ExecuteNonQuery();
                return true;
            }
            catch (MySqlException sqlex)
            {
                Console.WriteLine(sqlex.ToString());
                GlobalFunction.CreateDialogue(c,sqlex.ToString()).Show();
                //GlobalFunction.createDialog(c, "ไม่สามารถสมัครสมาชิกด้วยข้อมูลที่คุณระบุได้ กรุณาตรวจสอบข้อมูลอีกครั้ง").Show();
            }
            catch (Exception e)
            {
                GlobalFunction.CreateDialogue(c, e.ToString()).Show();
            }
            conn.Close();
            return false;
        }
    }
}