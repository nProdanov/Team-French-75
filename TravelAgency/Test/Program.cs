using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.ParseModels;
using Ionic.Zip;
using System.Globalization;
using System.Data.OleDb;
using System.Data.SQLite;

namespace Test
{
    public class Program
    {
        public static void Main()
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

            foreach (var pair in tripsWithContinent)
            {
                Console.WriteLine(pair.Key + "-"+pair.Value);
            }
        }
    }
}
