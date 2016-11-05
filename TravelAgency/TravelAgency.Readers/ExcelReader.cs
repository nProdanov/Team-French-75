using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Globalization;
using System.IO;
using System.Linq;

using Ionic.Zip;

using TravelAgency.ParseModels;
using TravelAgency.Readers.Contracts;

namespace TravelAgency.Readers
{
    public class ExcelReader : IExcelReader
    {
        private const string zipPath = "../../../../Reports-Departures.zip";
        private const string folderPath = "../../../../Reports-Departures";
        private IEnumerable<TouroperatorParseModel> touroperators;

        public ExcelReader()
        {
        }
        
        public void ReadExcel(IEnumerable<TouroperatorParseModel> touroperators)
        {
            if (touroperators == null)
            {
                throw new ArgumentNullException("Touroperators should not be null!");
            }

            this.touroperators = touroperators;

            this.Unzip();

            DirectoryInfo root = new DirectoryInfo(folderPath);
            var dateFolders = root.GetDirectories();

            foreach (var dateFolder in dateFolders)
            {
                this.ReadDateFolder(dateFolder);
            }

        }

        private void Unzip()
        {
            using (ZipFile zip = ZipFile.Read(zipPath))
            {
                Directory.CreateDirectory(folderPath);
                zip.ExtractAll(folderPath);
            }
        }

        private void ReadDateFolder(DirectoryInfo datefolder)
        {
            DateTime date = DateTime.ParseExact(datefolder.Name, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            var touroperatorsDirs = datefolder.GetDirectories();

            foreach (DirectoryInfo touroperatorDir in touroperatorsDirs)
            {
                var touroperatorName = touroperatorDir.Name.Replace('-', ' ');
                var touroperator = this.touroperators.FirstOrDefault(x => x.Name == touroperatorName);

                var tripFiles = touroperatorDir.GetFiles("*.*");
                foreach (var tripFile in tripFiles)
                {
                    ReadTripFile(tripFile, date, touroperator);
                }
            }
        }

        private void ReadTripFile(FileInfo tripFile, DateTime date, TouroperatorParseModel touroperator)
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
                        touroperator.Customers.Add(customer);
                    }
                }
            }

            connection.Close();
        }
    }
}
