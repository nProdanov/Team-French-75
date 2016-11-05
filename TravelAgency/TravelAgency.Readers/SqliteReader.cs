using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;

namespace TravelAgency.Readers
{
    public class SqliteReader
    {
        public IDictionary<string, string> ReadSqlite()
        {
            var relativePath = "../../../../trips.db";
            var fullPath = Path.GetFullPath(relativePath);
            var connectionString = $"Data Source = {fullPath}; Version = 3;";
            var tripsWithContinent = new Dictionary<string, string>();

            SQLiteConnection connection = new SQLiteConnection(connectionString);
            connection.Open();

            using (connection)
            {
                var getTripsDataCommand = new SQLiteCommand("SELECT * FROM Trips", connection);

                var reader = getTripsDataCommand.ExecuteReader();

                using (reader)
                {
                    while (reader.Read())
                    {
                        tripsWithContinent.Add(reader["Name"].ToString(), reader["Continent"].ToString());
                    }
                }
            }

            return tripsWithContinent;
        }
    }
}
