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
using MySql.Data.MySqlClient;
using System.IO;
using System.Data;

namespace HappyHealthyCSharp
{
    class RegisterTABLE
    {
        public RegisterTABLE()
        {
            //conn = new SQLiteConnection(GlobalFunction.dbPath);
            //conn.CreateTable<FoodTABLE>();
            //conn.Close();
            //constructor - no need for args since naming convention for instances variable mapping can be use : CB
        }
        public int getLastestIDNO()
        {
            var conn = new MySqlConnection(GlobalFunction.remoteaccess);
            conn.Open();
            var sql = "SELECT MAX(ud_iden_number) FROM USER_DETAIL";
            var cmd = new MySqlCommand(sql, conn);
            var result = cmd.ExecuteScalar();
            if (result != null)
            {
                return Convert.ToInt32(result);
            }
            return 0;
        }
        public void InsertRegToSQL(int idNo, string name, char gender, string birthdate, string email, string password, Context c)
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
            //sqlCommand.CommandText = $@"insert into fbs values(null,'{System.DateTime.Now.ToString("yyyy-MM-dd H:mm:ss")}',{BloodValue},1,{userID})";
            sqlCommand.CommandText = $@"INSERT INTO `user_detail`
                                        (`ud_iden_number`,
                                        `ud_name`,
                                        `ud_gender`,
                                        `ud_birthdate`,
                                        `ud_email`,
                                        `ud_pass`)
                                        VALUES
                                        ({idNo},
                                        '{name}',
                                        '{gender}',
                                        '{birthdate}',
                                        '{email}',
                                        '{password}');";
            try
            {
                sqlCommand.ExecuteNonQuery();
            }
            catch (MySqlException)
            {
                GlobalFunction.createDialog(c, "ไม่สามารถสมัครสมาชิกด้วยข้อมูลที่คุณระบุได้ กรุณาตรวจสอบข้อมูลอีกครั้ง").Show();
            }
            catch (Exception e)
            {
                GlobalFunction.createDialog(c, e.ToString()).Show();
            }
            conn.Close();

        }
    }
}