using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbaxTask
{
    public class VehicleRepo
    {
        private SQLiteConnection _sqlConnection;

        public VehicleRepo()
        {
            _sqlConnection = CreateConnection();
        }

        static SQLiteConnection CreateConnection()
        {

            SQLiteConnection sqlite_conn;
            // Create a new database connection:
            sqlite_conn = new SQLiteConnection("Data Source=c:/DatabaseFolder/database.db; Version = 3; New = True; Compress = True; ");
            // Open the connection:
            try
            {
                sqlite_conn.Open();
            }
            catch (Exception ex)
            {

            }
            return sqlite_conn;
        }

        public void CreateTable()
        {

            SQLiteCommand sqlite_cmd;
            string t1 = "CREATE TABLE IF NOT EXISTS Positions(Veh INT PRIMARY KEY, Lat DECIMAL(18,0), Long DECIMAL(18,0), Nextstop VARCHAR(50));";
            sqlite_cmd = _sqlConnection.CreateCommand();
            sqlite_cmd.CommandText = t1;
            sqlite_cmd.ExecuteNonQuery();

        }

        public void InsertData(int Veh, Decimal Lat, Decimal Long, string nextstop)
        {

            var sqlite_cmd = _sqlConnection.CreateCommand();
            sqlite_cmd.CommandText = $"INSERT OR REPLACE INTO Positions(Veh,Lat,Long,Nextstop) Values({Veh},{Lat},{Long},'{nextstop}')";
            sqlite_cmd.ExecuteNonQuery();

        }
        public List<VehiclePosition> GetAllBuses (){
            var list = new List<VehiclePosition>();
            var sqlite_cmd = _sqlConnection.CreateCommand();
            sqlite_cmd.CommandText = "SELECT Veh,Lat,Long,Nextstop FROM Positions";
            var qur = sqlite_cmd.ExecuteReader();
            while (qur.Read())
            {
                list.Add(new VehiclePosition() { VP = new VehiclePositionInner() { Veh = qur.GetInt32(0),Lat= qur.GetDecimal(1),Long = qur.GetDecimal(2),next_stop=qur.GetString(3) } } );
            }


            return list;
        }

        public List<VehiclePosition> GetBusesCloseBy(Decimal loc_lat, Decimal loc_long)
        {
            /*http://localhost:5157/closebuses?Lat=60&Long=60*/
            var List = new List<VehiclePosition>();
            var sqlite_cmd = _sqlConnection.CreateCommand();
            var cos_lat_2 = Math.Pow(Math.Cos((double)loc_lat * Math.PI / 100),2);
            sqlite_cmd.CommandText = $"SELECT Veh,Lat,Long,Nextstop AS Distance FROM Positions ORDER BY(({loc_lat}-Lat)*({loc_lat}-Lat) + ({loc_long}-Long)*({loc_long}-Long)*{cos_lat_2})ASC";
            var qur = sqlite_cmd.ExecuteReader();
            while (qur.Read())
            {
                List.Add(new VehiclePosition() { VP = new VehiclePositionInner() { Veh = qur.GetInt32(0), Lat = qur.GetDecimal(1), Long = qur.GetDecimal(2),next_stop = qur.GetString(3) } });
            }
            var littleList = List.Take(5).ToList();
            return littleList;

        }
    }
}
