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
using System.Data;

namespace Test
{
    class Program
    {

        private static string zipPath = "../../../../Reports-Departures-2.zip";
        private static string folderPath = "../../../../Reports-Departures";
        private static IEnumerable<TouroperatorMongoDbModel> Touroperators = null;

        static void Main(string[] args)
        {
            // Unzip();
            ReadExcel();
        }

        static void Unzip()
        {
            using (ZipFile zip = ZipFile.Read(zipPath))
            {
                Directory.CreateDirectory(folderPath);
                zip.ExtractAll(folderPath);
            }

        }

        static void ReadExcel()
        {
            DirectoryInfo root = new DirectoryInfo(folderPath);
            var dateFolders = root.GetDirectories();

            foreach (var dateFolder in dateFolders)
            {
                ReadDateFolder(dateFolder);
            }

        }

        static void ReadDateFolder(DirectoryInfo datefolder)
        {
            DateTime date = DateTime.ParseExact(datefolder.Name, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            var touroperatorsDirs = datefolder.GetDirectories();

            foreach (DirectoryInfo touroperatorDir in touroperatorsDirs)
            {
                var touroperatorName = touroperatorDir.Name.Replace('-', ' ');
                var touroperator = Touroperators.FirstOrDefault(x => x.Name == touroperatorName);

                var tripFiles = touroperatorDir.GetFiles("*.*");
                foreach (var tripFile in tripFiles)
                {
                    ReadTripFile(tripFile, date, touroperator);
                }
            }
        }

        static void ReadTripFile(FileInfo tripFile, DateTime date, TouroperatorMongoDbModel touroperator)
        {
            var tripName = Path.GetFileNameWithoutExtension(tripFile.Name).Replace('-', ' ');
            var trip = touroperator.Trips.FirstOrDefault(x => x.Name == tripName);

            var connection = new OleDbConnection($"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = {tripFile.FullName};Extended Properties = \"Excel 12.0 Xml;HDR=YES\";");
            connection.Open();

            using (connection)
            {
                DataTable tables = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                var tableName = tables.Rows[0]["TABLE_NAME"];
                Console.WriteLine(tableName);

                var getCustomersCommand = new OleDbCommand($"SELECT * FROM [{tableName}]", connection);
                var reader = getCustomersCommand.ExecuteReader();

                using (reader)
                {
                    while (reader.Read())
                    {
                        var firstName = (string)reader["First Name"];
                        var lastName = (string)reader["Last Name"];
                        var hasDiscount = (string)reader["Has Discount"] == "Yes" ? true : false;

                        var customer = new CustomerParseModel()
                        {
                            FirstName = firstName,
                            LastName = lastName,
                            HasDiscount = hasDiscount
                        };

                        trip.Customers.Add(customer);
                    }
                }
            }

            connection.Close();
        }

        static void ReadExcel(IEnumerable<TouroperatorMongoDbModel> touroperators)
        {

            DirectoryInfo root = null;

            FileInfo[] files = null;
            DirectoryInfo[] subDirs = null;

            files = root.GetFiles("*.*");

            if (files != null)
            {
                foreach (FileInfo fi in files)
                {

                }
            }

            subDirs = root.GetDirectories();

            foreach (DirectoryInfo dirInfo in subDirs)
            {

            }

        }




    }
}
