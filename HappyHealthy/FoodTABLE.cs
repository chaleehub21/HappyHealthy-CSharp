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
using System.IO;

namespace HappyHealthyCSharp
{
    class FoodTABLE
    {
        public string Food { get; set; }
        [PrimaryKey,AutoIncrement]
        public int Food_ID { get; set; }
        public string Food_NAME { get; set; }
        public string Food_CAL { get; set; }
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
        private string localPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
        private string dbPath = "";
        SQLiteConnection conn = null;
        public FoodTABLE()
        {
            dbPath = Path.Combine(localPath, Database.DB_NAME);
            conn = new SQLiteConnection(dbPath);
            conn.CreateTable<FoodTABLE>();
            //constructor - no need for args since naming convention for instances variable mapping can be use : CB
        }
        public int InsertToSQL(FoodTABLE foodinstance)
        {
            var conn = new SQLiteConnection(Database.DB_NAME);
            conn.CreateTable<FoodTABLE>();
            return conn.Insert(foodinstance);
        }
        public List<string> getFoodList(string word)
        {
            
            var query = $@"SELECT * FROM FoodTABLE where Food_NAME LIKE '%{word}%'";
            //var retFood = new List<Dictionary<string, string>>(); //what the fuck is this thing really?
            var backFromSQL = conn.Query<FoodTABLE>(query);
            List<string> retRecord = new List<string>();
            if (backFromSQL.Count > 0) {
                var foodCursor = backFromSQL[0];
                retRecord.Add(backFromSQL[0].Food);
                retRecord.Add(backFromSQL[0].Food_AMT);
                retRecord.Add(backFromSQL[0].Food_CAL);
                retRecord.Add(backFromSQL[0].Food_CARBOHYDRATE);
                retRecord.Add(backFromSQL[0].Food_Detail);
                retRecord.Add(backFromSQL[0].Food_FAT);
                retRecord.Add(Convert.ToString(backFromSQL[0].Food_ID));
                retRecord.Add(backFromSQL[0].Food_NAME);
                retRecord.Add(backFromSQL[0].Food_NET_UNIT);
                retRecord.Add(backFromSQL[0].Food_NET_WEIGHT);
                retRecord.Add(backFromSQL[0].Food_PROTEIN);
                retRecord.Add(backFromSQL[0].Food_SODIUM);
                retRecord.Add(backFromSQL[0].Food_SUGAR);
                retRecord.Add(backFromSQL[0].Food_UNIT);
            }
            return retRecord;       //if backFromSQL is equal to 0, it doesn't exist in database so the return object will be null
        }
        public async void InsertToSQL(FoodTABLE foodinstance,string just_to_make_overload)
        {
            var conn = new SQLiteAsyncConnection(Database.DB_NAME);
            await conn.CreateTableAsync<FoodTABLE>();
            int retRecord = await conn.InsertAsync(foodinstance);
            
        } //experiment method
    }
}